using Models.Base;

namespace Models.Account;

[Serializable]
public class RegisterRequest : BaseRequest
{
    public string FullName { get; set; } = string.Empty;
    private string _email = string.Empty;

    public string Email
    {
        get => _email.Trim();
        set => _email = value;
    }

    public string Password { get; set; } = string.Empty;
}