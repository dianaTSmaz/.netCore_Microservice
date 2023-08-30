using Mango.Services.AuthAPI.Models.DTO;

namespace Mango.Services.AuthAPI.Service.IService
{
    //Interface that is used to be able to implement the auth service in the API
    public interface IAuthService
    {
        //The method that is going to be used in the moment
        //It is used to register the user , so it is going to receive the registration request and it is goint to return the userDto
        Task<string> Register(RegistrationRequestDTO registrationRequestDTO);

        //The login methos is going to receive a LoginRequestDto and it is going to return the LoginResponseDto
        Task<LoginResponseDto> Login(LoginRequestDTO loginRequestDTO);

        //Method to assign the role or create the role
        Task<bool> RoleAssign(string email, string roleName);


    }
}
