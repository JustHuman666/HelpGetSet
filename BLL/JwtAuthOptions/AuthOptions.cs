using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BLL.JwtAuthOptions
{
    public static class AuthOptions
    {
        public static string? Issuer { get; set; }

        public static string? Audience { get; set; }

        public static string? Key { get; set; }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
