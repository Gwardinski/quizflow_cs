using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.RoundQuestion;
using QuizFlow.Services.RoundQuestionService;

namespace QuizFlow.Controllers {
  [ApiController]
  [Route("roundquestion")]
  public class RoundQuestionController : ControllerBase {

    private readonly IRoundQuestionService _service;

    public RoundQuestionController(IRoundQuestionService service) {
      _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> addRoundQuestion(RoundQuestionDtoAdd newRoundQuestion) {
      return Ok(await _service.addRoundQuestion(newRoundQuestion));
    }
  }
}