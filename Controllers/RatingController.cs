using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class RatingController : ControllerBase
    {
        public readonly RatingService _ratingService;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly JwtProvider _jwtProvider;
        private readonly JwtService _jwtService;
        public RatingController(
            RatingService ratingService,
            IHttpContextAccessor httpAccessor,
            JwtProvider jwtProvider,
            JwtService jwtService
        )
        {
            _ratingService = ratingService;
            _httpAccessor = httpAccessor;
            _jwtProvider = jwtProvider;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpPost("publication/{id:guid}/rate")]
        public async Task<IActionResult> RatePublication(Guid id)
        {
            var jwtDecode = await _jwtService.GetUserFromJwt(_httpAccessor, _jwtProvider);

            if(jwtDecode.user == null)
            {
                return BadRequest(jwtDecode.error);
            }
            await _ratingService.RatePublication(id, jwtDecode.user.Id);

            return Ok();
        }

        [Authorize]
        [HttpPost("publication/{id:guid}/unrate")]
        public async Task<IActionResult> UnratePublication(Guid id)
        {
            var jwtDecode = await _jwtService.GetUserFromJwt(_httpAccessor, _jwtProvider);

            if(jwtDecode.user == null)
            {
                return BadRequest(jwtDecode.error);
            }
            await _ratingService.UnratePublication(id, jwtDecode.user.Id);

            return Ok();
        }
    }
}