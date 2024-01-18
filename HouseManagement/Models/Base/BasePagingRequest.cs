namespace Models.Base;

[Serializable]
public class BasePagingRequest
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; } = 1;
}