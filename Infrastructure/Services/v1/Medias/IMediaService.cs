using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Medias
{
    public interface IMediaService
    {
        Task<Response> UploadMedia(object command);
        Task<Response> CheckMedia(string keyName);
        Task<Response> FindMedias();
    }
}