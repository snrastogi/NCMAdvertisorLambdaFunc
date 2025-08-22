using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCMAdvertisorLambdaFunc.Constants;
using NCMAdvertisorLambdaFunc.Dto;
using NCMAdvertisorLambdaFunc.Messaging.Interface;
using System.Text;
using System.Text.Json;

namespace NCMAdvertisorLambdaFunc.Messaging
{
    public class GetTokenHttpClient : IGetTokenHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GetTokenHttpClient> _logger;
        private readonly IConfiguration _configuration;

        public GetTokenHttpClient(HttpClient httpClient, ILogger<GetTokenHttpClient> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration =configuration;
        }

        public async Task<string> CallExternalApiAsync()
        {
            var baseUrl = _configuration[Constant.TokenApi];
            var tenentId = _configuration[Constant.TenentId];
            var url = $"{baseUrl}/{tenentId}";

            var requestBody = new
            {
                apiKey = _configuration[Constant.ApiKey],
                expiration = int.Parse(_configuration[Constant.Expiration] ?? Constant.DefaultExpiration),
                password = _configuration[Constant.Password],
                userId = _configuration[Constant.UserId]
            };

            var json = JsonSerializer.Serialize(requestBody);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, Constant.ContentType)
            };

            httpRequest.Headers.Add(Constant.UserAgentKey, Constant.UserAgentValue);

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