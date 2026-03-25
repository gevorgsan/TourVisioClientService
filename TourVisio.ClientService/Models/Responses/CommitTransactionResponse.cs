namespace TourVisio.ClientService.Models.Responses;

public class CommitTransactionResponseBody
{
    public string? ReservationNumber { get; set; }
    public int ReservationStatus { get; set; }
    public int ConfirmationStatus { get; set; }
    public ReservationPrice? SalePrice { get; set; }
    public ReservationPrice? TotalPrice { get; set; }
}

public class CommitTransactionResponse : ApiResponse<CommitTransactionResponseBody> { }
