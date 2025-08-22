using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCMAdvertisorLambdaFunc.Mappers;
using NCMAdvertisorLambdaFunc.Messaging;
using NCMAdvertisorLambdaFunc.Messaging.Interface;
using NCMAdvertisorLambdaFunc.Services;
using NCMAdvertisorLambdaFunc.Services.Interface;

public class Program
{
    private static IHost _host;

    // Static constructor ensures DI container is created only once per Lambda container (cold start optimization)
    static Program()
    {
        _host = Host.CreateDefaultBuilder()
             .ConfigureAppConfiguration((context, builder) =>
             {
                 builder.AddJsonFile("appsettings.json", optional: true);
             })
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient();
                services.AddScoped<IGetTokenHttpClient, GetTokenHttpClient>();
                services.AddScoped<IAdvertiserService, AdvertiserService>();
                services.AddScoped<IAdvertiserApiRepository, AdvertiserApiRepository>();
                services.AddAutoMapper(typeof(AutoMapperProfile));
                services.AddLogging();
            })
            .Build();
    }

    // Expose the IServiceProvider for resolving dependencies
    public static IServiceProvider Services => _host.Services;
}
