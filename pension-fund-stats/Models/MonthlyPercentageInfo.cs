using System;

namespace PensionAnalysis.Models
{
    public class MonthlyPercentageInfo
    {
        public Pillar Type { get; set; }
        public Provider Provider { get; set; }
        public DateTime DateOfReference { get; set; }
        public decimal Percentage { get; set; }
        public Risk Degree { get; set; }
    }
}
