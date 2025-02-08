using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure;
using API.Models;

namespace API.Services
{
    public class JwtService
    {
        private readonly UsersService _usersService;
        public JwtService(
            UsersService usersService
            )
        {
            _usersService = usersService;
        }

        public async Task<(User? user, string error)> GetUserFromJwt(IHttpContextAccessor _httpAccessor, JwtProvider _jwtProvider)
        {
            string _error = "";

            var jwtToken = _httpAccessor?.HttpContext?.Request.Cookies["Authentication"];
            if (jwtToken == null)
                _error = "JWT токен не найден в cookie";

            var creatorId = _jwtProvider.ExtractUserIdFromToken(jwtToken);
            if(!creatorId.HasValue)
                _error = "JWT токен некорректен";

            var _user = await _usersService.Get(creatorId.Value);

            if (_user == null)
                _error = "Пользователь с указаным Id не найден";


            return (_user, _error);
        }
    }
}