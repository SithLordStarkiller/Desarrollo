/****** Object:  StoredProcedure [dbo].[ObtenerTelefonosEnvioSMS]    Script Date: 01/21/2016 11:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/12
-- Description:	Retorna los registros en lo que no se ha enviado mensaje SMS
-- Modificacion: PJV : 2016/01/21 se agrega nueva salida para identificar si es de CC y cambiar mensaje
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerTelefonosEnvioSMS]
AS
BEGIN
	SELECT a.num_Cred, a.idOrden, a.Telefono, a.Clave, rr.Valor AS Dictamen,case when o.Tipo like '%CS%' then 1 else 0 end esCallCenter
	FROM (
		SELECT r.idOrden, r.idDominio, r.idUsuarioPadre, r.Valor
		FROM respuestas r WITH (NOLOCK)
		INNER JOIN camposrespuesta cr WITH (NOLOCK) ON r.idcampo = cr.idCampo
			AND r.idformulario = cr.idformulario
		WHERE cr.nombre LIKE 'Dictamen%'
		) AS rr
	INNER JOIN AutorizacionSMS a ON rr.idOrden = a.idOrden
	INNER JOIN Ordenes o ON o.idOrden=rr.idORden
	WHERE a.FechaEnvio IS NULL
END
