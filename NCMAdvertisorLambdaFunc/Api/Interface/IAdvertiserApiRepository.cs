using NCMAdvertisorLambdaFunc.Dto;

namespace NCMAdvertisorLambdaFunc.Api.Interface
{
    public interface IAdvertiserApiRepository
    {
        Task<AdvertiserResponse> GetAdvertiserByIdAsync(string token, string id);
    }
}
