using API.Contracts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/user/")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly IHttpContextAccessor _httpAccessor;
        public UsersController(UsersService usersService, IHttpContextAccessor httpAccessor)
        {
            _usersService = usersService;
            _httpAccessor = httpAccessor;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUserRequest request)
        {
            var userIfExists = await _usersService.GetByEmail(request.Email);
            if(userIfExists != null)
                return Ok("User with this email is exists already.");
            
            var userId = await _usersService.Register(request.Username, request.Email, request.Password);

            return Ok(new IdResponse(userId));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserRequest request)
        {
            var user = await _usersService.GetByEmail(request.Email);
            if(user == null)
                return BadRequest("User with this email is not exitsts");
            
            var token = await _usersService.Login(request.Email, request.Password);

            //_httpContext.HttpContext.Response.Cookies.Append("КУКИ!! СТЁПА УМЕР", token);
            _httpAccessor.HttpContext?.Response.Cookies.Append("Authentication", token);

            return Ok();
        }
    }
}