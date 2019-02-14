using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos;
using WebAPI.Dtos.UsersDto;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private readonly IUserRepository repo;
        private IMapper mapper;
        private readonly AppSettings appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUserRepository repo)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await userService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserDto userDto)
        {
            try
            {
                var user = await userService.Authenticate(userDto.Username, userDto.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("CompanyCode", user.CompanyCode),
                    new Claim("EmpCode", user.EmpCode)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    CompanyCode = user.CompanyCode,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenString
                });
            }
            catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
            
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] InsertUserDto userDto)
        {
            try
            {
                // save 
                var result = await userService.Create(userDto, userDto.User.Password);
                return Ok(result);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = userService.GetById(id);
            var userDto = mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                // save 
                userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
    }
}