using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using TourVisio.ClientService;
using TourVisio.ClientService.Models.Requests;
using TourVisio.ClientService.Models.Responses;

namespace TourVisio.ClientService.Tests;

public class TourVisioClientTests
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    private static TourVisioClient CreateClient(
        Mock<HttpMessageHandler> handlerMock,
        TourVisioClientOptions? options = null)
    {
        options ??= new TourVisioClientOptions
        {
            BaseUrl = "https://test.example.com",
            Agency = "TESTAGENCY",
            User = "testuser",
            Password = "testpass"
        };

        var httpClient = new HttpClient(handlerMock.Object);
        var optionsMock = new Mock<IOptions<TourVisioClientOptions>>();
        optionsMock.Setup(o => o.Value).Returns(options);

        return new TourVisioClient(httpClient, optionsMock.Object);
    }

    private static Mock<HttpMessageHandler> SetupHandler(object responseBody, string url = "")
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var json = JsonSerializer.Serialize(responseBody, JsonOpts);

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });

        return handlerMock;
    }

    [Fact]
    public async Task LoginAsync_ReturnsToken()
    {
        var loginResp = new
        {
            header = new { success = true, requestId = "req1", responseTime = DateTime.UtcNow },
            body = new { token = "jwt-token-abc", expiresAt = DateTime.UtcNow.AddHours(1) }
        };

        var handler = SetupHandler(loginResp);
        var client = CreateClient(handler);

        var result = await client.LoginAsync();

        Assert.NotNull(result);
        Assert.NotNull(result.Header);
        Assert.True(result.Header!.Success);
        Assert.Equal("jwt-token-abc", result.Body!.Token);
    }

    [Fact]
    public async Task GetArrivalAutocompleteAsync_ReturnsItems()
    {
        var loginResp = new
        {
            header = new { success = true },
            body = new { token = "t", expiresAt = DateTime.UtcNow.AddHours(1) }
        };
        var autocompleteResp = new
        {
            header = new { success = true },
            body = new
            {
                items = new[]
                {
                    new { id = "1", name = "Paris", type = "City" }
                }
            }
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var responses = new Queue<object>(new object[] { loginResp, autocompleteResp });
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                var body = responses.Dequeue();
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonSerializer.Serialize(body, JsonOpts),
                        System.Text.Encoding.UTF8, "application/json")
                };
            });

        var client = CreateClient(handlerMock);
        var result = await client.GetArrivalAutocompleteAsync("Paris");

        Assert.NotNull(result);
        Assert.True(result.Header!.Success);
        Assert.Single(result.Body!.Items!);
        Assert.Equal("Paris", result.Body.Items![0].Name);
    }

    [Fact]
    public async Task BeginTransactionAsync_ReturnsTransactionId()
    {
        var loginResp = new
        {
            header = new { success = true },
            body = new { token = "t", expiresAt = DateTime.UtcNow.AddHours(1) }
        };
        var transactionResp = new
        {
            header = new { success = true },
            body = new
            {
                transactionId = "txn-123",
                expiresOn = DateTime.UtcNow.AddMinutes(30),
                status = 1
            }
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var responses = new Queue<object>(new object[] { loginResp, transactionResp });
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                var body = responses.Dequeue();
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonSerializer.Serialize(body, JsonOpts),
                        System.Text.Encoding.UTF8, "application/json")
                };
            });

        var client = CreateClient(handlerMock);
        var request = new BeginTransactionRequest { OfferIds = new List<string> { "offer-abc" } };
        var result = await client.BeginTransactionAsync(request);

        Assert.NotNull(result);
        Assert.Equal("txn-123", result.Body!.TransactionId);
    }

    [Fact]
    public async Task CommitTransactionAsync_ReturnsReservationNumber()
    {
        var loginResp = new
        {
            header = new { success = true },
            body = new { token = "t", expiresAt = DateTime.UtcNow.AddHours(1) }
        };
        var commitResp = new
        {
            header = new { success = true },
            body = new
            {
                reservationNumber = "RES-456",
                reservationStatus = 1,
                confirmationStatus = 1
            }
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var responses = new Queue<object>(new object[] { loginResp, commitResp });
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                var body = responses.Dequeue();
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonSerializer.Serialize(body, JsonOpts),
                        System.Text.Encoding.UTF8, "application/json")
                };
            });

        var client = CreateClient(handlerMock);
        var result = await client.CommitTransactionAsync("txn-123");

        Assert.NotNull(result);
        Assert.Equal("RES-456", result.Body!.ReservationNumber);
    }

    [Fact]
    public async Task TokenIsReusedForMultipleRequests()
    {
        var loginResp = new
        {
            header = new { success = true },
            body = new { token = "t", expiresAt = DateTime.UtcNow.AddHours(1) }
        };
        var autocompleteResp = new
        {
            header = new { success = true },
            body = new { items = Array.Empty<object>() }
        };

        int callCount = 0;
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                callCount++;
                var body = callCount == 1 ? (object)loginResp : autocompleteResp;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonSerializer.Serialize(body, JsonOpts),
                        System.Text.Encoding.UTF8, "application/json")
                };
            });

        var client = CreateClient(handlerMock);

        // First call should trigger one login + one autocomplete = 2 HTTP calls
        await client.GetArrivalAutocompleteAsync("Rome");
        // Second call should reuse the token = 1 more HTTP call (no login)
        await client.GetArrivalAutocompleteAsync("Milan");

        Assert.Equal(3, callCount);
    }

    [Fact]
    public async Task GetProductInfoAsync_SendsCorrectProductId()
    {
        var loginResp = new
        {
            header = new { success = true },
            body = new { token = "t", expiresAt = DateTime.UtcNow.AddHours(1) }
        };
        var productResp = new
        {
            header = new { success = true },
            body = new
            {
                hotel = new { id = "hotel-99", name = "Grand Hotel", stars = 5 }
            }
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var responses = new Queue<object>(new object[] { loginResp, productResp });
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                var body = responses.Dequeue();
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonSerializer.Serialize(body, JsonOpts),
                        System.Text.Encoding.UTF8, "application/json")
                };
            });

        var client = CreateClient(handlerMock);
        var result = await client.GetProductInfoAsync("hotel-99");

        Assert.NotNull(result);
        Assert.Equal("Grand Hotel", result.Body!.Hotel!.Name);
    }
}
