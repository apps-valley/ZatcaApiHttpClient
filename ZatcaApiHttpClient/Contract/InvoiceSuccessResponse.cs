using ZatcaApiHttpClient.Enums;

namespace ZatcaApiHttpClient.Contract
{
    public class InvoiceSuccessResponse
    {
        // Unique identifier for the invoice version
        public string UUID { get; set; }

        // Status of the invoice version
        public InvoiceStatus Status { get; set; }

        // Validation results for the invoice version
        public ValidationResult ValidationResults { get; set; }

        // URL for the QR code image of the invoice version
        public string QrCodeImageUrl { get; set; }

        // String for the QR code 
        public string QrCodeString { get; set; }

        // URL for the XML version of the invoice
        public string XmlVersionUrl { get; set; }
    }

}
