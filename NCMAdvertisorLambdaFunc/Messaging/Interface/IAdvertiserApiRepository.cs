using NCMAdvertisorLambdaFunc.Dto;

namespace NCMAdvertisorLambdaFunc.Messaging.Interface
{
    public interface IAdvertiserApiRepository
    {
        Task<AdvertiserResponse> GetAdvertiserByIdAsync(string token, string id);
    }
}
