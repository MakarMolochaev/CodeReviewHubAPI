using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract;
using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodePublicationController : ControllerBase
    {
        private readonly ICodePublicationService _codePublicationService;

        public CodePublicationController(ICodePublicationService codePublicationService)
        {
            _codePublicationService = codePublicationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CodePublicationResponse>>> GetPublications()
        {
            var codePublications = await _codePublicationService.GetAllPublications();

            var response = codePublications.Select(el => new CodePublicationResponse(el.Id, el.Description, el.Code, el.Lang, el.PostedDate));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePublication([FromBody] CodePublicationRequest request)
        {
            var codePublication = new CodePublication(
                Guid.NewGuid(),
                request.Description,
                request.Code,
                request.Lang,
                DateTime.Now.ToUniversalTime()
            );

            var publicationId = await _codePublicationService.CreatePublication(codePublication);

            return Ok(publicationId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePublication(Guid id, [FromBody] CodePublicationRequest request)
        {
            var publicationId = await _codePublicationService.UpdatePublication(id, request.Description, request.Code, request.Lang);

            return Ok(publicationId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeletePublication(Guid id)
        {
            return Ok(await _codePublicationService.DeletePublication(id));
        }
    }
}