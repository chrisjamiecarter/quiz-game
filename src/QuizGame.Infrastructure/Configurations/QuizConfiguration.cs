using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Infrastructure.Models;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures a <see cref="QuizModel"/>, specifying table, primary key, and relationships with <see cref="GameModel"/> and <see cref="QuestionModel"/>.
/// </summary>
internal class QuizConfiguration : IEntityTypeConfiguration<QuizModel>
{
    public void Configure(EntityTypeBuilder<QuizModel> builder)
    {
        builder.ToTable("Quiz");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name).IsRequired();

        builder.Property(p => p.Name).HasDefaultValue("");
    }
}
