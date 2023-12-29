using System.Diagnostics;
using System.Text;
using Logics.Base;
using ErrorOr;
using Helper;
using Helper.CustomLogger;
using Microsoft.Extensions.Logging;
using Models.Account;
using Models.Entities;
using Models.Type;
using Repositories.User;
using ServiceStack;

namespace Logics.Account;

public class LogicAccount(
    ILogger<LogicAccount> logger,
    ICustomLogger customLogger,
    IUserRepository userRepository 
) : BaseLogic, ILogicAccount
{
    public async Task<ErrorOr<bool>> Register(RegisterRequest request)
    {
        var stringBuilder = new StringBuilder("LogicManifest.ConfirmByMultiTrip ");
        stringBuilder.Append($"Model: {request.ToJson()} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            if (request.FullName.IsEmpty() || request.Email.IsEmpty() || request.Password.IsEmpty())
            {
                stringBuilder.Append("RequestInvalid ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("Request.Invalid", "Thông tin không hợp lệ");
            }

            var (userEntity, error) = await userRepository.GetByEmail(request.Email, request.TrackId);
            if (error.IsNotEmpty())
            {
                stringBuilder.Append($"GetUserByEmailError: {error} ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("GetUserByEmail.Error", error);
            }

            if (userEntity is not null)
            {
                logLevel = CustomLogLevel.Warn;
                return Error.Validation("GetUserByEmail.Found", "Email đã tồn tại");
            }

            userEntity = new UserEntity
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password
            };

            error = await userRepository.Insert(userEntity, request.TrackId);
            if (error.IsNotEmpty())
            {
                stringBuilder.Append($"UserInsertError: {error} ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("UserInsert.Error", error);
            }

            return true;
        }
        catch (Exception e)
        {
            logLevel = CustomLogLevel.Critical;
            stringBuilder.Append($"Exception: {e.Message}, StackTrace:{e.StackTrace} ");
            return Error.Unexpected("Unexpected", e.Message);
        }
        finally
        {
            stopWatch.Stop();
            customLogger.WriteCustomLog(logger, request.TrackId, stringBuilder.ToString(), logLevel,
                stopWatch.ElapsedMilliseconds);
        }
    }
}