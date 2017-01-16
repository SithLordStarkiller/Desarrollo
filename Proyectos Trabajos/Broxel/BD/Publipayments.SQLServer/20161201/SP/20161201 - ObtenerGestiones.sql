/****** Object:  StoredProcedure [dbo].[ObtenerGestiones]    Script Date: 01/12/2016 10:45:15 a. m. ******/
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

	DECLARE 
		@CV_DESPACHO NVARCHAR(100) = '', 
		@CV_CREDITO VARCHAR(15) = '', 
		@TX_NOMBRE_DESPACHO VARCHAR(20) = '', 
		@CV_PROVEEDOR VARCHAR(10) = '', 
		@CV_CONTRATO VARCHAR(15) = '',  
		@CV_RUTA VARCHAR(10) = '' 

	SELECT @CV_CREDITO = NUM_CRED FROM ORDENES WITH(NOLOCK) WHERE IDORDEN = @IDORDEN

	SELECT 
		@CV_DESPACHO = CV_DESPACHO,
		@TX_NOMBRE_DESPACHO = TX_NOMBRE_DESPACHO,
		@CV_PROVEEDOR = CV_PROVEEDOR,
		@CV_CONTRATO = CV_CONTRATO,
		@CV_RUTA = CV_RUTA
	FROM CREDITOS WITH(NOLOCK) WHERE CV_CREDITO = @CV_CREDITO


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
		@CV_DESPACHO AS 'CV_DESPACHO',
		@CV_CREDITO AS 'CV_CREDITO', 
		@TX_NOMBRE_DESPACHO AS 'TX_NOMBRE_DESPACHO', 
		@CV_PROVEEDOR AS 'CV_PROVEEDOR', 
		@CV_CONTRATO AS 'CV_CONTRATO', 
		@CV_RUTA AS 'CV_RUTA' 
	FROM Ordenes O WITH (NOLOCK) WHERE O.idOrden = @IDORDEN

END

