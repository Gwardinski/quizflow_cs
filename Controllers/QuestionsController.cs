using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Models;
using QuizFlow.Services.QuestionServices;

namespace QuizFlow.Controllers {

  [Route("questions")]
  [ApiController]
  public class QuestionsController : ControllerBase {

    private readonly IQuestionService _service;

    public QuestionsController(IQuestionService service) {
      _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Question>> getAllQuestions() {
      var questionItems = _service.getAllQuestions();
      return Ok(questionItems);
    }

    [HttpGet("{id}")]
    public ActionResult<Question> getQuestionById(int id) {
      var questionItem = _service.getQuestionById(id);
      return Ok(questionItem);
    }

    [HttpPost]
    public IActionResult addQuestion(Question question) {
      var newQuestion = _service.addQuestion(question);
      return Ok(newQuestion);
    }
  }
}