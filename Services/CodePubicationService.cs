using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract;
using API.Models;

namespace API.Services
{
    public class CodePubicationService : ICodePublicationService
    {
        private readonly ICodePublicationRepository _publicationRepository;

        public CodePubicationService(ICodePublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<List<CodePublication>> GetAllPublications()
        {
            return await _publicationRepository.Get();
        }

        public async Task<Guid> CreatePublication(CodePublication codePublication)
        {
            return await _publicationRepository.Create(codePublication);
        }

        public async Task<Guid> UpdatePublication(Guid id, string description, string code, string lang)
        {
            return await _publicationRepository.Update(id, description, code, lang);
        }

        public async Task<Guid> DeletePublication(Guid id)
        {
            return await _publicationRepository.Delete(id);
        }
    }
}