using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizFlow.Models {
  public class Round {
    [Key]
    public int id { get; set; }
    [Required]
    public string title { get; set; }
    public string description { get; set; }
    public string imageURL { get; set; }
    public double totalPoints { get; set; }
    public bool isPublished { get; set; }
    public DateTime lastUpdated { get; set; }
    public DateTime createdAt { get; set; }
    // relational values
    public User user { get; set; }
    public List<Quiz> quizzes { get; set; }
    public List<Question> questions { get; set; } = new List<Question>();
  }
}