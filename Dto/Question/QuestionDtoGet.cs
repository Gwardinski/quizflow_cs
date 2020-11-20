using System;
using System.Collections.Generic;
using QuizFlow.Dto.Round;
using QuizFlow.Dto.User;

namespace QuizFlow.Dto.Question {
  public class QuestionDtoGet {
    public int id { get; set; }
    public string question { get; set; }
    public string answer { get; set; }
    public string difficulty { get; set; }
    public double points { get; set; }
    public string category { get; set; }
    public string questionType { get; set; }
    public string imageURL { get; set; }
    public bool isPublished { get; set; }
    // public DateTime lastUpdated { get; set; }
    // public DateTime createdAt { get; set; }
    // relational values
    public UserDtoGet user { get; set; }
    // public List<RoundDtoGet> rounds { get; set; }
  }
}