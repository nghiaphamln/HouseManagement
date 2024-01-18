namespace Models.Base;

[Serializable]
public class PagerSearch<T> where T : class
{
    public List<T> Results { get; set; }
    public int TotalRecord { get; set; }
    public int TotalPage { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int DisplayFrom { get; set; }
    public int DisplayTo { get; set; }

    public PagerSearch(int totalRecords, int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        Results = [];
        TotalPage = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
        TotalRecord = totalRecords;
        DisplayFrom = (pageNumber - 1) * pageSize + 1;
        DisplayTo = pageNumber * pageSize > TotalRecord ? TotalRecord : pageNumber * pageSize;
    }
}