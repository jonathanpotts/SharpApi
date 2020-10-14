using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SharpApi
{
    public class RequestDelegateResult : IActionResult
    {
        private RequestDelegate _requestDelegate;

        public RequestDelegateResult(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await _requestDelegate(context.HttpContext);
        }
    }
}
