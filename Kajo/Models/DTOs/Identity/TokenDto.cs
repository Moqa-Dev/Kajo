using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos.Identity
{
    public class TokenDto
    {
        [Key] // key added for ODATA edm models
        public string Token { get; set; }

        public TokenDto(String Token)
        {
            this.Token = Token;
        }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
