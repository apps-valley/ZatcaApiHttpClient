namespace ZatcaApiHttpClient.Contract
{
    public class ValidationResult
    {
        // List of informational messages from the validation
        public List<ValidationMessage> infoMessages { get; set; }

        // List of warning messages from the validation
        public List<ValidationMessage> warningMessages { get; set; }

        // List of error messages from the validation
        public List<ValidationMessage> errorMessages { get; set; }

        // Status of the validation
        public string status { get; set; }
    }

}
