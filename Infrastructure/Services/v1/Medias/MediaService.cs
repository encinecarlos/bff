using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Medias
{
    public class MediaServiceHandler : IMediaService
    {
        private readonly IConfigurationClient _configurationClient;

        public MediaServiceHandler(IConfigurationClient configurationClient)
        {
            _configurationClient = configurationClient;
        }


        public async Task<Response> UploadMedia(object command)
        {
            return await _configurationClient.UploadMedia(command);
        }

        public async Task<Response> CheckMedia(string keyName)
        {
            return await _configurationClient.CheckMedia(keyName);
        }

        public async Task<Response> FindMedias()
        {
            return await _configurationClient.FindMedias();
        }
    }
}