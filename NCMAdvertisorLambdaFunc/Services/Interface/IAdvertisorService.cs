using NCMAdvertisorLambdaFunc.Models;

namespace NCMAdvertisorLambdaFunc.Services.Interface
{
    public interface IAdvertisorService
    {
        void ProcessAdvertisor(AdvertisorEntity entity);
    }
}
