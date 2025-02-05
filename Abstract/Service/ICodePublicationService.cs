using API.Models;

namespace API.Abstract.Service
{
    public interface ICodePublicationService
    {
        Task<List<CodePublication>> GetAllPublications();
        Task<CodePublication> GetPublication(Guid id);
        Task<Guid> CreatePublication(CodePublication codePublication);
        Task<Guid> UpdatePublication(Guid id, string description, string code, string lang);
        Task<Guid> DeletePublication(Guid id);
    }
}