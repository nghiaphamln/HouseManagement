using System.Diagnostics;
using System.Text;
using Helper.CustomLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Type;
using Repositories.Base;
using Repositories.DbContext;
using ServiceStack;

namespace Repositories.Group;

public class GroupRepository(
    ICustomLogger customLogger,
    ILogger<GroupRepository> logger
) : BaseRepository, IGroupRepository
{
    public async Task<(GroupEntity? Data, string Error)> GetByGroupName(string groupName, string trackId)
    {
        var stringBuilder = new StringBuilder("GroupRepository.GetByGroupName ");
        stringBuilder.Append($"GroupName: {groupName} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await using var context = new ApplicationDbContext();

            var groupEntity = await context.GroupEntities
                .FirstOrDefaultAsync(e => e.GroupName.ToLower().Equals(groupName.ToLower()));

            return (groupEntity, string.Empty);
        }
        catch (Exception e)
        {
            logLevel = CustomLogLevel.Critical;
            stringBuilder.Append($"Exception: {e.Message}, StackTrace:{e.StackTrace} ");
            return (null, e.Message);
        }
        finally
        {
            stopWatch.Stop();
            customLogger.WriteCustomLog(logger, trackId, stringBuilder.ToString(), logLevel,
                stopWatch.ElapsedMilliseconds);
        }
    }
    
    public async Task<(long Data, string Error)> Insert(GroupEntity groupEntity, string trackId)
    {
        var stringBuilder = new StringBuilder("GroupRepository.Insert ");
        stringBuilder.Append($"Entity: {groupEntity.ToJson()} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await using var context = new ApplicationDbContext();

            context.Add(groupEntity);
            await context.SaveChangesAsync();

            stringBuilder.Append($"Id Added: {groupEntity.Id} ");

            return (groupEntity.Id, string.Empty);
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