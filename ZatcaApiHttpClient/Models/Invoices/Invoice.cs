using System.ComponentModel;
using ZatcaApiHttpClient.Enums;
using ZatcaApiHttpClient.Extensions;

namespace ZatcaApiHttpClient.Models.Invoices
{
    public class Invoice
    {
        public Invoice()
        {
            DocumentIssueDateTime = DateTime.Now.ToKsaDateTime();
        }

        /// <summary>
        /// The type of the invoice document. This can be any type defined in the InvoiceKind enum, 
        /// such as TaxInvoice, SimplifiedTaxInvoice, TaxInvoiceDebitNote, etc.
        /// </summary>
        /// <example>SimplifiedTaxInvoice</example>
        public InvoiceKind DocumentType { get; set; }

        /// <summary>
        /// The indicator of the invoice. This can be any type defined in the InvoiceIndicator enum, 
        /// such as ThirdParty, SelfBilled, Nominal, Export, Summary, or None (default).
        /// It provides additional information about the nature of the invoice.
        /// </summary>
        /// <example>InvoiceIndicator.None</example>
        [DefaultValue(InvoiceIndicator.None)]
        public InvoiceIndicator InvoiceIndicator { get; set; }

        /// <summary>
        /// The currency for the invoice. This is represented as an instance of the Currency enum. 
        /// By default, it is set to SAR (Saudi Riyal).
        /// </summary>
        /// <example>Currency.SAR</example>
        [DefaultValue(Currency.SAR)]
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the document issue date time.
        /// </summary>
        /// <example>2024-01-01T00:00:00Z</example>
        public DateTime DocumentIssueDateTime { get; set; }

        [DefaultValue(null)]
        public DateTime? SupplyDate { get; set; }

        public Buyer Buyer { get; set; }


        public List<DocumentLineItem> DocumentLineItems { get; set; }
        public string ReferenceId { get; set; }

        public int DocumentId { get; set; }


        public double? DiscountOnDocumentTotal { get; set; }

        /// <summary>
        /// Reason for issuance of credit / debit note as per the VAT Implementing Regulation 
        /// </summary>
        public string PaymentMeans { get; set; }
    }
}
