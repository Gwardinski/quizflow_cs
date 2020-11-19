using System.Collections.Generic;
using System.Threading.Tasks;
using QuizFlow.Dto.Question;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionService {
  public interface IQuestionService {
    Task<ServiceResponse<List<QuestionDtoGet>>> getAllQuestions();
    Task<ServiceResponse<QuestionDtoGet>> getQuestionById(int id);
    Task<ServiceResponse<List<QuestionDtoGet>>> getUserQuestions(int id);
    Task<ServiceResponse<QuestionDtoGet>> addQuestion(QuestionDtoAdd question);
    Task<ServiceResponse<QuestionDtoGet>> editQuestion(QuestionDtoEdit question);
    Task<ServiceResponse<string>> deleteQuestion(int id);
  }
}