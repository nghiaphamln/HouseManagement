using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Models.Type;

namespace Helper.CustomLogger;

public class CustomLogger : ICustomLogger
{
    public void WriteCustomLog<T>(ILogger<T> iLogger, string trackId, string logMsg,
        CustomLogLevel customLogLevel = CustomLogLevel.Info, long responseTime = 0)
    {
        var responseTimeStr = " ";
        if (responseTime > 0)
        {
            responseTimeStr = $"[{responseTime.ConvertMilisecondToHourMinSec()}]";
        }

        var stackInfoStr = "";

        if (customLogLevel != CustomLogLevel.Warn)
        {
            var st = new StackTrace(1, true);
            var stFrames = st.GetFrames();
            var stackTraceLogArr = stFrames.Where(x => x.ToString().Contains(".cs")).Select(x => x.ToString());
            stackInfoStr = $"Stack: {JsonSerializer.Serialize(stackTraceLogArr)}";
        }

        switch (customLogLevel)
        {
            case CustomLogLevel.Info:
                iLogger.LogInformation("{Responsetime}[{TrackId}] {LogMessage} {StackInfo}",
                    responseTimeStr, trackId, logMsg, stackInfoStr);
                break;
            case CustomLogLevel.Error:
                iLogger.LogError("{Responsetime}[{TrackId}] {LogMessage} {StackInfo}",
                    responseTimeStr, trackId, logMsg, stackInfoStr);
                break;
            case CustomLogLevel.Warn:
                iLogger.LogWarning("{Responsetime}[{TrackId}] {LogMessage} {StackInfo}",
                    responseTimeStr, trackId, logMsg, stackInfoStr);
                break;
            case CustomLogLevel.Critical:
                iLogger.LogCritical("{Responsetime}[{TrackId}] {LogMessage} {StackInfo}",
                    responseTimeStr, trackId, logMsg, stackInfoStr);
                break;
            default:
                iLogger.LogCritical("{Responsetime}[{TrackId}] {LogMessage} {StackInfo}",
                    responseTimeStr, trackId, logMsg, stackInfoStr);
                break;
        }
    }
}