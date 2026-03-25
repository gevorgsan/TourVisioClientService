using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TourVisio.ClientService.Models.Requests;
using TourVisio.ClientService.Models.Responses;

namespace TourVisio.ClientService;

/// <summary>
/// HTTP-based client for the TourVisio external service.
/// Handles authentication automatically: the JWT token is fetched on the first
/// authenticated request and reused until it expires.
/// </summary>
public class TourVisioClient : ITourVisioClient
{
    private readonly HttpClient _httpClient;
    private readonly TourVisioClientOptions _options;

    private string? _token;
    private DateTime _tokenExpiresAt = DateTime.MinValue;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public TourVisioClient(HttpClient httpClient, IOptions<TourVisioClientOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _httpClient.BaseAddress = new Uri(_options.BaseUrl.TrimEnd('/') + "/");
    }

    // ── Authentication ────────────────────────────────────────────────────────

    /// <inheritdoc />
    public async Task<LoginResponse> LoginAsync(CancellationToken cancellationToken = default)
    {
        var request = new LoginRequest
        {
            Agency = _options.Agency,
            User = _options.User,
            Password = _options.Password
        };

        var response = await PostAsync<LoginRequest, LoginResponse>(
            "api/authenticationservice/login",
            request,
            authenticated: false,
            cancellationToken);

        if (response?.Body?.Token is not null)
        {
            _token = response.Body.Token;
            _tokenExpiresAt = response.Body.ExpiresAt != default
                ? response.Body.ExpiresAt
                : DateTime.UtcNow.AddHours(1);
        }

        return response ?? new LoginResponse();
    }

    // ── Product Service ───────────────────────────────────────────────────────

    /// <inheritdoc />
    public Task<GetArrivalAutocompleteResponse> GetArrivalAutocompleteAsync(
        string query,
        string culture = "en-US",
        CancellationToken cancellationToken = default)
    {
        var request = new GetArrivalAutocompleteRequest
        {
            Query = query,
            Culture = culture
        };

        return PostAsync<GetArrivalAutocompleteRequest, GetArrivalAutocompleteResponse>(
            "api/productservice/getarrivalautocomplete",
            request,
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public Task<PriceSearchResponse> SearchByLocationAsync(
        PriceSearchRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<PriceSearchRequest, PriceSearchResponse>(
            "api/productservice/pricesearch",
            request,
            cancellationToken: cancellationToken);

    /// <inheritdoc />
    public Task<PriceSearchResponse> SearchByProductsAsync(
        PriceSearchRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<PriceSearchRequest, PriceSearchResponse>(
            "api/productservice/pricesearch",
            request,
            cancellationToken: cancellationToken);

    /// <inheritdoc />
    public Task<GetProductInfoResponse> GetProductInfoAsync(
        string productId,
        string culture = "en-US",
        CancellationToken cancellationToken = default)
    {
        var request = new GetProductInfoRequest
        {
            Product = productId,
            Culture = culture
        };

        return PostAsync<GetProductInfoRequest, GetProductInfoResponse>(
            "api/productservice/getproductInfo",
            request,
            cancellationToken: cancellationToken);
    }

    // ── Booking Service ───────────────────────────────────────────────────────

    /// <inheritdoc />
    public Task<BeginTransactionResponse> BeginTransactionAsync(
        BeginTransactionRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<BeginTransactionRequest, BeginTransactionResponse>(
            "api/bookingservice/begintransaction",
            request,
            cancellationToken: cancellationToken);

    /// <inheritdoc />
    public Task<SetReservationInfoResponse> SetReservationInfoAsync(
        SetReservationInfoRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<SetReservationInfoRequest, SetReservationInfoResponse>(
            "api/bookingservice/setreservationinfo",
            request,
            cancellationToken: cancellationToken);

    /// <inheritdoc />
    public Task<CommitTransactionResponse> CommitTransactionAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        var request = new CommitTransactionRequest { TransactionId = transactionId };
        return PostAsync<CommitTransactionRequest, CommitTransactionResponse>(
            "api/bookingservice/committransaction",
            request,
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public Task<GetReservationDetailResponse> GetReservationDetailAsync(
        string reservationNumber,
        CancellationToken cancellationToken = default)
    {
        var request = new GetReservationDetailRequest { ReservationNumber = reservationNumber };
        return PostAsync<GetReservationDetailRequest, GetReservationDetailResponse>(
            "api/bookingservice/getreservationdetail",
            request,
            cancellationToken: cancellationToken);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private async Task<TResponse> PostAsync<TRequest, TResponse>(
        string relativeUrl,
        TRequest body,
        bool authenticated = true,
        CancellationToken cancellationToken = default)
        where TResponse : new()
    {
        if (authenticated)
        {
            await EnsureTokenAsync(cancellationToken);
        }

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, relativeUrl);
        requestMessage.Content = JsonContent.Create(body, options: _jsonOptions);
        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (authenticated && _token is not null)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<TResponse>(json, _jsonOptions) ?? new TResponse();
    }

    private async Task EnsureTokenAsync(CancellationToken cancellationToken)
    {
        if (_token is null || DateTime.UtcNow >= _tokenExpiresAt.AddMinutes(-5))
        {
            await LoginAsync(cancellationToken);
        }
    }
}
