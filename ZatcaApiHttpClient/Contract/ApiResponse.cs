using System.Net;

namespace ZatcaApiHttpClient.Contract
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        // IsSuccess is true if Errors is null or empty
        public bool IsSuccess
        {
            get { return Errors == null || Errors?.Length == 0; }
        }
    }

}
