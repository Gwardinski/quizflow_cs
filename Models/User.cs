using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizFlow.Models {
  public class User {
    public int id { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string displayName { get; set; }
    public byte[] passwordHash { get; set; }
    public byte[] passwordSalt { get; set; }

    public List<Question> questions { get; set; }
    public List<Round> rounds { get; set; }
    public List<Quiz> quizzes { get; set; }

    public User(string email, string displayName) {
      this.email = email;
      this.displayName = displayName;
    }
  }
}