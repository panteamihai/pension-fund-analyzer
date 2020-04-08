namespace PensionAnalysis
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.grpProviders = new System.Windows.Forms.GroupBox();
            this.pnlProviders = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAllProviders = new System.Windows.Forms.CheckBox();
            this.chkSimplifyVUANGraph = new System.Windows.Forms.CheckBox();
            this.grpYears = new System.Windows.Forms.GroupBox();
            this.pnlYears = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAllYears = new System.Windows.Forms.CheckBox();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.rbtnP3 = new System.Windows.Forms.RadioButton();
            this.rbtnP2 = new System.Windows.Forms.RadioButton();
            this.scCharts = new System.Windows.Forms.SplitContainer();
            this.percentageChart = new LiveCharts.WinForms.CartesianChart();
            this.vuanChart = new LiveCharts.WinForms.CartesianChart();
            this.prg = new System.Windows.Forms.ProgressBar();
            this.pnlMenu.SuspendLayout();
            this.grpProviders.SuspendLayout();
            this.pnlProviders.SuspendLayout();
            this.grpYears.SuspendLayout();
            this.pnlYears.SuspendLayout();
            this.grpType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCharts)).BeginInit();
            this.scCharts.Panel1.SuspendLayout();
            this.scCharts.Panel2.SuspendLayout();
            this.scCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.grpProviders);
            this.pnlMenu.Controls.Add(this.grpYears);
            this.pnlMenu.Controls.Add(this.grpType);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenu.Location = new System.Drawing.Point(0, 23);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.pnlMenu.Size = new System.Drawing.Size(1584, 189);
            this.pnlMenu.TabIndex = 3;
            // 
            // grpProviders
            // 
            this.grpProviders.Controls.Add(this.pnlProviders);
            this.grpProviders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProviders.Location = new System.Drawing.Point(0, 121);
            this.grpProviders.Name = "grpProviders";
            this.grpProviders.Size = new System.Drawing.Size(1584, 68);
            this.grpProviders.TabIndex = 5;
            this.grpProviders.TabStop = false;
            this.grpProviders.Text = "Providers";
            // 
            // pnlProviders
            // 
            this.pnlProviders.Controls.Add(this.chkAllProviders);
            this.pnlProviders.Controls.Add(this.chkSimplifyVUANGraph);
            this.pnlProviders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviders.Location = new System.Drawing.Point(3, 16);
            this.pnlProviders.Name = "pnlProviders";
            this.pnlProviders.Size = new System.Drawing.Size(1578, 49);
            this.pnlProviders.TabIndex = 4;
            // 
            // chkAllProviders
            // 
            this.chkAllProviders.AutoSize = true;
            this.chkAllProviders.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkAllProviders.Location = new System.Drawing.Point(3, 3);
            this.chkAllProviders.Name = "chkAllProviders";
            this.chkAllProviders.Size = new System.Drawing.Size(37, 30);
            this.chkAllProviders.TabIndex = 4;
            this.chkAllProviders.Text = "All";
            this.chkAllProviders.UseVisualStyleBackColor = true;
            this.chkAllProviders.CheckedChanged += new System.EventHandler(this.AllProvidersSelectionChanged);
            // 
            // chkSimplifyVUANGraph
            // 
            this.chkSimplifyVUANGraph.AutoSize = true;
            this.chkSimplifyVUANGraph.Checked = true;
            this.chkSimplifyVUANGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSimplifyVUANGraph.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSimplifyVUANGraph.Location = new System.Drawing.Point(46, 3);
            this.chkSimplifyVUANGraph.Name = "chkSimplifyVUANGraph";
            this.chkSimplifyVUANGraph.Size = new System.Drawing.Size(97, 30);
            this.chkSimplifyVUANGraph.TabIndex = 5;
            this.chkSimplifyVUANGraph.Text = "Reduce VUAN\r\nGraph Points";
            this.chkSimplifyVUANGraph.UseVisualStyleBackColor = true;
            this.chkSimplifyVUANGraph.CheckedChanged += new System.EventHandler(this.ReduceVuanChartPoints_CheckedChanged);
            // 
            // grpYears
            // 
            this.grpYears.Controls.Add(this.pnlYears);
            this.grpYears.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpYears.Location = new System.Drawing.Point(0, 69);
            this.grpYears.Name = "grpYears";
            this.grpYears.Size = new System.Drawing.Size(1584, 52);
            this.grpYears.TabIndex = 4;
            this.grpYears.TabStop = false;
            this.grpYears.Text = "Years (selected in Pink)";
            // 
            // pnlYears
            // 
            this.pnlYears.Controls.Add(this.chkAllYears);
            this.pnlYears.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlYears.Location = new System.Drawing.Point(3, 16);
            this.pnlYears.Name = "pnlYears";
            this.pnlYears.Size = new System.Drawing.Size(1578, 33);
            this.pnlYears.TabIndex = 2;
            // 
            // chkAllYears
            // 
            this.chkAllYears.AutoSize = true;
            this.chkAllYears.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkAllYears.Location = new System.Drawing.Point(3, 3);
            this.chkAllYears.Name = "chkAllYears";
            this.chkAllYears.Size = new System.Drawing.Size(37, 17);
            this.chkAllYears.TabIndex = 4;
            this.chkAllYears.Text = "All";
            this.chkAllYears.UseVisualStyleBackColor = true;
            this.chkAllYears.CheckedChanged += new System.EventHandler(this.AllYearsSelectionChanged);
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.rbtnP3);
            this.grpType.Controls.Add(this.rbtnP2);
            this.grpType.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpType.Location = new System.Drawing.Point(0, 25);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(1584, 44);
            this.grpType.TabIndex = 6;
            this.grpType.TabStop = false;
            this.grpType.Text = "Type";
            // 
            // rbtnP3
            // 
            this.rbtnP3.AutoSize = true;
            this.rbtnP3.Location = new System.Drawing.Point(50, 19);
            this.rbtnP3.Name = "rbtnP3";
            this.rbtnP3.Size = new System.Drawing.Size(38, 17);
            this.rbtnP3.TabIndex = 1;
            this.rbtnP3.Text = "P3";
            this.rbtnP3.UseVisualStyleBackColor = true;
            // 
            // rbtnP2
            // 
            this.rbtnP2.AutoSize = true;
            this.rbtnP2.Checked = true;
            this.rbtnP2.Location = new System.Drawing.Point(6, 19);
            this.rbtnP2.Name = "rbtnP2";
            this.rbtnP2.Size = new System.Drawing.Size(38, 17);
            this.rbtnP2.TabIndex = 0;
            this.rbtnP2.TabStop = true;
            this.rbtnP2.Text = "P2";
            this.rbtnP2.UseVisualStyleBackColor = true;
            this.rbtnP2.CheckedChanged += new System.EventHandler(this.rbtnP2_CheckedChanged);
            // 
            // scCharts
            // 
            this.scCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCharts.Location = new System.Drawing.Point(0, 212);
            this.scCharts.Name = "scCharts";
            // 
            // scCharts.Panel1
            // 
            this.scCharts.Panel1.Controls.Add(this.percentageChart);
            // 
            // scCharts.Panel2
            // 
            this.scCharts.Panel2.Controls.Add(this.vuanChart);
            this.scCharts.Size = new System.Drawing.Size(1584, 649);
            this.scCharts.SplitterDistance = 528;
            this.scCharts.TabIndex = 4;
            // 
            // percentageChart
            // 
            this.percentageChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.percentageChart.Location = new System.Drawing.Point(0, 0);
            this.percentageChart.Name = "percentageChart";
            this.percentageChart.Size = new System.Drawing.Size(528, 649);
            this.percentageChart.TabIndex = 3;
            this.percentageChart.Text = "cartesianChart1";
            // 
            // vuanChart
            // 
            this.vuanChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vuanChart.Location = new System.Drawing.Point(0, 0);
            this.vuanChart.Name = "vuanChart";
            this.vuanChart.Size = new System.Drawing.Size(1052, 649);
            this.vuanChart.TabIndex = 3;
            this.vuanChart.Text = "cartesianChart1";
            // 
            // prg
            // 
            this.prg.Dock = System.Windows.Forms.DockStyle.Top;
            this.prg.Location = new System.Drawing.Point(0, 0);
            this.prg.Name = "prg";
            this.prg.Size = new System.Drawing.Size(1584, 23);
            this.prg.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prg.TabIndex = 6;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.scCharts);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.prg);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pension Fund Analysis";
            this.Load += new System.EventHandler(this.Dash_Load);
            this.pnlMenu.ResumeLayout(false);
            this.grpProviders.ResumeLayout(false);
            this.pnlProviders.ResumeLayout(false);
            this.pnlProviders.PerformLayout();
            this.grpYears.ResumeLayout(false);
            this.pnlYears.ResumeLayout(false);
            this.pnlYears.PerformLayout();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.scCharts.Panel1.ResumeLayout(false);
            this.scCharts.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCharts)).EndInit();
            this.scCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.SplitContainer scCharts;
        private LiveCharts.WinForms.CartesianChart percentageChart;
        private LiveCharts.WinForms.CartesianChart vuanChart;
        private System.Windows.Forms.GroupBox grpProviders;
        private System.Windows.Forms.FlowLayoutPanel pnlProviders;
        private System.Windows.Forms.CheckBox chkAllProviders;
        private System.Windows.Forms.CheckBox chkSimplifyVUANGraph;
        private System.Windows.Forms.GroupBox grpYears;
        private System.Windows.Forms.FlowLayoutPanel pnlYears;
        private System.Windows.Forms.CheckBox chkAllYears;
        private System.Windows.Forms.ProgressBar prg;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.RadioButton rbtnP3;
        private System.Windows.Forms.RadioButton rbtnP2;
    }
}

