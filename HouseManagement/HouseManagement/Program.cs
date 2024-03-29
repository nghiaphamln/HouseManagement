using Autofac;
using Autofac.Extensions.DependencyInjection;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Helper;
using HouseManagement;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Repositories.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new AutofacModule()));

builder.Services.AddDefaultCorrelationId(options =>
{
    options.CorrelationIdGenerator = () => Guid.NewGuid().ToString("N");
    options.AddToLoggingScope = false;
    options.EnforceHeader = false;
    options.IgnoreRequestHeader = false;
    options.IncludeInResponse = true;
    options.RequestHeader = "X-Request-ID";
    options.ResponseHeader = "X-Request-ID";
    options.UpdateTraceIdentifier = false;
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContextFactory<ApplicationDbContext>(option =>
        option.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services
    .AddDataProtection()
    .SetApplicationName("HouseManagement")
    .UseCryptographicAlgorithms(
    new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

builder.Services.AddAuthentication("HouseManagementSecurityScheme")
    .AddCookie("HouseManagementSecurityScheme", options =>
    {
        options.AccessDeniedPath = new PathString("/Account/Forbidden");
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = ".HouseManagement.Security.Cookie",
            Path = "/",
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = new PathString("/Account/Login");
        options.ReturnUrlParameter = "RequestPath";
        options.SlidingExpiration = true;
    });

builder.Services.AddAutofac().AddHeplerService();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCorrelationId();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();