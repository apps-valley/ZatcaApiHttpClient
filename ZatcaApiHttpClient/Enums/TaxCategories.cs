using System.ComponentModel;

namespace ZatcaApiHttpClient.Enums
{
    public enum TaxCategories
    {
        None = 0,
        [Description("التوريدات المعفاة")]
        E, //e

        [Description("التوريدات الخاضة للضريبة")]
        S,  // S

        [Description("التوريدات الخاضعة لنسبة الصفر")]
        Z, //Z

        [Description("التوريدات الخاضعة للضريبة")]
        O // O
    }

}
