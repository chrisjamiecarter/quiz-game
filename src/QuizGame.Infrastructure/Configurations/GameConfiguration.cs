using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Infrastructure.Models;

namespace QuizGame.Infrastructure.Configurations;

/// <summary>
/// Configures a <see cref="GameModel"/>, specifying table, primary key, and relationships with <see cref="QuizModel"/>.
/// </summary>
internal class GameConfiguration : IEntityTypeConfiguration<GameModel>
{
    public void Configure(EntityTypeBuilder<GameModel> builder)
    {
        builder.ToTable("Game");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Played).IsRequired();

        builder.HasOne(q => q.Quiz)
               .WithMany(a => a.Games)
               .HasForeignKey(fk => fk.QuizId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
