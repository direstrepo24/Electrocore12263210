using System;
using Microsoft.IdentityModel.Tokens;

namespace Electrocore.Options
{
    public class TokenOptions
    {
         public string Type { get; set; } = "Bearer";
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(1);

         public string Authority { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SigningKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}