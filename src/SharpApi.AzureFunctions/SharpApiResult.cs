using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpApi.AzureFunctions
{
    /// <summary>
    /// An <see cref="IActionResult"/> that handles an <see cref="ApiResult"/>.
    /// </summary>
    public class SharpApiResult : IActionResult
    {
        /// <summary>
        /// The <see cref="ApiResult"/> to handle.
        /// </summary>
        private readonly ApiResult _apiResult;

        /// <summary>
        /// Creates an <see cref="IActionResult"/> to handle an <see cref="ApiResult"/>.
        /// </summary>
        /// <param name="apiResult">The <see cref="ApiResult"/> to handle.</param>
        public SharpApiResult(ApiResult apiResult)
        {
            _apiResult = apiResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            foreach (var header in _apiResult.Headers ?? Enumerable.Empty<KeyValuePair<string, List<string>>>())
            {
                context.HttpContext.Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
            }

            context.HttpContext.Response.Headers.ContentLength = _apiResult.Body?.Length;

            context.HttpContext.Response.StatusCode = (int)_apiResult.StatusCode;

            if (context.HttpContext.Request.Method.ToUpper() != "HEAD")
            {
                await _apiResult.Body.CopyToAsync(context.HttpContext.Response.Body);
            }
        }
    }
}
