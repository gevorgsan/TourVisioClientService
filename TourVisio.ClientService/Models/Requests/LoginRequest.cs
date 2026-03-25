namespace TourVisio.ClientService.Models.Requests;

public class LoginRequest
{
    public string Agency { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
}
