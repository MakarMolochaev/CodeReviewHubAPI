using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using API.Abstract;
using API.Abstract.Repository;
using API.Abstract.Service;
using API.Contracts.Caching;
using API.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Services
{
    public class CodePubicationService : ICodePublicationService
    {
        private readonly ICodePublicationRepository _publicationRepository;
        private readonly IDistributedCache _cache;
        public CodePubicationService(ICodePublicationRepository publicationRepository, IDistributedCache cache)
        {
            _cache = cache;
            _publicationRepository = publicationRepository;
        }

        public async Task<List<CodePublication>> GetAllPublications()
        {
            var cacheJson = _cache.GetString("publications");
            if(cacheJson != null)
            {
                var publicationsFromCache = JsonSerializer.Deserialize<List<CachedCodePublication>>(cacheJson)
                    ?? throw new Exception("Error in Deserializing");

                return publicationsFromCache.Select(el =>
                    new CodePublication(
                        el.Description,
                        el.Code,
                        el.Lang,
                        el.rating,
                        el.Creator.id,
                        new User(el.Creator.Username, "", ""),
                        el.PostedTime
                    )).ToList();
            }

            var publications = await _publicationRepository.GetAll();
            return publications;
        }

        public async Task<CodePublication?> GetPublication(Guid id)
        {
            return await _publicationRepository.Get(id);
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