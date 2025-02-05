using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract.Service;
using API.Contracts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/user/")]
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
            var userId = await _usersService.Register(request.Username, request.Email, request.Password);

            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);

            //_httpContext.HttpContext.Response.Cookies.Append("КУКИ!! СТЁПА УМЕР", token);
            _httpAccessor.HttpContext?.Response.Cookies.Append("ZOV", token);

            return Ok();
        }
    }
}