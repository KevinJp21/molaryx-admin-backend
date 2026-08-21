using Infrastructure.DependencyInjection;
using Application.DependencyInjection;
using Presentation.Config.Jwt;
using Presentation.Config.Options;
using Presentation.Handlers;
using Presentation.Behaviors;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Authorization;
using Infrastructure.BackgroundServices;
using Infrastructure.Pdf;
var builder = WebApplication.CreateBuilder(args);

PdfFontBootstrap.Configure();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Molaryx API Admin",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});
builder.Services.AddConfiguredOptions(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApiBehaviorConfiguration();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, PermissionAuthorizationMiddlewareResultHandler>();
builder.Services.AddHostedService<ExpiredPromotionBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MOLARYX API V1");
    });

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Molaryx Api Documentation");
        options.OpenApiRoutePattern = "/swagger/v1/swagger.json";
    });
}
else
{
    app.UseHsts();
}
#region Cabeceras de seguridad
app.Use(async (context, next) =>
{
    if (!context.Response.HasStarted)
    {
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    }
    await next();
});
#endregion

app.UseForwardedHeaders();

app.UseHttpsRedirection();

app.UseCors("PoliticaCors");

app.UseExceptionHandler(app => { });

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "MolaryxApiAdmin Activo");

app.Run();