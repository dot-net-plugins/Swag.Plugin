using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swag.Plugin.Swagger.Operations
{
    public class AcceptLanguageHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Adds a predefined "Accept-Language" parameter to the OpenAPI operation, 
        /// indicating the desired language for the response. The parameter is not required 
        /// and defaults to "nl-NL" if not specified by the client.
        /// </summary>
        /// <param name="operation">The OpenAPI operation to which the "Accept-Language" parameter is added.</param>
        /// <param name="context">The context of the operation filter, providing additional information for operation configuration. This parameter is not used in the current implementation but is included for compatibility with interface requirements or future extensions.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context) { operation.Parameters.Add(new OpenApiParameter { Name = "Accept-Language", Required = false, In = ParameterLocation.Header, Schema = new OpenApiSchema { Type = "string", Default = new OpenApiString("nl-NL") } }); }
    }
}
