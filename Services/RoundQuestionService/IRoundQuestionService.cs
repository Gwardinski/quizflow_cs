using System.Threading.Tasks;
using QuizFlow.Dto.Round;
using QuizFlow.Dto.RoundQuestion;
using QuizFlow.Models;

namespace QuizFlow.Services.RoundQuestionService {
  public interface IRoundQuestionService {
    Task<ServiceResponse<RoundDtoGet>> addRoundQuestion(RoundQuestionDtoAdd newRoundQuestion);
    Task<ServiceResponse<RoundDtoGet>> deleteRoundQuestion(RoundQuestionDtoAdd deleteRoundQuestion);
  }
}