using System.ComponentModel;

namespace PensionAnalysis.Models
{
    public enum Provider
    {
        [Description("BCR")]
        BCR,
        [Description("BRD")]
        BRD,
        [Description("AZT VIITORUL TĂU")]
        AllianzViitorul,
        [Description("ARIPI")]
        GeneraliAripi,
        [Description("METROPOLITAN LIFE")]
        MetropolitanLife,
        [Description("VITAL")]
        AegonVital,
        [Description("NN")]
        NN,
        [Description("STABIL")]
        GeneraliStabil,
        [Description("RAIFFEISEN ACUMULARE")]
        Raiffeisen,
        [Description("BRD MEDIO")]
        BRDMedio,
        [Description("AEGON ESENȚIAL")]
        AegonEssential,
        [Description("NN OPTIM")]
        NNOptim,
        [Description("NN ACTIV")]
        NNActiv,
        [Description("AZT MODERATO")]
        AllianzModerato,
        [Description("AZT VIVACE")]
        AllianzVivace,
        [Description("BCR PLUS")]
        BCRPlus,
        [Description("PENSIA MEA")]
        CertInvestPensia,
    }
}
