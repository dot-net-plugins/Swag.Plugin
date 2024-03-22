using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swag.Plugin.Swagger.Attributes;
using Swag.Plugin.Swagger.Constants;
using Swag.Plugin.Swagger.Enums;
using Swag.Plugin.Swagger.Extensions;
using Swag.Plugin.Swagger.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swag.Plugin.Swagger.Operations;

/// <summary>
/// A Swashbuckle operation filter that customizes OpenAPI documentation for CRUD operations.
/// It dynamically adjusts operation summaries, descriptions, and response descriptions
/// based on the HTTP method, controller attributes, and Swag plugin configurations.
/// </summary>
internal class SwagCrudOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies custom documentation settings to an OpenAPI operation based on Swag plugin annotations and HTTP method type.
    /// </summary>
    /// <param name="operation">The OpenAPI operation being documented.</param>
    /// <param name="context">The context providing metadata and tools for documenting the operation.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        var methodActionDescriptor = controllerActionDescriptor.MethodInfo;

        var swagControllerAttribute = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttribute<SwagControllerAttribute>();
        if (swagControllerAttribute == null) return;
        var swagOverrideAnnotationClassWide = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttribute<SwagOverrideAnnotationClassAttribute>() ?? new SwagOverrideAnnotationClassAttribute();
        var swagOverrideAnnotationAttribute = methodActionDescriptor.GetCustomAttribute<SwagOverrideAnnotationAttribute>() ?? new SwagOverrideAnnotationAttribute();
        var allowDescription = BoolHelper.GetBoolValue(swagOverrideAnnotationClassWide.AllowDescription, swagOverrideAnnotationAttribute.AllowDescription);
        var allowSummary = BoolHelper.GetBoolValue(swagOverrideAnnotationClassWide.AllowSummary, swagOverrideAnnotationAttribute.AllowSummary);
        switch (context.ApiDescription.HttpMethod?.ToLowerInvariant())
        {
            case "get":
                operation.Description = operation.Description.CheckString(GetConstant.Description, allowDescription);
                operation.Summary = operation.Summary.CheckString(GetConstant.Summary, allowSummary);
                GetResponse(operation, context, swagControllerAttribute, [ResponseCodes.Success, ResponseCodes.NoContent, ResponseCodes.BadRequest, ResponseCodes.Unauthorized, ResponseCodes.NotFound]);
                break;
            case "post":
                operation.Description = operation.Description.CheckString(PostConstant.Description, allowDescription);
                operation.Summary = operation.Summary.CheckString(PostConstant.Summary, allowSummary);
                GetResponse(operation, context, swagControllerAttribute, [ResponseCodes.Created, ResponseCodes.BadRequest, ResponseCodes.Unauthorized, ResponseCodes.Conflicted]);
                break;
            case "put":
                operation.Description = operation.Description.CheckString(PutConstant.Description, allowDescription);
                operation.Summary = operation.Summary.CheckString(PutConstant.Summary, allowSummary);
                GetResponse(operation, context, swagControllerAttribute, [ResponseCodes.Success, ResponseCodes.BadRequest, ResponseCodes.Unauthorized, ResponseCodes.NotFound, ResponseCodes.Conflicted]);
                break;
            case "delete":
                operation.Description = operation.Description.CheckString(DeleteConstant.Description, allowDescription);
                operation.Summary = operation.Summary.CheckString(DeleteConstant.Summary, allowSummary);
                GetResponse(operation, context, swagControllerAttribute, [ResponseCodes.Success, ResponseCodes.BadRequest, ResponseCodes.Unauthorized, ResponseCodes.NotFound]);
                break;
            case "patch":
                operation.Description = operation.Description.CheckString(PatchConstant.Description, allowDescription);
                operation.Summary = operation.Summary.CheckString(PatchConstant.Summary, allowSummary);
                GetResponse(operation, context, swagControllerAttribute, [ResponseCodes.Success, ResponseCodes.BadRequest, ResponseCodes.Unauthorized, ResponseCodes.NotFound]);
                break;
            default:
                operation.Description = "This case acts as a fallback for undefined or unsupported HTTP methods requested by the client, ensuring robust error handling in the API.";
                operation.Summary = "Serves as a safeguard against unsupported or incorrectly specified HTTP methods, enhancing API error handling and client feedback.";
                GetResponse(operation, context, swagControllerAttribute, Enum.GetValues<ResponseCodes>());
                break;
        }
    }

    /// <summary>
    /// Configures the response descriptions for an operation based on predefined response codes and the type of the controller action's return value.
    /// </summary>
    /// <param name="operation">The OpenAPI operation being modified.</param>
    /// <param name="context">The context providing metadata and tools for documenting the operation.</param>
    /// <param name="controllerAttribute">The SwagControllerAttribute applied to the controller, containing metadata for documentation.</param>
    /// <param name="Codes">An array of response codes to generate documentation for.</param>
    private void GetResponse(OpenApiOperation operation, OperationFilterContext context, SwagControllerAttribute controllerAttribute, ResponseCodes[] Codes)
    {
        foreach (var code in Codes)
        {
            var statusCode = ((int)code).ToString();
            var response = new OpenApiResponse { Description = GetDefaultDescriptionForStatusCode((int)code) };
            response.Content = code switch
                               {
                                   ResponseCodes.Success or ResponseCodes.Created => new Dictionary<string, OpenApiMediaType> { ["application/json"] = new() { Schema = context.SchemaGenerator.GenerateSchema(controllerAttribute.PassedType, context.SchemaRepository) } },
                                   >= ResponseCodes.BadRequest and <= ResponseCodes.InternalServerError => new Dictionary<string, OpenApiMediaType> { ["application/json"] = new() { Schema = context.SchemaGenerator.GenerateSchema(typeof(ErrorResponse), context.SchemaRepository) } },
                                   _ => response.Content
                               };

            operation.Responses[statusCode] = response;
        }
    }

    /// <summary>
    /// Provides a default description for a given HTTP status code.
    /// </summary>
    /// <param name="statusCode">The HTTP status code needing a description.</param>
    /// <param name="passedType">Optional parameter for specifying a type associated with the status code.</param>
    /// <returns>A default description for the provided status code.</returns>
    private string GetDefaultDescriptionForStatusCode(int statusCode, string passedType = "")
    {
        return statusCode switch
               {
                   200 => "Success - The request has succeeded, and the requested information is returned in the response.",
                   201 => "Created - The request has been fulfilled, resulting in the creation of a new resource.",
                   204 => "No Content - The server has successfully fulfilled the request, but there is no content to send in the response.",
                   400 => "Bad Request - The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax).",
                   401 => "Unauthorized - The request has not been applied because it lacks valid authentication credentials for the target resource.",
                   404 => "Not Found - The server has not found anything matching the Request-URI. No indication is given of whether the condition is temporary or permanent.",
                   409 => "Conflict - The request could not be completed due to a conflict with the current state of the resource.",
                   500 => "Internal Server Error - The server encountered an unexpected condition that prevented it from fulfilling the request.",
                   _ => $"HTTP Status Code {statusCode} - This status code is not standard, or specific information for this code is not available."
               };
    }
}
