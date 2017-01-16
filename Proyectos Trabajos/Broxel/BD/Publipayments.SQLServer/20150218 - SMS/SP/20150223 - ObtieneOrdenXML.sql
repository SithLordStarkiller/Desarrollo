
/****** Object:  StoredProcedure [dbo].[ObtieneOrdenXML]    Script Date: 02/23/2015 11:23:35 ******/
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

	IF (ISNULL(@Credito, '') != '')
	BEGIN
		SELECT @idVisita = [idVisita]
		FROM Ordenes WITH(NOLOCK)
		WHERE [num_Cred] = @Credito
			AND idOrden = @idOrden
	END
	ELSE
	BEGIN
		SELECT @idVisita = [idVisita]
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
	FROM AutorizacionSMS WITH(NOLOCK)
	WHERE idOrden = @idOrden

	SELECT @DicGestAnt = CASE 
			WHEN @idVisita = 1
				THEN ''
			ELSE ISNULL(br.Valor, '')
			END
	FROM BitacoraRespuestas br WITH(NOLOCK)
	LEFT JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = br.idCampo
	WHERE idOrden = @idOrden
	and cr.Nombre like 'Dictamen%'
	and br.Fecha = (
	select max(Fecha) from BitacoraRespuestas WITH(NOLOCK) where idOrden=@idOrden)

	SELECT TOP 1 @idOrden idOrden
		,@usuario Usuario
		,c.*
		,@idVisita idVisita
		,d.Descripcion TX_DELEGACION
		,f.Version
		,f.Nombre AS tipoFormulario
		,@Tipo Tipo
		,@TelefonoSMS AS CelularSMS_Recibido
		,@DicGestAnt DicGest_Ant
		,ISNULL(e.TX_DESCRIPCION_ETIQUETA, 'No encontrada')
	FROM dbo.CREDITOS c WITH(NOLOCK)
	INNER JOIN CatDelegaciones d WITH(NOLOCK) ON c.CV_DELEGACION = d.Delegacion
	INNER JOIN Formulario f  WITH(NOLOCK) ON f.Ruta = c.CV_RUTA
	LEFT JOIN dbo.CatEtiqueta e  WITH(NOLOCK) ON c.CV_ETIQUETA = e.CV_ETIQUETA
	WHERE CV_CREDITO = @Credito
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
