/****** Script for SelectTopNRows command from SSMS  ******/
-- reset Id
DBCC CHECKIDENT ('Kategorie');

SELECT 'INSERT INTO Kategorie VALUES (''' + nazwa + ''')' 
  FROM [SecDB].[dbo].[Kategorie]