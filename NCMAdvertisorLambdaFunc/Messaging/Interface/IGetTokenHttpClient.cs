namespace NCMAdvertisorLambdaFunc.Messaging.Interface
{
    public interface IGetTokenHttpClient
    {
        Task<string> CallExternalApiAsync();
    }
}
