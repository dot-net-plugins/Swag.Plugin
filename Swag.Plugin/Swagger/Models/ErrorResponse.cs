namespace Swag.Plugin.Swagger.Models;

/// <summary>
/// Represents an error response with details about the error.
/// This can include the status code, a message explaining the error, and optionally a stack trace for debugging purposes.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the status code associated with the error.
    /// This is typically an HTTP status code when used in web contexts.
    /// </summary>
    public string? StatusCode { get; set; }

    /// <summary>
    /// Gets or sets a message that describes the error.
    /// This is intended to be a human-readable explanation of the error.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the stack trace for the error.
    /// This can be useful for debugging purposes to understand where and how the error occurred.
    /// Including a stack trace is optional and may be omitted in production environments for security reasons.
    /// </summary>
    public string? StackTrace { get; set; }
}
