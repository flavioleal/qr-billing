using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Serilog;
using System;
using System.Linq;

namespace QR.Billings.Api.Extensions
{
    /// <summary>
    /// Extension methods for enriching logs with information from HTTP requests.
    /// </summary>
    public static class LogEnricherExtensions
    {
        /// <summary>
        /// Enriches the diagnostic context with information from the HTTP request.
        /// </summary>
        /// <param name="diagnosticContext">The diagnostic context.</param>
        /// <param name="httpContext">The HTTP context representing the current request.</param>
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name);
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("Resource", httpContext.GetMetricsCurrentResourceName());
        }

        /// <summary>
        /// Gets the name of the current resource for metrics from the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context representing the current request.</param>
        /// <returns>The name of the current resource for metrics.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided HTTP context is null.</exception>
        public static string GetMetricsCurrentResourceName(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            Endpoint endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;

            return endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
        }
    }
}
