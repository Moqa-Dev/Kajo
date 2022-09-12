using log4net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kajo.DataContext;
using Kajo.Infrastucture.Identity;
using Kajo.Models.Exceptions;
using System.Net;

namespace Kajo.Services.Identity
{
    public class RolesService : IdentityServiceBase
    {
        private readonly UserManager<KajoUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly KajoContext _context;
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RolesService(
            UserManager<KajoUser> userManager,
            RoleManager<IdentityRole> roleManager,
            KajoContext context
            ) : base(userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IEnumerable<RoleDto> Get()
        {
            var dbRoles = _roleManager.Roles.ToList();
            var roles = dbRoles.Select(r => GenerateRoleDto(r).Result);
            return roles;
        }

        public async Task<RoleDto> GetById(Guid id)
        {
            var role = await GetRoleById(id);
            return await GenerateRoleDto(role);
        }

        public async Task<RoleDto> Post(UpdateRoleDto roleDto)
        {

            IdentityRole identityRole = new IdentityRole { Name = roleDto.Role };

            IdentityResult result = await _roleManager.CreateAsync(identityRole).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return await GenerateRoleDto(identityRole);
            }
            String errorMessage = string.Join(System.Environment.NewLine, result.Errors.Select(x => x.Description));
            throw new ApiException(errorMessage, ApiException.UPDATE_ROLE_FAILED_ERROR_CODE);
        }

        public async Task<RoleDto> Put(Guid id, UpdateRoleDto update)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);

            role.Name = update.Role;

            IdentityResult result = await _roleManager.UpdateAsync(role).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return await GenerateRoleDto(role);
            }
            String errorMessage = string.Join(System.Environment.NewLine, result.Errors.Select(x => x.Description));
            throw new ApiException(errorMessage, ApiException.UPDATE_ROLE_FAILED_ERROR_CODE);
        }

        public async void Delete(Guid roleId)
        {
            var role = await GetRoleById(roleId);
            await _roleManager.DeleteAsync(role);
        }

        public async Task<RoleDto> UpdateRolePermissions(Guid roleId, JArray permissions)
        {
            var role = await GetRoleById(roleId);
            var availablePermissions = Permissions.GetAllPermissions();
            foreach (var newPermission in permissions)
            {
                if (!availablePermissions.Any(s => s.Equals(newPermission.ToString(), StringComparison.OrdinalIgnoreCase)))
                    throw new ApiException($"Permission {newPermission.ToString()} doesn't Exist", ApiException.NOT_FOUND_CODE);
            }

            var oldRoleClaims = await _roleManager.GetClaimsAsync(role);
            oldRoleClaims = oldRoleClaims.Where(c => c.Type == CustomClaimTypes.Permission).ToList();
            foreach (var oldClaim in oldRoleClaims)
            {
                await _roleManager.RemoveClaimAsync(role, oldClaim);
            }
            foreach (var newPermission in permissions)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, newPermission.ToString()));
            }
            return await GenerateRoleDto(role);
        }

        public async Task<RoleDto> UpdateRoleUsers(Guid roleId, JArray usersIds)
        {
            var role = await GetRoleById(roleId);
            var newUsers = new List<KajoUser>();
            foreach (var userId in usersIds)
            {
                var user = await GetUserById(Guid.Parse(userId.ToString()));
                newUsers.Add(user);
            }
            var oldRoleUsers = await _userManager.GetUsersInRoleAsync(role.Name);
            foreach (var oldUser in oldRoleUsers)
            {
                await _userManager.RemoveFromRoleAsync(oldUser, role.Name);
            }
            foreach (var newUser in newUsers)
            {
                await _userManager.AddToRoleAsync(newUser, role.Name);
            }
            return await GenerateRoleDto(role);
        }

        public async Task<UserDto> UpdateUserRoles(Guid userId, JArray rolesIds)
        {
            var user = await GetUserById(userId);
            var newRoles = new List<IdentityRole>();
            foreach (var roleId in rolesIds)
            {
                var role = await GetRoleById(Guid.Parse(roleId.ToString()));
                newRoles.Add(role);
            }
            var oldUserRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldUserRoles);

            await _userManager.AddToRolesAsync(user, newRoles.Select(r => r.Name));

            return new UserDto(user, await GetUserRoles(user));
        }

        public Task<List<String>> GetAllPermissions()
        {
            return Task.Run(() => Permissions.GetAllPermissions());
        }

        public async Task<List<String>> GetUserPermissions(Guid userId)
        {
            var user = await GetUserById(userId);
            var permissions = new List<string>();

            var roles = await GetUserRoles(user);
            foreach (var roleName in roles)
            {
                var role = _roleManager.FindByNameAsync(roleName).Result;
                var claims = _roleManager.GetClaimsAsync(role).Result;
                permissions.AddRange(claims.Where(x => x.Type == CustomClaimTypes.Permission)
                .Select(x => x.Value).ToList());
            }
            return permissions;
        }

        private async Task<RoleDto> GenerateRoleDto(IdentityRole identityRole)
        {
            return new RoleDto
            {
                RoleID = identityRole.Id,
                Role = identityRole.Name,
                Permissions = await GetRolePermissions(identityRole),
                Users = await GetRoleUsers(identityRole),
            };
        }

        private async Task<IdentityRole> GetRoleById(Guid roleId)
        {
            return await Task.Run(() =>
            {
                var role = _roleManager.FindByIdAsync(roleId.ToString()).Result;
                if (role == null)
                {
                    throw new ApiException("ID doesn't match any Entry!", ApiException.NOT_FOUND_CODE);
                }
                return role;
            });
        }

        private async Task<List<UserDto>> GetRoleUsers(IdentityRole role)
        {
            return _userManager.GetUsersInRoleAsync(role.Name).Result
                .Select(u => new UserDto(u, GetUserRoles(u).Result))
                .ToList();
        }

        private async Task<List<String>> GetRolePermissions(IdentityRole role)
        {
            return _roleManager.GetClaimsAsync(role).Result
                .Where(x => x.Type == CustomClaimTypes.Permission)
                .Select(x => x.Value)
                .ToList();
        }
    }
}
