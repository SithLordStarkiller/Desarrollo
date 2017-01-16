-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 27/10/2014
-- Description:	Controla y direcciona a los distintos SP del Ranking por indicador
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerRankingIndicadoresMaster] 
	 @Master VARCHAR(100) = NULL, 
	 @fc_DashBoard VARCHAR(100) = NULL, 
	 @Indicador VARCHAR(100) = NULL, 
	 @fc_Despacho VARCHAR(100) = NULL, 
	 @idUsuarioPadre VARCHAR(100) = NULL, 
	 @valorSuperior INT = 1, 
	 @fc_Delegacion VARCHAR(100) = NULL,
	 @TipoFormulario varchar(10) =NULL
	 
AS
BEGIN
	DECLARE @sql NVARCHAR(2000)
		DECLARE @ParmDefinition NVARCHAR(2000) = N'
			@Master VARCHAR(100), 
			@fc_DashBoard VARCHAR(100), 
			@fc_Despacho VARCHAR(100), 
			@idUsuarioPadre VARCHAR(100),	
			@valorSuperior INT = 1, 
			@fc_Delegacion VARCHAR(100), 
			@TipoFormulario VARCHAR(10)'

		SET @sql = CONCAT ('ObtenerRankInd', @Indicador, ' 
			@Master = @Master, 
			@fc_DashBoard = @fc_DashBoard, 
			@fc_Despacho = @fc_Despacho, 
			@idUsuarioPadre = @idUsuarioPadre, 
			@valorSuperior = @valorSuperior, 
			@fc_Delegacion = @fc_Delegacion, 
			@TipoFormulario = @TipoFormulario')

		EXECUTE sp_executesql @sql, @ParmDefinition, 
			@Master = @Master, 
			@fc_DashBoard = @fc_DashBoard, 
			@fc_Despacho = @fc_Despacho, 
			@idUsuarioPadre = @idUsuarioPadre, 
			@valorSuperior = @valorSuperior, 
			@fc_Delegacion = @fc_Delegacion, 
			@TipoFormulario = @TipoFormulario
END
