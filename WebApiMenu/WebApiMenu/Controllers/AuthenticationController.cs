using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiRestaurant.Contracts;
using WebApiRestaurant.Custom;
using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;

namespace WebApiRestaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly Utilities _utilities;

        public AuthenticationController(IAuthService service, Utilities utilities)
        {
            _service = service;
            _utilities = utilities;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var userModel = await _service.Register(user);
            if (userModel.Id != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var foundedUser = await _service.Login(login);
            if (foundedUser == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilities.generateJWT(foundedUser) });
        }
    }
}
