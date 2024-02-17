namespace ZatcaApiHttpClient.Enums
{
    public enum InvoiceKind
    {
        // For Tax Invoice
        TaxInvoice = 38801,

        // For Simplified Tax Invoice
        SimplifiedTaxInvoice = 38802,

        // For tax invoice debit note
        TaxInvoiceDebitNote = 38301,

        // For simplified debit note
        SimplifiedDebitNote = 38302,

        // For tax invoice credit note
        TaxInvoiceCreditNote = 38101,

        // For simplified credit note
        SimplifiedCreditNote = 38102,

        // For Prepayment Tax Invoice
        PrepaymentTaxInvoice = 38601,

        // For Prepayment Simplified Tax Invoice
        PrepaymentSimplifiedTaxInvoice = 38602
    }

}
