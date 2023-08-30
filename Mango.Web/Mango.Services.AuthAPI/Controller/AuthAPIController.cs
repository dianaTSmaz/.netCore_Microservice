using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controller
{
    [Route("api/auth")]
    [ApiController]

    //In this section we are going to consume the Authentication Service
    public class AuthAPIController : ControllerBase
    {
        //In this section we need to inject the Auth Service and the ResponseDto
        //Cycle of MVC : https://www.geeksforgeeks.org/asp-net-mvc-life-cycle/
        private readonly IAuthService _authService;

        //Use protected to use the vavriable in the derived classes
        //https://stackoverflow.com/questions/614818/in-c-what-is-the-difference-between-public-private-protected-and-having-no
        protected ResponseDto _response;
        //private readonly IConfiguration _configuration;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService; 
            _response = new ResponseDto();   
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model) 
        {
            var errorMessage = await _authService.Register(model);
            
            //If this is not null neither empty 
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response); 
            }

           // await _messageBus.PublisjMessage(model.Email);
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDTO model) 
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null) 
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model) 
        {
            //Bool variable to evaluate if the role was assigned or not successfully
            var loginResponseResult = await _authService.RoleAssign(model.Email,model.Role.ToUpper());

            if (!loginResponseResult) 
            {
                _response.IsSuccess = false;
                _response.Message = "Username or/ and password are incorrect";
                return BadRequest(_response);
            }
           // _response.Result = loginResponseResult;
            return Ok(_response);
        }

    }
}
