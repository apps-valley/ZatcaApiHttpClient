using System.ComponentModel;

namespace ZatcaApiHttpClient.Enums
{
    public enum TaxReasonCode
    {
        [Description("")]
        None = 0,
        #region ExemptFromTax

        [Description("Financial services mentioned in Article 29 of the VAT Regulations")]
        VATEX_SA_29,

        [Description("Life insurance services mentioned in Article 29 of the VAT Regulations")]
        VATEX_SA_29_7,

        [Description("Real estate transactions mentioned in Article 30 of the VAT Regulations")]
        VATEX_SA_30,
        #endregion ExemptFromTax
        #region StandardRate


        #endregion StandardRate

        #region ZeroRatedGoods
        [Description("Export of goods")]
        VATEX_SA_32,

        [Description("Export of services")]
        VATEX_SA_33,

        [Description("The international transport of Goods")]
        VATEX_SA_34_1,
        [Description("International transport of passengers")]
        VATEX_SA_34_2,

        [Description("Services directly connected and incidental to a Supply of international passenger transport")]
        VATEX_SA_34_3,

        [Description("Supply of a qualifying means of transport")]
        VATEX_SA_34_4,

        [Description("Any services relating to Goods or passenger transportation, as defined in article twenty five of these Regulations")]
        VATEX_SA_34_5,

        [Description("Medicines and medical equipment")]
        VATEX_SA_35,

        [Description("Qualifying metals")]
        VATEX_SA_36,

        [Description("Private education to citizen")]
        VATEX_SA_EDU,

        [Description("Private healthcare to citizen")]
        VATEX_SA_HEA,

        [Description("Supply of qualified military goods")]
        VATEX_SA_MLTRY,
        #endregion ZeroRatedGoods


        #region ServicesOutsideScopeOfTax
        [Description("Reason is free text, to be provided by the taxpayer on case to case basis.")]
        VATEX_SA_OOS
        #endregion ServicesOutsideScopeOfTax

    }
}
