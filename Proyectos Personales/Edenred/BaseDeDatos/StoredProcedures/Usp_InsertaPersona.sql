-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure Usp_InsertaPersonas .SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 27/10/2015
-- Description:	Stored Procedured dedicado para insertar personas
-- =============================================
CREATE PROCEDURE Usp_InsertaPersonas 
	-- Parametros de entrada
	 @Nombre varchar(100),
	 @Sexo varchar(10),
	 @Edad int,

	-- Parametros de salida
	@IdPersona int OUT
AS
BEGIN
	INSERT INTO PER_PERSONAS(NOMBRE,SEXO,EDAD) VALUES (@Nombre,@Sexo,@Edad);

	SET @IdPersona = @@IDENTITY;
END
GO
