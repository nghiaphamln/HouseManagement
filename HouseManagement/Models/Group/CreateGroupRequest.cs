using Models.Base;

namespace Models.Group;

[Serializable]
public class CreateGroupRequest : BaseRequest
{
    private string _groupName = string.Empty;
    public string GroupName
    {
        get => _groupName.Trim();
        set => _groupName = value;
    }

    public int? LimitMember { get; set; }

    private string? _note;
    public string? Note
    {
        get => _note?.Trim();
        set => _note = value;
    }
}