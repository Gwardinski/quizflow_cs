using System;
using QuizFlow.Dto.User;

namespace QuizFlow.Dto.Quiz {
  public class QuizDtoGet {
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string imageURL { get; set; }
    public double totalPoints { get; set; }
    public bool isPublished { get; set; }
    public DateTime lastUpdated { get; set; }
    public DateTime createdAt { get; set; }
    // relational values
    public UserDtoGet user { get; set; }
  }
}