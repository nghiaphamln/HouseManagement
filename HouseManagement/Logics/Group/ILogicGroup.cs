using Models.Group;
using ErrorOr;

namespace Logics.Group;

public interface ILogicGroup
{
    Task<ErrorOr<bool>> Create(CreateGroupRequest request);
}