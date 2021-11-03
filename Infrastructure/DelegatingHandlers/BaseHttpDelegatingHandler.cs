using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace POC.Bff.Web.Infrastructure.DelegatingHandlers
{
    public abstract class BaseHttpDelegatingHandler : DelegatingHandler
    {
        protected const string CorrelationHeader = "X-Correlation-ID";
        protected const string LogInformationMessage = "Correlation: {0}  Execute Request: {1} {2} {3}/{4}";
        protected const string SchemaHeaderAuthorization = "bearer";
        protected readonly IHttpContextAccessor HttpContextAccessor;

        protected BaseHttpDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        protected string GetCorrelation()
        {
            if (HttpContextAccessor.HttpContext.Request.Headers.TryGetValue(CorrelationHeader, out var correlation))
                HttpContextAccessor.HttpContext.Response.Headers.TryAdd(CorrelationHeader, correlation);

            return correlation;
        }

        protected string GetToken()
        {
            var ownToken = HttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);

            return ownToken ? Regex.Replace(token, "bearer", "", RegexOptions.IgnoreCase).Trim() : string.Empty;
        }
    }
}