namespace Models.Base;

[Serializable]
public class BaseRequest
{
    public string TrackId { get; set; } = Guid.NewGuid().ToString();
}