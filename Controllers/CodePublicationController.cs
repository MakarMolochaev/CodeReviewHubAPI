using API.Abstract.Service;
using API.Contracts;
using API.Infrastructure;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/publication/")]
    public class CodePublicationController : ControllerBase
    {
        private readonly ICodePublicationService _codePublicationService;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly JwtProvider _jwtProvider;
        private readonly UsersService _usersService;
        private readonly JwtService _jwtService;
        public CodePublicationController(
            ICodePublicationService codePublicationService,
            IHttpContextAccessor httpAccessor,
            JwtProvider jwtProvider,
            UsersService usersService,
            JwtService jwtService
        )
        {
            _codePublicationService = codePublicationService;
            _httpAccessor = httpAccessor;
            _jwtProvider = jwtProvider;
            _usersService = usersService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CodePublicationResponse>>> GetPublications()
        {
            var codePublications = await _codePublicationService.GetAllPublications();

            return Ok(codePublications);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CodePublicationResponse>> GetPublication(Guid id)
        {
            var publication = await _codePublicationService.GetPublication(id);
            if(publication == null)
                return BadRequest("Publication is not exists");

            return Ok(publication);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePublication([FromBody] CodePublicationRequest request)
        {
            var jwtDecode = await _jwtService.GetUserFromJwt(_httpAccessor, _jwtProvider);

            if(jwtDecode.user == null)
            {
                return BadRequest(jwtDecode.error);
            }

            var publicationId = await _codePublicationService.CreatePublication(request, jwtDecode.user);

            return Ok(new IdResponse(publicationId));
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeletePublication(Guid id)
        {
            var jwtDecode = await _jwtService.GetUserFromJwt(_httpAccessor, _jwtProvider);

            if(jwtDecode.user == null)
            {
                return BadRequest(jwtDecode.error);
            }

            var publication = await _codePublicationService.GetPublication(id);
            if(publication == null)
            {
                return NotFound();
            }

            
            if(publication.Creator.Id != jwtDecode.user.Id)
            {
                return Forbid();
            }
            else
            {
                await _codePublicationService.DeletePublication(id);
                return Ok();
            }
        }
    }
}