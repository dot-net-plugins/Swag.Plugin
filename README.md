# Swag.Plugin

- [Swag.Plugin](#swagplugin)
  - [Features](#features)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [Usage](#usage)
      - [CamelCase](#camelcase)
      - [AcceptedLanguage](#acceptedlanguage)
      - [AutoSwaggerDocumentation](#autoswaggerdocumentation)
        - [Override of annotation](#override-of-annotation)

Swag.Plugin is an innovative Swagger plugin designed to enhance and simplify the usage of Swagger documentation in .NET projects. With features like CamelCasing parameters, templating HTTP methods, and adding language headers, Swag.Plugin streamlines the process of creating comprehensive and user-friendly API documentation.

## Features

In its initial release, Swag.Plugin v1 provides the following features to improve your Swagger documentation:

- **CamelCasing Parameters:** Automatically formats all parameters to use CamelCase notation, ensuring consistency and adherence to common JavaScript and JSON standards.
- **Templated HTTP Methods:** Templates for HTTP methods (GET, POST, PUT, DELETE, etc.) with basic information, making it easier to document standard API operations.
- **Adding Language Headers:** Facilitates the inclusion of Accept-Language headers in your API documentation, making it straightforward to document APIs that support multiple languages.

## Getting Started

### Prerequisites

Before installing Swag.Plugin, ensure you have the following:

- .NET Core 3.1 SDK or later.
- A compatible IDE (Visual Studio, Visual Studio Code, etc.).

### Installation

Install Swag.Plugin via NuGet with the following command:

```bash
dotnet add package Swag.Plugin --version 1.0.0
```

### Usage

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(SwaggerGenOptions =>
{
    SwaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Your title", Version = "v1" });
    SwaggerGenOptions.EnableAnnotations();
    // This is the base method
    SwaggerGenOptions.RegisterSwag(configureSwaggerOptions =>
    {
        // Configurable Options
        configureSwaggerOptions.CamelCaseParameters();
        configureSwaggerOptions.EnableAcceptedLanguage();
        configureSwaggerOptions.EnableAutoSwaggerDocumentation();
    });
});
```

#### CamelCase

Automatically converts the given parameters to camelCase.

```cs
public virtual IActionResult Get(string ThisParameter) => Ok("Swag.Plugin is awesome!");
```

This will Convert ThisParameter to thisParameter

#### AcceptedLanguage

This will automatically inject the AcceptedLanguage header on every call.

```json
 "parameters": 
 [
    {
        "name": "Accept-Language",
        "in": "header",
        "schema": {
            "type": "string",
            "default": "en-US"
        }
    }
]
```

#### AutoSwaggerDocumentation

Automatically generating the documentation to ease the swagger implementation.

```cs
[Route("api/[controller]")]
[ApiController]
[SwagController(typeof(SampleClass))]
public class FooController : ControllerBase
{

}
```

Now by default it will implement the following decorations on your Http Methods.
It will automatically add a generic value on the following methods.

- GET
- POST
- DELETE
- PATCH
- PUT

Summary, Description, Return type, Possible status codes.

##### Override of annotation

```cs
[SwaggerOperation(Summary = "Swag base implementation", OperationId = "Get")]
public virtual IActionResult Get(string ThisParameter) => Ok("Swag.Plugin is awesome!");

[SwagOverrideAnnotation(true)]
public override IActionResult Get(string ThisParameter)
{
    return base.Get(ThisParameter);
}
```

in this sample the inherited version will implement the default values provided by Swag.Plugin.
Note: This can also be used on the class to prevent inheriting the values provided in the base class.
