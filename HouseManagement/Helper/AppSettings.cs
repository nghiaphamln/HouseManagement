using Microsoft.Extensions.Configuration;
namespace Helper;

public class AppSettings
{
    private static AppSettings? _appSettings;

    private string? AppSettingValue { get; set; }

    public static string? Get(string key)
    {
        _appSettings = GetCurrentSettings(key);
        return _appSettings.AppSettingValue;
    }

    private AppSettings(IConfiguration config, string key)
    {
        AppSettingValue = config.GetValue<string>(key);
    }

    private static AppSettings GetCurrentSettings(string key)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: false
            ).AddEnvironmentVariables();

        var configuration = builder.Build();

        var settings = new AppSettings(configuration, key);

        return settings;
    }
}