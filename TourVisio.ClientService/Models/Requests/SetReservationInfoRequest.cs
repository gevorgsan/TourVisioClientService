namespace TourVisio.ClientService.Models.Requests;

public class SetReservationInfoRequest
{
    public string TransactionId { get; set; } = null!;
    public List<Traveller> Travellers { get; set; } = null!;
    public CustomerInfo? CustomerInfo { get; set; }
    public string ReservationNote { get; set; } = string.Empty;
    public string AgencyReservationNumber { get; set; } = string.Empty;
}

public class Traveller
{
    public string TravellerId { get; set; } = "1";
    public int Type { get; set; } = 1;
    public int Title { get; set; } = 1;
    public int PassengerType { get; set; } = 1;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public bool IsLeader { get; set; } = true;
    public DateTime BirthDate { get; set; } = DateTime.Now;
    public TravellerNationality Nationality { get; set; } = new();
    public string IdentityNumber { get; set; } = string.Empty;
    public PassportInfo PassportInfo { get; set; } = new();
    public TravellerAddress Address { get; set; } = new();
    public int OrderNumber { get; set; } = 1;
    public List<object>? Documents { get; set; }
    public List<object>? InsertFields { get; set; }
    public int Status { get; set; } = 0;
}

public class TravellerNationality
{
    public string TwoLetterCode { get; set; } = "DE";
}

public class PassportInfo
{
    public string Serial { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime ExpireDate { get; set; } = DateTime.Now;
    public DateTime IssueDate { get; set; } = DateTime.Now;
    public string CitizenshipCountryCode { get; set; } = string.Empty;
}

public class TravellerAddress
{
    public ContactPhone ContactPhone { get; set; } = new();
    public string Email { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? ZipCode { get; set; }
    public LocationRef? City { get; set; }
    public LocationRef? Country { get; set; }
    public string? Phone { get; set; }
}

public class ContactPhone
{
    public string CountryCode { get; set; } = "90";
    public string AreaCode { get; set; } = "555";
    public string PhoneNumber { get; set; } = "5555555";
}

public class LocationRef
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class CustomerInfo
{
    public bool IsCompany { get; set; }
    public PassportInfo? PassportInfo { get; set; }
    public TravellerAddress? Address { get; set; }
    public int Title { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? BirthDate { get; set; }
    public string? IdentityNumber { get; set; }
}
