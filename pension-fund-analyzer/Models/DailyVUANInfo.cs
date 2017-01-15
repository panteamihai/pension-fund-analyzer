using System;

namespace PensionAnalysis.Models
{
    public class DailyVuanInfo
    {
        public Pillar Type { get; set; }
        public Provider Provider { get; set; }
        public DateTime DateOfReference { get; set; }
        public decimal Vuan { get; set; }
    }
}
