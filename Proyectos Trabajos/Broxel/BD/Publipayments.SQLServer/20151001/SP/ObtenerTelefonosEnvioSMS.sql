
/****** Object:  StoredProcedure [dbo].[ObtenerTelefonosEnvioSMS]    Script Date: 09/23/2015 17:17:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/12
-- Description:	Retorna los registros en lo que no se ha enviado mensaje SMS
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerTelefonosEnvioSMS]
AS
BEGIN
	SELECT a.num_Cred, a.idOrden, a.Telefono, a.Clave, rr.Valor AS Dictamen
	FROM (
		SELECT r.idOrden, r.idDominio, r.idUsuarioPadre, r.Valor
		FROM respuestas r WITH (NOLOCK)
		INNER JOIN camposrespuesta cr WITH (NOLOCK) ON r.idcampo = cr.idCampo
			AND r.idformulario = cr.idformulario
		WHERE cr.nombre LIKE 'Dictamen%'
		) AS rr
	INNER JOIN AutorizacionSMS a ON rr.idOrden = a.idOrden
	WHERE a.FechaEnvio IS NULL
END
