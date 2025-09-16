using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestePositivo.Domain.Entities;

namespace TestePositivo.Data.Configurations;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> cfg)
    {
        cfg.ToTable("Alunos");
        cfg.HasKey(x => x.Id);
        cfg.Property(x => x.Id).ValueGeneratedOnAdd();
        cfg.Property(x => x.Matricula).IsRequired();
        cfg.HasIndex(x => x.Matricula).IsUnique();

        cfg.Property(x => x.NomeCompleto).IsRequired();
        cfg.Property(x => x.DataNascimento).IsRequired();
        cfg.Property(x => x.Serie).IsRequired();
        cfg.Property(x => x.Segmento).IsRequired();

        cfg.HasOne(x => x.Endereco)
           .WithOne()
           .HasForeignKey<Endereco>(e => e.AlunoId);
    }
}
