using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {

        //When we register an user it is saved in the database, we need the datacontext helper
        private readonly DbContextAuthApi _db;

        //Method helpers (dependency injection) from Identity
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //Inject the IJWTTokenGenerator
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        //The constructor to inject through this element the helpers and databaseContext
        public AuthService(DbContextAuthApi db,
                           IJWTTokenGenerator jWTTokenGenerator,
                           UserManager<AppUser> userManager, 
                           RoleManager<IdentityRole> roleManager) 
        {
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jWTTokenGenerator;
            _roleManager = roleManager;
        }
        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO) 
        {
            //In this method we create a new user and we pass the information
            //from the registrationrequestDto to the AppUser object
            AppUser user = new()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber,
            };

            try
            {
                //This method is used to create the specified user in the backing store with given password(ASYNC method)
                //Microsoft info: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1.createasync?view=aspnetcore-7.0

                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded) 
                {
                    var userToReturn = _db.AppUsers.First(user => user.UserName == registrationRequestDTO.Email);

                    //Create the userDTO that is going to be shown to the user
                    UserDTO userDto = new UserDTO()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                    };
                    return "";       
                }
                else
                {
                    //That is basically the identity error, in the description there is all the information about error
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex) 
            {
               //await Console.Out.WriteLineAsync(ex.Message);
            }
            return "Has Happened an error";
        }

        public async Task<LoginResponseDto> Login(LoginRequestDTO loginRequestDto) 
        {   
            //Find the User wich enters the user 
            var user = _db.AppUsers.FirstOrDefault(user => user.UserName.ToLower() == loginRequestDto.User.ToLower());

            //Use the userManager to implement the CheckPasswordAsync and evalaute if the user
            //and password are the same
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false) 
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }


            //Generate the JWT TOKEN
            var token = _jwtTokenGenerator.GenerateToken(user);

            /*Once the user has logged successfully we have to generate the JWT Token
             and return the userDto*/
            UserDTO userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber

            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;

        }

        public async Task<bool> RoleAssign(string email, string roleName)
        {
            //Find the user that user enters
            var user = _db.AppUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                //Add in the database the role (AddToRoleAsync is a helper method)
                await _userManager.AddToRoleAsync(user, roleName);
                return true;

            }
            return false;
         }
    }
}
