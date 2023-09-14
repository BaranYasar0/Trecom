using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Trecom.Api.Identity.Application.Features.Commands;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Models.Dtos;
using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };

            RegisterResponseDto result = await _mediator.Send(registerCommand);
            return Created("", result.AccessToken);


        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto model)
        {
            LoginCommand command = new();
            command.UserForLoginDto = model;
            var result= await _mediator.Send(command);
            //SetAccessTokenToCookie(result.AccessToken);
            CookieOptions cookieOptions = new()
            {
                Expires = DateTime.Now
                    .AddDays(7),
                Secure = true,
            };
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("TechBuddySecretKeyShouldBeLonggggggggg"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);
            var token = new JwtSecurityToken(claims: new Claim[] { new Claim(ClaimTypes.NameIdentifier, "denemeJwt") },
                expires: expiry,
                signingCredentials: creds, notBefore: DateTime.Now);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return Ok(await Task.FromResult(encodedJwt));
        }




        private void SetAccessTokenToCookie(AccessToken accessToken)
        {
            CookieOptions cookieOptions = new()
            {
                Expires =DateTime.Now
                    .AddDays(7),
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("accessToken", accessToken.Token, cookieOptions);
        }

        protected string? GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}
