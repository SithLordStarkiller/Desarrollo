
/****** Object:  StoredProcedure [dbo].[ActualizaEstatusOrdenes]    Script Date: 09/28/2015 11:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 26/01/2015
-- Description:	Actualiza estatus de ordenes separadas por coma (maximo 100 ordenes) 
-- solo actualiza el estatus no modifica las respuestas			
--- Modification date: 28/09/2015
--- Author: Pablo Jaimes
--- Description: Se agrega reverso de tipo si quien llama a SP es un Reverso
-- =============================================
ALTER PROCEDURE [dbo].[ActualizaEstatusOrdenes] (
	@TextoOrdenes VARCHAR(1500)
	,@Estatus INT
	,@ActualizaFecha INT = 0 
	,@esReverso INT = 0 
	)
AS
BEGIN
	DECLARE @sql VARCHAR(8000) = '';

	SET @sql = @sql + 'UPDATE Ordenes SET Estatus = ' + CONVERT(VARCHAR(10), @Estatus)
	IF (@ActualizaFecha = 1)
	BEGIN
		SET @sql = @sql + ' ,  FechaModificacion = GETDATE() '
	END
	IF (@esReverso = 1)
	BEGIN
		SET @sql = @sql + ' ,  Tipo = case when Tipo =''C'' then '''' when Tipo =''CS'' then ''S'' else Tipo end '
	END
	SET @sql = @sql + ' WHERE idOrden IN (' + @TextoOrdenes + ') '

	EXECUTE sp_sqlexec @sql
END
