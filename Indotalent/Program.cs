using Indotalent;
using Indotalent.Application;
using Indotalent.AppSettings;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Middlewares;
using Indotalent.Infrastructures.ODatas;
using Indotalent.Infrastructures.Pdfs;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerUI;

using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

using DotNetEnv;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.WebHost.ConfigureKestrel(opt => opt.ListenAnyIP(5007));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "API de Indotalent",
            Description = "Una API para gestionar la aplicación de Indotalent",
            Contact = new OpenApiContact { Name = "Tu Nombre", Email = "tu.email@example.com" }
        });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.SupportNonNullableReferenceTypes();
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services
    .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .Configure<IdentitySettings>(builder.Configuration.GetSection(IdentitySettings.IdentitySettingsName));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        var identitySettings = builder.Configuration.GetSection(IdentitySettings.IdentitySettingsName)
            .Get<IdentitySettings>();
        if (identitySettings != null)
        {
            options.SignIn.RequireConfirmedAccount = identitySettings.RequireConfirmedAccount;
            options.Password.RequireDigit = identitySettings.RequireDigit;
            options.Password.RequiredLength = identitySettings.RequiredLength;
            options.Password.RequireNonAlphanumeric = identitySettings.RequireNonAlphanumeric;
            options.Password.RequireUppercase = identitySettings.RequireUppercase;
            options.Password.RequireLowercase = identitySettings.RequireLowercase;
            options.Lockout.DefaultLockoutTimeSpan = identitySettings.DefaultLockoutTimeSpan;
            options.Lockout.MaxFailedAccessAttempts = identitySettings.MaxFailedAccessAttempts;
        }
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("Bearer", options =>
    {
        var domain = $"https://{builder.Configuration["Auth0Domain"]}/";
        var audience = builder.Configuration["ApiIdentifier"];
        options.Authority = domain;
        options.Audience = audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = domain,
            ValidateAudience = true,
            ValidAudiences = new[] { audience, $"{domain}userinfo" },
            ValidateLifetime = true,
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"🔴 Auth Failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine("🔴 Token challenge triggered");
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                Console.WriteLine("🔵 Message received for authentication");
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                Console.WriteLine("🔴 Forbidden access");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(opt =>
    opt.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

builder.Services
    .Configure<SmtpConfiguration>(builder.Configuration.GetSection("SmtpConfiguration"));

builder.Services
    .Configure<RegistrationConfiguration>(builder.Configuration.GetSection("RegistrationConfiguration"));

builder.Services.Configure<ApplicationConfiguration>(builder.Configuration.GetSection("ApplicationConfiguration"));

builder.Services
    .AddAllCustomServices();

builder.Services
    .AddApplicationServices();

builder.Services
    .AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services
    .AddSingleton<IPdfService, PdfService>();

builder.Services
    .AddSingleton<SyncPdfService>();

builder.Services
    .AddCustomOData();

var app = builder.Build();

var license = app.Configuration.GetSection("SyncfusionLicense").Get<string>();
Console.WriteLine($"License: {license?.Replace(license, new string('*', license.Length))}");
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Indotalent v1");
        c.RoutePrefix = string.Empty;
        c.DocExpansion(DocExpansion.None);
    });
}
else
{
    app.UseExceptionHandler("/Error");
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var appConfig = services.GetRequiredService<IOptions<ApplicationConfiguration>>();
    if (appConfig.Value.IsDevelopment)
    {
        await context.Database.EnsureCreatedAsync(); //<===*** Development Only !!! ***
    }

    await DbInitializer.InitializeAsync(services);
    await context.CreateInventoryStockView();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

// app.UseMiddleware<LogAnalyticMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePages();

app.UseODataBatching();

app.MapControllers();

await app.RunAsync();
