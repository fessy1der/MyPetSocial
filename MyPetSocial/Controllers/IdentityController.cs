using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyPetSocial.Models;
using MyPetSocial.Services;

namespace MyPetSocial.Controllers
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IIdentityService _identity;

        public IdentityController(UserManager<User> userManager, IOptions<ApplicationSettings> appSettings, IIdentityService identity)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _identity = identity;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordCorrect)
            {
                return Unauthorized();
            }

            var token = _identity.GenerateJwtToken(
                user.Id,
                user.UserName,
                _appSettings.Secret);

            return new LoginResponseModel
            {
                Token = token
            };

        }

    }
}