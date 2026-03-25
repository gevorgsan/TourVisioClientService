namespace TourVisio.ClientService.Models.Responses;

public class BeginTransactionResponseBody
{
    public string? TransactionId { get; set; }
    public DateTime ExpiresOn { get; set; }
    public ReservationData? ReservationData { get; set; }
    public int Status { get; set; }
    public int TransactionType { get; set; }
}

public class ReservationData
{
    public List<ReservationTraveller>? Travellers { get; set; }
    public ReservationInfo? ReservationInfo { get; set; }
    public List<ReservationService>? Services { get; set; }
    public PaymentDetail? PaymentDetail { get; set; }
    public List<object>? Invoices { get; set; }
}

public class ReservationTraveller
{
    public string? TravellerId { get; set; }
    public int Type { get; set; }
    public int Title { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public bool IsLeader { get; set; }
    public DateTime BirthDate { get; set; }
    public TravellerNationalityInfo? Nationality { get; set; }
    public string? IdentityNumber { get; set; }
    public ReservationPassportInfo? PassportInfo { get; set; }
    public ReservationAddress? Address { get; set; }
    public List<ReservationService>? Services { get; set; }
    public int OrderNumber { get; set; }
    public DateTime BirthDateFrom { get; set; }
    public DateTime BirthDateTo { get; set; }
    public List<string>? RequiredFields { get; set; }
    public List<object>? Documents { get; set; }
    public int PassengerType { get; set; }
    public int Status { get; set; }
}

public class TravellerNationalityInfo
{
    public string? TwoLetterCode { get; set; }
}

public class ReservationPassportInfo
{
    public string? Serial { get; set; }
    public string? Number { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime IssueDate { get; set; }
    public string? IssueCountryCode { get; set; }
    public string? CitizenshipCountryCode { get; set; }
}

public class ReservationAddress
{
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? ZipCode { get; set; }
    public HotelLocation? City { get; set; }
    public HotelLocation? Country { get; set; }
    public string? Phone { get; set; }
}

public class ReservationInfo
{
    public string? BookingNumber { get; set; }
    public ReservationAgency? Agency { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Note { get; set; }
    public ReservationPrice? SalePrice { get; set; }
    public ReservationPrice? TotalPrice { get; set; }
    public ReservationPrice? PriceToPay { get; set; }
    public int ReservationStatus { get; set; }
    public int ConfirmationStatus { get; set; }
    public int PaymentStatus { get; set; }
    public DateTime CreateDate { get; set; }
}

public class ReservationAgency
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}

public class ReservationPrice
{
    public double Amount { get; set; }
    public string? Currency { get; set; }
}

public class ReservationService
{
    public string? Id { get; set; }
    public int Type { get; set; }
    public ReservationPrice? Price { get; set; }
    public int PassengerType { get; set; }
    public int OrderNumber { get; set; }
    public string? Note { get; set; }
    public ReservationServiceDetails? ServiceDetails { get; set; }
    public bool IsMainService { get; set; }
    public bool IsRefundable { get; set; }
    public List<CancellationPolicy>? CancellationPolicies { get; set; }
    public List<string>? Travellers { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Adult { get; set; }
    public int Child { get; set; }
    public int Infant { get; set; }
    public int ProductType { get; set; }
    public int ServiceStatus { get; set; }
    public int ConfirmationStatus { get; set; }
}

public class ReservationServiceDetails
{
    public string? ServiceId { get; set; }
    public HotelInfo? HotelDetail { get; set; }
    public int Night { get; set; }
    public string? Room { get; set; }
    public string? Board { get; set; }
}

public class CancellationPolicy
{
    public DateTime BeginDate { get; set; }
    public DateTime DueDate { get; set; }
    public ReservationPrice? Price { get; set; }
    public int Provider { get; set; }
}

public class PaymentDetail
{
    public List<PaymentPlan>? PaymentPlan { get; set; }
    public List<object>? PaymentInfo { get; set; }
}

public class PaymentPlan
{
    public int PaymentNo { get; set; }
    public DateTime DueDate { get; set; }
    public ReservationPrice? Price { get; set; }
    public bool PaymentStatus { get; set; }
}

public class BeginTransactionResponse : ApiResponse<BeginTransactionResponseBody> { }
