using Models.Group;
using ErrorOr;
using Models.Base;
using Models.Entities;

namespace Logics.Group;

public interface ILogicGroup
{
    Task<ErrorOr<bool>> Create(CreateGroupRequest request);

    Task<ErrorOr<PagerSearch<GroupEntity>>> GetForPaging(GroupGetForPagingRequest request, string trackId);
}