using System;

namespace QuizFlow.Dto.Question {
  public class QuestionDtoEdit {
    public int id { get; set; }
    public string question { get; set; }
    public string answer { get; set; }
    public string difficulty { get; set; }
    public double points { get; set; }
    public string category { get; set; }
    public string questionType { get; set; }
    public string imageURL { get; set; }
    public bool isPublished { get; set; }
  }
}