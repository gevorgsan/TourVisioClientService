namespace TourVisio.ClientService.Models.Responses;

public class GetProductInfoResponseBody
{
    public HotelInfo? Hotel { get; set; }
}

public class HotelInfo
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Stars { get; set; }
    public HotelLocation? Country { get; set; }
    public HotelLocation? City { get; set; }
    public HotelLocation? Location { get; set; }
    public HotelDetailAddress? Address { get; set; }
    public HotelGeolocation? Geolocation { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FaxNumber { get; set; }
    public string? HomePage { get; set; }
    public string? Description { get; set; }
    public List<HotelFacility>? Facilities { get; set; }
    public List<HotelImage>? Images { get; set; }
    public List<HotelRoom>? Rooms { get; set; }
}

public class HotelDetailAddress
{
    public string? AddressLine { get; set; }
    public string? ZipCode { get; set; }
    public HotelLocation? City { get; set; }
    public HotelLocation? Country { get; set; }
}

public class HotelGeolocation
{
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}

public class HotelFacility
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public bool IsPaid { get; set; }
}

public class HotelImage
{
    public string? UrlFull { get; set; }
    public string? UrlBig { get; set; }
    public string? UrlSmall { get; set; }
    public string? UrlThumbnail { get; set; }
    public int OrderNumber { get; set; }
}

public class HotelRoom
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<string>? Images { get; set; }
    public List<HotelFacility>? Facilities { get; set; }
}

public class GetProductInfoResponse : ApiResponse<GetProductInfoResponseBody> { }
