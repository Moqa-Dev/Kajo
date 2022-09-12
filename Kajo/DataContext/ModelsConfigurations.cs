using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
//using System;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using System;

namespace Kajo.DataContext
{
    public class ModelsConfigurations : IModelConfiguration
    {
        /// <summary>
        /// Applies model configurations using the provided builder for the specified API version.
        /// </summary>
        /// <param name="builder">The <see cref="ODataModelBuilder">builder</see> used to apply configurations.</param>
        /// <param name="apiVersion">The <see cref="ApiVersion">API version</see> associated with the <paramref name="builder"/>.</param>
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            //HOWTO: Add Models, Controllers here
            //To be registered with OData EDM
            //builder.EntitySet<Model>("Controller");
            //builder.Namespace = "Namespace";
            //builder.Function("FunctionName").Returns<ReturnModer>();

            builder.EntitySet<KajoUser>("Users");
            builder.EntitySet<TokenDto>("Token");

            builder.EntitySet<UserDto>("User");
            builder.EntitySet<RoleDto>("Roles");

            builder.Function("Login").ReturnsFromEntitySet<TokenDto>("Token");
            builder.Function("Register").ReturnsFromEntitySet<UserDto>("User");


            builder.Function("UpdatePassword").ReturnsFromEntitySet<UserDto>("User");
            builder.Function("GetUserData").ReturnsFromEntitySet<UserDto>("User");
            builder.Function("UpdateUserProfile").ReturnsFromEntitySet<UserDto>("User");



            builder
                .Function("UpdateRolePermissions")
                .ReturnsFromEntitySet<RoleDto>("Role")
                .Parameter<Guid>("roleId");
            builder
                .Function("UpdateRoleUsers")
                .ReturnsFromEntitySet<RoleDto>("Role")
                .Parameter<Guid>("roleId");
            builder
                .Function("UpdateUserRoles")
                .ReturnsFromEntitySet<UserDto>("User")
                .Parameter<Guid>("userId");
            builder
                .Function("GetAllPermissions")
                .Returns(typeof(string));
            builder
                .Function("GetUserPermissions")
                .Returns(typeof(string));

            builder
                .Function("GetDashboardData")
                .Returns(typeof(string));

            builder
                .Function("EditUserData")
                .ReturnsFromEntitySet<UserDto>("User")
                .Parameter<Guid>("userId");
            builder
                .Function("UpdateUserPassword")
                .ReturnsFromEntitySet<UserDto>("User")
                .Parameter<Guid>("userId");

            builder.EntitySet<Topic>("Topics");
            builder.EntitySet<Post>("Posts");

        }
    }
}