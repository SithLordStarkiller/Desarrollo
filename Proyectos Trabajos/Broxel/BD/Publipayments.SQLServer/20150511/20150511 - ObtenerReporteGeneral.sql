
/****** Object:  StoredProcedure [dbo].[ObtenerReporteGeneral]    Script Date: 11/05/2015 08:13:08 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Modificó: MJNS 20150420
ALTER PROCEDURE [dbo].[ObtenerReporteGeneral] (@idUsuarioPadre INT = 0)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idDominio INT = 0
		,@permitirReporte INT = - 1
		,@fechaExpiro INT = - 1

	SELECT @idDominio = idDominio
	FROM Usuario WITH (NOLOCK)
	WHERE idUsuario = @idUsuarioPadre

	--IF (@idDominio > 0)
	--BEGIN
	--	RETURN NULL
	--END

	SELECT @permitirReporte = count(idReporte)
	FROM [dbo].[Reportes]
	WHERE Tipo = 1
		AND idPadre = @idUsuarioPadre
		AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 

	SELECT @fechaExpiro = count(idReporte)
	FROM [dbo].[Reportes]
	WHERE Tipo = 1
		AND idPadre = @idUsuarioPadre
		AND CONVERT(VARCHAR(20), Fecha, 120) > CONVERT(VARCHAR(20), CAST(getdate() - .0822 AS DATETIME), 121)
		AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 

	IF (@fechaExpiro > 0)
	BEGIN
		RETURN NULL
	END

	IF (@permitirReporte < 1)
	BEGIN
		INSERT INTO [dbo].[Reportes] (
			[Tipo]
			,[idPadre]
			,[Estatus]
			,[Fecha]
			,[ReporteTxt]
			)
		VALUES (
			1
			,@idUsuarioPadre
			,'2'
			,GETDATE()
			,''
			)
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Reportes]
		SET [Estatus] = '2'
		WHERE Tipo = 1
			AND idPadre = @idUsuarioPadre
			AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 
	END

SELECT *
FROM (
	SELECT l.CV_CREDITO num_cred
		,o.idOrden idOrden
		,o.idUsuario
		,u.Usuario GestorOrd
		,o.idVisita
		,cat.Estado AS Estatus
		,cat.Codigo AS EstatusCodigo
		,ISNULL(Dictamen.Valor, 'Sin dictamen') AS Dictamen
		,o.FechaModificacion
		,o.FechaEnvio
		,CASE o.estatus
			WHEN 3
			THEN
				o.FechaRecepcion	
			WHEN 4
			THEN
				o.FechaRecepcion	
			ELSE
			NULL
		END as FechaRecepcion
		,l.TX_NOMBRE_ACREDITADO nombreAcreditado
		,l.NU_TELEFONO_CASA telefonoCasa
		,l.NU_TELEFONO_CELULAR telefonoCelular
		,l.TX_MUNICIPIO municipio
		,cd.Descripcion delegacion
		,cd.Descripcion estado
		,l.CV_CODIGO_POSTAL cp
		,l.TX_COLONIA colonia
		,l.TX_CALLE calle
		,l.CV_ETIQUETA desc_etiq
		,ISNULL(TX_DESCRIPCION_ETIQUETA, 'No encontrada') AS Etiqueta
		,l.TX_SOLUCIONES soluciones
		,l.CV_DESPACHO nom_corto
		,o.FechaAlta
		,o.Auxiliar
		,l.IM_SALDO saldo
		,l.NU_MESES_RECUPERAR mesesRecuperar
		,l.IM_MONTO_RECUPERAR montoRecuperar
		,l.IM_PAGO_MINIMO pagoMinimo
		,l.IM_PAGO_RECOMENDADO pagoRecomendado
		,l.IM_PAGO_TOPE pagoTope
		,l.TX_PAGO_1MES pago_1mes
		,l.TX_PAGO_2MESES pago_2mes
		,l.TX_PAGO_3MESES pago_3mes
		,l.TX_PAGO_4MESES pago_4mes
				,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN t.ppagoMensualAct
			WHEN  t.DictamenpromdepagoTOM IS NOT NULL   
					THEN  l.IM_MONTO_RECUPERAR
			WHEN  t.Dictamenliquida IS NOT NULL   
					THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
			WHEN  t.DictamenBCN IS NOT NULL 
					THEN l.IM_PAGO_MENSUAL_SIN_SEG_DAN
			WHEN  t.DictamenDCP IS NOT NULL  
					THEN
						CASE
							WHEN 
								t.factorSinFee = ''
								THEN
									null	
								ELSE
									 t.factorSinFee
							END
			WHEN  t.DictamenSTM IS NOT NULL  OR t.DictamenSiAceptaSTM IS NOT NULL  
					THEN 	
							CASE t.IM_OPC_SELEC
						WHEN 'IM_OPC1_STM'
							THEN l.IM_OPC1_STM 
						WHEN 'IM_OPC2_STM'
							THEN l.IM_OPC2_STM
						WHEN  'IM_OPC3_STM'
							THEN l.IM_OPC3_STM
						WHEN  'IM_OPC4_STM'
							THEN l.IM_OPC4_STM
						ELSE
								l.IM_OPC1_STM 
							END				
			END AS IM_MONTO_MENSUALIDAD_PESOS
			  ,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN   t.montoPromesa
				WHEN  t.DICTAMENPROMDEPAGOTOM IS NOT NULL   
					THEN   'TOM'
				WHEN  t.Dictamenliquida IS NOT NULL   
					THEN   'LIQ'
				WHEN  t.DictamenBCN IS NOT NULL 
					THEN 'BCN'
				WHEN  t.DictamenDCP IS NOT NULL 
					THEN 'DCP'		
				WHEN  t.DictamenSTM IS NOT NULL  OR t.DictamenSiAceptaSTM IS NOT NULL 
					THEN	CASE t.IM_OPC_SELEC
								WHEN 'IM_OPC1_STM'
									THEN 'STM1'
								WHEN 'IM_OPC2_STM'
									THEN 'STM2'
								WHEN  'IM_OPC3_STM'
									THEN 'STM3'
								WHEN  'IM_OPC4_STM'
								THEN 'STM4'
								else
								'STM1'
							END				
			END AS CV_PRODUCTO_CONVENIO
		,f.Descripcion AS TIPO_FORMULARIO
		,t.*
	FROM (
		SELECT idOrden idOrden2
			,ISNULL(Fecha, CONVERT(DATETIME, '3000-01-01')) Fecha
			,InitialDate
			,FinalDate
			,FechaRecepcion FechaRecepcion2
			,FechaModificacion FechaModificacion2
			,AssignedTo GestorResp
			,calle calle2
			,colonia colonia2
			,municipio municipio2
			,cp cp2
			,estado estado2
			,NU_TELEFONO_CASA_ACT
			,NU_TELEFONO_CELULAR_ACT
			,correcElectronicoEstadoCuenta
			,TX_CORREO_ELECTRONICO_ACT
			,FH_NACIMIENTO
			,promPago
			,FH_PROMESA_PAGO
			,aceptaBCN
			,ISNULL(ppagoMensualAct, ppagoOmisosAct) ppagoMensualAct
			,AgTelefonoT1
			,AgTelefonoT2
			,TX_EDIFICIO_ACT
			,TX_MUNICIPIO_ACT
			,TX_COLONIA_ACT
			,TX_ESTADO_ACT
			,CV_CODIGO_POSTAL_ACT
			,TX_ENTRE_CALLE1_ACT
			,TX_ENTRE_CALLE2_ACT
			,comentario_final
			,DICTAMENPROMDEPAGO
			,DictamenpromdepagoTOM
			,DictamenBCN
			,DictamenSTM
			,DictamenSiAceptaSTM
			,DictamenDCP
			,factorSinFee
			,montoPromesa
			,IM_OPC_SELEC
			,Dictamenliquida
		FROM (
			SELECT r.idOrden
				,c.Nombre
				,r.Valor
				,r.Fecha
			FROM [CamposRespuesta] c WITH (NOLOCK)
			LEFT JOIN (
				SELECT  br.idOrden
				,br.idCampo
				,br.Valor
				,MAX(br.Fecha) AS Fecha
			FROM BitacoraRespuestas br WITH (NOLOCK)
			WHERE br.idDominio = CASE 
					WHEN (@idDominio > 1)
						THEN @idDominio
					ELSE br.idDominio
					END
			GROUP BY br.idOrden
				,br.idCampo
				,br.Valor
						
				UNION ALL
				
				SELECT  rp.idOrden
				,rp.idCampo
				,rp.Valor
				,NULL AS Fecha
			FROM Respuestas rp WITH (NOLOCK)
			WHERE rp.idDominio = CASE 
					WHEN (@idDominio > 1)
						THEN @idDominio
					ELSE rp.idDominio
					END
				) r ON c.idCampo = r.idCampo
			) d
		PIVOT(MAX(Valor) FOR Nombre IN (
					InitialDate
					,FinalDate
					,FechaRecepcion
					,FechaModificacion
					,AssignedTo
					,calle
					,colonia
					,municipio
					,cp
					,estado
					,NU_TELEFONO_CASA_ACT
					,NU_TELEFONO_CELULAR_ACT
					,correcElectronicoEstadoCuenta
					,TX_CORREO_ELECTRONICO_ACT
					,FH_NACIMIENTO
					,promPago
					,FH_PROMESA_PAGO
					,aceptaBCN
					,ppagoMensualAct
					,ppagoOmisosAct
					,AgTelefonoT1
					,AgTelefonoT2
					,TX_EDIFICIO_ACT
					,TX_MUNICIPIO_ACT
					,TX_COLONIA_ACT
					,TX_ESTADO_ACT
					,CV_CODIGO_POSTAL_ACT
					,TX_ENTRE_CALLE1_ACT
					,TX_ENTRE_CALLE2_ACT
					,comentario_final
					,DICTAMENPROMDEPAGO
					,DictamenpromdepagoTOM
					,DictamenBCN
					,DictamenSTM
					,DictamenSiAceptaSTM
					,DictamenDCP
					,factorSinFee
					,montoPromesa
					,IM_OPC_SELEC
					,Dictamenliquida
					)) piv
		WHERE idOrden > 0
		) t
	INNER JOIN dbo.Ordenes o WITH (NOLOCK) ON t.idOrden2 = o.idOrden
	INNER JOIN dbo.CatEstatusOrdenes cat WITH (NOLOCK) ON o.Estatus = cat.Codigo
	INNER JOIN dbo.Creditos l WITH (NOLOCK) ON o.num_Cred = l.CV_CREDITO
	INNER JOIN dbo.Usuario u WITH (NOLOCK) ON o.idUsuario = u.idUsuario
	INNER JOIN dbo.Formulario f WITH (NOLOCK) ON l.CV_RUTA=f.Ruta
	INNER JOIN CatDelegaciones cd WITH (NOLOCK) ON l.CV_DELEGACION = cd.Delegacion
	LEFT JOIN (
		SELECT DISTINCT r.idOrden
			,r.Valor
			,r.Fecha
		FROM (
			SELECT idOrden
				,idCampo
				,Valor
				,CONVERT(DATETIME, '3000-01-01') Fecha
			FROM Respuestas WITH (NOLOCK)
			WHERE idUsuarioPadre = @idUsuarioPadre
			
			UNION
			
			SELECT idOrden
				,idCampo
				,Valor
				,Fecha
			FROM BitacoraRespuestas WITH (NOLOCK)
			WHERE idUsuarioPadre = @idUsuarioPadre
			) r
		INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
		WHERE c.Nombre LIKE 'dictamen%'
		) Dictamen ON Dictamen.idOrden = t.idOrden2
		AND Dictamen.Fecha = t.Fecha
	LEFT JOIN (
		SELECT DISTINCT r.idOrden
			,r.Valor
		FROM (
			SELECT idOrden
				,idCampo
				,Valor
			FROM Respuestas
			WHERE Respuestas.idOrden IN (
					SELECT Ordenes.idOrden
					FROM Ordenes
					)
			) r
		INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo
		WHERE c.idCampo in (select idCampo from CamposRespuesta where Nombre='montoPromesa' and idFormulario in (select idFormulario from formulario where idaplicacion=(SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion') and Estatus=1 and Captura=1))
		) montoPromesa ON montoPromesa.idOrden = o.idOrden
	LEFT JOIN CatEtiqueta etiq WITH (NOLOCK) ON l.CV_ETIQUETA = etiq.CV_ETIQUETA
	WHERE o.idUsuarioPadre = CASE @idUsuarioPadre
			WHEN 0
				THEN o.idUsuarioPadre
			ELSE @idUsuarioPadre
			END
			AND f.idAplicacion=(SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion') AND f.Estatus=1 AND f.Captura=1
	--ORDER BY o.num_Cred,o.idVisita
	
	UNION
	
	SELECT l.CV_CREDITO num_cred
		,ordenes.idOrden idOrden
		,ordenes.idUsuario
		,u.Usuario GestorOrd
		,ordenes.idVisita
		,cat.Estado AS Estatus
		,cat.Codigo AS EstatusCodigo
		,NULL Dictamen
		,ordenes.FechaModificacion
		,ordenes.FechaEnvio
		,CASE ordenes.estatus
			WHEN 3
			THEN
				ordenes.FechaRecepcion	
			WHEN 4
			THEN
				ordenes.FechaRecepcion	
			ELSE
			NULL
		END as FechaRecepcion
		,l.TX_NOMBRE_ACREDITADO nombreAcreditado
		,l.NU_TELEFONO_CASA telefonoCasa
		,l.NU_TELEFONO_CELULAR telefonoCelular
		,l.TX_MUNICIPIO municipio
		,cd.Descripcion delegacion
		,cd.Descripcion estado
		,l.CV_CODIGO_POSTAL cp
		,l.TX_COLONIA colonia
		,l.TX_CALLE calle
		,l.CV_ETIQUETA desc_etiq
		,ISNULL(TX_DESCRIPCION_ETIQUETA, 'No encontrada') AS Etiqueta
		,l.TX_SOLUCIONES soluciones
		,l.CV_DESPACHO nom_corto
		,ordenes.FechaAlta
		,ordenes.Auxiliar
		,l.IM_SALDO saldo
		,l.NU_MESES_RECUPERAR mesesRecuperar
		,l.IM_MONTO_RECUPERAR montoRecuperar
		,l.IM_PAGO_MINIMO pagoMinimo
		,l.IM_PAGO_RECOMENDADO pagoRecomendado
		,l.IM_PAGO_TOPE pagoTope
		,l.TX_PAGO_1MES pago_1mes
		,l.TX_PAGO_2MESES pago_2mes
		,l.TX_PAGO_3MESES pago_3mes
		,l.TX_PAGO_4MESES pago_4mes
		,NULL IM_MONTO_MENSUALIDAD_PESOS
		,NULL CV_PRODUCTO_CONVENIO
		,f.Descripcion AS TIPO_FORMULARIO
		,ordenes.idOrden idOrden2
		,CONVERT(DATETIME, '3000-01-01') Fecha
		,NULL InitialDate
		,NULL FinalDate
		,NULL FechaRecepcion2
		,NULL FechaModificacion2
		,NULL GestorResp
		,NULL calle2
		,NULL colonia2
		,NULL municipio2
		,NULL cp2
		,NULL estado2
		,NULL NU_TELEFONO_CASA_ACT
		,NULL NU_TELEFONO_CELULAR_ACT
		,NULL correcElectronicoEstadoCuenta
		,NULL TX_CORREO_ELECTRONICO_ACT
		,NULL FH_NACIMIENTO
		,NULL promPago
		,NULL FH_PROMESA_PAGO
		,NULL aceptaBCN
		,NULL ppagoMensualAct
		,NULL AgTelefonoT1
		,NULL AgTelefonoT2
		,NULL TX_EDIFICIO_ACT
		,NULL TX_MUNICIPIO_ACT
		,NULL TX_COLONIA_ACT
		,NULL TX_ESTADO_ACT
		,NULL CV_CODIGO_POSTAL_ACT
		,NULL TX_ENTRE_CALLE1_ACT
		,NULL TX_ENTRE_CALLE2_ACT
		,NULL comentario_final
		,NULL DICTAMENPROMDEPAGO
		,NULL DictamenpromdepagoTOM
		,NULL DictamenBCN
		,NULL DictamenSTM
		,NULL DictamenSiAceptaSTM
		,NULL DictamenDCP
		,NULL Res_factorDCP
		,NULL montoPromesa
		,NULL IM_OPC_SELEC
		,NULL Dictamenliquida
	FROM Creditos l
	INNER JOIN CatDelegaciones cd WITH (NOLOCK) ON l.CV_DELEGACION = cd.Delegacion
	INNER JOIN (
		SELECT o.*
		FROM [Ordenes] o
		INNER JOIN (
			SELECT num_Cred
				,MAX(FechaAlta) AS MaxDateTime
			FROM [Ordenes] WITH (NOLOCK)
			GROUP BY num_Cred
			) g ON o.num_Cred = g.num_Cred
			AND o.FechaAlta = g.MaxDateTime
		WHERE Estatus IN (
				1
				,11
				,6
				)
			AND idUsuarioPadre = @idUsuarioPadre
		) ordenes ON l.CV_CREDITO = ordenes.num_cred
	INNER JOIN dbo.CatEstatusOrdenes cat WITH (NOLOCK) ON ordenes.Estatus = cat.Codigo
	LEFT JOIN CatEtiqueta etiq WITH (NOLOCK) ON l.CV_ETIQUETA = etiq.CV_ETIQUETA
	INNER JOIN dbo.Usuario u WITH (NOLOCK) ON ordenes.idUsuario = u.idUsuario
	INNER JOIN dbo.Formulario f WITH (NOLOCK) ON l.CV_RUTA=f.Ruta
	WHERE ordenes.idDominio = @idDominio AND f.idAplicacion=(SELECT valor FROM CatalogoGeneral WHERE Llave = 'idAplicacion') AND f.Estatus=1 AND f.Captura=1 ) reporte
ORDER BY reporte.num_cred,fecha desc
END
