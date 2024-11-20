using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Domain.Entities;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures a <see cref="Quiz"/>, specifying table, primary key, and relationships with <see cref="Game"/> and <see cref="Question"/>.
/// </summary>
internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quiz");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name).IsRequired();

        builder.Property(p => p.Description).HasDefaultValue("");

        builder.Property(p => p.ImageUrl).HasDefaultValue("https://chrisjamiecarter.github.io/quiz-game/img/default.png");
    }
}
