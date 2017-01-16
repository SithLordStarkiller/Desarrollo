
/****** Object:  StoredProcedure [dbo].[ObtenerOrdenes]    Script Date: 11/05/2016 12:55:03 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Maximiliano Silva
* Fecha de creación:	24/03/2014
* Descripción:			Muesta las ordenes asignadas, canceladas y respondidas
*		@Tipo:
*			0 = Muestra todas las ordenes creadas
*			1 = Muestra las ordenes asignadas a usuarios
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerOrdenes] (
	@Tipo INT = 0
	,@idUsuarioPadre INT = 0
	,@num_Cred VARCHAR(20) = ''
	,@idUsuario INT = 0
	,@TipoFormulario VARCHAR(10) = ''
	)
AS
BEGIN
	SET NOCOUNT ON;

	IF (@Tipo = 1) --Solo para Supervisor - Obtiene todas las ordenes asignadas a alguno de sus gestores
	BEGIN
		SELECT o.idOrden
			,o.idUsuario
			,u.Usuario NombreUsuario
			,o.idUsuarioPadre
			,up.Usuario NombreUsuarioPadre
			,l.CV_CREDITO num_cred
			,RTRIM(CONVERT(VARCHAR(2),o.Estatus)+o.Tipo) EstatusExtra
			,o.Estatus
			,o.idVisita
			,l.CV_ETIQUETA desc_etiq
			,l.TX_SOLUCIONES soluciones
			,l.TX_NOMBRE_ACREDITADO nombre
			,l.TX_CALLE calle
			,l.TX_COLONIA colonia
			,l.TX_MUNICIPIO municipio
			,l.CV_CODIGO_POSTAL cp
			,cd.Descripcion estado
			,l.CV_DESPACHO nom_corto
			,l.TX_PAGO_1MES pago_1mes
			,l.TX_PAGO_2MESES pago_2mes
			,l.TX_PAGO_3MESES pago_3mes
			,l.TX_PAGO_4MESES pago_4mes
			,l.CV_CANAL
			,o.Auxiliar auxiliar
		FROM CREDITOS l
		INNER JOIN Ordenes o ON l.CV_CREDITO = o.num_cred
		INNER JOIN Usuario up ON o.idUsuarioPadre = up.idUsuario
		INNER JOIN Usuario u ON o.idUsuario = u.idUsuario
		INNER JOIN CatDelegaciones cd ON l.CV_DELEGACION = cd.Delegacion
		WHERE o.idUsuarioPadre = CASE @idUsuarioPadre
				WHEN 0
					THEN o.idUsuarioPadre
				ELSE @idUsuarioPadre
				END
			AND o.idUsuario = CASE @idUsuario
				WHEN 0
					THEN o.idUsuario
				ELSE @idUsuario
				END
			AND o.idUsuario > 0
			AND o.num_Cred LIKE '%' + @num_Cred + '%'
			AND o.Estatus NOT IN (3,4)
			AND o.Estatus NOT IN ( 
			CASE @idUsuario
				WHEN 0
					THEN 3
				ELSE 6
				END
			)
			AND l.CV_RUTA=@TipoFormulario
		ORDER BY idOrden DESC
	END
	ELSE IF (@Tipo = 2) --Solo Administrador Despacho - Obtiene todas las ordenes asignadas a sus supervisores
	BEGIN
		SELECT o.idOrden
			,o.idUsuario
			,up.Usuario NombreUsuario
			,o.idUsuarioPadre
			,up.Usuario NombreUsuarioPadre
			,l.CV_CREDITO num_cred
			,RTRIM(CONVERT(VARCHAR(2),o.Estatus)+o.Tipo) EstatusExtra
			,o.Estatus
			,o.idVisita
			,l.CV_ETIQUETA desc_etiq
			,l.TX_SOLUCIONES soluciones
			,l.TX_NOMBRE_ACREDITADO nombre
			,l.TX_CALLE calle
			,l.TX_COLONIA colonia
			,l.TX_MUNICIPIO municipio
			,l.CV_CODIGO_POSTAL cp
			,cd.Descripcion estado
			,l.CV_DESPACHO nom_corto
			,l.TX_PAGO_1MES pago_1mes
			,l.TX_PAGO_2MESES pago_2mes
			,l.TX_PAGO_3MESES pago_3mes
			,l.TX_PAGO_4MESES pago_4mes
			,l.CV_CANAL
			,o.Auxiliar auxiliar
		FROM Creditos l
		INNER JOIN Ordenes o ON l.CV_CREDITO = o.num_cred
		INNER JOIN RelacionUsuarios r ON r.idPadre = @idUsuarioPadre
		AND o.idUsuarioPadre = r.idHijo
		INNER JOIN Usuario up ON o.idUsuarioPadre = up.idUsuario
		INNER JOIN Usuario u ON o.idUsuario = u.idUsuario
		INNER JOIN CatDelegaciones cd ON l.CV_DELEGACION = cd.Delegacion
		WHERE o.idUsuario = 0
			AND o.num_Cred LIKE '%' + @num_Cred + '%'
			AND O.Estatus NOT IN (3,4)
			AND l.CV_RUTA=@TipoFormulario
		ORDER BY idOrden DESC
	END
	
END


