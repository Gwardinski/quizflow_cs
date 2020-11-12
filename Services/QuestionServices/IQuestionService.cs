using System.Collections.Generic;
using System.Threading.Tasks;
using QuizFlow.Dto.Question;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionServices {
  public interface IQuestionService {
    Task<ServiceResponse<List<QuestionDtoGet>>> getAllQuestions();
    Task<ServiceResponse<QuestionDtoGet>> getQuestionById(int id);
    Task<ServiceResponse<QuestionDtoGet>> addQuestion(QuestionDtoAdd question);
    Task<ServiceResponse<QuestionDtoGet>> editQuestion(QuestionDtoEdit question);
  }
}