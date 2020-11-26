using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.RoundQuestion;
using QuizFlow.Services.RoundQuestionService;

namespace QuizFlow.Controllers {

  //   [Authorize]
  [ApiController]
  [Route("roundquestion")]
  public class RoundQuestionController : ControllerBase {

    private readonly IRoundQuestionService _roundQuestionService;

    public RoundQuestionController(IRoundQuestionService service) {
      _roundQuestionService = service;
    }

    [HttpPost]
    public async Task<IActionResult> addQuizRound(RoundQuestionDtoAdd newRoundQuestion) {
      return Ok(await _roundQuestionService.addRoundQuestion(newRoundQuestion));
    }

    [HttpDelete]
    public async Task<IActionResult> deleteQuizRound(RoundQuestionDtoAdd deleteRoundQuestion) {
      return Ok(await _roundQuestionService.deleteRoundQuestion(deleteRoundQuestion));
    }
  }
}