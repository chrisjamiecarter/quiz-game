using Microsoft.EntityFrameworkCore;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Configurations;

namespace QuizGame.Infrastructure.Contexts;

/// <summary>
/// Represents the Entity Framework Core database context for the QuizGame data store.
/// </summary>
internal class QuizGameDataContext(DbContextOptions<QuizGameDataContext> options) : DbContext(options)
{
    public DbSet<Answer> Answer { get; set; } = default!;

    public DbSet<Game> Game { get; set; } = default!;

    public DbSet<Question> Question { get; set; } = default!;

    public DbSet<Quiz> Quiz { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new QuizConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
