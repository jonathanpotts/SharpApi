using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
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

            var headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToList());
            var query = context.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToList());

            using var apiRequest = new ApiRequest(headers, query, context.Request.Body);
            using var result = await endpoint.RunAsync(apiRequest);

            foreach (var header in result.Headers ?? Enumerable.Empty<KeyValuePair<string, List<string>>>())
            {
                context.Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
            }

            context.Response.Headers.ContentLength = result.Body?.Length;

            context.Response.StatusCode = (int)result.StatusCode;

            await result.Body.CopyToAsync(context.Response.Body);
        }
    }
}
