namespace Koncar.Interview.Server.Infrastructure.Db.Postgres.Configurations;

using Koncar.Interview.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> entity)
    {
        entity.ToTable("characters");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        entity.Property(e => e.Name)
            .IsRequired();

        entity.Property(e => e.Description);
    }
}
