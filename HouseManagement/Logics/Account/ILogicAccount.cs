using Models.Account;
using ErrorOr;
using Models.Entities;

namespace Logics.Account;

public interface ILogicAccount
{
    Task<ErrorOr<bool>> Register(RegisterRequest request);
    Task<ErrorOr<UserEntity>> Login(RegisterRequest request);
}