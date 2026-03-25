namespace TourVisio.ClientService.Models.Responses;

public class AutocompleteItem
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Country { get; set; }
    public string? CountryId { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Provider { get; set; }
    public string? IsTopRegion { get; set; }
    public string? ParentId { get; set; }
    public string? CountryCode { get; set; }
}

public class GetArrivalAutocompleteResponseBody
{
    public List<AutocompleteItem>? Items { get; set; }
}

public class GetArrivalAutocompleteResponse : ApiResponse<GetArrivalAutocompleteResponseBody> { }
