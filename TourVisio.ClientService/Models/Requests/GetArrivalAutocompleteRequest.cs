namespace TourVisio.ClientService.Models.Requests;

public class GetArrivalAutocompleteRequest
{
    public int ProductType { get; set; } = 2;
    public string Query { get; set; } = null!;
    public string Culture { get; set; } = "en-US";
}
