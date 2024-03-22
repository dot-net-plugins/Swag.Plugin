namespace Swag.Plugin.Swagger.Extensions;

/// <summary>
/// Provides helper methods for boolean operations.
/// </summary>
internal static class BoolHelper
{
    /// <summary>
    /// Returns the logical OR result of two boolean values.
    /// </summary>
    /// <param name="value">The first boolean value.</param>
    /// <param name="value2">The second boolean value.</param>
    /// <returns>The logical OR result of <paramref name="value"/> and <paramref name="value2"/>.</returns>
    internal static bool GetBoolValue(bool value, bool value2) => value || value2;
}
