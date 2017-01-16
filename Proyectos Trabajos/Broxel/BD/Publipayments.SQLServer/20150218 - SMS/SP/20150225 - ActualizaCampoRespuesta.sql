
-- =============================================
-- Author:		Alberto rojas
-- Create date: 2015/02/25
-- Description: Actualiza un campo de la tabla de Respuestas
-- =============================================
CREATE PROCEDURE ActualizaCampoRespuesta @idOrden int, @NombreCampo nvarchar(50), @ValorAct nvarchar (350)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Respuestas SET Valor=@ValorAct
	 FROM CamposRespuesta cr INNER JOIN Respuestas r ON cr.idCampo=r.idcampo AND cr.idFormulario=r.idFormulario
	WHERE cr.Nombre=@NombreCampo AND r.idOrden=@idOrden
	SET NOCOUNT OFF;
END
GO
