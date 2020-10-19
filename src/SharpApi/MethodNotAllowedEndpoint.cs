using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpApi
{
    /// <summary>
    /// Endpoint used when the requested path is valid but the method is not allowed.
    /// </summary>
    public class MethodNotAllowedEndpoint : ApiEndpoint
    {
        /// <summary>
        /// Allowed methods.
        /// </summary>
        private readonly string[] _allowedMethods;

        /// <summary>
        /// Creates an instance of <see cref="MethodNotAllowedEndpoint"/>.
        /// </summary>
        /// <param name="allowedMethods">Allowed methods.</param>
        public MethodNotAllowedEndpoint(string[] allowedMethods)
        {
            _allowedMethods = allowedMethods;
        }

        public override Task<ApiResult> RunAsync(ApiRequest request)
        {
            var result = new StatusCodeResult(StatusCodes.Status405MethodNotAllowed)
            {
                Headers = new Dictionary<string, IList<string>>
                {
                    { "Allow", new List<string> { string.Join(", ", _allowedMethods) } }
                }
            };

            return Task.FromResult((ApiResult)result);
        }
    }
}
