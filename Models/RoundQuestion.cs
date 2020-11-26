namespace QuizFlow.Models {
  public class RoundQuestion {
    public int roundId { get; set; }
    public Round round { get; set; }
    public int questionId { get; set; }
    public Question question { get; set; }
  }
}