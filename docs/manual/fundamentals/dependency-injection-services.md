# Dependency Injection (Services)

SharpAPI uses a dependency injection (services) system based on [ASP.NET Core dependency injection](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1).

## Adding services

To add services to your API:

1. If your API does not have a class that implements [`IApiStartup`](~/obj/api/SharpApi.IApiStartup.yml) then create one.
2. In the [`ConfigureServices(IServiceCollection, IConfiguration)`](~/obj/api/SharpApi.IApiStartup.yml#SharpApi_IApiStartup_ConfigureServices_Microsoft_Extensions_DependencyInjection_IServiceCollection_Microsoft_Extensions_Configuration_IConfiguration_) method, add your services.
    * You can use services provided by SharpAPI and <span>ASP.NET</span> Core as well as your own.
    * Some services provide extension methods to add themselves to the [`IServiceCollection`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-3.1) while others have to be manually added using the following extension methods or similar:
        * **Singleton (single instance for the entire lifetime of the API):** [`AddSingleton<TService,TImplementation>(IServiceCollection)`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton?view=dotnet-plat-ext-3.1#Microsoft_Extensions_DependencyInjection_ServiceCollectionServiceExtensions_AddSingleton__2_Microsoft_Extensions_DependencyInjection_IServiceCollection_)
        * **Scoped (single instance per scope for the entire lifetime of the API):** [`AddScoped<TService,TImplementation>(IServiceCollection)`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addscoped?view=dotnet-plat-ext-3.1#Microsoft_Extensions_DependencyInjection_ServiceCollectionServiceExtensions_AddScoped__2_Microsoft_Extensions_DependencyInjection_IServiceCollection_)
        * **Transient (new instance for each request from the API):** [`AddTransient<TService,TImplementation>(IServiceCollection)`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addtransient?view=dotnet-plat-ext-3.1#Microsoft_Extensions_DependencyInjection_ServiceCollectionServiceExtensions_AddTransient__2_Microsoft_Extensions_DependencyInjection_IServiceCollection_)

**Example:**

```cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpApi;

public class Startup : IApiStartup
{
    public void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
    {
        // Add configuration sources here
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMyTransientService, MyTransientService>();
    }
}
```

## Using services

To use a service within an API endpoint, use dependency injection to get an instance of the service.

1. Add a class field used to store the reference to the service.
2. In the constructor of the API endpoint:
    1. Add a parameter for the requested service.
    2. Store the argument value in the class field you created in step 1.
3. Use the class field to use the service.

**Example:**

```cs
using SharpApi;
using System.Threading.Tasks;

[ApiEndpoint("/my-service", "GET")]
public class MyServiceEndpoint : ApiEndpoint
{
    private IMyTransientService _myService;

    public SendEmailEndpoint(IMyTransientService myService)
    {
        _myService = myService;
    }

    public override async Task<ApiResult> RunAsync(ApiRequest request)
    {
        _myService.DoSomething();

        // ...
    }
}
```
