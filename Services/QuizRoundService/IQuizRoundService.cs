using System.Threading.Tasks;
using QuizFlow.Dto.Quiz;
using QuizFlow.Dto.QuizRound;
using QuizFlow.Models;

namespace QuizFlow.Services.QuizRoundService {
  public interface IQuizRoundService {
    Task<ServiceResponse<QuizDtoGet>> addQuizRound(QuizRoundDtoAdd newQuizRound);
    Task<ServiceResponse<QuizDtoGet>> deleteQuizRound(QuizRoundDtoAdd deleteQuizRound);
  }
}