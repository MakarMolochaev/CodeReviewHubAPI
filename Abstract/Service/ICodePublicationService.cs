using API.Contracts;
using API.Models;

namespace API.Abstract.Service
{
    public interface ICodePublicationService
    {
        Task<List<CodePublicationResponse>> GetAllPublications();
        Task<CodePublicationResponse?> GetPublication(Guid id);
        Task<Guid> CreatePublication(CodePublicationRequest codePublication, User author);
        Task<Guid> DeletePublication(Guid id);
    }
}