using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIPortal.FilterAttributes
{
  public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                          .Union(context.MethodInfo.GetCustomAttributes(true))
                          .OfType<AuthorizeAttribute>();


      if (attributes != null && attributes.Count() > 0)
      {
        if (operation.Parameters == null)
          operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
          Name = "Authorization",
          In = ParameterLocation.Header,
          Description = "access token",
          Required = true,
          Schema = new OpenApiSchema
          {
            Type = "string",
            Default = new OpenApiString("Bearer ")
          }
        });
      }
    }
  }
}