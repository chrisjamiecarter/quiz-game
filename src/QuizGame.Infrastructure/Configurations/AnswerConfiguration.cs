using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Domain.Entities;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures an <see cref="Answer"/>, specifying table, primary key, and relationships with <see cref="Question"/>.
/// </summary>
internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("Answer");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Text).IsRequired();
        builder.Property(p => p.IsCorrect).IsRequired();

        builder.HasOne(q => q.Question)
               .WithMany(a => a.Answers)
               .HasForeignKey(fk => fk.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
