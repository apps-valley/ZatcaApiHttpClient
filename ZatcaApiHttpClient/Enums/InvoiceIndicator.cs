namespace ZatcaApiHttpClient.Enums
{
    public enum InvoiceIndicator
    {
        None = 0,
        ThirdParty,    // Flag indicating whether the invoice was created by a third party
        SelfBilled,    // The invoice is issued by the buyer instead of the supplier
        Nominal,       // The invoice is issued for goods that are provided without consideration
        Export,        // The invoice is issued to a foreign buyer
        Summary        // The invoice is issued for sales occurring over a period of time
    }

}
