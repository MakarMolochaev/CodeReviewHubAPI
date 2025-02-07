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
        public CodePublicationController(
            ICodePublicationService codePublicationService,
            IHttpContextAccessor httpAccessor,
            JwtProvider jwtProvider,
            UsersService usersService
        )
        {
            _codePublicationService = codePublicationService;
            _httpAccessor = httpAccessor;
            _jwtProvider = jwtProvider;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CodePublicationResponse>>> GetPublications()
        {
            var codePublications = await _codePublicationService.GetAllPublications();

            var response = codePublications.Select(el => new CodePublicationResponse(el.Id, el.Description, el.Code, el.Lang, el. Rating, el.PostedDate));

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
                publication.PostedDate
            );

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePublication([FromBody] CodePublicationRequest request)
        {
            var jwtToken = _httpAccessor?.HttpContext?.Request.Cookies["Authentication"];
            if (jwtToken == null)
                return BadRequest("JWT токен не найден в cookie");

            var creatorId = _jwtProvider.ExtractUserIdFromToken(jwtToken);
            if(!creatorId.HasValue)
                return BadRequest("JWT токен некорректен");

            var user = await _usersService.Get(creatorId.Value);
            if (user == null)
                return NotFound("Пользователь с указаным Id не найден");

            var codePublication = new CodePublication(
                Guid.NewGuid(),
                request.Description,
                request.Code,
                request.Lang,
                0,
                creatorId.Value,
                user,
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