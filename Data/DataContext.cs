using Microsoft.EntityFrameworkCore;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {

    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }
  }
}