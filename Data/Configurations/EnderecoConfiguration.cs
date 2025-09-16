using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestePositivo.Domain.Entities;

namespace TestePositivo.Data.Configurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> cfg)
    {
        cfg.ToTable("Enderecos");
        cfg.HasKey(x => x.Id);
        cfg.Property(x => x.Id).ValueGeneratedOnAdd();
        cfg.Property(x => x.Tipo).IsRequired();
        cfg.Property(x => x.Rua).IsRequired();
        cfg.Property(x => x.CEP).IsRequired();
        cfg.Property(x => x.Numero).IsRequired();
    }
}
