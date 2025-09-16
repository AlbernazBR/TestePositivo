using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestePositivo.Migrations
{
    public partial class SeedAlunos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Alunos ON;

                -- Serie/Segmento/Tipo usam inteiros dos enums
                -- Serie: G1=1,G2=2,G3=3, Ano1=11..Ano5=15, Ano6=21..Ano9=24, EM1=31..EM3=33
                -- Segmento: Infantil=1, AnosIniciais=2, AnosFinais=3, EnsinoMedio=4
                -- TipoEndereco: Cobranca=1, Residencial=2, Correspondencia=3

                INSERT INTO Alunos (Id, Matricula, NomeCompleto, DataNascimento, Serie, Segmento, NomePai, NomeMae) VALUES
                (1, 1, N'João Silva',   '2017-05-12', 11, 2, N'Carlos Silva',    N'Maria Silva'),     -- Ano1 / AnosIniciais
                (2, 2, N'Ana Souza',    '2015-09-22', 14, 2, N'Pedro Souza',     N'Fernanda Souza'),  -- Ano4 / AnosIniciais
                (3, 3, N'Lucas Oliveira','2012-02-18', 22, 3, N'Rafael Oliveira', N'Patrícia Oliveira'); -- Ano7 / AnosFinais

                SET IDENTITY_INSERT Alunos OFF;

                SET IDENTITY_INSERT Enderecos ON;

                INSERT INTO Enderecos (Id, AlunoId, Tipo, Rua, CEP, Numero, Complemento) VALUES
                (1, 1, 2, N'Rua A', N'12345000', N'10', N'Casa'),
                (2, 2, 2, N'Rua B', N'23456000', N'20', N'Apto 202'),
                (3, 3, 1, N'Rua C', N'34567000', N'30', N'Bloco C');

                SET IDENTITY_INSERT Enderecos OFF;
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM Enderecos WHERE Id IN (1,2,3);
                DELETE FROM Alunos    WHERE Id IN (1,2,3);
                ");
        }
    }
}
