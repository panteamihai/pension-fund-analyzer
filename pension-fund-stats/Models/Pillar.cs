using System.ComponentModel;

namespace PensionAnalysis.Models
{
    public enum Pillar
    {
        None,
        Government,
        [Description("Pilon 2")]
        Mandatory,
        [Description("Pilon 3")]
        Optional
    }
}
