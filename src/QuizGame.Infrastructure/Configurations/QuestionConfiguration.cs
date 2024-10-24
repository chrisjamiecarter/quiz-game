using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Infrastructure.Models;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures a <see cref="QuestionModel"/>, specifying table, primary key, and relationships with <see cref="AnswerModel"/> and <see cref="QuizModel"/>.
/// </summary>
internal class QuestionConfiguration : IEntityTypeConfiguration<QuestionModel>
{
    public void Configure(EntityTypeBuilder<QuestionModel> builder)
    {
        builder.ToTable("Question");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Text).IsRequired();

        builder.HasOne(q => q.Quiz)
               .WithMany(a => a.Questions)
               .HasForeignKey(fk => fk.QuizId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
