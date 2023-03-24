using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Models.Request;
using SalesSystem.Models.Response;
using SalesSystem.Services;

namespace SalesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthReq credentials)
        {
            Response oResponse = new();

            var userResponse = _userService.Auth(credentials);

            if (userResponse == null)
            {
                oResponse.Message = "Wrong credentials";
             

                return BadRequest(oResponse);                
            }

            oResponse.Sucess = true;
            oResponse.Data = userResponse;
            return Ok(oResponse);
        }
    }
}
