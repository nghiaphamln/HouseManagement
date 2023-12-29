using Models.Account;
using ErrorOr;

namespace Logics.Account;

public interface ILogicAccount
{
    Task<ErrorOr<bool>> Register(RegisterRequest request);
    Task<ErrorOr<bool>> Login(RegisterRequest request);
}