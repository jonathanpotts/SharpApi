using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace SharpApi.AspNetCore
{
    public static class AspNetCoreEndpoint
    {
        public static async Task HandleAsync(HttpContext context)
        {
            var endpoint = ApiEndpointManager.GetApiEndpoint(context.Request.Method, context.Request.Path);

            if (endpoint == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
        }
    }
}
