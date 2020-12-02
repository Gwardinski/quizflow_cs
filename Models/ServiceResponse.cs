namespace QuizFlow.Models {
  public class ServiceResponse<T> {
    public T data { get; set; }
    public int code { get; set; } = 200;
    public string message { get; set; } = "";
  }
}