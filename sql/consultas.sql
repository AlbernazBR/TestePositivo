-- 1) Total de alunos cadastrados
SELECT COUNT(*) AS TotalAlunos
FROM dbo.Alunos;

-- 2) Total de alunos por série
SELECT Serie, COUNT(*) AS Total
FROM dbo.Alunos
GROUP BY Serie
ORDER BY Serie;

-- 3) Total de alunos por segmento
SELECT Segmento, COUNT(*) AS Total
FROM dbo.Alunos
GROUP BY Segmento
ORDER BY Segmento;

-- 4) Quantidade e segmento de alunos entre 4 e 8 anos (inclusive)
;WITH Idades AS (
    SELECT
        a.Id,
        a.Segmento,
        Idade = DATEDIFF(YEAR, a.DataNascimento, GETDATE())
                - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, a.DataNascimento, GETDATE()), a.DataNascimento) > GETDATE()
                       THEN 1 ELSE 0 END
    FROM dbo.Alunos a
)
SELECT Segmento, COUNT(*) AS Qtde
FROM Idades
WHERE Idade BETWEEN 4 AND 8
GROUP BY Segmento
ORDER BY Segmento;

--5) Quantos e quais são irmãos (baseado no nome do Pai ou da Mãe);

-- Alunos que pertencem a algum grupo de irmãos (>=2) por Pai ou por Mãe
WITH GruposPai AS (
    SELECT NomePai
    FROM dbo.Alunos
    WHERE ISNULL(LTRIM(RTRIM(NomePai)), N'') <> N''
    GROUP BY NomePai
    HAVING COUNT(*) >= 2
),
GruposMae AS (
    SELECT NomeMae
    FROM dbo.Alunos
    WHERE ISNULL(LTRIM(RTRIM(NomeMae)), N'') <> N''
    GROUP BY NomeMae
    HAVING COUNT(*) >= 2
),
Irmaos AS (
    SELECT a.Id, a.Matricula, a.NomeCompleto, 'Pai' AS Tipo, a.NomePai AS Responsavel
    FROM dbo.Alunos a
    JOIN GruposPai p ON a.NomePai = p.NomePai
    UNION
    SELECT a.Id, a.Matricula, a.NomeCompleto, 'Mãe' AS Tipo, a.NomeMae AS Responsavel
    FROM dbo.Alunos a
    JOIN GruposMae m ON a.NomeMae = m.NomeMae
)
SELECT COUNT(DISTINCT Id) AS QtdeAlunosIrmaos
FROM Irmaos;

-- Listagem dos alunos que possuem irmãos
WITH GruposPai AS (
    SELECT NomePai
    FROM dbo.Alunos
    WHERE ISNULL(LTRIM(RTRIM(NomePai)), N'') <> N''
    GROUP BY NomePai
    HAVING COUNT(*) >= 2
),
GruposMae AS (
    SELECT NomeMae
    FROM dbo.Alunos
    WHERE ISNULL(LTRIM(RTRIM(NomeMae)), N'') <> N''
    GROUP BY NomeMae
    HAVING COUNT(*) >= 2
),
Irmaos AS (
    SELECT a.Id, a.Matricula, a.NomeCompleto, 'Pai' AS Tipo, a.NomePai AS Responsavel
    FROM dbo.Alunos a
    JOIN GruposPai p ON a.NomePai = p.NomePai
    UNION
    SELECT a.Id, a.Matricula, a.NomeCompleto, 'Mãe' AS Tipo, a.NomeMae AS Responsavel
    FROM dbo.Alunos a
    JOIN GruposMae m ON a.NomeMae = m.NomeMae
)

SELECT DISTINCT
    Id,
    Matricula,
    NomeCompleto
FROM Irmaos
ORDER BY NomeCompleto;