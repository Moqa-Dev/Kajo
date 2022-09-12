using System;
using Microsoft.AspNetCore.Identity;
using Kajo.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Kajo.Models.Dtos.Identity
{
    public class UserDto
    {
        public UserDto(KajoUser user, List<String> roles = null)
        {
            UserID = user.Id;
            Username = user.UserName;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Team = user.Team;
            About = user.About;
            PhoneNumber = user.PhoneNumber;
            Roles = roles;
        }

        [Key]
        public string UserID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public string About { get; set; }
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
