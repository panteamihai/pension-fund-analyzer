using System.ComponentModel;

namespace PensionAnalysis.Models
{
    public enum Provider
    {
        None,
        [Description("BCR")]
        BCR,
        [Description("BRD")]
        BRD,
        [Description("AZT VIITORUL TĂU")]
        Allianz,
        [Description("ARIPI")]
        Generali,
        [Description("METROPOLITAN LIFE")]
        MetropolitanLife,
        [Description("VITAL")]
        Aegon,
        [Description("NN")]
        NN
    }
}
