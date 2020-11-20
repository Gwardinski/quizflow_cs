using System.Collections.Generic;

namespace QuizFlow.Models {
  public class User {
    public int id { get; set; }
    public string username { get; set; }
    public byte[] passwordHash { get; set; }
    public byte[] passwordSalt { get; set; }

    public List<Question> questions { get; set; }
    public List<Round> rounds { get; set; }
    public List<Quiz> quizzes { get; set; }

    public User(string username) {
      this.username = username;
    }
  }
}