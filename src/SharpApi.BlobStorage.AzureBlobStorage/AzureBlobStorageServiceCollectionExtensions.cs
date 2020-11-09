using Microsoft.Extensions.DependencyInjection;
using System;

namespace SharpApi.BlobStorage.AzureBlobStorage
{
    public static class AzureBlobStorageServiceCollectionExtensions
    {
        public static void AddAzureBlobStorage(this IServiceCollection services, Action<AzureBlobStorageOptions> options)
        {
            services.AddOptions<AzureBlobStorageOptions>().Configure(options);

            services.AddHttpClient<IBlobStorageService, AzureBlobStorageService>();
        }
    }
}
