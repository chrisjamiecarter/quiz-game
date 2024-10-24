using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Infrastructure.Models;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures an <see cref="AnswerModel"/>, specifying table, primary key, and relationships with <see cref="QuestionModel"/>.
/// </summary>
internal class AnswerConfiguration : IEntityTypeConfiguration<AnswerModel>
{
    public void Configure(EntityTypeBuilder<AnswerModel> builder)
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
