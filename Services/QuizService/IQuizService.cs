using System.Collections.Generic;
using System.Threading.Tasks;
using QuizFlow.Dto.Quiz;
using QuizFlow.Models;

namespace QuizFlow.Services.QuizService {
  public interface IQuizService {
    Task<ServiceResponse<List<QuizDtoGet>>> getAllQuizzes();
    Task<ServiceResponse<QuizDtoGet>> getQuizById(int id);
    Task<ServiceResponse<List<QuizDtoGet>>> getUserQuizzes();
    Task<ServiceResponse<QuizDtoGet>> addQuiz(QuizDtoAdd quiz);
    Task<ServiceResponse<QuizDtoGet>> editQuiz(QuizDtoEdit quiz);
    Task<ServiceResponse<string>> deleteQuiz(int id);
  }
}