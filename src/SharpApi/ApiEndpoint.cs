﻿using System.Threading.Tasks;

namespace SharpApi
{
    /// <summary>
    /// An endpoint that handles API requests.
    /// </summary>
    public abstract class ApiEndpoint
    {
        /// <summary>
        /// Runs upon receiving API requests.
        /// </summary>
        /// <param name="request">API request data.</param>
        /// <returns>API response data.</returns>
        public abstract Task<ApiResult> RunAsync(ApiRequest request);
    }

    /// <summary>
    /// An endpoint that handles API requests.
    /// </summary>
    /// <typeparam name="TModel">Model type for body data.</typeparam>
    public abstract class ApiEndpoint<TModel> : ApiEndpoint
    {
        /// <summary>
        /// Runs upon receiving API requests.
        /// </summary>
        /// <param name="request">API request data.</param>
        /// <returns>API response data.</returns>
        public abstract Task<ApiResult> RunAsync(ApiRequest<TModel> request);

        /// <summary>
        /// Runs upon receiving API requests.
        /// </summary>
        /// <param name="request">API request data.</param>
        /// <returns>API response data.</returns>
        public override Task<ApiResult> RunAsync(ApiRequest request) => RunAsync(new ApiRequest<TModel>(request));
    }
}
