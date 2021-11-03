using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.DelegatingHandlers
{
    public class HttpClientAuthorizationDelegatingHandler : BaseHttpDelegatingHandler
    {
        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor) : base(
            httpContextAccessor)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(SchemaHeaderAuthorization, GetToken());

            request.Headers.Add(CorrelationHeader, GetCorrelation());

            return await base.SendAsync(request, cancellationToken);
        }
    }
}