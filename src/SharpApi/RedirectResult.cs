using System.Collections.Generic;
using System.Net;

namespace SharpApi
{
    public class RedirectResult : ApiResult
    {
        public RedirectResult(string url, bool permanent = false, bool preserveMethod = false)
        {
            StatusCode = permanent
                ? (preserveMethod ? HttpStatusCode.PermanentRedirect : HttpStatusCode.Moved)
                : (preserveMethod ? HttpStatusCode.TemporaryRedirect : HttpStatusCode.Found);

            Headers = new Dictionary<string, List<string>>
            {
                { "Location", new List<string> { url } }
            };
        }
    }
}
