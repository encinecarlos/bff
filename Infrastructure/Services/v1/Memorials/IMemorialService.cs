using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Memorials
{
    public interface IMemorialService
    {
        Task<Response> GetMemorialDetailConfig();
        Task<Response> FindMemorialComponents(Guid id, FindComponentDto query);
        Task<Response> FindMemorialGroupComponents(Guid id, FindMemorialDto query);
        Task<Response> BatchChangeMemorialComponentCmv(Guid id, BatchChangeMemorialComponentDto command);
        Task<Response> BatchChangeComponentMarkup(Guid id, BatchChangeComponentMarkupDto command);
        Task<Response> UpdateMemorialComponentCmv(Guid id, string erpCode, UpdateMemorialComponentCmvDto command);
        Task<Response> UpdateMemorialComponentMarkup(Guid id, string erpCode, UpdateMemorialComponentMarkupDto command);
        Task<Response> CreateMemorial(CreateMemorialRequest command);
        Task<Response> FindMemorials(FindMemorialsRequest query);
        Task<Response> GetMemorial(Guid id);
        Task<Response> SaveMemorialComponents(Guid id, SaveMemorialRequest command);
        Task<Response> CloneMemorial(Guid id);
        Task<Response> PublishMemorial(Guid id);
        Task<Response> BatchDeleteMemorials(BatchDeleteMemorialsRequest command);
        Task<Response> GetMemorialSession();
        Task<Response> ChangeMemorial(Guid id, ChangeMemorialRequest query);
    }
}