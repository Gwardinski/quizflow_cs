using System.Threading.Tasks;
using QuizFlow.Dto.User;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public interface IAuthRepository {
    Task<ServiceResponse<int>> register(UserDtoRegister userDto);
    Task<ServiceResponse<UserDtoGet>> login(UserDtoLogin userDto);
    Task<bool> userExists(string email);
  }
}