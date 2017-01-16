SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 20151027
-- Description:	Obtiene las ordenes de los convenios que fueron aceptadas por el acreditado o su conyuge 
-- =============================================
CREATE PROCEDURE ObtenerOrdenesPorAceptacionConvenio @Familiar BIT
AS
BEGIN
	SET NOCOUNT ON;

	IF (@Familiar = 1)
	BEGIN
		-- Convenios aceptados por Aceptado por familiar
		SELECT o.idOrden
		FROM Ordenes o WITH (NOLOCK)
		INNER JOIN Respuestas r WITH (NOLOCK) ON o.idDominio = r.idDominio
			AND o.idUsuarioPadre = r.idUsuarioPadre
			AND o.idOrden = r.idOrden
		INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
		WHERE o.Tipo LIKE '%s%'
			AND c.Nombre = 'locAcrSol'
			AND r.Valor != 'Si'
			AND o.idOrden IN (
				SELECT o.idOrden
				FROM Ordenes o WITH (NOLOCK)
				INNER JOIN Respuestas r WITH (NOLOCK) ON o.idDominio = r.idDominio
					AND o.idUsuarioPadre = r.idUsuarioPadre
					AND o.idOrden = r.idOrden
				INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
				WHERE o.Tipo LIKE '%s%'
					AND c.Nombre != 'Dictamenpromdepago'
					AND c.Nombre != 'DictamenpromdepagoTOM'
					AND c.Nombre != 'Dictamenliquida'
					AND c.Nombre != 'DictamenFPP'
					AND c.Nombre LIKE 'Dictamen%'
				)
			AND o.idOrden NOT IN (
				-- Quita las ordenes que ya fueron procesadas
				SELECT r.idOrden
				FROM Respuestas r WITH (NOLOCK)
				INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
				WHERE c.Nombre = 'EtiquetaOCR'
				)
	END
	ELSE
	BEGIN
		-- Convenios aceptados por Acreditado revisar por OCR
		SELECT o.IdOrden
		FROM Ordenes o WITH (NOLOCK)
		INNER JOIN Respuestas r WITH (NOLOCK) ON o.idDominio = r.idDominio
			AND o.idUsuarioPadre = r.idUsuarioPadre
			AND o.idOrden = r.idOrden
		INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
		WHERE o.Tipo LIKE '%s%'
			AND c.Nombre = 'locAcrSol'
			AND r.Valor = 'Si'
			AND o.idOrden IN (
				SELECT o.idOrden
				FROM Ordenes o WITH (NOLOCK)
				INNER JOIN Respuestas r WITH (NOLOCK) ON o.idDominio = r.idDominio
					AND o.idUsuarioPadre = r.idUsuarioPadre
					AND o.idOrden = r.idOrden
				INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
				WHERE o.Tipo LIKE '%s%'
					AND c.Nombre != 'Dictamenpromdepago'
					AND c.Nombre != 'DictamenpromdepagoTOM'
					AND c.Nombre != 'Dictamenliquida'
					AND c.Nombre != 'DictamenFPP'
					AND c.Nombre LIKE 'Dictamen%'
				)
			AND o.idOrden NOT IN (
				-- Quita las ordenes que ya fueron procesadas
				SELECT r.idOrden
				FROM Respuestas r WITH (NOLOCK)
				INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
				WHERE c.Nombre = 'EtiquetaOCR'
				)
	END
END


-- ObtenerOrdenesPorAceptacionConvenio 0