namespace NCMAdvertisorLambdaFunc.Repositories.Contract
{
    public interface IGetTokenHttpClient
    {
        Task<string> CallExternalApiAsync();
    }
}
