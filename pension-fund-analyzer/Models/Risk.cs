using System.ComponentModel;

namespace PensionAnalysis.Models
{
    public enum Risk
    {
        None,
        [Description("mediu")]
        Medium,
        [Description("ridicat")]
        High
    }
}
