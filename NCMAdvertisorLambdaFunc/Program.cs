using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCMAdvertisorLambdaFunc.Mapper;
using NCMAdvertisorLambdaFunc.Repositories.Contract;
using NCMAdvertisorLambdaFunc.Repositories.Implementation;
using NCMAdvertisorLambdaFunc.Services.Contracts;
using NCMAdvertisorLambdaFunc.Services.Implementation;

public class Program
{
    private static IHost _host;

    // Static constructor ensures DI container is created only once per Lambda container (cold start optimization)
    static Program()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient();
                services.AddScoped<IGetTokenHttpClient, GetTokenHttpClient>();
                services.AddScoped<IAdvertisorService, AdvertisorService>();
                services.AddScoped<IAdvertiserApiRepository, AdvertiserApiRepository>();
                services.AddAutoMapper(typeof(AutoMapperProfile));
                services.AddLogging();
            })
            .Build();
    }

    // Expose the IServiceProvider for resolving dependencies
    public static IServiceProvider Services => _host.Services;
}
