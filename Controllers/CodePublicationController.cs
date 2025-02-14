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

            var response = codePublications.Select(el => el.ToResponse());

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CodePublicationResponse>> GetPublication(Guid id)
        {
            var publication = await _codePublicationService.GetPublication(id);
            if(publication == null)
                return BadRequest("Publication is not exists");

            var response = new CodePublicationResponse(
                publication.Id,
                publication.Description,
                publication.Code,
                publication.Lang,
                publication.Rating,
                publication.PostedDate,
                new UserResponse(publication.Creator.Username, publication.CreatorId),
                publication.RatedUsers
            );

            return Ok(response);
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

            var codePublication = new CodePublication(
                request.Description,
                request.Code,
                request.Lang,
                0,
                jwtDecode.user.Id,
                jwtDecode.user,
                DateTime.Now.ToUniversalTime()
            );

            var publicationId = await _codePublicationService.CreatePublication(codePublication);

            return Ok(new IdResponse(publicationId));
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePublication(Guid id, [FromBody] CodePublicationRequest request)
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

            if(publication.CreatorId != jwtDecode.user.Id)
            {
                return Forbid();
            }
            else
            {
                var publicationId = await _codePublicationService.UpdatePublication(id, request.Description, request.Code, request.Lang);
                return Ok(new IdResponse(publicationId));
            }
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

            
            if(publication.CreatorId != jwtDecode.user.Id)
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