using Microsoft.EntityFrameworkCore;
using TestePositivo.Domain.Entities;

namespace TestePositivo.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Aluno>(cfg =>
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
        });

        b.Entity<Endereco>(cfg =>
        {
            cfg.ToTable("Enderecos");
            cfg.HasKey(x => x.Id);
            cfg.Property(x => x.Id).ValueGeneratedOnAdd();
            cfg.Property(x => x.Tipo).IsRequired();
            cfg.Property(x => x.Rua).IsRequired();
            cfg.Property(x => x.CEP).IsRequired();
            cfg.Property(x => x.Numero).IsRequired();
        });
    }
}
