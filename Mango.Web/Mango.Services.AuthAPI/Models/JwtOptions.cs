namespace Mango.Services.AuthAPI.Models
{
    //This class is used to access to the Token conigurations in the API
    public class JwtOptions
    {
        //The person or client who implements the token
        public string Issuer { get; set; } = string.Empty;

        //The people who can use the Jwt token
        public string Audience { get; set; } = string.Empty;
        
        //A secret key use is configured in appsettings.json to be able to access to the token
        public string Secret { get; set; } = string.Empty;
     }
}
