using NCMAdvertisorLambdaFunc.Model.Response;

namespace NCMAdvertisorLambdaFunc.Repositories.Contract
{
    public interface IAdvertiserApiRepository
    {
        Task<AdvertiserResponse> GetAdvertiserByIdAsync(string token, string id);
    }
}
