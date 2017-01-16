SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 20151027
-- Description:	Obtiene las imagenes a procesar por el OCR
-- =============================================
CREATE PROCEDURE ObtenerImagenesOCR
AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.idOrden
		,r.Valor
		,cr.[TX_NOMBRE_ACREDITADO]
	FROM Respuestas r
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
			FROM Respuestas r
			INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
			WHERE c.Nombre = 'EtiquetaOCR'
			)
END
GO

/*
FotoIDanversoPP

foto_reciboNomina
foto_pagoMesAnte
foto_pagoMesAct
foto_vivienda
FotoAcreditadoSol
foto_otrouso1

FotoIDreversoPP
foto_ifeRevSTM
FotoIDreversoSTM
fotoIFERevDCP
foto_identificacionBCN2

*/
