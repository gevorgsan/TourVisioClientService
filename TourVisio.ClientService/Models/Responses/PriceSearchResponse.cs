namespace TourVisio.ClientService.Models.Responses;

public class PriceSearchResponseBody
{
    public List<HotelOffer>? Hotels { get; set; }
    public int SearchId { get; set; }
    public int ExpiresIn { get; set; }
    public bool HasMoreOffer { get; set; }
}

public class HotelOffer
{
    public string? OfferId { get; set; }
    public string? HotelId { get; set; }
    public string? HotelName { get; set; }
    public HotelLocation? Country { get; set; }
    public HotelLocation? City { get; set; }
    public HotelLocation? Location { get; set; }
    public OfferPrice? Price { get; set; }
    public string? CheckIn { get; set; }
    public int Night { get; set; }
    public string? BoardName { get; set; }
    public string? RoomName { get; set; }
    public int Stars { get; set; }
    public int Adult { get; set; }
    public int Child { get; set; }
    public int Infant { get; set; }
    public int Provider { get; set; }
    public bool IsAvailable { get; set; }
    public bool HasDynamicStop { get; set; }
    public bool IsRefundable { get; set; }
}

public class HotelLocation
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int Type { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}

public class OfferPrice
{
    public double Amount { get; set; }
    public string? Currency { get; set; }
}

public class PriceSearchResponse : ApiResponse<PriceSearchResponseBody> { }
