namespace Swag.Plugin.Swagger.Extensions;

/// <summary>
/// Provides helper methods for string operations.
/// </summary>
internal static class StringHelper
{
    /// <summary>
    /// Checks a string and possibly replaces it based on its content and an allowed flag.
    /// </summary>
    /// <param name="baseString">The string to check. This is an extension method parameter.</param>
    /// <param name="replacement">The replacement string to use if the base string is null, empty, or whitespace, or if the operation is not allowed.</param>
    /// <param name="allowed">A boolean flag indicating whether the check and potential replacement operation is allowed.</param>
    /// <returns>The original string if it's not null, empty, or whitespace and the operation is allowed; otherwise, the replacement string.</returns>
    internal static string CheckString(this string baseString, string replacement, bool allowed)
    {
        if (string.IsNullOrWhiteSpace(baseString) || !allowed)
            return replacement;
        return baseString;
    }
}
