namespace Mango.Services.AuthAPI.Models.DTO
{
    /*This is a Dto to express information about the user information
     (remember DTOs are used to show specific information to the user)*/
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
