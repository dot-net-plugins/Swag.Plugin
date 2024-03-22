namespace Swag.Plugin.Swagger.Attributes;

/// <summary>
/// Specifies an attribute that can be applied to classes to override swagger annotation settings.
/// This attribute allows control over whether summaries and descriptions are allowed for the annotated class.
/// </summary>
/// <param name="allowSummary">Indicates if the summary annotations are allowed. Default is false.</param>
/// <param name="allowDescription">Indicates if the description annotations are allowed. Default is false.</param>
[AttributeUsage(AttributeTargets.Class)]
public class SwagOverrideAnnotationClassAttribute(bool allowSummary = false, bool allowDescription = false) : Attribute
{
    /// <summary>
    /// Gets or sets a value indicating whether summary annotations are allowed for the class.
    /// </summary>
    public bool AllowSummary { get; set; } = allowSummary;

    /// <summary>
    /// Gets or sets a value indicating whether description annotations are allowed for the class.
    /// </summary>
    public bool AllowDescription { get; set; } = allowDescription;
}
