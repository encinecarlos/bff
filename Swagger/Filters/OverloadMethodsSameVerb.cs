using POC.Shared.Configuration.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Text;

namespace POC.Bff.Web.Swagger.Filters
{
    /// <summary>
    ///     Adds the parameters to the method to avoid same method name.
    /// </summary>
    /// <seealso>
    ///     <cref>Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter</cref>
    /// </seealso>
    public sealed class OverloadMethodsSameVerb : IOperationFilter
    {
        /// <summary>
        ///     Changes the verbs by concatenating the parameters and verbs.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) return;

            var builder = new StringBuilder($"{operation.OperationId}By");

            operation.Parameters.Each(parameter =>
                builder.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parameter.Name)));
            operation.OperationId = builder.ToString();
        }
    }
}