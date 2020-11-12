using System.Collections.Generic;
using System.Linq;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionServices {
  public class QuestionService : IQuestionService {

    private static List<Question> questions = new List<Question>{
            new Question { id = 0, question = "q1", answer = "a1" },
            new Question { id = 1, question = "q2", answer = "a2" },
            new Question { id = 2, question = "q3", answer = "a3" }
        };

    public List<Question> getAllQuestions() {
      return questions;
    }

    public Question getQuestionById(int id) {
      return questions.FirstOrDefault(q => q.id == id);
    }

    public Question addQuestion(Question question) {
      questions.Add(question);
      return question;
    }

  }
}