using System.ComponentModel;
using ZatcaApiHttpClient.Enums;

namespace ZatcaApiHttpClient.Models.Invoices
{
    public class DocumentLineItem
    {

        public string LineItemName { get; set; }
        [DefaultValue(250)]
        public decimal LineItemPrice { get; set; }
        [DefaultValue(2)]
        public decimal LineItemQty { get; set; }
        public decimal DiscountOnLineItem { get; set; }

        public TaxCategories TaxCategoryCode { get; set; }
        [DefaultValue(TaxReasonCode.None)]
        public TaxReasonCode TaxReasonCode { get; set; }
        public string TaxReasonDescription { get; set; }
        public string ItemVatCategoryCodeReasonCode { get; set; }
        public decimal VatRateOnLineItem { get; set; }
    }
}
