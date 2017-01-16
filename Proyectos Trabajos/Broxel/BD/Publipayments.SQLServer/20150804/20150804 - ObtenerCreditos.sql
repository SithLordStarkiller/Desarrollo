
/****** Object:  StoredProcedure [dbo].[ObtenerCreditos]    Script Date: 04/08/2015 06:40:49 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*********************************************************************
Proyecto:				London-PubliPayments-Formiik
Autor:				Maximiliano Silva
Fecha de creación:	02/05/2014
Descripción:			Obtiene los creditos que no estan asignados
	Parametros:
		@num_cred 
		@nombre 
		@calle
		@colonia 
		@municipio 
		@cp 
		@estado
Modificó: Maximiliano Silva
Fecha Modificación: 29/08/2014
Modificación: Manejo de estatus 11,12,15
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerCreditos] (
	@num_cred NVARCHAR(20) = ''
	,@usuarioPadre INT = 0
	,@idDominio INT = 0
	,@TipoFormulario VARCHAR(10) = ''
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @nombreCorto VARCHAR(20) = '';

	SELECT @nombreCorto = nom_corto
	FROM Dominio
	WHERE idDominio = @idDominio

	IF (@usuarioPadre = 0)
	BEGIN
		SELECT l.CV_CREDITO num_cred
			,l.CV_ETIQUETA desc_etiq
			,l.TX_SOLUCIONES soluciones
			,l.TX_NOMBRE_ACREDITADO nombre
			,l.TX_CALLE calle
			,l.TX_COLONIA colonia
			,l.TX_MUNICIPIO municipio
			,l.CV_CODIGO_POSTAL cp
			,cd.Descripcion estado
			,l.TX_NOMBRE_DESPACHO nom_corto
			,0 idOrden
			,1 idVisita
			,l.TX_PAGO_1MES pago_1mes
			,l.TX_PAGO_2MESES pago_2mes
			,l.TX_PAGO_3MESES pago_3mes
			,l.TX_PAGO_4MESES pago_4mes
			,l.CV_CANAL
			,'' auxiliar
		FROM Creditos l
		INNER JOIN CatDelegaciones cd ON l.CV_DELEGACION = cd.Delegacion
		LEFT JOIN (
			SELECT o.*
			FROM [Ordenes] o
			INNER JOIN (
				SELECT num_Cred
					,MAX(FechaAlta) AS MaxDateTime
				FROM [Ordenes]
				GROUP BY num_Cred
				) g ON o.num_Cred = g.num_Cred
				AND o.FechaAlta = g.MaxDateTime
			WHERE Estatus IN (
					1
					,11
					,12
					,15
					,3
					,4
					,5
					,6
					)
				AND idDominio = @idDominio
			) ordenes ON l.CV_CREDITO = ordenes.num_cred
		WHERE ordenes.num_cred IS NULL
			AND l.CV_CREDITO LIKE '%' + @num_cred + '%'
			AND l.TX_NOMBRE_DESPACHO = CASE @idDominio
				WHEN 1
					THEN l.TX_NOMBRE_DESPACHO
				ELSE @nombreCorto
				END
			AND l.CV_RUTA=@TipoFormulario
	END
	ELSE
	BEGIN
		SELECT l.CV_CREDITO num_cred
			,l.CV_ETIQUETA desc_etiq
			,l.TX_SOLUCIONES soluciones
			,l.TX_NOMBRE_ACREDITADO nombre
			,l.TX_CALLE calle
			,l.TX_COLONIA colonia
			,l.TX_MUNICIPIO municipio
			,l.CV_CODIGO_POSTAL cp
			,cd.Descripcion estado
			,l.TX_NOMBRE_DESPACHO nom_corto
			,ordenes.idOrden idOrden
			,ordenes.idVisita idVisita
			,l.TX_PAGO_1MES pago_1mes
			,l.TX_PAGO_2MESES pago_2mes
			,l.TX_PAGO_3MESES pago_3mes
			,l.TX_PAGO_4MESES pago_4mes
			,l.CV_CANAL
			,ordenes.Auxiliar auxiliar
		FROM Creditos l
		INNER JOIN CatDelegaciones cd ON l.CV_DELEGACION = cd.Delegacion
		INNER JOIN (
			SELECT o.*
			FROM [Ordenes] o
			INNER JOIN (
				SELECT num_Cred
					,MAX(FechaAlta) AS MaxDateTime
				FROM [Ordenes]
				GROUP BY num_Cred
				) g ON o.num_Cred = g.num_Cred
				AND o.FechaAlta = g.MaxDateTime
			WHERE Estatus in (1, 12)
				AND idUsuarioPadre = @usuarioPadre
				AND idUsuario = 0
			) ordenes ON l.CV_CREDITO = ordenes.num_cred
		WHERE ordenes.idDominio = @idDominio
			AND l.CV_CREDITO LIKE '%' + @num_cred + '%'
			AND l.TX_NOMBRE_DESPACHO = @nombreCorto
			AND l.CV_RUTA=@TipoFormulario
	END
END

