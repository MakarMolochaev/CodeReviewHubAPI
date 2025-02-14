using API.Abstract.Service;
using API.Infrastructure;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/comment/")]
    public class CommentController : ControllerBase
    {
        private readonly ICodePublicationService _codePublicationService;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly JwtProvider _jwtProvider;
        private readonly UsersService _usersService;
        private readonly JwtService _jwtService;
        private readonly CommentService _commentService;

        public CommentController(
            ICodePublicationService codePublicationService,
            IHttpContextAccessor httpAccessor,
            JwtProvider jwtProvider,
            UsersService usersService,
            JwtService jwtService,
            CommentService commentService
        )
        {
            _codePublicationService = codePublicationService;
            _httpAccessor = httpAccessor;
            _jwtProvider = jwtProvider;
            _usersService = usersService;
            _jwtService = jwtService;
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(Guid id)
        {
            var jwtDecode = await _jwtService.GetUserFromJwt(_httpAccessor, _jwtProvider);

            if(jwtDecode.user == null)
            {
                return BadRequest(jwtDecode.error);
            }

            //_commentService

            return Ok();
        }
    }
}