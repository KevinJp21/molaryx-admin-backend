using Infrastructure.DependencyInjection;
using Application.DependencyInjection;
using WebAPI.Config.Jwt;
using WebAPI.Config.Options;
using WebAPI.Handlers;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Domain.Contracts.IServices;
var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddConfiguredOptions(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddJwtAuthentication();

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
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    }
    await next();
});
#endregion

app.UseCors("PoliticaCors");
app.UseExceptionHandler(app => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.MapGet("/", () => "MolaryxApiAdmin Activo");

// Generar hash de prueba
using (var scope = app.Services.CreateScope())
{
    var hasherService = scope.ServiceProvider
        .GetRequiredService<IHasherService>();

    var salt = hasherService.GenerateSalt();

    var passwordHash = hasherService.ComputeHash(
        "Test123456",
        salt
    );

    Console.WriteLine($"Hash: {passwordHash}");
    Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
}

app.Run();
