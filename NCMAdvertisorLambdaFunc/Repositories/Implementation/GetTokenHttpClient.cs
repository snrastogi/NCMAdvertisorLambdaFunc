using Microsoft.Extensions.Logging;
using NCMAdvertisorLambdaFunc.Repositories.Contract;
using NCMAdvertisorLambdaFunc.Model.Response;
using System.Text;
using System.Text.Json;

namespace NCMAdvertisorLambdaFunc.Repositories.Implementation
{
    public class GetTokenHttpClient: IGetTokenHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GetTokenHttpClient> _logger;

        public GetTokenHttpClient(HttpClient httpClient, ILogger<GetTokenHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> CallExternalApiAsync()
        {
            var url = "https://staging-api.aos.operative.com/mayiservice/tenant/ncmsandbox";
            var requestBody = new
            {
                apiKey = "6a5781b4-5058-43d5-b65a-be5ce353e69d",
                expiration = 100,
                password = "NCMstgD3LL0ite",
                userId = "deloitte@ncmsandbox.com"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            try
            {
                _logger.LogInformation("Calling external API: {Url}", url);
                var response = await _httpClient.SendAsync(httpRequest);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API call failed. Status: {StatusCode}, Response: {Response}", response.StatusCode, responseContent);
                    throw new HttpRequestException($"API call failed with status code: {response.StatusCode}");
                }

                _logger.LogInformation("API response: {Response}", responseContent);

                var apiResponse = JsonSerializer.Deserialize<GetTokenResponse>(responseContent);

                if (apiResponse == null || string.IsNullOrEmpty(apiResponse.token))
                {
                    _logger.LogError("Invalid API response: {Response}", responseContent);
                    throw new InvalidOperationException("Invalid API response received. Token is null or empty");
                }
                _logger.LogInformation("API Token: {Response}", apiResponse.token);

                return apiResponse.token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling external API.");
                throw;
            }
        }
    }
}
