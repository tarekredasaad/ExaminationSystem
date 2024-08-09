using Domain.DTO;
using Domain.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IAuthService authService;
        public UserController( IAuthService authService) 
        {
            this.authService = authService;
        }

        [HttpPost("RegisterUser")]
        public ActionResult<ResultDTO> Register(UserDTO InsDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState }); };

            return Ok(authService.RegisterAsyncUser(InsDTO));
        }
        [HttpPost("LoginUser")]

        public ActionResult<ResultDTO> Login(UserLoginDTO InsDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState }); };

            return Ok(authService.LoginAsyncUser(InsDTO));
        }
    }
}
