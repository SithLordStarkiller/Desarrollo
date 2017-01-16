SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2015-12-29
-- Description:	Proceso para monitorear en caso de caida del OCR
-- =============================================
CREATE PROCEDURE MonitorearOCR 
AS
BEGIN
	SET NOCOUNT ON;

	-- Cuenta las imagenes de ocr que no hayan sido procesadas y tengan mas de 1 hs de recibidas

	SELECT count(r.idOrden)
	FROM Respuestas r  WITH (NOLOCK)
	INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
	INNER JOIN Ordenes o WITH (NOLOCK)  ON o.idOrden =  r.idOrden
	INNER JOIN Creditos cr WITH (NOLOCK) ON o.[num_Cred] = cr.[CV_CREDITO]
	WHERE r.idOrden IN (
			SELECT o.idOrden
			FROM Ordenes o WITH (NOLOCK)
			INNER JOIN Respuestas r WITH (NOLOCK) ON o.idDominio = r.idDominio
				AND o.idUsuarioPadre = r.idUsuarioPadre
			INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
			WHERE o.Tipo LIKE '%s%'
				AND c.Nombre != 'Dictamenpromdepago'
				AND c.Nombre != 'DictamenpromdepagoTOM'
				AND c.Nombre != 'Dictamenliquida'
				AND c.Nombre LIKE 'Dictamen%'
			)
		AND r.valor LIKE 'http%'
		AND c.nombre IN (
			'fotoIFEFDCP'
			,'foto_ifeFrenteSTM'
			,'FotoIDanversoSTM'
			,'foto_identificacionBCN'
			,'fotoIFEFFPP'
			)
		AND r.idOrden NOT IN (
			SELECT r.idOrden
			FROM Respuestas r  WITH (NOLOCK)
			INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
			WHERE c.Nombre = 'EtiquetaOCR'
			)
		AND FechaRecepcion < DATEADD(hh,-1,GETDATE()) 
END
GO
