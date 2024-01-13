using Models.Entities;

namespace Repositories.Group;

public interface IGroupRepository
{
    Task<(GroupEntity? Data, string Error)> GetByGroupName(string groupName, string trackId);
    Task<(long Data, string Error)> Insert(GroupEntity groupEntity, string trackId);
}