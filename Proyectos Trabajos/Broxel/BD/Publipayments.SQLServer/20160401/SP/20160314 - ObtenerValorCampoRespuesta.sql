
/****** Object:  StoredProcedure [dbo].[ObtenerValorCampoRespuesta]    Script Date: 14/03/2016 10:53:03 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==================================================================
-- Author:	Alberto rojas
-- Create date: 2015/02/09
-- Description: regresa el valor del campo que se solicite
-- *Modificacion: 20160314 - se agrega la capacidad de poder buscar valores globales distintos que se encuentren como respuesta - JARO
-- ==================================================================
ALTER PROCEDURE [dbo].[ObtenerValorCampoRespuesta] (
	@idOrden INT
	,@Nombre NVARCHAR(50)
	)
AS
BEGIN

	IF(@idOrden=-9999)
	BEGIN 
		SELECT 
		r.Valor
		FROM Respuestas r WITH (NOLOCK)
		INNER JOIN CamposRespuesta cr  WITH (NOLOCK) ON r.idFormulario = cr.idFormulario
		AND r.idCampo = cr.idCampo
		WHERE cr.Nombre = @Nombre
		GROUP BY  r.Valor
	END
	ELSE
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


END





