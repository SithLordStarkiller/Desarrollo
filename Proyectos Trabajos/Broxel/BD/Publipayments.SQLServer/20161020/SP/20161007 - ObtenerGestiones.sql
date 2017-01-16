-- =============================================
-- AUTHOR:		LAURA DOTOR
-- CREATE DATE: 2016-10-07
-- DESCRIPTION:	OBTIENE DATOS DE LA GESTION QUE 
-- SE REALIZO POR MEDIO DEL ID DE LA ORDEN, SE PROCESA EN WS
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerGestiones]
@IDORDEN INT = 0
AS
BEGIN
	
	SET NOCOUNT ON;

		SELECT
			O.idOrden, 
			O.idPool, 
			O.num_Cred, 
			O.idUsuario, 
			O.idUsuarioPadre, 
			O.idUsuarioAlta, 
			O.idDominio, 
			O.idVisita, 
			O.FechaAlta, 
			O.Estatus, 
			O.usuario, 
			O.FechaModificacion, 
			O.FechaEnvio, 
			O.FechaRecepcion, 
			O.Auxiliar, 
			O.idUsuarioAnterior, 
			O.Tipo, 
			O.cvDelegacion, 
			O.CvRuta, 
			O.idDictamen,
			c.CV_DESPACHO, 
			c.CV_CREDITO, 
			c.TX_NOMBRE_DESPACHO, 
			c.CV_PROVEEDOR, 
			c.CV_CONTRATO, 
			c.CV_RUTA 
		FROM Ordenes O WITH (NOLOCK)
	JOIN Creditos c WITH (NOLOCK) ON O.num_Cred = c.CV_CREDITO AND o.idOrden = @IDORDEN

END

