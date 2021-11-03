using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace POC.Bff.Web.Swagger.Filters
{
    /// <summary>
    ///     The Swagger document custom sort filter class.
    /// </summary>
    /// <seealso cref="IDocumentFilter" />
    public sealed class DocumentSortFilter : IDocumentFilter
    {
        /// <summary>
        ///     Sorts the operations list alphabetically.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The context.</param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value);
        }
    }
}