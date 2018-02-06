using System;
using Electro.model.datatakemodel;
using Microsoft.IdentityModel.Tokens;

namespace Electrocore.Options
{
    public interface ITokenProvider
    {
         string CreateToken(Usuario user, DateTime expiry);

    // TokenValidationParameters is from Microsoft.IdentityModel.Tokens
    TokenValidationParameters GetValidationParameters();
    }
}