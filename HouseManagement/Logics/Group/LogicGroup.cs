using System.Diagnostics;
using System.Text;
using Helper.CustomLogger;
using Logics.Base;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Group;
using Models.Type;
using ServiceStack;
using ErrorOr;
using Helper;
using Repositories.Group;
using Repositories.GroupDetail;

namespace Logics.Group;

public class LogicGroup(
    ILogger<LogicGroup> logger,
    ICustomLogger customLogger,
    IGroupRepository groupRepository,
    IGroupDetailRepository groupDetailRepository
) : BaseLogic, ILogicGroup
{
    public async Task<ErrorOr<bool>> Create(CreateGroupRequest request)
    {
        var stringBuilder = new StringBuilder("LogicGroup.Create ");
        stringBuilder.Append($"Model: {request.ToJson()} ");
        var logLevel = CustomLogLevel.Info;
        var stopWatch = Stopwatch.StartNew();
        try
        {
            if (request.GroupName.IsEmpty() || request.GroupName.Length < 3 || request.GroupName.Length > 50)
            {
                stringBuilder.Append("RequestInvalid ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("Request.Invalid", "Thông tin không hợp lệ");
            }

            var (groupEntity, error) = await groupRepository.GetByGroupName(request.GroupName, request.TrackId);
            if (error.IsNotEmpty())
            {
                stringBuilder.Append($"GetByGroupNameError: {error} ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("GetByGroupName.Error", error);
            }
            
            if (groupEntity is not null)
            {
                return Error.Validation("GetByGroupName.IsExist", "Tên nhóm đã tồn tại");
            }

            groupEntity = new GroupEntity
            {
                GroupName = request.GroupName,
                LimitMember = request.LimitMember,
                Note = request.Note
            };

            (var groupId, error) = await groupRepository.Insert(groupEntity, request.TrackId);
            if (error.IsNotEmpty())
            {
                stringBuilder.Append($"InsertGroupError: {error} ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("InsertGroup.Error", error);
            }

            var groupDetailEntity = new GroupDetailEntity
            {
                Email = request.CreatedUser,
                GroupId = groupId,
                CreatedUser = request.CreatedUser
            };

            (_, error) = await groupDetailRepository.Insert(groupDetailEntity, request.TrackId);
            if (error.IsNotEmpty())
            {
                stringBuilder.Append($"InsertGroupDetailError: {error} ");
                logLevel = CustomLogLevel.Error;
                return Error.Unexpected("InsertGroupDetail.Error", error);
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