
GO
/****** Object:  StoredProcedure [dbo].[ObtenerCreditosNoAsignados]    Script Date: 13/02/2015 01:30:23 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Alberto Rojas
* Fecha de creación:	09/07/2014
* Descripción:			obtiene los creditos que estan sin asignar
Modificó: Maximiliano Silva
Fecha Modificación: 29/08/2014
Modificación: Manejo de estatus 11,12,15
Modificó: Alberto Rojas
Fecha Modificacion: 2015/02/13
Modificacion: se agregan los estatus 17,27,37,47
*****************************************************************************/
ALTER PROCEDURE [dbo].[ObtenerCreditosNoAsignados] @usuarioPadre INT = 0
	,@idDominio INT = 0,@tipoFormulario varchar(10)
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
			,l.IM_SALDO saldo
			,l.NU_MESES_RECUPERAR mesesRecuperar
			,l.IM_MONTO_RECUPERAR montoRecuperar
			,l.IM_PAGO_MINIMO pagoMinimo
			,l.IM_PAGO_RECOMENDADO pagoRecomendado
			,l.IM_PAGO_TOPE pagoTope
			,l.TX_CALLE calle
			,l.TX_COLONIA colonia
			,l.TX_MUNICIPIO municipio
			,l.CV_CODIGO_POSTAL cp
			,l.NU_TELEFONO_CASA telefonoCasa
			,l.NU_TELEFONO_CELULAR telefonoCelular
			,cd.Descripcion delegacion
			,l.TX_NOMBRE_DESPACHO nom_corto
			,l.TX_PAGO_1MES pago_1mes
			,l.TX_PAGO_2MESES pago_2mes
			,l.TX_PAGO_3MESES pago_3mes
			,l.TX_PAGO_4MESES pago_4mes
			,0 idOrden
			,0 idUsuario
			,0 Estatus
			,0 idVisita
			,'' auxiliar
			,(select top 1 Descripcion from Formulario where Ruta=l.CV_RUTA and idaplicacion=(SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion') and Estatus=1) as TIPO_FORMULARIO
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
					,17
					,27
					,37
					,47
					,3
					,4
					,5
					,6
					)
				AND idDominio = @idDominio
			) ordenes ON l.CV_CREDITO = ordenes.num_cred
		WHERE ordenes.num_cred IS NULL
			AND l.TX_NOMBRE_DESPACHO = CASE @idDominio
				WHEN 1
					THEN l.TX_NOMBRE_DESPACHO
				ELSE @nombreCorto
				END
			AND  l.CV_RUTA=@tipoFormulario
	END
	ELSE
	BEGIN
		SELECT l.CV_CREDITO num_cred
			,l.CV_ETIQUETA desc_etiq
			,l.TX_SOLUCIONES soluciones
			,l.TX_NOMBRE_ACREDITADO nombre
			,l.IM_SALDO saldo
			,l.NU_MESES_RECUPERAR mesesRecuperar
			,l.IM_MONTO_RECUPERAR montoRecuperar
			,l.IM_PAGO_MINIMO pagoMinimo
			,l.IM_PAGO_RECOMENDADO pagoRecomendado
			,l.IM_PAGO_TOPE pagoTope
			,l.TX_CALLE calle
			,l.TX_COLONIA colonia
			,l.TX_MUNICIPIO municipio
			,l.CV_CODIGO_POSTAL cp
			,l.NU_TELEFONO_CASA telefonoCasa
			,l.NU_TELEFONO_CELULAR telefonoCelular
			,cd.Descripcion delegacion
			,l.TX_NOMBRE_DESPACHO nom_corto
			,l.TX_PAGO_1MES pago_1mes
			,l.TX_PAGO_2MESES pago_2mes
			,l.TX_PAGO_3MESES pago_3mes
			,l.TX_PAGO_4MESES pago_4mes
			,ordenes.idOrden idOrden
			,ordenes.idUsuario
			,ordenes.Estatus
			,ordenes.idVisita
			,ordenes.Auxiliar auxiliar
			,(select top 1 Descripcion from Formulario where Ruta=l.CV_RUTA and idaplicacion=(SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion') and Estatus=1) as TIPO_FORMULARIO
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
			WHERE Estatus IN (
					1
					,11
					,17
					)
				AND idUsuarioPadre = @usuarioPadre
				AND idUsuario = 0
			) ordenes ON l.CV_CREDITO = ordenes.num_cred
		WHERE ordenes.idDominio = @idDominio
			AND l.TX_NOMBRE_DESPACHO = @nombreCorto
			AND l.CV_RUTA=@tipoFormulario
	END
END

