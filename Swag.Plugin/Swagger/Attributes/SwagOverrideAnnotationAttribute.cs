namespace Swag.Plugin.Swagger.Attributes;

/// <summary>
/// Specifies whether a method's summary and description can be overridden in the Swagger documentation.
/// </summary>
/// <remarks>
/// This attribute is useful for controlling the level of detail included in the generated Swagger documentation
/// for specific methods. By setting the AllowSummary and AllowDescription properties, the documentation generation
/// process can be customized to include or exclude summaries and descriptions.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class SwagOverrideAnnotationAttribute : Attribute
{
    /// <summary>
    /// Specifies whether a method's summary and description can be overridden in the Swagger documentation.
    /// </summary>
    /// <remarks>
    /// This attribute is useful for controlling the level of detail included in the generated Swagger documentation
    /// for specific methods. By setting the AllowSummary and AllowDescription properties, the documentation generation
    /// process can be customized to include or exclude summaries and descriptions.
    /// </remarks>
    /// <param name="allowSummary">Indicates whether the summary can be overridden in the Swagger documentation.</param>
    /// <param name="allowDescription">Indicates whether the description can be overridden in the Swagger documentation.</param>
    public SwagOverrideAnnotationAttribute(bool allowSummary = false, bool allowDescription = false)
    {
        AllowSummary = allowSummary;
        AllowDescription = allowDescription;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the summary can be overridden in the Swagger documentation.
    /// </summary>
    public bool AllowSummary { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the description can be overridden in the Swagger documentation.
    /// </summary>
    public bool AllowDescription { get; set; }
}
