using System.Threading.Tasks;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public interface IAuthRepository {
    Task<ServiceResponse<int>> register(string username, string password);
    Task<ServiceResponse<string>> login(string username, string password);
    Task<bool> userExists(string username);
  }
}