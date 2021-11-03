using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.DelegatingHandlers
{
    public class HttpLoggingDelegatingHandler : BaseHttpDelegatingHandler
    {
        private readonly ILogger<HttpLoggingDelegatingHandler> _logger;

        public HttpLoggingDelegatingHandler(IHttpContextAccessor httpContextAccessor,
            ILogger<HttpLoggingDelegatingHandler> logger) : base(httpContextAccessor)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var message = string.Format(LogInformationMessage, GetCorrelation(), request.Method,
                request.RequestUri.PathAndQuery, request.RequestUri.Scheme, request.Version);

            _logger.LogInformation(message);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}