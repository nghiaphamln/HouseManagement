namespace Helper.Password;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Compare(string password, string hashedPassword);
}