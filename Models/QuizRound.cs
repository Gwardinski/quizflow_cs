namespace QuizFlow.Models {
  public class QuizRound {
    public int quizId { get; set; }
    public Quiz quiz { get; set; }
    public int roundId { get; set; }
    public Round round { get; set; }
  }
}