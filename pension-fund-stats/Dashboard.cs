using LiveCharts;
using LiveCharts.Wpf;
using MoreLinq;
using PensionAnalysis.Helpers;
using PensionAnalysis.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using PensionAnalysis.Extensions;
using DColor = System.Drawing.Color;
using MColor = System.Windows.Media.Color;

namespace PensionAnalysis
{
    public partial class Dashboard : Form
    {
        private IEnumerable<MonthlyPercentageInfo> _monthlyPercentages = new List<MonthlyPercentageInfo>();
        private readonly List<DailyVuanInfo> _monthlyVuanValues = new List<DailyVuanInfo>();
        private IEnumerable<DailyVuanInfo> _dailyVuanValues = new List<DailyVuanInfo>();

        private IEnumerable<Provider> _allAvailableProviders;
        private IEnumerable<int> _allYears;

        private readonly ReactiveList<Provider> _selectedProviders = new ReactiveList<Provider>();
        private readonly ReactiveList<int> _selectedYears = new ReactiveList<int>();
        private readonly Subject<Unit> _simplifyVuanChart = new Subject<Unit>();

        private static readonly List<Provider> DefaultProvidersSecondPillar = new List<Provider> { Provider.BCR, Provider.NN };
        private static readonly List<Provider> DefaultProvidersThirdPillar = new List<Provider> { Provider.BCRPlus, Provider.NNOptim };
        private static readonly List<int> RecentYears = Enumerable.Range(DateTime.Now.Year - 2, 3).ToList();
        private static readonly List<Provider> _secondPillar = new List<Provider> { Provider.BCR, Provider.BRD, Provider.AllianzViitorul, Provider.GeneraliAripi, Provider.MetropolitanLife, Provider.AegonVital, Provider.NN };
        private static readonly List<Provider> _thirdPillar = Enum.GetValues(typeof(Provider)).OfType<Provider>().Except(_secondPillar).ToList();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dash_Load(object sender, EventArgs ea)
        {
            ParseData((int)Pillar.Mandatory);

            _allAvailableProviders = _secondPillar;

            _selectedYears.ItemsAdded.ToUnit()
                .Merge(_simplifyVuanChart)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => RebuildGraph());

            _selectedYears.ItemsAdded.ToUnit()
                .Subscribe(_ => SelectedYearsChanged());

            _selectedYears.ItemsAdded.ToUnit()
                .Throttle(TimeSpan.FromMilliseconds(100))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => TickAllYearsIfNecessary());

            _selectedProviders.ItemsAdded.ToUnit()
                .Merge(_selectedProviders.ItemsRemoved.ToUnit())
                .Subscribe(_ => SelectedProvidersChanged());
            _selectedProviders.ItemsAdded.ToUnit()
                .Merge(_selectedProviders.ItemsRemoved.ToUnit())
                .Throttle(TimeSpan.FromMilliseconds(50))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(
                    _ =>
                    {
                        TickAllProvidersIfNecessary();
                        ToggleProviderLineSeriesVisibility();
                    });

            _allYears.ForEach(y =>
                {
                    var button = new Button { Text = y.ToString(), Tag = y };
                    button.Click += YearSelectionClick;
                    pnlYears.Controls.Add(button);
                });

            _allAvailableProviders.ForEach(
                e =>
                {
                    var button = new Button { Text = e.ToString(), Tag = e, BackColor = _selectedProviders.Contains(e) ? GetProviderColor(e) : DColor.DarkGray };
                    button.Click += ProviderSelectionClick;
                    pnlProviders.Controls.Add(button);
                });

            percentageChart.LegendLocation = LegendLocation.None;
            percentageChart.DisableAnimations = true;
            vuanChart.LegendLocation = LegendLocation.None;
            vuanChart.DisableAnimations = true;

            vuanChart.AxisY.Clear();
            vuanChart.AxisY.Add(new Axis
            {
                Title = "VUAN",
                LabelFormatter = value => value.ToString("N4", new CultureInfo("en-US"))
            });

            percentageChart.AxisY.Clear();
            percentageChart.AxisY.Add(new Axis
            {
                Title = "Annualized (2 years) Profitability Percentage",
                LabelFormatter = value => value.ToString("N4", new CultureInfo("en-US"))
            });

            //Trigger
            _selectedYears.AddRange(RecentYears);
        }

        private void ToggleProviderLineSeriesVisibility()
        {
            var selectedProviderNames = _selectedProviders.Select(p => p.ToString()).ToList();
            percentageChart.Series.OfType<LineSeries>().ForEach(s => s.Visibility = selectedProviderNames.Contains(s.Title) ? Visibility.Visible : Visibility.Hidden);
            vuanChart.Series.OfType<LineSeries>().ForEach(s => s.Visibility = selectedProviderNames.Contains(s.Title) ? Visibility.Visible : Visibility.Hidden);
        }

        private void ParseData(int type)
        {
            var percentages = XDocument.Load($@"Data\export-RR-{type}.xml");
            _monthlyPercentages = percentages.Root?.Descendants("item").Select(
                i => new MonthlyPercentageInfo
                {
                    Type = EnumHelper.GetValueFromDescription<Pillar>(i.Element("pilon")?.Value),
                    Provider = EnumHelper.GetValueFromDescription<Provider>(i.Element("entitate")?.Value),
                    DateOfReference = DateTime.ParseExact(i.Element("data")?.Value, "dd.MM.yyyy", null),
                    Percentage = decimal.Parse(i.Element("rata")?.Value ?? string.Empty),
                    Degree = EnumHelper.GetValueFromDescription<Risk>(i.Element("risc")?.Value)
                }) ?? Enumerable.Empty<MonthlyPercentageInfo>();

            var vuans = XDocument.Load($@"Data\export-VUAN-{type}.xml");
            _dailyVuanValues = vuans.Root?.Descendants("item").Select(
                i => new DailyVuanInfo
                {
                    Type = EnumHelper.GetValueFromDescription<Pillar>(i.Element("pilon")?.Value),
                    Provider = EnumHelper.GetValueFromDescription<Provider>(i.Element("entitate")?.Value),
                    DateOfReference = DateTime.ParseExact(i.Element("data")?.Value, "dd.MM.yyyy", null),
                    Vuan = decimal.Parse(i.Element("vuan")?.Value ?? string.Empty)
                }) ?? Enumerable.Empty<DailyVuanInfo>();

            _allYears = _monthlyPercentages.Select(mi => mi.DateOfReference.Year).Distinct().OrderByDescending(y => y).ToList();

            var dailyVuanByProviders = _dailyVuanValues.GroupBy(v => v.Provider).Select(g => new { Provider = g.Key, VUANs = g.ToList().OrderByDescending(v => v.DateOfReference) });
            foreach (var kv in dailyVuanByProviders)
            {
                foreach (var year in _allYears)
                {
                    var byMonth = kv.VUANs.Where(v => v.DateOfReference.Year == year).GroupBy(v => v.DateOfReference.Month).Select(g => new { Month = g.Key, VUANs = g.ToList() });
                    foreach (var kvm in byMonth)
                    {
                        var count = kvm.VUANs.Count;
                        if (count <= 3)
                            _monthlyVuanValues.AddRange(kvm.VUANs);
                        else
                        {
                            var firstSample = (int)Math.Round(0.33 * count, MidpointRounding.ToEven);
                            _monthlyVuanValues.Add(kvm.VUANs[firstSample]);
                            var secondSample = (int)Math.Round(0.66 * count, MidpointRounding.ToEven);
                            _monthlyVuanValues.Add(kvm.VUANs[secondSample]);
                            if (secondSample != count - 1)
                                _monthlyVuanValues.Add(kvm.VUANs[count - 1]);
                        }
                    }
                }
            }
        }

        private void YearSelectionClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedYear = int.Parse(button.Text);

            _selectedYears.Clear();
            _selectedYears.AddRange(_allYears.OrderBy(y => y).SkipWhile(y => y < selectedYear).ToList());
        }

        private void ProviderSelectionClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var provider = (Provider)button.Tag;

            var isDisplayed = _selectedProviders.Contains(provider);
            if (isDisplayed) _selectedProviders.Remove(provider);
            else _selectedProviders.Add(provider);
        }

        private void RebuildGraph()
        {
            var selectedProviders = _selectedProviders.ToList();
            _selectedProviders.Clear();

            prg.Style = ProgressBarStyle.Marquee;
            prg.MarqueeAnimationSpeed = 30;
            pnlMenu.Enabled = false;
            scCharts.Visible = false;

            percentageChart.Series.Clear();
            _allAvailableProviders.ForEach(
                p =>
                {
                    var values = new ChartValues<decimal>();
                    values.AddRange(
                        _monthlyPercentages
                            .Where(mi => mi.Provider == p && _selectedYears.Contains(mi.DateOfReference.Year))
                            .OrderBy(mi => mi.DateOfReference)
                            .Select(mi => mi.Percentage));

                    percentageChart.Series.Add(
                        new LineSeries
                        {
                            Values = values,
                            LineSmoothness = 1, //straight lines, 1 really smooth lines
                            Title = p.ToString(),
                            Fill = new SolidColorBrush(ToMediaColor(GetProviderColor(p), 64)),
                            Stroke = new SolidColorBrush(ToMediaColor(GetProviderColor(p), 255)),
                            Visibility = Visibility.Visible
                        });
                });

            percentageChart.AxisX.Clear();
            percentageChart.AxisX.Add(new Axis
            {
                Title = "Months",
                Labels = _monthlyPercentages
                                           .Where(mi => mi.Provider == Provider.BCR || mi.Provider == Provider.BCRPlus) //just one, doesn't matter which
                                           .Where(mi => _selectedYears.Contains(mi.DateOfReference.Year))
                                           .Select(mi => mi.DateOfReference)
                                           .OrderBy(d => d)
                                           .Select(d => d.ToString("MMM yy"))
                                           .Distinct()
                                           .ToArray()
            });

            vuanChart.Series.Clear();
            var vuanValues = chkSimplifyVUANGraph.Checked ? _monthlyVuanValues : _dailyVuanValues;

            _allAvailableProviders.ForEach(
                p =>
                {
                    var values = new ChartValues<decimal>();
                    values.AddRange(
                        vuanValues
                            .Where(mi => mi.Provider == p && _selectedYears.Contains(mi.DateOfReference.Year))
                            .OrderBy(mi => mi.DateOfReference)
                            .Select(mi => mi.Vuan));

                    vuanChart.Series.Add(
                        new LineSeries
                        {
                            Values = values,
                            LineSmoothness = 1, //straight lines, 1 really smooth lines
                            Title = p.ToString(),
                            Tag = p,
                            Fill = new SolidColorBrush(ToMediaColor(GetProviderColor(p), 64)),
                            Stroke = new SolidColorBrush(ToMediaColor(GetProviderColor(p), 255)),
                            Visibility = Visibility.Visible
                        });
                });

            vuanChart.AxisX.Clear();
            vuanChart.AxisX.Add(new Axis
            {
                Title = "Day",
                Labels = vuanValues
                                            .Where(mi => mi.Provider == Provider.BCR) //just one, doesn't matter which
                                            .Where(mi => _selectedYears.Contains(mi.DateOfReference.Year))
                                            .Select(mi => mi.DateOfReference)
                                            .OrderBy(d => d)
                                            .Select(d => d.ToString("dd MMM yy"))
                                            .Distinct()
                                            .ToArray()
            });

            _selectedProviders.AddRange(selectedProviders.Any() ? selectedProviders : rbtnP2.Checked ? DefaultProvidersSecondPillar : DefaultProvidersThirdPillar);
            pnlMenu.Enabled = true;
            scCharts.Visible = true;
            prg.Style = ProgressBarStyle.Continuous;
            prg.MarqueeAnimationSpeed = 0;
        }

        public static MColor ToMediaColor(DColor color, byte? opacity = null)
        {
            return MColor.FromArgb(opacity ?? color.A, color.R, color.G, color.B);
        }

        public DColor GetProviderColor(Provider provider)
        {
            switch (provider)
            {
                case Provider.BCR:
                case Provider.BCRPlus:
                    return DColor.DarkBlue;
                case Provider.GeneraliAripi:
                case Provider.GeneraliStabil:
                    return DColor.Brown;
                case Provider.AllianzViitorul:
                case Provider.AllianzModerato:
                    return DColor.DodgerBlue;
                case Provider.MetropolitanLife:
                    return DColor.DarkSlateBlue;
                case Provider.NN:
                case Provider.NNOptim:
                    return DColor.Orange;
                case Provider.AegonVital:
                case Provider.AegonEssential:
                    return DColor.YellowGreen;
                case Provider.BRD:
                case Provider.BRDMedio:
                    return DColor.Red;

                case Provider.AllianzVivace:
                    return DColor.Aquamarine;
                case Provider.NNActiv:
                    return DColor.Plum;
                case Provider.CertInvestPensia:
                    return DColor.DeepPink;
                case Provider.Raiffeisen:
                    return DColor.Yellow;
            }

            return DColor.Indigo;
        }

        private void AllYearsSelectionChanged(object sender, EventArgs e)
        {
            _selectedYears.Clear();
            _selectedYears.AddRange(chkAllYears.Checked ? _allYears : RecentYears);
        }
        private void SelectedYearsChanged()
        {
            pnlYears.Controls.OfType<Button>()
                .ForEach(b => b.BackColor = _selectedYears.Contains((int)b.Tag) ? DColor.LightCoral : DColor.DarkGray);
        }

        private void TickAllYearsIfNecessary()
        {
            chkAllYears.CheckedChanged -= AllYearsSelectionChanged;
            chkAllYears.Checked = !_allYears.Except(_selectedYears).Any();
            chkAllYears.CheckedChanged += AllYearsSelectionChanged;
        }

        private void AllProvidersSelectionChanged(object sender, EventArgs e)
        {
            _selectedProviders.Clear();
            _selectedProviders.AddRange(chkAllProviders.Checked ? _allAvailableProviders : Enumerable.Empty<Provider>());
        }

        private void SelectedProvidersChanged()
        {
            pnlProviders.Controls.OfType<Button>()
                .ForEach(b =>
                {
                    var provider = (Provider)b.Tag;
                    b.BackColor = _selectedProviders.Contains(provider) ? GetProviderColor(provider) : DColor.DarkGray;
                });
        }

        private void TickAllProvidersIfNecessary()
        {
            chkAllProviders.CheckedChanged -= AllProvidersSelectionChanged;
            chkAllProviders.Checked = !_allAvailableProviders.Except(_selectedProviders).Any();
            chkAllProviders.CheckedChanged += AllProvidersSelectionChanged;
        }

        private void ReduceVuanChartPoints_CheckedChanged(object sender, EventArgs e)
        {
            _simplifyVuanChart.OnNext(Unit.Default);
        }

        private void rbtnP2_CheckedChanged(object sender, EventArgs ea)
        {
            ParseData((int)(rbtnP2.Checked ? Pillar.Mandatory : Pillar.Optional));

            var buttons = pnlProviders.Controls.OfType<Button>().ToList();
            buttons.ForEach(b =>
            {
                b.Click -= ProviderSelectionClick;
                pnlProviders.Controls.Remove(b);
            });

            _selectedProviders.Clear();

            _allAvailableProviders = rbtnP2.Checked ? _secondPillar : _thirdPillar;
            _allAvailableProviders.ForEach(
                e =>
                {
                    var button = new Button { Text = e.ToString(), Tag = e, BackColor = _selectedProviders.Contains(e) ? GetProviderColor(e) : DColor.DarkGray };
                    button.Click += ProviderSelectionClick;
                    pnlProviders.Controls.Add(button);
                });

            RebuildGraph();
        }
    }
}
