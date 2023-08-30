using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service.IService;

//https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options?view=dotnet-plat-ext-7.0
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Service
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        /*Now we are going to inject through dependency injection the 
         options whoch are configured in appettings.json
        -Secret
        -Issuer
        -Audience*/
         
        //Inject the jwtOptions which are in the appSettings.json
        private readonly JwtOptions _jwtOptions;
        public JWTTokenGenerator(IOptions<JwtOptions> jwtOptions) 
        {
            _jwtOptions = jwtOptions.Value;
        }





        public string GenerateToken(AppUser appuser)
        {
            //Now we are going to generate code to generate the token 
            //based on the app User
            // https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytokenhandler?view=msal-web-dotnet-latest
            var tokenHandler = new JwtSecurityTokenHandler();

            //In this variable we can save the secret key configured in the appsettings field
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            //Claims are data contained by the token. They are information about the user which helps to authorize access.
            var claimsList = new List<Claim>
            {
                /*To generate a claim which is not found in the token
                new Claim("Name", appuser.Name)*/
                new Claim(JwtRegisteredClaimNames.Email,appuser.Email),
                new Claim(JwtRegisteredClaimNames.Name, appuser.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, appuser.Id)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                //Subject contains  claims informations
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
             };

            //Generate the token with the information specified
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Write the token 
            return tokenHandler.WriteToken(token);

        }
    }
}
