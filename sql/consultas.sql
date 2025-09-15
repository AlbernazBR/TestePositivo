SELECT COUNT(*) AS TotalAlunos FROM Alunos;

SELECT Serie, COUNT(*) AS Total
FROM Alunos
GROUP BY Serie
ORDER BY Serie;

SELECT Segmento, COUNT(*) AS Total
FROM Alunos
GROUP BY Segmento
ORDER BY Segmento;

WITH Idades AS (
  SELECT
    Id,
    Segmento,
    CASE
      WHEN DATEADD(YEAR, DATEDIFF(YEAR, DataNascimento, GETDATE()), DataNascimento) > CAST(GETDATE() AS DATE)
        THEN DATEDIFF(YEAR, DataNascimento, GETDATE()) - 1
      ELSE DATEDIFF(YEAR, DataNascimento, GETDATE())
    END AS Idade
  FROM Alunos
)
SELECT Segmento, COUNT(*) AS Qtde
FROM Idades
WHERE Idade BETWEEN 4 AND 8
GROUP BY Segmento
ORDER BY Segmento;

WITH Grupos AS (
  SELECT 'PAI' AS Tipo, NomePai AS Nome, COUNT(*) AS Qtde
  FROM Alunos
  WHERE NULLIF(LTRIM(RTRIM(NomePai)), '') IS NOT NULL
  GROUP BY NomePai
  HAVING COUNT(*) > 1

  UNION ALL

  SELECT 'MAE' AS Tipo, NomeMae AS Nome, COUNT(*) AS Qtde
  FROM Alunos
  WHERE NULLIF(LTRIM(RTRIM(NomeMae)), '') IS NOT NULL
  GROUP BY NomeMae
  HAVING COUNT(*) > 1
)
SELECT g.Tipo, g.Nome, g.Qtde,
       STRING_AGG(a.NomeCompleto, '; ') WITHIN GROUP (ORDER BY a.NomeCompleto) AS Irmaos
FROM Grupos g
JOIN Alunos a
  ON (g.Tipo = 'PAI' AND a.NomePai = g.Nome)
  OR (g.Tipo = 'MAE' AND a.NomeMae = g.Nome)
GROUP BY g.Tipo, g.Nome, g.Qtde
ORDER BY g.Qtde DESC;
