namespace TourVisio.ClientService.Models.Responses;

public class SetReservationInfoResponseBody
{
    public string? TransactionId { get; set; }
    public ReservationData? ReservationData { get; set; }
    public int Status { get; set; }
}

public class SetReservationInfoResponse : ApiResponse<SetReservationInfoResponseBody> { }
