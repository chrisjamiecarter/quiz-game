using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Domain.Entities;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures a <see cref="Game"/>, specifying table, primary key, and relationships with <see cref="Quiz"/>.
/// </summary>
internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Played).IsRequired();
        builder.Property(p => p.Score).IsRequired();

        builder.HasOne(q => q.Quiz)
               .WithMany(a => a.Games)
               .HasForeignKey(fk => fk.QuizId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
