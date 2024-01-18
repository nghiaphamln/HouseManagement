namespace Models.Base;

[Serializable]
public class BasePagingResponse<T>
{
    public required T Data { get; set; }
    public int TotalRecord { get; set; }
}