using System.Net;

namespace Models.Base;

[Serializable]
public class IntegrationResponse
{
    public HttpStatusCode Status { get; set; }
    public string? Message { get; set; }
    public string? TrackId { get; set; }
}

[Serializable]
public class IntegrationResponse<T>
{
    public HttpStatusCode Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public string? TrackId { get; set; }
}