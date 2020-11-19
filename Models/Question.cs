namespace QuizFlow.Models {
  public class Question {
    public int id { get; set; }
    public string question { get; set; }
    public string answer { get; set; }
    public User user { get; set; }
  }
}