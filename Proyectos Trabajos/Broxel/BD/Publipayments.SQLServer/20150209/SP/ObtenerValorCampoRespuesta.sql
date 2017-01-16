-- ==================================================================
-- Author:	Alberto rojas
-- Create date: 2015/02/09
-- Description: regresa el valor del campo que se solicite
-- ==================================================================
CREATE PROCEDURE ObtenerValorCampoRespuesta (
	@idOrden INT
	,@Nombre NVARCHAR(50)
	)
AS
BEGIN
	SELECT r.idOrden
		,r.idCampo
		,r.Valor
		,r.idDominio
		,r.idUsuarioPadre
		,r.idFormulario
	FROM Respuestas r WITH (NOLOCK)
	INNER JOIN CamposRespuesta cr  WITH (NOLOCK) ON r.idFormulario = cr.idFormulario
		AND r.idCampo = cr.idCampo
	WHERE r.idOrden = @idOrden
		AND cr.Nombre = @Nombre
END
GO


