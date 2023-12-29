using Autofac;
using Autofac.Extensions.DependencyInjection;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Helper;
using HouseManagement;
using Microsoft.AspNetCore.Authentication.Cookies;

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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    "HouseManagementSecurityScheme", options =>
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
        options.Events = new CookieAuthenticationEvents
        {
            OnSignedIn = context =>
            {
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                    "OnSignedIn", context.Principal?.Identity?.Name);
                return Task.CompletedTask;
            },
            OnSigningOut = context =>
            {
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                    "OnSigningOut", context.HttpContext.User.Identity?.Name);
                return Task.CompletedTask;
            },
            OnValidatePrincipal = context =>
            {
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                    "OnValidatePrincipal", context.Principal?.Identity?.Name);
                return Task.CompletedTask;
            }
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

app.Run();