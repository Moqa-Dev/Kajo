using Kajo.Models.Entities;
using Kajo.Models.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kajo.Services.Identity
{
    public class IdentityServiceBase
    {
        private readonly UserManager<KajoUser> _userManager;

        protected IdentityServiceBase(UserManager<KajoUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<KajoUser> GetUserById(Guid userId)
        {
            return await Task.Run(() =>
            {
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                if (user == null)
                {
                    throw new ApiException("ID doesn't match any Entry!", ApiException.NOT_FOUND_CODE);
                }
                return user;
            });
        }

        protected async Task<List<String>> GetUserRoles(KajoUser user)
        {
            return (List<string>)await _userManager.GetRolesAsync(user);
        }

    }
}
