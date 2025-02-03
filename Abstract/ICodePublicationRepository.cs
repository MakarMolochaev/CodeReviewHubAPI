using API.Models;

namespace API.Abstract
{
    public interface ICodePublicationRepository
    {
        Task<List<CodePublication>> Get();
        Task<Guid> Create(CodePublication codePublication);
        Task<Guid> Update(Guid id, string description, string code, string lang);
        Task<Guid> Delete(Guid id);
    }
}