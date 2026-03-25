namespace TourVisio.ClientService.Models.Responses;

public class LoginResponseBody
{
    public string? Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}

public class LoginResponse : ApiResponse<LoginResponseBody> { }
