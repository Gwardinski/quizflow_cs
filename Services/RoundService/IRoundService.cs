using System.Collections.Generic;
using System.Threading.Tasks;
using QuizFlow.Dto.Round;
using QuizFlow.Models;

namespace QuizFlow.Services.RoundService {
  public interface IRoundService {
    Task<ServiceResponse<List<RoundDtoGet>>> getAllRounds();
    Task<ServiceResponse<RoundDtoGet>> getRoundById(int id);
    Task<ServiceResponse<List<RoundDtoGet>>> getUserRounds();
    Task<ServiceResponse<RoundDtoGet>> addRound(RoundDtoAdd round);
    Task<ServiceResponse<RoundDtoGet>> editRound(RoundDtoEdit round);
    Task<ServiceResponse<string>> deleteRound(int id);
  }
}