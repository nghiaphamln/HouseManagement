using System.Diagnostics;
using System.Text;
using Helper.CustomLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Group;
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
    
    public async Task<(List<GroupEntity> Data, int TotalRecord, string Error)> GetForPaging(
        GroupGetForPagingRequest request, string trackId
    )
    {
        var stringBuilder = new StringBuilder($"GroupRepository.GetForPaging, Request: {request.ToJson()} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await using var context = new ApplicationDbContext();

            var queryGetAllByFilter = context.GroupEntities
                .Where(e => (request.FromDate == null || e.CreatedDate.Date >= request.FromDate.Value.Date) &&
                            (request.ToDate == null || e.CreatedDate.Date <= request.ToDate.Value.Date) &&
                            (request.CreatedUser == null || e.CreatedUser == request.CreatedUser));

            var totalRecord = await queryGetAllByFilter.CountAsync();
            var result = await queryGetAllByFilter
                .OrderByDescending(e => e.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (result, totalRecord, string.Empty);
        }
        catch (Exception e)
        {
            logLevel = CustomLogLevel.Critical;
            stringBuilder.Append($"Exception: {e.Message}, StackTrace:{e.StackTrace} ");
            return ([], 0, e.Message);
        }
        finally
        {
            stopWatch.Stop();
            customLogger.WriteCustomLog(logger, trackId, stringBuilder.ToString(), logLevel,
                stopWatch.ElapsedMilliseconds);
        }
    }
}