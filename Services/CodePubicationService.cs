using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Abstract;
using API.Abstract.Repository;
using API.Abstract.Service;
using API.Contracts;
using API.Extensions;
using API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Services
{
    public class CodePubicationService : ICodePublicationService
    {
        private readonly ICodePublicationRepository _publicationRepository;
        private readonly IDistributedCache _cache;

        public CodePubicationService(ICodePublicationRepository publicationRepository, IDistributedCache cache)
        {
            _publicationRepository = publicationRepository;
            _cache = cache;
        }

        public async Task<List<CodePublicationResponse>> GetAllPublications()
        {
            var data = await _publicationRepository.GetAll();

            return data.Select(el => el.ToResponse()).ToList();
        }

        public async Task<CodePublicationResponse?> GetPublication(Guid id)
        {
            var publication = await _publicationRepository.Get(id);
            return publication.ToResponse();
        }

        public async Task<Guid> CreatePublication(CodePublicationRequest codePublication, User author)
        {
            var publication = new CodePublication(
                codePublication.Description,
                codePublication.Code,
                codePublication.Lang,
                0,
                author.Id,
                author,
                DateTime.Now.ToUniversalTime()
            );

            return await _publicationRepository.Create(publication);
        }

        public async Task<Guid> DeletePublication(Guid id)
        {
            return await _publicationRepository.Delete(id);
        }
    }
}