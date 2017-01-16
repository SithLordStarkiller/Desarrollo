
/****** Object:  StoredProcedure [dbo].[ObtieneOrdenXML]    Script Date: 14/11/2016 16:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Autor: Maximiliano Silva
--Fecha: 2014/11/10
--Descripcion: Se obtiene la orden y los datos del credito para generar el XML
--Fecha Modificación: 2015/02/17
--Moidificó: Alberto Rojas
--Modificación: se agrega el campo de tipo, con esto sabemos si la orden tiene un tipo en especifico
--Modificacion: se agregan campos de TX_PAGO asi como el calculo de la cantidad 
ALTER PROCEDURE [dbo].[ObtieneOrdenXML] (
	@idPool INT
	,@Credito NVARCHAR(50)
	,@idOrden INT = - 1
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idVisita INT = 1
		,@usuario VARCHAR(50)
		,@Ruta VARCHAR(10)
		,@Tipo CHAR(1) = ' '
		,@TelefonoSMS VARCHAR(10)
		,@DicGestAnt VARCHAR(100)
		,@Incompleto INT=-1
		,@ClaveSMS VARCHAR(10)
		,@EstatusOrden INT=0
		,@DicAplicaMttoFPP varchar(3)=0
		,@CantidadPagos int=0
		,@MensajeTelSMS varchar(100)

		DECLARE @ResultadoTelefono  TABLE(TELEFONO VARCHAR(15),MENSAJE VARCHAR (100))

	IF (ISNULL(@Credito, '') != '')
	BEGIN
		SELECT @idVisita = [idVisita]
		FROM Ordenes WITH(NOLOCK)
		WHERE [num_Cred] = @Credito
			AND idOrden = @idOrden
	END
	ELSE
	BEGIN
		SELECT @idVisita = [idVisita],@EstatusOrden=[Estatus]
		FROM Ordenes WITH(NOLOCK)
		WHERE idOrden = @idOrden

		SELECT TOP 1 @Credito = num_cred
		FROM Ordenes WITH(NOLOCK)
		WHERE idOrden = @idOrden
	END

	SELECT TOP 1 @Ruta = CV_RUTA
	FROM creditos WITH(NOLOCK)
	WHERE CV_CREDITO = @Credito

	SELECT @Tipo = Tipo
	FROM Ordenes WITH(NOLOCK)
	WHERE idOrden = @idOrden

	SET @idVisita = ISNULL(@idVisita, 1);

	SELECT @usuario = u.Usuario
	FROM USUARIO u WITH(NOLOCK)
	INNER JOIN Ordenes o WITH(NOLOCK) ON o.idUsuario = u.idUsuario
	WHERE idOrden = @idOrden

	SELECT @TelefonoSMS = CASE 
			WHEN @idVisita = 1
				THEN ''
			ELSE ISNULL(Telefono, '')
			END
			,@ClaveSMS=clave
	FROM AutorizacionSMS WITH(NOLOCK)
	WHERE idOrden = @idOrden


	DECLARE @FechaBitacora DATETIME

	select  @FechaBitacora=max(Fecha) from BitacoraRespuestas WITH(NOLOCK) where idOrden=@idOrden
	
	SELECT @DicGestAnt = CASE 
			WHEN @idVisita = 1
				THEN ''
			ELSE ISNULL(br.Valor, '')
			END
	FROM BitacoraRespuestas br WITH(NOLOCK)
	LEFT JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = br.idCampo
	WHERE idOrden = @idOrden
	and cr.Nombre like 'Dictamen%'
	and br.Fecha = @FechaBitacora


	INSERT INTO @ResultadoTelefono  EXEC ObtenerUsoTelefono @idOrden

	IF(@TelefonoSMS ='' OR @TelefonoSMS IS NULL )
		BEGIN
			SELECT @TelefonoSMS=TELEFONO from @ResultadoTelefono
		END
		
	SELECT @MensajeTelSMS=MENSAJE from @ResultadoTelefono


	SELECT @Incompleto = count(tipo)
	from Ordenes  WITH(NOLOCK)
	WHERE idOrden = @idOrden and tipo='I'
	
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
	

	SELECT TOP 1 @idOrden idOrden
		,@usuario Usuario
		,c.*
		,p.CV_ESTATUS_PAGO
		,p.NU_PAGO_MES_CORRIENTE
		,p.IM_PAGO_MES_CORRIENTE
		,p.TX_PAGO_MES_CORRIENTE
		,@idVisita idVisita
		,@EstatusOrden EstatusOrden
		,d.Descripcion TX_DELEGACION
		,f.Version
		,f.Nombre AS tipoFormulario
		,@Tipo Tipo
		,@TelefonoSMS AS CelularSMS_Recibido
		,@ClaveSMS ClaveSMS
		,@MensajeTelSMS AS MensajeTelSMS 
		,@DicGestAnt DicGest_Ant
		,ISNULL(e.TX_DESCRIPCION_ETIQUETA, 'No encontrada') as TX_DESCRIPCION_ETIQUETA
		,@DicAplicaMttoFPP DicAplicaMttoFPP
		,case  when CV_REGIMEN IN ('REA','EXT') then 1 when CV_REGIMEN in ('ROA')  then 2 else 0 end  as OP_REGIMEN
		,originacion.*
		,@CantidadPagos CantidadPagos
		,f.EnviarMovil AS EnviarMovil
	FROM dbo.CREDITOS c WITH(NOLOCK)
	INNER JOIN CatDelegaciones d WITH(NOLOCK) ON c.CV_DELEGACION = d.Delegacion
	INNER JOIN Formulario f  WITH(NOLOCK) ON f.Ruta = c.CV_RUTA
	LEFT JOIN dbo.CatEtiqueta e  WITH(NOLOCK) ON c.CV_ETIQUETA = e.CV_ETIQUETA
	LEFT JOIN pagos p  WITH(NOLOCK) ON p.CV_CREDITO = c.CV_CREDITO
	INNER JOIN (
	SELECT  CASE  @Incompleto WHEN 1 THEN  CASE  WHEN FotoActNacimiento IS NULL AND @idVisita=1  THEN 'Si'  ELSE 'No' END ELSE 'No' END [FotoActNacimiento], CASE @Incompleto WHEN 1 THEN   CASE WHEN AvisoRetencion IS NULL AND @idVisita=2 THEN 'Si'  ELSE 'No' END ELSE 'No' END  AvisoRetencion , [DocAcuRecTarjeta], [DocAvRetencion], [DocCarContrato], [DocConAperCredito], [DocContrato], [DocSolInscripcion], [FotoAutBuro], CASE @Incompleto WHEN 1 THEN   CASE  WHEN FotoAcuRecTarjeta is null AND @idVisita=3 then 'Si'  ELSE 'No' END ELSE 'No' END [FotoAcuRecTarjeta],CASE @Incompleto WHEN 1 THEN  CASE  WHEN FotoCarContrato IS NULL AND @idVisita=3 THEN 'Si'  ELSE 'No' END ELSE 'No' END [FotoCarContrato],CASE @Incompleto WHEN 1 THEN  CASE  WHEN FotoCompDomicilio IS NULL AND @idVisita=1 THEN 'Si'  ELSE 'No' END ELSE 'No' END [FotoCompDomicilio], CASE @Incompleto WHEN 1 THEN   CASE  WHEN FotoConAperCredito IS NULL AND @idVisita=3 THEN 'Si'  ELSE 'No' END ELSE 'No' END  [FotoConAperCredito], CASE @Incompleto WHEN 1 THEN   CASE  WHEN FotoContrato IS NULL AND @idVisita=3 THEN 'Si'  ELSE 'No' END ELSE 'No' END [FotoContrato], CASE @Incompleto WHEN 1 THEN   CASE  WHEN FotoIdOficAnverso IS NULL AND @idVisita=1 THEN 'Si'  ELSE 'No' END ELSE 'No' END [FotoIdOficAnverso],CASE @Incompleto WHEN 1 THEN   CASE WHEN FotoIdOficReverso IS NULL AND @idVisita=1 THEN 'Si'  ELSE 'No' END ELSE 'No' END [FotoIdOficReverso], CASE @Incompleto WHEN 1 THEN   CASE  WHEN FotoSolInscripcion IS NULL AND @idVisita=1 THEN 'Si'  ELSE 'No' END ELSE 'No' END  [FotoSolInscripcion],CASE @Incompleto WHEN 1 THEN   CASE  WHEN SolicitudFormalCredito IS NULL AND @idVisita=2 THEN 'Si'  ELSE 'No' END ELSE 'No' END [SolicitudFormalCredito], @Credito CV_CREDITO2, CASE @Incompleto WHEN 1 THEN   CASE @idVisita WHEN 1 THEN 'Si'  ELSE 'No' END ELSE 'No' END  AplicaOriginacion ,CASE @Incompleto WHEN 1 THEN   CASE @idVisita WHEN 2 THEN 'Si'  ELSE 'No' END ELSE 'No' END  AplicaFormalizacion, CASE @Incompleto WHEN 1 THEN   CASE @idVisita WHEN 3 THEN 'Si'  ELSE 'No' END ELSE 'No' END  AplicaPreautorizacion, CASE @idVisita WHEN 2 THEN CASE WHEN @Incompleto=0 THEN 'Si' ELSE 'No' END  ELSE 'No' END  AplicaFormatoFormalizacion,CASE @idVisita WHEN 3 THEN CASE WHEN @Incompleto=0 THEN 'Si' ELSE 'No' END   ELSE 'No' END  AplicaFormatoPreautorizacion  FROM (
SELECT 
		rd.NombreCampo AS Titulo
		,cr2.Valor
	FROM RelacionDocumento rd
	LEFT JOIN (
		SELECT cr.Nombre
			,r.idCampo
			,o.idVisita
			,r.Valor
			,o.idOrden
			,o.num_Cred
			,o.Estatus
		FROM CamposRespuesta cr
		LEFT JOIN Respuestas r ON r.idCampo = cr.idCampo
		LEFT JOIN Ordenes o ON o.idOrden = r.idOrden
		WHERE o.idOrden = @idOrden
		) cr2 ON cr2.Nombre = rd.NombreCampo
		) cr3 pivot ( MAX(cr3.Valor) for Titulo  in ( [FotoActNacimiento], [AvisoRetencion], [DocAcuRecTarjeta], [DocAvRetencion], [DocCarContrato], [DocConAperCredito], [DocContrato], [DocSolInscripcion], [FotoAutBuro], [FotoAcuRecTarjeta], [FotoCarContrato], [FotoCompDomicilio], [FotoConAperCredito], [FotoContrato], [FotoIdOficAnverso], [FotoIdOficReverso], [FotoSolInscripcion], [SolicitudFormalCredito]) ) pivOrig

	)  originacion ON originacion.CV_CREDITO2=c.CV_CREDITO
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
