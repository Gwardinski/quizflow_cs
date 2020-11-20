using System.Collections.Generic;
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
      ServiceResponse<List<QuestionDtoGet>> res = await _service.getAllQuestions();
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> getQuestionById(int id) {
      ServiceResponse<QuestionDtoGet> res = await _service.getQuestionById(id);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [Route("user")]
    [HttpGet]
    public async Task<IActionResult> getUserQuestions() {
      ServiceResponse<List<QuestionDtoGet>> res = await _service.getUserQuestions();
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> addQuestion(QuestionDtoAdd question) {
      ServiceResponse<QuestionDtoGet> res = await _service.addQuestion(question);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
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