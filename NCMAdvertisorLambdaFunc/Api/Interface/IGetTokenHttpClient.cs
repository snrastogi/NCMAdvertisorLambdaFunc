namespace NCMAdvertisorLambdaFunc.Api.Interface
{
    public interface IGetTokenHttpClient
    {
        Task<string> CallExternalApiAsync();
    }
}
