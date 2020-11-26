using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.QuizRound;
using QuizFlow.Services.QuizRoundService;

namespace QuizFlow.Controllers {

  //   [Authorize]
  [ApiController]
  [Route("quizround")]
  public class QuizRoundController : ControllerBase {

    private readonly IQuizRoundService _quizRoundService;

    public QuizRoundController(IQuizRoundService service) {
      _quizRoundService = service;
    }

    [HttpPost]
    public async Task<IActionResult> addQuizRound(QuizRoundDtoAdd newQuizRound) {
      return Ok(await _quizRoundService.addQuizRound(newQuizRound));
    }

    [HttpDelete]
    public async Task<IActionResult> deleteQuizRound(QuizRoundDtoAdd deleteQuizRound) {
      return Ok(await _quizRoundService.deleteQuizRound(deleteQuizRound));
    }
  }
}