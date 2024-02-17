namespace ZatcaApiHttpClient.Contract
{
    public class ValidationMessage
    {

        // Type of the validation message
        public string type { get; set; }

        // Code of the validation message
        public string code { get; set; }

        // Category of the validation message
        public string category { get; set; }

        // Content of the validation message
        public string message { get; set; }

        // Status of the validation message
        public string status { get; set; }
    }

}
