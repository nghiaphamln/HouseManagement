using Models.Entities;

namespace Repositories.User;

public interface IUserRepository
{
    Task<(UserEntity? Data, string Error)> GetByEmail(string email, string trackId);
    Task<string> Insert(UserEntity userEntity, string trackId);
}