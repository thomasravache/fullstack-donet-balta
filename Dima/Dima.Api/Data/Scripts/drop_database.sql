USE [master];
GO

ALTER DATABASE [NOME_DA_BASE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE [NOME_DA_BASE]
GO