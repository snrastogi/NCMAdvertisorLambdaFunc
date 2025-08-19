using NCMAdvertisorLambdaFunc.Model.Entities;

namespace NCMAdvertisorLambdaFunc.Services.Contracts
{
    public interface IAdvertisorService
    {
        void ProcessAdvertisor(AdvertisorEntity entity);
    }
}
