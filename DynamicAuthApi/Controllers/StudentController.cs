using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IAuthService authService;
        public StudentController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("RegisterStudent")]
        public ActionResult<ResultDTO> Register(UserDTO InsDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState }); };

            return Ok(authService.RegisterAsyncInstructor(InsDTO));
        }
        [HttpPost("LoginStudent")]

        public ActionResult<ResultDTO> Login(UserLoginDTO InsDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState }); };

            return Ok(authService.LoginAsyncInstructor(InsDTO));
        }
    }
}
