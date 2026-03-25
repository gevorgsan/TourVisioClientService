namespace TourVisio.ClientService.Models.Responses;

public class ApiHeader
{
    public string? RequestId { get; set; }
    public bool Success { get; set; }
    public DateTime ResponseTime { get; set; }
    public List<ApiMessage>? Messages { get; set; }
}

public class ApiMessage
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public int MessageType { get; set; }
    public string? Message { get; set; }
}

public class ApiResponse<T>
{
    public T? Body { get; set; }
    public ApiHeader? Header { get; set; }
}
