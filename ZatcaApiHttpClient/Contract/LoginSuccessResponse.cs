namespace ZatcaApiHttpClient.Contract
{
    public sealed class LoginSuccessResponse
    {
        public string access_token { get; set; }
        public string expires { get; set; }
    }
}
