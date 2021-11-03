using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace POC.Bff.Web.Swagger.Filters
{
    public sealed class HostDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Host = "";

            swaggerDoc.Schemes = new List<string> { "http" };
        }
    }
}