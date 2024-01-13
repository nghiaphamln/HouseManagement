using Models.Entities;

namespace Repositories.GroupDetail;

public interface IGroupDetailRepository
{
    Task<(long Data, string Error)> Insert(GroupDetailEntity groupDetailEntity, string trackId);
}