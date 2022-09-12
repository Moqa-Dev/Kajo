using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kajo.Infrastucture.Identity
{
    public static class Permissions
    {
        public static class Profile
        {
            public const string Edit = "Permissions.Profile.Edit";
        }

        public static class RolesService
        {
            public const string UpdateRolePermissions = "Permissions.RolesService.UpdateRolePermissions";
            public const string UpdateRoleUsers = "Permissions.RolesService.UpdateRoleUsers";
            public const string UpdateUserRoles = "Permissions.RolesService.UpdateUserRoles";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }

        public static class UsersService
        {
            public const string EditUserData = "Permissions.UsersService.EditUserData";
            public const string UpdateUserPassword = "Permissions.UsersService.UpdateUserPassword";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class Posts
        {
            public const string View = "Permissions.WorkCenters.View";
            public const string Create = "Permissions.WorkCenters.Create";
            public const string Edit = "Permissions.WorkCenters.Edit";
            public const string Delete = "Permissions.WorkCenters.Delete";
        }

        public static class Topics
        {
            public const string View = "Permissions.Departments.View";
            public const string Create = "Permissions.Departments.Create";
            public const string Edit = "Permissions.Departments.Edit";
            public const string Delete = "Permissions.Departments.Delete";
        }

        public static List<string> GetAllPermissions()
        {
            List<string> allPermissions = new List<string>();
            Type[] nestedTypes = typeof(Permissions).GetNestedTypes();
            foreach (var type in nestedTypes)
            {
                allPermissions.AddRange(GetConstants(type));
            }
            return allPermissions;
        }

        private static List<string> GetConstants(Type type)
        {
            List<string> constants = new List<string>();

            FieldInfo[] fieldInfos = type.GetFields(
                // Gets all public and static fields

                BindingFlags.Public | BindingFlags.Static |
                // This tells it to get the fields from all base types as well

                BindingFlags.FlattenHierarchy);

            // Go through the list and only pick out the constants
            foreach (FieldInfo fi in fieldInfos)
                // IsLiteral determines if its value is written at 
                //   compile time and not changeable
                // IsInitOnly determines if the field can be set 
                //   in the body of the constructor
                // for C# a field which is readonly keyword would have both true 
                //   but a const field would have only IsLiteral equal to true
                if (fi.IsLiteral && !fi.IsInitOnly)
                    constants.Add(fi.GetRawConstantValue().ToString());

            // Return an array of FieldInfos
            return constants;
        }
    }
}
