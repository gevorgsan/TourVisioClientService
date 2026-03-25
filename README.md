# TourVisioClientService

A complete .NET 8 client library for the [TourVisio](https://docs.santsg.com/tourvisio/) external service by SanTSG, usable in any .NET solution.

## Features

- **Authentication** — automatic JWT login and token refresh
- **Product Service** — arrival autocomplete, price search (by location or product ID), product info
- **Booking Service** — begin transaction, set reservation info, commit transaction, get reservation detail
- **Dependency Injection** — single-line registration via `AddTourVisioClient`
- **Strongly-typed models** for every request and response
- **Unit-tested** with Moq and xUnit

## Project Structure

```
TourVisio.ClientService/         # Class library
  ITourVisioClient.cs            # Public interface
  TourVisioClient.cs             # HTTP implementation (auto-login)
  TourVisioClientOptions.cs      # Configuration model
  ServiceCollectionExtensions.cs # DI helper
  Models/
    Requests/                    # All request DTOs
    Responses/                   # All response DTOs

TourVisio.ClientService.Tests/   # xUnit test project
```

## Quick Start

### 1. Register the client (ASP.NET Core)

```csharp
builder.Services.AddTourVisioClient(options =>
{
    options.BaseUrl  = "https://your-tourvisio-instance.example.com";
    options.Agency   = "YOUR_AGENCY";
    options.User     = "YOUR_USER";
    options.Password = "YOUR_PASSWORD";
});
```

Or bind from `appsettings.json`:

```json
{
  "TourVisio": {
    "BaseUrl":  "https://your-tourvisio-instance.example.com",
    "Agency":   "YOUR_AGENCY",
    "User":     "YOUR_USER",
    "Password": "YOUR_PASSWORD"
  }
}
```

```csharp
builder.Services.AddTourVisioClient(
    builder.Configuration.GetSection("TourVisio").Bind);
```

### 2. Inject and use `ITourVisioClient`

```csharp
public class HotelSearchService
{
    private readonly ITourVisioClient _tourVisio;

    public HotelSearchService(ITourVisioClient tourVisio)
        => _tourVisio = tourVisio;

    public async Task<PriceSearchResponse> SearchHotelsInCityAsync(
        string cityId, string checkIn, int nights)
    {
        var request = new PriceSearchRequest
        {
            ArrivalLocations = new List<ArrivalLocation>
            {
                new() { Id = cityId, Type = 2 }
            },
            CheckIn  = checkIn,
            Night    = nights,
            RoomCriteria = new List<RoomCriteria> { new() { Adult = 2 } }
        };

        return await _tourVisio.SearchByLocationAsync(request);
    }
}
```

## API Coverage

| Method | Endpoint |
|--------|----------|
| `LoginAsync` | `POST /api/authenticationservice/login` |
| `GetArrivalAutocompleteAsync` | `POST /api/productservice/getarrivalautocomplete` |
| `SearchByLocationAsync` | `POST /api/productservice/pricesearch` |
| `SearchByProductsAsync` | `POST /api/productservice/pricesearch` |
| `GetProductInfoAsync` | `POST /api/productservice/getproductInfo` |
| `BeginTransactionAsync` | `POST /api/bookingservice/begintransaction` |
| `SetReservationInfoAsync` | `POST /api/bookingservice/setreservationinfo` |
| `CommitTransactionAsync` | `POST /api/bookingservice/committransaction` |
| `GetReservationDetailAsync` | `POST /api/bookingservice/getreservationdetail` |

## Running the Tests

```bash
dotnet test TourVisio.ClientService.Tests/TourVisio.ClientService.Tests.csproj
```
