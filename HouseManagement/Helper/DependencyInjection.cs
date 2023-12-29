using Helper.CustomLogger;
using Microsoft.Extensions.DependencyInjection;

namespace Helper;

public static class DependencyInjection
{
    public static void AddHeplerService(this IServiceCollection services)
    {
        services.AddSingleton<ICustomLogger, CustomLogger.CustomLogger>();
    }
}