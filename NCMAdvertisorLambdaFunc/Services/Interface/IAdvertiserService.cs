using NCMAdvertisorLambdaFunc.Models;

namespace NCMAdvertisorLambdaFunc.Services.Interface
{
    public interface IAdvertiserService
    {
        void ProcessAdvertisor(AdvertisorEntity entity);
    }
}
