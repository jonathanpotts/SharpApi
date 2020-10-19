using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SharpApi
{
    /// <summary>
    /// An API result that instructs client to perform a redirect.
    /// </summary>
    public class RedirectResult : ApiResult
    {
        /// <summary>
        /// Creates an API result that instructs client to perform a redirect.
        /// </summary>
        /// <param name="url">Location to redirect to.</param>
        /// <param name="permanent">Determines if all future requests to this location should be redirected to the new location.</param>
        /// <param name="preserveMethod">Determines if the redirect must use the same HTTP method.</param>
        public RedirectResult(string url, bool permanent = false, bool preserveMethod = false)
        {
            StatusCode = permanent
                ? (preserveMethod ? StatusCodes.Status308PermanentRedirect : StatusCodes.Status301MovedPermanently)
                : (preserveMethod ? StatusCodes.Status307TemporaryRedirect : StatusCodes.Status302Found);

            Headers = new Dictionary<string, IList<string>>
            {
                { "Location", new List<string> { url } }
            };
        }
    }
}
