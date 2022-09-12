using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kajo.DataContext;
using Kajo.Models.Exceptions;

namespace Kajo.Services.Identity
{
    public class AuthenticateService
    {
        private readonly SignInManager<KajoUser> _signInManager;
        private readonly UserManager<KajoUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly KajoContext _context;
        //private readonly IHubContext<ChatHub> _hubContext;
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuthenticateService(
            UserManager<KajoUser> userManager,
            SignInManager<KajoUser> signInManager,
            IConfiguration configuration,
            KajoContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<UserDto> Register([FromBody] RegisterUserDto model)
        {
            var user = model.ToEntity(null);
            var result = await _userManager.CreateAsync(user, model.Password);

            var createdUser = new UserDto(user);

            if (result.Succeeded)
            {
                return createdUser;
            }
            var errorMessage = "Registration Failed: " + string.Join(", ", result.Errors);
            throw new ApiException(errorMessage, ApiException.REGISTERATION_FAILED_ERROR_CODE);
        }

        public async Task<TokenDto> Login([FromBody] LoginUserDto model)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
            if (appUser != null)
            {
                var result = //await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                    await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(model.Username, appUser);
                    return token;
                }
            }
            throw new ApiException("Login Failed", ApiException.REGISTERATION_FAILED_ERROR_CODE);
        }

        /// <summary>
        /// Login using Email, Password.
        /// </summary>
        /// <param Email="String">User Email</param>
        /// <param user="KajoUser">The Identity user to generate token for</param>
        /// <returns>Generated token</returns>
        private TokenDto GenerateJwtToken(string email, KajoUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["JwtExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token));
        }



    }
}
