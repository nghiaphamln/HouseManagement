using Microsoft.Extensions.Logging;
using Models.Type;


namespace Helper.CustomLogger;

public interface ICustomLogger
{
    void WriteCustomLog<T>(ILogger<T> iLogger, string trackId, string logMsg,
        CustomLogLevel customLogLevel = CustomLogLevel.Info, long responseTime = 0);
}