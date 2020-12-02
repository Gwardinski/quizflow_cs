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
    public async Task<IActionResult> register(UserDtoRegister userDto) {
      ServiceResponse<int> res = await _authRepo.register(userDto);
      if (res.code == 400) {
        return BadRequest(res);
      }
      if (res.code == 409) {
        return Conflict(res);
      }
      return Ok(res);
    }

    [HttpPost("login")]
    public async Task<IActionResult> login(UserDtoLogin userDto) {
      ServiceResponse<UserDtoGet> res = await _authRepo.login(userDto);
      if (res.code == 400) {
        return BadRequest(res);
      }
      if (res.code == 404) {
        return NotFound(res);
      }
      return Ok(res);
    }
  }
}