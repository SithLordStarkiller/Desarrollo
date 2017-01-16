
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/04/11
-- Description:	Manda una reasignacion de las ordenes que se gestionaron con un telefono que ya se encuentra registrado con otro credito
-- =============================================
create PROCEDURE ReasignarGestionesTelDuplicados
	
AS
BEGIN
	
	SET NOCOUNT ON;

   
declare @OrdenesReasignar varchar (8000) 

SELECT 
		@OrdenesReasignar = isnull(+ @OrdenesReasignar + ', ', '') +convert(varchar(10),t1.idOrden)
		
	FROM AutorizacionSMS au WITH(NOLOCK) 
	INNER JOIN (
		SELECT o.idOrden
			,o.num_Cred
			,Valor
		FROM Respuestas r WITH(NOLOCK) 
		LEFT JOIN CamposRespuesta cr WITH(NOLOCK)  ON r.idCampo = cr.idCampo
		LEFT JOIN Ordenes o WITH(NOLOCK) ON r.idOrden = o.idOrden
		WHERE cr.Nombre LIKE 'CelularSMS_%'
			AND cr.Nombre <> 'CelularSMS_Ant'
		) t1 ON (
			t1.Valor = au.Telefono
			AND au.num_Cred <> t1.num_Cred
			)
	LEFT JOIN Ordenes o WITH(NOLOCK) ON o.idOrden = t1.idOrden
	LEFT JOIN Creditos c WITH(NOLOCK) ON c.CV_CREDITO = t1.num_Cred
	LEFT JOIN Usuario ug WITH(NOLOCK) ON ug.idUsuario = o.idUsuario
	LEFT JOIN Usuario us WITH(NOLOCK) ON us.idUsuario = o.idUsuarioPadre
	WHERE
	 c.CV_RUTA='CSD'
	 AND o.estatus=3
	 AND o.idusuario>0

	

	 EXEC ActualizarEstatusUsuarioOrdenes @OrdenesReasignar,15,-1,1,1
END
GO