using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Data;
using QuizFlow.Models;

namespace QuizFlow.Controllers {

  [Route("api/questions")]
  [ApiController]
  public class QuestionsController : ControllerBase {

    private readonly IQuizFlowRepo _repository;

    public QuestionsController(IQuizFlowRepo repository) {
      _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Question>> getAllQuestions() {
      var questionItems = _repository.getAllQuestions();
      return Ok(questionItems);
    }

    [HttpGet("{id}")]
    public ActionResult<Question> getQuestionById(int id) {
      var questionItem = _repository.getQuestionById(id);
      return Ok(questionItem);
    }


  }
}