using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.Quiz;
using QuizFlow.Models;
using QuizFlow.Services.QuizService;

namespace QuizFlow.Controllers {

  // [Authorize]
  [ApiController]
  [Route("quizzes")]
  public class QuizController : ControllerBase {

    private readonly IQuizService _service;

    public QuizController(IQuizService service) {
      _service = service;
    }

    [Route("all")]
    [HttpGet]
    public async Task<IActionResult> getAllQuizzes() {
      ServiceResponse<List<QuizDtoGet>> res = await _service.getAllQuizzes();
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getQuizById(int id) {
      ServiceResponse<QuizDtoGet> res = await _service.getQuizById(id);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> getUserQuizzes() {
      ServiceResponse<List<QuizDtoGet>> res = await _service.getUserQuizzes();
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> addQuiz(QuizDtoAdd quiz) {
      ServiceResponse<QuizDtoGet> res = await _service.addQuiz(quiz);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> editQuiz(QuizDtoEdit quiz) {
      ServiceResponse<QuizDtoGet> res = await _service.editQuiz(quiz);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteQuiz(int id) {
      ServiceResponse<string> res = await _service.deleteQuiz(id);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }
  }
}