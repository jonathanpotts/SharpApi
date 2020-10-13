using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpApi.AspNetCore
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

        /// <summary>
        /// Executes the result asynchronously. Used when only a <see cref="HttpContext"/> is available.
        /// </summary>
        /// <param name="context">The context to execute the result in.</param>
        /// <returns>A task that processes the result.</returns>
        public async Task ExecuteAsync(HttpContext context)
        {
            foreach (var header in _apiResult.Headers ?? Enumerable.Empty<KeyValuePair<string, List<string>>>())
            {
                context.Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
            }

            context.Response.Headers.ContentLength = _apiResult.Body?.Length;

            context.Response.StatusCode = (int)_apiResult.StatusCode;

            if (context.Request.Method.ToUpper() != "HEAD")
            {
                await _apiResult.Body.CopyToAsync(context.Response.Body);
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await ExecuteAsync(context.HttpContext);
        }
    }
}
