using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCMAdvertisorLambdaFunc.Constants;
using NCMAdvertisorLambdaFunc.Dto;
using NCMAdvertisorLambdaFunc.Messaging.Interface;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NCMAdvertisorLambdaFunc.Messaging
{
    public class AdvertiserApiRepository : IAdvertiserApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AdvertiserApiRepository> _logger;
        private readonly IConfiguration _configuration;

        public AdvertiserApiRepository(HttpClient httpClient, ILogger<AdvertiserApiRepository> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<AdvertiserResponse> GetAdvertiserByIdAsync(string token, string id)
        {
            var baseUrl = _configuration[Constant.AdvertisorApiBaseUrl];
            var apiKey = _configuration[Constant.ApiKey];
            var advertisers = Constant.Advertisers;
            var url = $"{baseUrl}/{apiKey}/{advertisers}/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue(Constant.Bearer, token);
            request.Headers.Add(Constant.UserAgentKey, Constant.UserAgentValue);

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
