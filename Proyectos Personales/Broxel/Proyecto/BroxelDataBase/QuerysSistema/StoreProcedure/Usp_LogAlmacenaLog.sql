USE [Broxel]
GO
/****** Object:  StoredProcedure [dbo].[Usp_LogAlmacenaLog]    Script Date: 19/02/2017 12:23:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Cruz
-- Create date: 01/01/2017
-- Description:	Se encargara de almacenar un log
-- =============================================
CREATE PROCEDURE [dbo].[Usp_LogAlmacenaLog] 
	@tipoLog varchar(MAX),
    @proyecto VarChar(MAX),
    @clase VarChar(MAX),
    @metodo VarChar(MAX),
    @mensage varchar(MAX),
    @log VarChar(MAX),
    @Excepcion varchar(MAX),
    @auxiliar VarChar(MAX)
AS
BEGIN

	INSERT INTO LOGLOGGER (TIPOLOG, PROYECTO, CLASE, METODO,MENSAGE,LOG,EXCEPCION,AUXILIAR,CREADO)  
	VALUES (@tipoLog , @proyecto, @clase,@metodo,@mensage,@log ,@Excepcion,@auxiliar, GETDATE());

	SELECT @@IDENTITY
END