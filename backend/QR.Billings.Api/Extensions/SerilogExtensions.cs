using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;

namespace QR.Billings.Api.Extensions
{
    /// <summary>
    /// Extensions for configuring Serilog in a Web application.
    /// </summary>
    public static class SerilogExtensions
    {
        /// <summary>
        /// Adds Serilog to the Web application builder.
        /// </summary>
        /// <param name="builder">The Web application builder.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="applicationName">The name of the application.</param>
        /// <returns>The Web application builder with Serilog configured.</returns>
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string applicationName)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("ApplicationName", $"{applicationName}")
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithDemystifiedStackTraces()
                .Enrich.WithExceptionDetails()
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Seq(configuration["Seq:uri"]))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Log.Logger, true);

            return builder;
        }
    }
}
