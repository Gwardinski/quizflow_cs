using System.Collections.Generic;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public class MockQuizFlowRepo : IQuizFlowRepo {
    public IEnumerable<Question> getAllQuestions() {
      var questions = new List<Question> {
            new Question { id = 0, question = "q1", answer = "a1" },
            new Question { id = 1, question = "q2", answer = "a2" },
            new Question { id = 2, question = "q3", answer = "a3" }
        };
      return questions;
    }

    public Question getQuestionById(int id) {
      return new Question { id = 0, question = "q", answer = "a" };
    }
  }
}