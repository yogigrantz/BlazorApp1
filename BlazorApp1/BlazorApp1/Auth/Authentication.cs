using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorApp1.Auth
{
    public class Authentication
    {
        private async Task<ClaimsIdentity> CreateIdentityAsync(string username, string password, string role)
        {
            Claim[] claims = {new Claim("Name", username), new Claim("username", username), new Claim("role", role) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);    
            return claimsIdentity;
        }

        public async Task<bool> AuthenticateAsync(string username, string password, IHttpContextAccessor httpContextAccessor)
        {
            if (username == "demo" && password == "demo")
            {
                ClaimsIdentity c= await CreateIdentityAsync(username, password, "Admin");
                httpContextAccessor.HttpContext.User = new ClaimsPrincipal(c);
                return true;
            }
            else
                return false;
        }

        public async Task<string> IsAuthorized(IHttpContextAccessor httpContextAccessor, string role)
        {
            if (httpContextAccessor.HttpContext.User == null)
                return null;
            if (httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "role" && x.Value == role))
            {
                Claim claim = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "Name");
                
                return claim?.Value;
            }
            else
                return null;
        }


    }
}
