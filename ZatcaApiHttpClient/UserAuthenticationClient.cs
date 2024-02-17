using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using ZatcaApiHttpClient.Contract;
using ZatcaApiHttpClient.Models.Login;

namespace ZatcaApiHttpClient
{
    public class UserAuthenticationClient
    {
        private readonly HttpClient _client;

        public UserAuthenticationClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://zatca.quixcel.com/")
            };
        }

        // This method is responsible for handling the login process.
        public async Task<ApiResponse<LoginSuccessResponse>> Login(LoginCommand command)
        {
            // Send a POST request to the login API endpoint with the login command as JSON.
            var response = await _client.PostAsJsonAsync("api/UserAuthentication/Login", command);

            // Initialize a new ApiResponse object with the status code of the HTTP response.
            var apiResponse = new ApiResponse<LoginSuccessResponse>
            {
                StatusCode = response.StatusCode
            };

            // Read the content of the HTTP response as a string.
            var responseString = await response.Content.ReadAsStringAsync();

            // Handle the HTTP response based on its status code.
            switch (response.StatusCode)
            {
                // If the status code is OK (200), the login was successful.
                case HttpStatusCode.OK:
                    // Deserialize the response content into a LoginSuccessResponse object.
                    apiResponse.Data = JsonConvert.DeserializeObject<LoginSuccessResponse>(responseString);
                    // Set the message of the ApiResponse to indicate a successful login.
                    apiResponse.Message = "Login successful.";
                    break;

                // If the status code is Unauthorized (401) or NotFound (404), there was an error with the login.
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                    // Set the message and errors of the ApiResponse to the content of the HTTP response.
                    apiResponse.Message = responseString;
                    apiResponse.Errors = [responseString];
                    break;

                // For all other status codes, an unexpected error occurred.
                default:
                    // Set the message and errors of the ApiResponse to indicate an error.
                    apiResponse.Message = $"An error occurred: {response.ReasonPhrase}";
                    apiResponse.Errors = [$"An error occurred: {response.ReasonPhrase}"];
                    break;
            }

            // Return the ApiResponse object.
            return apiResponse;
        }




    }
}
