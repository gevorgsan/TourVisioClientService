namespace TourVisio.ClientService.Models.Requests;

public class BeginTransactionRequest
{
    public List<string> OfferIds { get; set; } = null!;
    public string Currency { get; set; } = "EUR";
    public string Culture { get; set; } = "en-US";
}
