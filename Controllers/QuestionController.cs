using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.Question;
using QuizFlow.Models;
using QuizFlow.Services.QuestionService;

namespace QuizFlow.Controllers {

  [Authorize]
  [ApiController]
  [Route("questions")]
  public class QuestionController : ControllerBase {

    private readonly IQuestionService _service;

    public QuestionController(IQuestionService service) {
      _service = service;
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllQuestions() {
      var questionItems = await _service.getAllQuestions();
      return Ok(questionItems);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> getQuestionById(int id) {
      var questionItem = await _service.getQuestionById(id);
      return Ok(questionItem);
    }

    [Route("user")]
    [HttpGet]
    public async Task<IActionResult> getUserQuestions() {
      int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
      var questionItems = await _service.getUserQuestions(id);
      return Ok(questionItems);
    }

    [HttpPost]
    public async Task<IActionResult> addQuestion(QuestionDtoAdd question) {
      var newQuestion = await _service.addQuestion(question);
      return Ok(newQuestion);
    }

    [HttpPut]
    public async Task<IActionResult> editQuestion(QuestionDtoEdit question) {
      ServiceResponse<QuestionDtoGet> res = await _service.editQuestion(question);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteQuestion(int id) {
      ServiceResponse<string> res = await _service.deleteQuestion(id);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }
  }
}