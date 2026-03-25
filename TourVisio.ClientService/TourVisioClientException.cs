namespace TourVisio.ClientService;

/// <summary>
/// Exception thrown when a TourVisio API request fails due to an HTTP or network error.
/// </summary>
public class TourVisioClientException : HttpRequestException
{
    /// <summary>
    /// Initializes a new instance of <see cref="TourVisioClientException"/>.
    /// </summary>
    /// <param name="message">A message describing the failure.</param>
    /// <param name="inner">The underlying exception, if any.</param>
    public TourVisioClientException(string message, Exception? inner = null)
        : base(message, inner)
    {
    }
}
