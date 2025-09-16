-- ========================================
-- Script de criação e carga inicial
-- Banco: TestePositivo
-- ========================================

-- Cria o banco de dados (ajuste o caminho se necessário)
IF DB_ID('TestePositivo') IS NOT NULL
    DROP DATABASE TestePositivo;
GO

CREATE DATABASE TestePositivo;
GO

USE TestePositivo;
GO

-- ========================================
-- Tabelas
-- ========================================
CREATE TABLE [dbo].[Alunos] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [Matricula]      BIGINT         NOT NULL,
    [NomeCompleto]   NVARCHAR (200) NOT NULL,
    [DataNascimento] DATETIME2 (7)  NOT NULL,
    [Serie]          INT            NOT NULL,
    [Segmento]       INT            NOT NULL,
    [NomePai]        NVARCHAR (200) NULL,
    [NomeMae]        NVARCHAR (200) NULL,
    CONSTRAINT [PK_Alunos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_Alunos_Matricula]
    ON [dbo].[Alunos]([Matricula] ASC);

CREATE TABLE [dbo].[Enderecos] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [AlunoId]     BIGINT         NOT NULL,
    [Tipo]        INT            NOT NULL,
    [Rua]         NVARCHAR (200) NOT NULL,
    [CEP]         NVARCHAR (20)  NOT NULL,
    [Numero]      NVARCHAR (20)  NOT NULL,
    [Complemento] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Enderecos_Alunos_AlunoId] FOREIGN KEY ([AlunoId]) REFERENCES [dbo].[Alunos] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_Enderecos_AlunoId]
    ON [dbo].[Enderecos]([AlunoId] ASC);

-- ========================================
-- Dados de exemplo
-- ========================================
INSERT INTO [dbo].[Alunos] (Matricula, NomeCompleto, DataNascimento, Serie, Segmento, NomePai, NomeMae)
VALUES
(1001, 'João Silva',           '2017-05-12', 11, 1, 'Carlos Silva', 'Maria Silva'), -- 1º Ano
(1002, 'Ana Souza',            '2015-09-22', 14, 1, 'Pedro Souza',  'Fernanda Souza'), -- 4º Ano
(1003, 'Lucas Oliveira',       '2012-02-18', 22, 2, 'Rafael Oliveira', 'Patrícia Oliveira'), -- 7º Ano
(1004, 'Luiza Machado',        '2017-03-17', 14, 1, 'Sylvio Machado', 'Karina Machado'), -- 4º Ano
(1005, 'Marina Oliveira',      '2014-08-30', 21, 2, 'Rafael Oliveira', 'Patrícia Oliveira'), -- 6º Ano (irmã do Lucas)
(1006, 'Pedro Silva',          '2018-01-10', 11, 1, 'Carlos Silva', 'Maria Silva'); -- 1º Ano (irmão do João)

-- Endereços
INSERT INTO [dbo].[Enderecos] (AlunoId, Tipo, Rua, CEP, Numero, Complemento)
VALUES
(1, 2, 'Rua A', '14000-000', '10', 'Bloco 1'),
(2, 2, 'Rua B', '14001-000', '20', NULL),
(3, 2, 'Rua C', '14002-000', '30', 'Bloco C'),
(4, 2, 'Rua Campos Sales', '14027-250', '100', 'Quadra 4 lote 16'),
(5, 2, 'Rua D', '14003-000', '50', NULL),
(6, 2, 'Rua E', '14004-000', '60', 'Casa 2');
