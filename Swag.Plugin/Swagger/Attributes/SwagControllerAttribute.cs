namespace Swag.Plugin.Swagger.Attributes;

/// <summary>
/// Specifies that a class should be treated as a Swagger controller. This attribute is used
/// to indicate that the associated class provides Swagger documentation for the API
/// endpoints it contains. The attribute captures a specific Type to associate with the
/// controller for Swagger generation purposes.
/// </summary>
/// <remarks>
/// Initializes a new instance of the SwagControllerAttribute class with the specified Type.
/// </remarks>
/// <param name="type">The Type of the controller this attribute is associated with.</param>
[AttributeUsage(AttributeTargets.Class)]
public class SwagControllerAttribute(Type type) : Attribute
{
    /// <summary>
    /// Gets or sets the Type that is passed to the attribute, indicating the specific
    /// controller type this attribute is associated with for Swagger documentation purposes.
    /// </summary>
    public Type PassedType { get; set; } = type;
}
