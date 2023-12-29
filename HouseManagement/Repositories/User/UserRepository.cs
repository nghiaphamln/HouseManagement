using System.Diagnostics;
using System.Text;
using Helper.CustomLogger;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Type;
using Repositories.Base;
using Repositories.DbContext;
using Microsoft.EntityFrameworkCore;
using ServiceStack;

namespace Repositories.User;

public class UserRepository(
    ICustomLogger customLogger,
    ILogger<UserRepository> logger
) : BaseRepository, IUserRepository
{
    public async Task<(UserEntity? Data, string Error)> GetByEmail(string email, string trackId)
    {
        var stringBuilder = new StringBuilder("UserRepository.GetByEmail ");
        stringBuilder.Append($"Email: {email} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await using var context = new ApplicationDbContext();

            var userEntity = await context.UserEntities.FirstOrDefaultAsync(e => e.Email.Equals(email));

            return (userEntity, string.Empty);
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
    
    public async Task<string> Insert(UserEntity userEntity, string trackId)
    {
        var stringBuilder = new StringBuilder("UserRepository.Insert ");
        stringBuilder.Append($"Entity: {userEntity.ToJson()} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await using var context = new ApplicationDbContext();

            await context.UserEntities.AddAsync(userEntity);

            return string.Empty;
        }
        catch (Exception e)
        {
            logLevel = CustomLogLevel.Critical;
            stringBuilder.Append($"Exception: {e.Message}, StackTrace:{e.StackTrace} ");
            return e.Message;
        }
        finally
        {
            stopWatch.Stop();
            customLogger.WriteCustomLog(logger, trackId, stringBuilder.ToString(), logLevel,
                stopWatch.ElapsedMilliseconds);
        }
    }
}