namespace TourVisio.ClientService.Models.Requests;

public class GetProductInfoRequest
{
    public int ProductType { get; set; } = 2;
    public int OwnerProvider { get; set; } = 2;
    public string Product { get; set; } = null!;
    public string Culture { get; set; } = "en-US";
}
