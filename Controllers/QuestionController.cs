using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.Question;
using QuizFlow.Models;
using QuizFlow.Services.QuestionServices;

namespace QuizFlow.Controllers {

  [Route("questions")]
  [ApiController]
  public class QuestionController : ControllerBase {

    private readonly IQuestionService _service;

    public QuestionController(IQuestionService service) {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> getAllQuestions() {
      var questionItems = await _service.getAllQuestions();
      return Ok(questionItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getQuestionById(int id) {
      var questionItem = await _service.getQuestionById(id);
      return Ok(questionItem);
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
  }
}