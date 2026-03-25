namespace TourVisio.ClientService;

/// <summary>
/// Configuration options for the TourVisio API client.
/// </summary>
public class TourVisioClientOptions
{
    /// <summary>
    /// The base URL of the TourVisio web service (e.g., https://tourvisio.example.com).
    /// </summary>
    public string BaseUrl { get; set; } = null!;

    /// <summary>
    /// The agency code used for authentication.
    /// </summary>
    public string Agency { get; set; } = null!;

    /// <summary>
    /// The username used for authentication.
    /// </summary>
    public string User { get; set; } = null!;

    /// <summary>
    /// The password used for authentication.
    /// </summary>
    public string Password { get; set; } = null!;
}
