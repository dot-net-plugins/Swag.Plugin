using Microsoft.Extensions.DependencyInjection;
using Swag.Plugin.Swagger.Attributes;
using Swag.Plugin.Swagger.Operations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swag.Plugin.Swagger.Models
{
    /// <summary>
    /// Provides a set of options and utilities to configure Swagger documentation for an API.
    /// This includes options for accepted languages, parameter naming conventions, and automated
    /// documentation for CRUD operations.
    /// </summary>
    public class SwagOptions
    {
        private readonly SwaggerGenOptions _options;

        /// <summary>
        /// Provides a set of options and utilities to configure Swagger documentation for an API.
        /// This includes options for accepted languages, parameter naming conventions, and automated
        /// documentation for CRUD operations.
        /// </summary>
        public SwagOptions(SwaggerGenOptions options) { _options = options; }

        /// <summary>
        /// Registers the AcceptLanguageHeaderOperationFilter with the API options to ensure
        /// the "Accept-Language" header is included in the API documentation and handled appropriately in API operations.
        /// </summary>
        public void EnableAcceptedLanguage() { _options.OperationFilter<AcceptLanguageHeaderOperationFilter>(); }

        /// <summary>
        /// Registers the CamelCaseParametersOperationFilter with the API options to ensure
        /// that all parameter names in the API operations use camelCase naming conventions.
        /// </summary>
        public void CamelCaseParameters() { _options.OperationFilter<CamelCaseParametersOperationFilter>(); }

        /// <summary>
        /// Registers the SwagCrudOperationFilter with the API options to automate Swagger documentation
        /// for CRUD operations. For optimal functionality, controllers should be annotated with the
        /// <see cref="SwagControllerAttribute"/>, specifying the associated type. The <see cref="SwagOverrideAnnotationAttribute"/>
        /// can also be used for further customization of the documentation.
        /// </summary>
        /// <remarks>
        /// Usage sample would be [SwagController(typeof(<see cref="Type"/>))] 
        /// </remarks>
        public void EnableAutoSwaggerDocumentation() { _options.OperationFilter<SwagCrudOperationFilter>(); }
    }
}
