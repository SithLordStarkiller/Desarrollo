
/****** Object:  StoredProcedure [dbo].[ObtenerGestiones]    Script Date: 21/10/2016 12:41:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- AUTHOR:		LAURA DOTOR
-- CREATE DATE: 2016-10-07
-- DESCRIPTION:	OBTIENE DATOS DE LA GESTION QUE 
-- SE REALIZO POR MEDIO DEL ID DE LA ORDEN, SE PROCESA EN WS
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerGestiones]
@IDORDEN INT = 0
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @CV_DESPACHO VARCHAR(100),@CV_CREDITO  VARCHAR(15),@TX_NOMBRE_DESPACHO varchar,@CV_PROVEEDOR  VARCHAR(10),@CV_CONTRATO VARCHAR(15),@CV_RUTA VARCHAR(10)

SELECT	   @CV_DESPACHO=c.CV_DESPACHO, 
			@CV_CREDITO=c.CV_CREDITO, 
			@TX_NOMBRE_DESPACHO=c.TX_NOMBRE_DESPACHO, 
			@CV_PROVEEDOR=c.CV_PROVEEDOR, 
			@CV_CONTRATO=c.CV_CONTRATO, 
			@CV_RUTA=c.CV_RUTA 
			FROM  Creditos c WITH (NOLOCK) WHERE CV_CREDITO IN (SELECT num_cred FROM ordenes WITH(NOLOCK) WHERE idOrden = @IDORDEN )
IF (@CV_RUTA IS NULL )
BEGIN
	SELECT @CV_RUTA=Ruta from formulario where idformulario in (
	SELECT  TOP 1 idformulario from respuestas where idorden=@IDORDEN
)
END


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
			ISNULL(@CV_DESPACHO,'') AS CV_DESPACHO,
			ISNULL(@CV_CREDITO,'') AS CV_CREDITO,
			ISNULL(@TX_NOMBRE_DESPACHO,'') AS TX_NOMBRE_DESPACHO,
			ISNULL(@CV_PROVEEDOR,'') AS CV_PROVEEDOR,
			ISNULL(@CV_CONTRATO,'') AS CV_CONTRATO,
			ISNULL(@CV_RUTA,O.CvRuta) AS CV_RUTA
		FROM Ordenes O WITH (NOLOCK) WHERE idOrden = @IDORDEN

END

