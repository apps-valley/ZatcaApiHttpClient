namespace ZatcaApiHttpClient.Models.Invoices
{
    public class Buyer
    {
        /// <summary>
        /// The address of the buyer. This is represented as an instance of the BuyerAddress class.
        /// </summary>
        public PartyPostalAddress BuyerAddress { get; set; }


        public string BuyerName { get; set; }

        public string BuyerVatId { get; set; }
    }
}
