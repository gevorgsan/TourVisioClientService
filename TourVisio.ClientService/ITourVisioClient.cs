using TourVisio.ClientService.Models.Requests;
using TourVisio.ClientService.Models.Responses;

namespace TourVisio.ClientService;

/// <summary>
/// Provides a complete client interface for the TourVisio external service.
/// </summary>
public interface ITourVisioClient
{
    // ── Authentication ────────────────────────────────────────────────────────

    /// <summary>
    /// Authenticates with the TourVisio service and returns a bearer token.
    /// Subsequent calls are authenticated automatically, so this method rarely needs
    /// to be called directly.
    /// </summary>
    Task<LoginResponse> LoginAsync(CancellationToken cancellationToken = default);

    // ── Product Service ───────────────────────────────────────────────────────

    /// <summary>
    /// Returns autocomplete suggestions for arrival locations matching <paramref name="query"/>.
    /// </summary>
    Task<GetArrivalAutocompleteResponse> GetArrivalAutocompleteAsync(
        string query,
        string culture = "en-US",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for available hotel prices in the specified arrival location.
    /// </summary>
    Task<PriceSearchResponse> SearchByLocationAsync(
        PriceSearchRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for available hotel prices for the specified product IDs.
    /// </summary>
    Task<PriceSearchResponse> SearchByProductsAsync(
        PriceSearchRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves detailed information about a specific hotel product.
    /// </summary>
    Task<GetProductInfoResponse> GetProductInfoAsync(
        string productId,
        string culture = "en-US",
        CancellationToken cancellationToken = default);

    // ── Booking Service ───────────────────────────────────────────────────────

    /// <summary>
    /// Begins a booking transaction for the specified offer IDs.
    /// Returns a transaction ID that must be used in subsequent booking calls.
    /// </summary>
    Task<BeginTransactionResponse> BeginTransactionAsync(
        BeginTransactionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves traveller and customer information for an open transaction.
    /// </summary>
    Task<SetReservationInfoResponse> SetReservationInfoAsync(
        SetReservationInfoRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits and confirms the booking transaction.
    /// </summary>
    Task<CommitTransactionResponse> CommitTransactionAsync(
        string transactionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the details of an existing reservation by reservation number.
    /// </summary>
    Task<GetReservationDetailResponse> GetReservationDetailAsync(
        string reservationNumber,
        CancellationToken cancellationToken = default);
}
