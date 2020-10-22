# Configuration

SharpAPI uses a configuration system based on [ASP.NET Core configuration](https://docs.microsoft.com/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1).

## Adding configuration sources

To add configuration sources to your API:

1. If your API does not have a class that implements [`IApiStartup`](~/obj/api/SharpApi.IApiStartup.yml) then create one.
2. In the [`ConfigureAppConfiguration(IConfigurationBuilder)`](~/obj/api/SharpApi.IApiStartup.yml#SharpApi_IApiStartup_ConfigureAppConfiguration_Microsoft_Extensions_Configuration_IConfigurationBuilder_) method, add your configuration sources.
    * You can use configuration sources available through SharpAPI such as JSON files and environment variables as well as other configuration sources built for [ASP.NET Core configuration](https://docs.microsoft.com/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1).

**Example:**

```cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpApi;

public class Startup : IApiStartup
{
    public void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddJsonFile("mysettings.json", true);
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Configure dependency injection (services) here
    }
}
```

## Using configuration

Configuration can be used via [`IConfiguration`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-3.1).

### In ConfigureAppConfiguration(IConfigurationBuilder)

In the [`ConfigureAppConfiguration(IConfigurationBuilder)`](~/obj/api/SharpApi.IApiStartup.yml#SharpApi_IApiStartup_ConfigureAppConfiguration_Microsoft_Extensions_Configuration_IConfigurationBuilder_) method, an instance of [`IConfiguration`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-3.1) can be created using the [`Build()`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfigurationbuilder.build?view=dotnet-plat-ext-3.1#Microsoft_Extensions_Configuration_IConfigurationBuilder_Build) method of the [`IConfigurationBuilder`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfigurationbuilder?view=dotnet-plat-ext-3.1).

### In ConfigureServices(IServiceCollection, IConfiguration)

The [`ConfigureServices(IServiceCollection, IConfiguration)`](~/obj/api/SharpApi.IApiStartup.yml#SharpApi_IApiStartup_ConfigureServices_Microsoft_Extensions_DependencyInjection_IServiceCollection_Microsoft_Extensions_Configuration_IConfiguration_) method provides an instance of [`IConfiguration`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-3.1) as a parameter.

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
        var secret = configuration.GetValue<string>("MySecret");

        // ...
    }
}
```

### In API endpoints

API endpoints can request an instance of [`IConfiguration`](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-3.1) via [dependency injection](dependency-injection-services.md).