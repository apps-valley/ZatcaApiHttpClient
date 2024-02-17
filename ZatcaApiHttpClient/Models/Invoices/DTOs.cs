using ZatcaApiHttpClient.Enums;

namespace ZatcaApiHttpClient.Models.Invoices
{
    public class DTOs
    {
        public sealed record GetInvoicesQuery(List<InvoiceKind> InvoiceTypes, int PageNumber, int PageSize);
        public record InvoiceStatusResponse(string invoiceUUID, InvoiceStatus InvoiceStatus,
  InvoiceReportingResponse InvoiceReportingResponse, InvoiceClearanceResponse InvoiceClearanceResponse);
        public record InvoiceReportingResponse(Contract.ValidationResult ValidationResults, InvoiceStatus ReportingStatus);

        public record InvoiceClearanceResponse(Contract.ValidationResult ValidationResults, string ClearedInvoice, InvoiceStatus ClearanceStatus);
        public class GetInvoicesResponse : Invoice
        {
            public InvoiceStatusResponse InvoiceStatus { get; set; }
        }
    }
}
