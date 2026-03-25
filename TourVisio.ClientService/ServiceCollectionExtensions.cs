using Microsoft.Extensions.DependencyInjection;

namespace TourVisio.ClientService;

/// <summary>
/// Extension methods for registering the TourVisio client with the .NET dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="ITourVisioClient"/> and its dependencies using the provided
    /// configuration action.
    /// </summary>
    /// <example>
    /// <code>
    /// builder.Services.AddTourVisioClient(options =>
    /// {
    ///     options.BaseUrl  = "https://your-tourvisio-instance.example.com";
    ///     options.Agency   = "YOUR_AGENCY";
    ///     options.User     = "YOUR_USER";
    ///     options.Password = "YOUR_PASSWORD";
    /// });
    /// </code>
    /// </example>
    public static IServiceCollection AddTourVisioClient(
        this IServiceCollection services,
        Action<TourVisioClientOptions> configure)
    {
        services.Configure(configure);
        services.AddHttpClient<ITourVisioClient, TourVisioClient>();
        return services;
    }
}
