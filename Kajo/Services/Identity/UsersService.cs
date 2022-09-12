using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using System;
using System.Threading.Tasks;
using Kajo.DataContext;
using Kajo.Models.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Kajo.Services.Identity
{
    public class UsersService : IdentityServiceBase
    {
        private readonly SignInManager<KajoUser> _signInManager;
        private readonly UserManager<KajoUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly KajoContext _context;
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UsersService(
            UserManager<KajoUser> userManager,
            SignInManager<KajoUser> signInManager,
            IConfiguration configuration,
            KajoContext context
            ) : base(userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public IEnumerable<UserDto> Get()
        {
            return _context.KajoUser.ToList()
                    .Select(u => new UserDto(u, GetUserRoles(u).Result));
        }

        public async Task<UserDto> GetById(Guid userId)
        {
            var user = await GetUserById(userId);
            return new UserDto(user, await GetUserRoles(user));
        }

        public async void Delete(Guid userId)
        {
            var user = await GetUserById(userId);
            _userManager.DeleteAsync(user).RunSynchronously();
        }

        public async Task<UserDto> UpdateUserData(Guid userId, UpdateUserDto updateUserDto)
        {
            var user = await GetUserById(userId);
            updateUserDto.Update(user);
            await _context.SaveChangesAsync();
            return new UserDto(user, await GetUserRoles(user));
        }

        public async Task<UserDto> ResetPassword(Guid userId, ResetPasswordDto model)
        {
            var user = await GetUserById(userId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            return new UserDto(user, await GetUserRoles(user));
        }

        public async Task<UserDto> UpdatePassword(Guid userId, UpdatePasswordDto model)
        {
            if (model.OldPassword == model.NewPassword)
                throw new ApiException("Old & New Password are the same", ApiException.INVALID_BODY_CODE);

            var user = await GetUserById(userId);

            var oldPasswordHash = new PasswordHasher<KajoUser>
            (new OptionsWrapper<PasswordHasherOptions>
            (new PasswordHasherOptions()))
            .HashPassword(user, model.OldPassword);

            if (oldPasswordHash == user.PasswordHash)
                throw new ApiException("Incorrect old Password", ApiException.INVALID_BODY_CODE);

            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            return new UserDto(user, await GetUserRoles(user));
        }

        public async Task<UserDto> GetUserData(Guid userId)
        {
            var user = await GetUserById(userId);
            return new UserDto(user, await GetUserRoles(user));
        }

    }
}
