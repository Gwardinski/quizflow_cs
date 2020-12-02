namespace QuizFlow.Dto.User {
  public class UserDtoGet {
    public int id { get; set; }
    public string email { get; set; }
    public string displayName { get; set; }
    public string authToken { get; set; }
    public int noOfQuestions { get; set; }
    public int noOfRounds { get; set; }
    public int noOfQuizzes { get; set; }
    public string token { get; set; }
  }
}