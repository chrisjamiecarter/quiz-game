using Microsoft.EntityFrameworkCore;
using QuizGame.Infrastructure.Configurations;
using QuizGame.Infrastructure.Models;

namespace QuizGame.Infrastructure.Contexts;

/// <summary>
/// Represents the Entity Framework Core database context for the QuizGame data store.
/// </summary>
internal class QuizGameDataContext(DbContextOptions<QuizGameDataContext> options) : DbContext(options)
{
    public DbSet<AnswerModel> Answer { get; set; } = default!;
    
    public DbSet<GameModel> Game { get; set; } = default!;
    
    public DbSet<QuestionModel> Question { get; set; } = default!;
    
    public DbSet<QuizModel> Quiz { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new QuizConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
