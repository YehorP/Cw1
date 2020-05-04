using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cw7.DTOs.Requests;
using Cw7.DTOs.Responses;
using Cw7.Models;
using Cw7.Services;
using Cw7.DTOs.Requests;
using Cw7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cw7.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        IStudentsDbService dbService;
        IPasswordService passwordService;
        IConfiguration configuration;

        public EnrollmentsController(IStudentsDbService dbService,IPasswordService passwordService,IConfiguration configuration)
        {
            this.dbService = dbService;
            this.passwordService = passwordService;
            this.configuration = configuration;
        }
       
        [HttpPost("promotions")]
        [Authorize]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            PromoteStudentResponse response = dbService.PromoteStudent(request);
            if (response == null)
                return NotFound("Wrong data was passed ");
            else
                return StatusCode(201,response);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response=dbService.EnrollStudent(request);
            if (response == null)
                return NotFound("Wrong data was passed");
            else
                return StatusCode(201,response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token/{refreshToken}")]
        public IActionResult refreshToken(String refreshToken)
        {
            string login = dbService.GetRefreshTokenOwner(refreshToken);
            if(login==null)
                return BadRequest("Wrong refresh token was passed");

            var claims = new[]
           {
                new Claim(ClaimTypes.Name,login),
                new Claim(ClaimTypes.Role,"Student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "Gakko",
                audience: "Student",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {

            if (!dbService.IsStudentExists(request.Login))
                return BadRequest("Wrong password or login");

            var requestedPasswordsData = dbService.getStudentPasswordData(request.Login);
            if (!passwordService.ValidatePassword(requestedPasswordsData.Password, request.Password, requestedPasswordsData.Salt))
                return BadRequest("Wrong password or login");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Login),
                new Claim(ClaimTypes.Role,"Student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "Gakko",
                audience: "Student",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );
            var TmpRefreshToken = Guid.NewGuid();
            dbService.SetRefreshToken(request.Login, TmpRefreshToken.ToString());
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refershToken = TmpRefreshToken
            });
        }
    }
}