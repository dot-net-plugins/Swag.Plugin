using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swag.Plugin.Swagger.Operations
{
    public class CamelCaseParametersOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies a naming policy to the parameters of an OpenAPI operation to ensure they follow the camelCase convention.
        /// If the operation has no parameters, initializes the parameter list.
        /// </summary>
        /// <param name="operation">The OpenAPI operation whose parameters are to be modified or initialized.</param>
        /// <param name="context">The context of the operation filter, containing additional data and tools for operation configuration.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Initialize parameters list if it's null
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            else
            {
                // Convert existing parameter names to camelCase
                foreach (var item in operation.Parameters)
                {
                    item.Name = System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(item.Name);
                }
            }
        }
    }
}
