using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NamiCustomers.API.Filters;

public class SwaggerAuthorizeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Add the "Authorization" header to all endpoints that require authentication
        if (context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(em => em is AuthorizeAttribute))
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                Required = true,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
