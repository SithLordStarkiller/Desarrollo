
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/06/21
-- Description:	Manda una reasignacion de las ordenes que no cumplen con las condiciones para enviar el mensaje SMS
-- =============================================
CREATE PROCEDURE [dbo].[ReasignarGestionesTelInvalidos]
	
AS
BEGIN
	
	SET NOCOUNT ON;

   
DECLARE @OrdenesReasignar varchar (8000) 

SELECT  @OrdenesReasignar = ISNULL(+ @OrdenesReasignar + ', ', '') +CONVERT(varchar(10),o.idOrden)
FROM ordenes o WITH (NOLOCK) INNER JOIN respuestas r WITH (NOLOCK) ON o.idorden=r.idorden
INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON r.idCampo = cr.idCampo
WHERE o.estatus=3  AND CvRuta='CSD' AND o.idusuario>0
AND cr.Nombre LIKE 'CelularSMS_%' AND cr.Nombre <> 'CelularSMS_Ant'
AND r.idorden  IN (
		SELECT r.idorden from Respuestas r  WITH (NOLOCK) 
		INNER JOIN CamposRespuesta cr WITH (NOLOCK) on cr.idCampo=r.idCampo 
		WHERE cr.Nombre like 'TIPO_TEL'
	)
AND o.idorden NOT IN (
SELECT idorden  FROM autorizacionsms WITH (NOLOCK)
)
	 EXEC ActualizarEstatusUsuarioOrdenes @OrdenesReasignar,15,-1,1,1
END
