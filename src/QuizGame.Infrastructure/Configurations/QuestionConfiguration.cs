using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Domain.Entities;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures a <see cref="Question"/>, specifying table, primary key, and relationships with <see cref="Answer"/> and <see cref="Quiz"/>.
/// </summary>
internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
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
