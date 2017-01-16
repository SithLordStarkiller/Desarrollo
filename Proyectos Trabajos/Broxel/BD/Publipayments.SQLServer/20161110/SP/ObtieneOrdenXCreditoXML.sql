
/****** Object:  StoredProcedure [dbo].[ObtieneOrdenXCreditoXML]    Script Date: 15/11/2016 12:59:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Autor: Alberto rojas
--Fecha: 2015/10/01
--Descripcion: Se obtiene los datos del credito para generar el XML
ALTER PROCEDURE [dbo].[ObtieneOrdenXCreditoXML] (
	@Credito NVARCHAR(50)
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idVisita INT = 1
		,@Ruta VARCHAR(10)
		,@Incompleto INT=-1
		,@DicAplicaMttoFPP varchar(3)=0
		,@CantidadPagos int=0

	SELECT TOP 1 @Ruta = CV_RUTA
	FROM creditos WITH(NOLOCK)
	WHERE CV_CREDITO = @Credito

	SELECT @DicAplicaMttoFPP=(case when CV_ETIQUETA IN('C02','C06') then 'Si' else 'No' end) 
	FROM Creditos WITH(NOLOCK) 
	WHERE CV_CREDITO=@Credito
	
	select @CantidadPagos=(
	(case when TX_PAGA1 ='' OR TX_PAGA1  is null then 0 else 1 end)+
	(case when TX_PAGA2 ='' OR TX_PAGA2  is null then 0 else 1 end)+
	(case when TX_PAGA3 ='' OR TX_PAGA3  is null then 0 else 1 end)+
	(case when TX_PAGA4 ='' OR TX_PAGA4  is null then 0 else 1 end)+
	(case when TX_PAGA5 ='' OR TX_PAGA5  is null then 0 else 1 end)+
	(case when TX_PAGA6 ='' OR TX_PAGA6  is null then 0 else 1 end)) from Creditos WITH(NOLOCK) 
	WHERE CV_CREDITO=@Credito
	

	SELECT TOP 1 -1 idOrden
		,'' Usuario
		,c.*
		,p.CV_ESTATUS_PAGO
		,p.NU_PAGO_MES_CORRIENTE
		,p.IM_PAGO_MES_CORRIENTE
		,p.TX_PAGO_MES_CORRIENTE
		,1 idVisita
		,1 EstatusOrden
		,d.Descripcion TX_DELEGACION
		,f.Version
		,f.Nombre AS tipoFormulario
		,'' Tipo
		,'' AS CelularSMS_Recibido
		,'' ClaveSMS
		,'' DicGest_Ant
		,'0' SMSDuplicado 
		,ISNULL(e.TX_DESCRIPCION_ETIQUETA, 'No encontrada') as TX_DESCRIPCION_ETIQUETA
		,@DicAplicaMttoFPP DicAplicaMttoFPP
		,case  when CV_REGIMEN IN ('REA','EXT') then 1 when CV_REGIMEN in ('ROA')  then 2 else 0 end  as OP_REGIMEN
		,@CantidadPagos CantidadPagos
		,f.EnviarMovil AS EnviarMovil
	FROM dbo.CREDITOS c WITH(NOLOCK)
	INNER JOIN CatDelegaciones d WITH(NOLOCK) ON c.CV_DELEGACION = d.Delegacion
	INNER JOIN Formulario f  WITH(NOLOCK) ON f.Ruta = c.CV_RUTA
	LEFT JOIN dbo.CatEtiqueta e  WITH(NOLOCK) ON c.CV_ETIQUETA = e.CV_ETIQUETA
	LEFT JOIN pagos p  WITH(NOLOCK) ON p.CV_CREDITO = c.CV_CREDITO
	WHERE c.CV_CREDITO = @Credito
		AND f.idAplicacion = (
			SELECT valor
			FROM [CatalogoGeneral]
			WHERE Llave = 'idAplicacion'
			)
		AND f.Estatus = 1
		AND f.Captura = 1
	ORDER BY ID_ARCHIVO DESC

	SELECT cxml.*
	FROM CamposXML2 cxml WITH(NOLOCK)
	INNER JOIN Formulario f WITH(NOLOCK) ON f.idFormulario = cxml.idFormulario
	INNER JOIN Aplicacion a WITH(NOLOCK) ON f.idAplicacion = a.idAplicacion
	WHERE f.Estatus = 1
		AND a.idAplicacion = (
			SELECT valor
			FROM [CatalogoGeneral]
			WHERE Llave = 'idAplicacion'
			)
		AND f.Ruta = @Ruta
	ORDER BY cxml.Orden
END
