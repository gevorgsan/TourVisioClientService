namespace TourVisio.ClientService.Models.Requests;

public class PriceSearchRequest
{
    public bool CheckAllotment { get; set; } = true;
    public bool CheckStopSale { get; set; } = true;
    public bool GetOnlyDiscountedPrice { get; set; } = false;
    public bool GetOnlyBestOffers { get; set; } = true;
    public int ProductType { get; set; } = 2;
    public List<ArrivalLocation>? ArrivalLocations { get; set; }
    public List<string>? Products { get; set; }
    public List<RoomCriteria> RoomCriteria { get; set; } = new() { new RoomCriteria { Adult = 1 } };
    public string Nationality { get; set; } = "DE";
    public string CheckIn { get; set; } = null!;
    public int Night { get; set; } = 1;
    public string Currency { get; set; } = "EUR";
    public string Culture { get; set; } = "en-US";
}

public class ArrivalLocation
{
    public string Id { get; set; } = null!;
    public int Type { get; set; } = 2;
}

public class RoomCriteria
{
    public int Adult { get; set; } = 1;
    public List<int>? ChildAges { get; set; }
}
