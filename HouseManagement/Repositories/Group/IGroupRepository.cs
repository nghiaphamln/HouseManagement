using Models.Entities;
using Models.Group;

namespace Repositories.Group;

public interface IGroupRepository
{
    Task<(GroupEntity? Data, string Error)> GetByGroupName(string groupName, string trackId);
    Task<(long Data, string Error)> Insert(GroupEntity groupEntity, string trackId);

    Task<(List<GroupEntity> Data, int TotalRecord, string Error)> GetForPaging(
        GroupGetForPagingRequest request, string trackId);
}