using API.Models;

namespace API.Abstract.Repository
{
    public interface ICodePublicationRepository
    {
        Task<List<CodePublication>> GetAll();
        Task<CodePublication?> Get(Guid id);
        Task<Guid> Create(CodePublication codePublication);
        Task<Guid> Update(Guid id, string description, string code, string lang);
        Task<Guid> Delete(Guid id);
    }
}