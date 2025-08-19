using Microsoft.Extensions.Logging;
using NCMAdvertisorLambdaFunc.Model.Entities;
using NCMAdvertisorLambdaFunc.Repositories.Contract;
using NCMAdvertisorLambdaFunc.Services.Contracts;

namespace NCMAdvertisorLambdaFunc.Services.Implementation
{
    public class AdvertisorService : IAdvertisorService
    {
        private readonly IGetTokenHttpClient _getToken;
        private readonly IAdvertiserApiRepository _advertiserApiRepository;
        private readonly ILogger<AdvertisorService> _logger;

        public AdvertisorService(
            IGetTokenHttpClient getToken,
            IAdvertiserApiRepository advertiserApiRepository,
            ILogger<AdvertisorService> logger)
        {
            _getToken = getToken;
            _advertiserApiRepository = advertiserApiRepository;
            _logger = logger;
        }

        public async void ProcessAdvertisor(AdvertisorEntity entity)
        {
            try
            {
                var token = await _getToken.CallExternalApiAsync();
                _logger.LogInformation("Received the token for Advertisor Id {Id}: {Token}", entity.Id, token);

                //ar token = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJkZWxvaXR0ZUBuY21zYW5kYm94LmNvbSIsImRuIjoib3U9bmNtc2FuZGJveCxkYz1vcGVyYXRpdmVvbmUiLCJwZXJtaXNzaW9ucyI6Wzg0MSwyMDEzOTI2NDEsMTY3ODE4ODQsLTUzNjIxNTUzNiwxMTUzODk0Nyw0MzA3MzUzNyw1MzcwMDIwMTYsMjA5NzY2NCwxNjc4MTMxMywyNjg1MDEwMDgsMTA0ODgzMiwxNjc4MTMxMywyNjg1MDEwMDgsMTA0ODgzMiwtMSwxNDMxNjU1NzY1LDE0MzE2NTYyNzcsLTEsMzM3NTE1NTMsMTQzMTY1NTc2NSwxNDMxNjg4NTMzLDE2OTY4OTU1NjEsLTE4NDA3MDAyNzAsNjEzNTY2NzU2LDE0MzE2NTU3NjUsMTQ5OTgxNzMwMSwtMSwxNzAzOTYxOCwtMSw0MjA1NDg4ODEsMjg2MzMxMTUzLDI4NjMzMTE1MywyODYzMzExNTMsLTEsMTQzNTg1MDA2OSwxNDMxNjU1NzY1LDE2ODQzMDEwLC0xLC0xLC0xLDE0NDg0MzI5ODEsMTQzMTY1NTc2NSw1MDQ2Mjk3OCwxMjI3MTMzNTQ1LC0xODQwNDM4MTI2LDYxMzU2Njc1NiwxNDMxNjU1NzY1LDE0MzE2NTY3ODksLTEsMTQzMTY1NTc2NSwxNDMxNjU5ODYxLDUwNTI4NzcwLDEyMjczMzAxMjEsLTE4NDA3MDAyNzAsNjEzNTY2NzU2LDEyNzc0NjUxNjEsLTE4NDA3MDAyNzAsNjEzNTY2NzU2LDE0MzE2NTU3NjUsMTQzMjcyMDcyNSwxNDMxNjU1NzY1LC0xNzg5NTY5NzA3LDEsLTEsNywyNzA1NDkxMjEsLTIxMzA1NzQzMjgsNDA1ODA3MTY4LDEwODIxOTY0ODQsNjc2MzcyODAsNTY2NjU3MjgyLDMzOTUxNzYwLC0yMTIyMjE5MTM1LC0yMTIyMjE5MTM1LDEyOV0sInBlcm1pc3Npb25zQ29tcHJlc3NlZCI6dHJ1ZSwidGVuYW50IjoibmNtc2FuZGJveCIsImlzcyI6Ik9wZXJhdGl2ZSIsImp0aSI6ImM1MDAyMWNlLTc5NDctNDA2ZS1iMjUwLTRlYmI5NjI2ZjBjOSIsImlhdCI6MTc1NTYwNDQxOSwiZXhwIjoxNzU1NjEwNDE5fQ.V2Z3YAQrY6JOJflCc_9EnJpvYVIZ5WjGmLwGL86hM6ILFwsxzgGkT0kGEvp6o6PUaK1DrIhO5vw6vFaQX7EVZA"; // For testing purposes, use a hardcoded token or mock the call

                var apiResponse = await _advertiserApiRepository.GetAdvertiserByIdAsync(token, entity.Id);
                _logger.LogInformation("Received Advertiser API response for Id {Id}: {Response}", entity.Id, apiResponse);

                // Further business logic here
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing advertiser with Id: {Id}", entity.Id);
                throw;
            }
        }
    }
}
