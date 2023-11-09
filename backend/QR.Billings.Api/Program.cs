using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Polly;
using Polly.Extensions.Http;
using QR.Billings.Api.Extensions;
using QR.Billings.Api.Middleware;
using QR.Billings.Business.Configuration;
using QR.Billings.Business.ExternalServices;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.Services;
using QR.Billings.CrossCutting.IoC;
using Serilog;
using System;
using System.Text;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog(builder.Configuration, "BILLING");
    Log.Information("Getting the motors running...");

    string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(MyAllowSpecificOrigins, builder => builder
         .WithOrigins("http://localhost:4200")
         .SetIsOriginAllowed((host) => true)
         .AllowAnyMethod()
         .AllowAnyHeader());
    });
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwagger();

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    builder.Services.Configure<ApiExternalSettings>(builder.Configuration);
    builder.Services.AddHttpClient<IBillingExternalService, BillingExternalService>()
        .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
                                {
                                TimeSpan.FromSeconds(1),
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10),
                                TimeSpan.FromSeconds(20),
                                })
        ).AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

    builder.Services.RegisterServices();

    var mongoDbSettings = builder.Configuration.GetSection("BillingDatabase");
    MongoClient mongoClient = new MongoClient(mongoDbSettings["ConnectionString"]);
    IMongoDatabase mongoDatabase = mongoClient.GetDatabase(mongoDbSettings["DatabaseName"]);

    builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

    builder.Services.AddHostedService<BillingTransactionBackgroundService>();

    builder.Services.AddHttpContextAccessor();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors(MyAllowSpecificOrigins);

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogEnricherExtensions.EnrichFromRequest);
    app.UseMiddleware<RequestSerilLogMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}