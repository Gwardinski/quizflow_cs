using System.Collections.Generic;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionServices {
  public interface IQuestionService {
    List<Question> getAllQuestions();
    Question getQuestionById(int id);
    Question addQuestion(Question question);
  }
}