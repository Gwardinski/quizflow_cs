using System;
using System.Collections.Generic;

namespace QuizFlow.Models {
  public class Round {
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string imageURL { get; set; }
    public double totalPoints { get; set; }
    public bool isPublished { get; set; }
    public DateTime lastUpdated { get; set; }
    public DateTime createdAt { get; set; }
    // relational values
    public User user { get; set; }
    public List<RoundQuestion> roundQuestions { get; set; }
  }
}