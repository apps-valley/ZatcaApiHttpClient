using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using ZatcaApiHttpClient.Contract;
using ZatcaApiHttpClient.Models.Invoices;
using static ZatcaApiHttpClient.Models.Invoices.DTOs;

namespace ZatcaApiHttpClient
{
    public class InvoiceClient : IDisposable
    {
        private readonly HttpClient _client;

        public InvoiceClient(string accessToken)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7181/")
            };

            // Set the access token in the Authorization header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        /// <summary>
        /// Submits an invoice using the provided SubmitInvoice command.
        /// </summary>
        /// <param name="command">The SubmitInvoice command containing the invoice details.</param>
        /// <returns>An ApiResponse object containing the result of the invoice submission.</returns>
        public async Task<ApiResponse<InvoiceSuccessResponse>> SubmitInvoice(SubmitInvoiceCommand command)
        {
            // Send a POST request to the "SubmitInvoice" endpoint with the invoice details.
            var response = await _client.PostAsJsonAsync("api/Invoice/SubmitInvoice", command);

            // Initialize a new ApiResponse object with the status code of the HTTP response.
            var apiResponse = new ApiResponse<InvoiceSuccessResponse>
            {
                StatusCode = response.StatusCode
            };

            // Read the content of the HTTP response as a string.
            var responseString = await response.Content.ReadAsStringAsync() ?? string.Empty;

            // Handle the HTTP response based on its status code.
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    // Deserialize the response content into a InvoiceSuccessResponse object.
                    apiResponse.Data = JsonConvert.DeserializeObject<InvoiceSuccessResponse>(responseString);

                    // Set the message of the ApiResponse to indicate a successful submission.
                    apiResponse.Message = "Invoice Submitted to Zatca";

                    // If the invoice status is NOT_REPORTED or NOT_CLEARED, set the errors of the ApiResponse.
                    if (apiResponse.Data.Status == Enums.InvoiceStatus.NOT_REPORTED || apiResponse.Data.Status == Enums.InvoiceStatus.NOT_CLEARED)
                        apiResponse.Errors = apiResponse.Data.ValidationResults.errorMessages.Select(a => a.message).ToArray();
                    break;

                case HttpStatusCode.BadRequest:
                    // Deserialize the response content into an ApiErrorResponse object.
                    var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(responseString);

                    // Set the errors of the ApiResponse to the errors from the ApiErrorResponse.
                    apiResponse.Errors = errorResponse.Errors;
                    break;

                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.Unauthorized:
                    // For these status codes, set the message and errors of the ApiResponse to the content of the HTTP response.
                    apiResponse.Message = responseString;
                    apiResponse.Errors = [responseString];
                    break;

                default:
                    // For all other status codes, an unexpected error occurred.
                    // Set the message and errors of the ApiResponse to indicate an error.
                    apiResponse.Message = $"An unexpected error occurred: {response.ReasonPhrase}";
                    apiResponse.Errors = [$"An unexpected error occurred: {response.ReasonPhrase}"];
                    break;
            }

            // Return the ApiResponse object.
            return apiResponse;
        }

        /// <summary>
        /// Fetches the status of an invoice.
        /// </summary>
        /// <param name="UUID">The UUID of the invoice.</param>
        /// <returns>An ApiResponse object containing the status of the invoice.</returns>
        public async Task<ApiResponse<InvoiceStatusResponse>> GetInvoiceStatus(string UUID)
        {
            // Send a GET request to the "GetInvoiceStatus" endpoint with the UUID of the invoice.
            var response = await _client.GetAsync($"api/Invoice/GetInvoiceStatus/{UUID}/status");

            // Initialize a new ApiResponse object with the status code of the HTTP response.
            var apiResponse = new ApiResponse<InvoiceStatusResponse>
            {
                StatusCode = response.StatusCode
            };
            // Read the content of the HTTP response as a string.
            var responseString = await response.Content.ReadAsStringAsync() ?? string.Empty;

            // Handle the HTTP response based on its status code.
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    // Deserialize the response content into an InvoiceStatusResponse object
                    apiResponse.Data = JsonConvert.DeserializeObject<InvoiceStatusResponse>(responseString);

                    // Set the message of the ApiResponse to indicate a successful retrieval of invoice status.
                    apiResponse.Message = "Invoice status retrieved successfully.";

                    break;
                case HttpStatusCode.NotFound:
                case HttpStatusCode.Unauthorized:
                    // For these status codes, set the message and errors of the ApiResponse to the content of the HTTP response.
                    apiResponse.Message = responseString;
                    apiResponse.Errors = [responseString];
                    break;

                default:
                    // For all other status codes, an unexpected error occurred.
                    // Set the message and errors of the ApiResponse to indicate an error.
                    apiResponse.Message = $"An unexpected error occurred: {response.ReasonPhrase}";
                    apiResponse.Errors = [$"An unexpected error occurred: {response.ReasonPhrase}"];
                    break;
            }

            // Return the ApiResponse object.
            return apiResponse;
        }

        /// <summary>
        /// Fetches the PDF/A3 version of an invoice.
        /// </summary>
        /// <param name="UUID">The UUID of the invoice.</param>
        /// <returns>An ApiResponse object containing the PDF/A3 version of the invoice.</returns>
        public async Task<ApiResponse<byte[]>> PrintA3Invoice(string UUID)
        {
            // Send a GET request to the "PrintInvoice" endpoint with the UUID of the invoice.
            var response = await _client.GetAsync($"api/Invoice/PrintInvoice/{UUID}/PrintA3");

            // Initialize a new ApiResponse object with the status code of the HTTP response.
            var apiResponse = new ApiResponse<byte[]>
            {
                StatusCode = response.StatusCode
            };

            // Handle the HTTP response based on its status code.
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:

                    // Read the content of the HTTP response as a byte array.
                    apiResponse.Data = await response.Content.ReadAsByteArrayAsync();

                    // Set the message of the ApiResponse to indicate a successful retrieval of the invoice PDF/A3.
                    apiResponse.Message = "Invoice PDF/A3 retrieved successfully.";

                    break;
                case HttpStatusCode.NotFound:
                case HttpStatusCode.Unauthorized:
                    // Read the content of the HTTP response as a string.
                    var responseString = await response.Content.ReadAsStringAsync();
                    // For these status codes, set the message and errors of the ApiResponse to the content of the HTTP response.
                    apiResponse.Message = responseString;
                    apiResponse.Errors = [responseString];
                    break;

                default:
                    // For all other status codes, an unexpected error occurred.
                    // Set the message and errors of the ApiResponse to indicate an error.
                    apiResponse.Message = $"An unexpected error occurred: {response.ReasonPhrase}";
                    apiResponse.Errors = [$"An unexpected error occurred: {response.ReasonPhrase}"];
                    break;
            }

            // Return the ApiResponse object.
            return apiResponse;
        }

        /// <summary>
        /// Fetches a list of invoices.
        /// </summary>
        /// <param name="GetInvoicesQuery">The query parameters for fetching the invoices.</param>
        /// <returns>An ApiResponse object containing a list of invoices.</returns>
        public async Task<ApiResponse<List<GetInvoicesResponse>>> GetInvoices(GetInvoicesQuery GetInvoicesQuery)
        {
            // Start building the URL.
            var url = new StringBuilder("api/Invoice/GetInvoices");

            // Add the InvoiceTypes parameter if it's not null and has items.
            if (GetInvoicesQuery.InvoiceTypes != null && GetInvoicesQuery.InvoiceTypes.Any())
            {
                var invoiceTypesString = string.Join(",", GetInvoicesQuery.InvoiceTypes);
                url.Append($"?InvoiceTypes={invoiceTypesString}");
            }

            // Add the other parameters.
            url.Append($"&PageNumber={GetInvoicesQuery.PageNumber}&PageSize={GetInvoicesQuery.PageSize}");

            var response = await _client.GetAsync(url.ToString());

            // Initialize a new ApiResponse object with the status code of the HTTP response.
            var apiResponse = new ApiResponse<List<GetInvoicesResponse>>
            {
                StatusCode = response.StatusCode
            };
            // Read the content of the HTTP response as a string.
            var responseString = await response.Content.ReadAsStringAsync() ?? string.Empty;

            // Handle the HTTP response based on its status code.
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:

                    // Deserialize the response content into a list of GetInvoicesResponse objects.
                    apiResponse.Data = JsonConvert.DeserializeObject<List<GetInvoicesResponse>>(responseString);

                    // Set the message of the ApiResponse to indicate a successful retrieval of invoices.
                    apiResponse.Message = "Invoices retrieved successfully.";

                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.BadRequest:
                    // For these status codes, set the message and errors of the ApiResponse to the content of the HTTP response.
                    apiResponse.Message = responseString;
                    apiResponse.Errors = [responseString];
                    break;
                default:
                    // For all other status codes, an unexpected error occurred.
                    // Set the message and errors of the ApiResponse to indicate an error.
                    apiResponse.Message = $"An unexpected error occurred: {response.ReasonPhrase}";
                    apiResponse.Errors = [$"An unexpected error occurred: {response.ReasonPhrase}"];
                    break;
            }

            // Return the ApiResponse object.
            return apiResponse;
        }
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
