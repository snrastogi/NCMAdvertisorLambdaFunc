using Microsoft.Extensions.Logging;
using NCMAdvertisorLambdaFunc.Model.Response;
using NCMAdvertisorLambdaFunc.Repositories.Contract;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NCMAdvertisorLambdaFunc.Repositories.Implementation
{
    public class AdvertiserApiRepository : IAdvertiserApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AdvertiserApiRepository> _logger;

        public AdvertiserApiRepository(HttpClient httpClient, ILogger<AdvertiserApiRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AdvertiserResponse> GetAdvertiserByIdAsync(string token, string id)
        {
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            var url = $"https://staging-api.aos.operative.com/mdm_2/v1/{apiKey}/advertisers/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                _logger.LogInformation("Calling Advertiser API: {Url}", url);
                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Advertiser API call failed. Status: {StatusCode}, Response: {Response}", response.StatusCode, responseContent);
                    throw new HttpRequestException($"Advertiser API call failed with status code: {response.StatusCode}");
                }

                _logger.LogInformation("Advertiser API response: {Response}", responseContent);

                var advertiser = JsonSerializer.Deserialize<AdvertiserResponse>(responseContent);

                if (advertiser == null)
                {
                    _logger.LogError("Invalid Advertiser API response: {Response}", responseContent);
                    throw new InvalidOperationException("Invalid Advertiser API response received.");
                }

                return advertiser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Advertiser API.");
                throw;
            }
        }
    }
}
