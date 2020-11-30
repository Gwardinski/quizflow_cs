using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Data;
using QuizFlow.Dto.User;
using QuizFlow.Models;

namespace QuizFlow.Controllers {

  [ApiController]
  [Route("auth")]
  public class AuthController : ControllerBase {

    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo) {
      _authRepo = authRepo;
    }

    [HttpPost("register")]
    public async Task<IActionResult> register(UserDtoRegister request) {
      ServiceResponse<int> res = await _authRepo.register(username: request.username, password: request.password);
      if (!res.success) {
        return BadRequest(res);
      }
      return Ok(res);
    }

    [HttpPost("login")]
    public async Task<IActionResult> login(UserDtoLogin request) {
      ServiceResponse<string> res = await _authRepo.login(username: request.username, password: request.password);
      if (!res.success) {
        return BadRequest(res);
      }
      return Ok(res);
    }

    [HttpPost("sanity")]
    public async Task<IActionResult> sanity(UserDtoLogin request) {
      ServiceResponse<string> res = new ServiceResponse<string>();
      res.success = true;
      res.data = "hello";
      res.message = "hello there";
      return Ok(res);
    }
  }
}