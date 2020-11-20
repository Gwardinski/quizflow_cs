using Microsoft.EntityFrameworkCore;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<Question> Questions { get; set; }

    public DbSet<RoundQuestion> RoundQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<RoundQuestion>().HasKey(rq => new { rq.roundId, rq.questionId });
    }
  }
}