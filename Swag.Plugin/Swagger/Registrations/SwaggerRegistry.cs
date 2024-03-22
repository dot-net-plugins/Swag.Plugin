using Swag.Plugin.Swagger.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swag.Plugin.Swagger.Registrations;

/// <summary>
/// Provides extension methods for SwaggerGenOptions to facilitate the registration of Swag plugin components.
/// </summary>
public static class SwaggerRegistry
{
    /// <summary>
    /// Registers the SwagCrudOperationFilter to the Swagger generation options.
    /// This method enhances the Swagger documentation process by applying custom operation filters 
    /// that adjust API descriptions based on the Swag plugin's annotations and configurations.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to configure.</param>
    /// <param name="configureSwaggerOptions">Options offered by Swag.Plugin</param>
    public static void RegisterSwag(this SwaggerGenOptions options, Action<SwagOptions>? configureSwaggerOptions = null)
    {
        var swagOptions = new SwagOptions(options);
        configureSwaggerOptions?.Invoke(swagOptions);
    }
}
