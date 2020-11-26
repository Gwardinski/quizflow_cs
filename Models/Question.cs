using System;
using System.Collections.Generic;

namespace QuizFlow.Models {
  public class Question {
    public int id { get; set; }
    public string question { get; set; }
    public string answer { get; set; }
    public string difficulty { get; set; }
    public double points { get; set; }
    public string category { get; set; }
    public string questionType { get; set; }
    public string imageURL { get; set; }
    public bool isPublished { get; set; }
    public DateTime lastUpdated { get; set; }
    public DateTime createdAt { get; set; }
    // relational values
    public User user { get; set; }
    public List<RoundQuestion> roundQuestions { get; set; }
  }
}