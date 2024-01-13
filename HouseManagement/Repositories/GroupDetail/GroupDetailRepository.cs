using System.Diagnostics;
using System.Text;
using Helper.CustomLogger;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Type;
using Repositories.Base;
using Repositories.DbContext;
using ServiceStack;

namespace Repositories.GroupDetail;

public class GroupDetailRepository(
    ICustomLogger customLogger,
    ILogger<GroupDetailRepository> logger
) : BaseRepository, IGroupDetailRepository
{
    public async Task<(long Data, string Error)> Insert(GroupDetailEntity groupDetailEntity, string trackId)
    {
        var stringBuilder = new StringBuilder("GroupDetailRepository.Insert ");
        stringBuilder.Append($"Entity: {groupDetailEntity.ToJson()} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await using var context = new ApplicationDbContext();

            context.Add(groupDetailEntity);
            await context.SaveChangesAsync();

            stringBuilder.Append($"Id Added: {groupDetailEntity.Id} ");

            return (groupDetailEntity.Id, string.Empty);
        }
        catch (Exception e)
        {
            logLevel = CustomLogLevel.Critical;
            stringBuilder.Append($"Exception: {e.Message}, StackTrace:{e.StackTrace} ");
            return (-1, e.Message);
        }
        finally
        {
            stopWatch.Stop();
            customLogger.WriteCustomLog(logger, trackId, stringBuilder.ToString(), logLevel,
                stopWatch.ElapsedMilliseconds);
        }
    }
}