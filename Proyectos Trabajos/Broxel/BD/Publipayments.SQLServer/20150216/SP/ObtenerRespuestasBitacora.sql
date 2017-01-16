
GO
/****** Object:  StoredProcedure [dbo].[ObtenerRespuestasBitacora]    Script Date: 15/02/2015 10:13:46 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autores:				Maximiliano Silva
						Alberto Ortiz
* Fecha de creación:	02/04/2014
* Descripción:			Obtiene las respuestas de las ordenes asignadas
	@tipo		Cantidad de campos, 0 = todos
	@idOrden	Numero de la orden o parte
	@num_Cred	Numero del credito
	@fechaAlta	Fecha de la asignacion
* Modificó: Maximiliano Silva
* Fecha Modificación: 18/08/2014
* Modificó: Alberto Rojas
* Fecha Modificación: 29/01/2015
* Descripción: Se agrega el filtro para obtener el reporte por el tipo de formulario
* Modificó: Alberto Rojas
* Fecha Modificación: 15/02/2015
* Descripción: Se agrega filtros para los nuevos estatus
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerRespuestasBitacora] (
	@tipo INT = 0
	,@idOrden VARCHAR(20) = ''
	,@reporte INT = 0
	,@idUsuarioPadre INT = 0
	,@tipoFormulario VARCHAR(10) = ''
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idDominio INT
	DECLARE @formularios nvarchar(max);

	SELECT @idDominio = idDominio
	FROM Usuario
	WHERE idUsuario = @idUsuarioPadre

			
	SELECT l.ID_ARCHIVO id_Carga
		,l.CV_CREDITO num_cred
		,l.CV_ETIQUETA desc_etiq
		,l.TX_SOLUCIONES soluciones
		,l.TX_NOMBRE_ACREDITADO nombre
		,l.TX_CALLE calle
		,l.TX_COLONIA colonia
		,l.TX_MUNICIPIO municipio
		,l.CV_CODIGO_POSTAL cp
		,cd.Descripcion estado
		,l.CV_DESPACHO nom_corto
		,u.Usuario
		,cat.Estado AS Estatus
		,cat.Codigo AS EstatusCodigo
		,(select top 1 Descripcion from Formulario where Ruta=l.CV_RUTA and idaplicacion=(SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion') and Estatus=1) as TIPO_FORMULARIO
		,o.idVisita
		,o.FechaAlta
		,o.num_Cred
		,o.FechaModificacion
		,o.FechaEnvio
		,o.FechaRecepcion
		,CASE 
			WHEN ISNUMERIC(ISNULL(t.DICTAMENPROMDEPAGO,1))<> 1  
					THEN 	
							CASE t.montoPromesa
						WHEN 'MEN'
							THEN l.IM_PAGO_MENSUAL
						WHEN 'TOM'
							THEN l.IM_MONTO_RECUPERAR
						WHEN  'LIQ'
							THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
							END
			WHEN ISNUMERIC(ISNULL(t.Dictamenliquida,1))<> 1  
					THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
			WHEN  ISNUMERIC((ISNULL(t.DictamenBCN,1)))<> 1
					THEN l.IM_PAGO_MENSUAL_SIN_SEG_DAN
			WHEN ISNUMERIC(ISNULL(t.DictamenDCP,1))<> 1  
					THEN t.Res_factorDCP
			WHEN ISNUMERIC(ISNULL(t.DictamenSTM,ISNULL(t.DictamenSiAceptaSTM,1)))<> 1  
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
						else
								l.IM_OPC1_STM 
							END				
			END AS IM_MONTO_MENSUALIDAD_PESOS
			,CASE 
				WHEN ISNUMERIC(ISNULL(t.DICTAMENPROMDEPAGO,1))<> 1  
					THEN   t.montoPromesa
				WHEN ISNUMERIC(ISNULL(t.Dictamenliquida,1))<> 1  
					THEN   'LIQ'
				WHEN  ISNUMERIC((ISNULL(t.DictamenBCN,1)))<> 1
					THEN 'BCN'
				WHEN ISNUMERIC(ISNULL(t.DictamenDCP,1))<> 1  
					THEN 'DCP'	
				WHEN ISNUMERIC(ISNULL(t.DictamenSTM,ISNULL(t.DictamenSiAceptaSTM,1)))<> 1  
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
		,ISNULL(Dictamen.Valor, 'Sin dictamen') AS Dictamen
		,ISNULL(TX_DESCRIPCION_ETIQUETA, 'No encontrada') AS Etiqueta
		,t.*
	FROM (
		SELECT idOrden
			,ISNULL(Fecha, CONVERT(DATETIME, '3000-01-01')) Fecha
			,InitialDate
			,FinalDate
			,FechaRecepcion
			,FechaModificacion
			,AssignedTo
			,TX_NOMBRE_ACREDITADO
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
			,DictamenBCN
			,DictamenSTM
			,DictamenSiAceptaSTM
			,DictamenDCP
			,Res_factorDCP
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
					SELECT rp.idOrden
						,rp.idCampo
						,rp.Valor
						,NULL AS Fecha
						,idFormulario
					FROM Respuestas rp WITH (NOLOCK)
					WHERE rp.idOrden IN (
							SELECT o.idOrden
							FROM Ordenes o WITH (NOLOCK)
							WHERE CONVERT(VARCHAR(20), o.FechaEnvio, 120) >= CONVERT(VARCHAR(10), CAST(DATEADD(mm, - 1, getdate()) AS DATETIME), 121)
								AND o.idDominio = CASE 
									WHEN (@idDominio > 1)
										THEN @idDominio
									ELSE o.idDominio
									END
								AND rp.idFormulario  in (select  f.idFormulario
											FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
											WHERE 
												 a.idAplicacion = (SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion')
												AND f.Ruta = CASE 
													WHEN ISNULL(@TipoFormulario, '') != ''
														THEN @TipoFormulario
													ELSE f.Ruta
													END)
							)
						
				
				UNION ALL
			
					SELECT br.idOrden
						,br.idCampo
						,br.Valor
						,br.Fecha
						,idFormulario
					FROM BitacoraRespuestas br WITH (NOLOCK)
					WHERE br.idOrden IN (
							SELECT o.idOrden
							FROM Ordenes o WITH (NOLOCK)
							WHERE CONVERT(VARCHAR(20), o.FechaEnvio, 120) >= CONVERT(VARCHAR(10), CAST(DATEADD(mm, - 1, getdate()) AS DATETIME), 121)
								AND o.idDominio = CASE 
									WHEN (@idDominio > 1)
										THEN @idDominio
									ELSE o.idDominio
									END
								AND br.idFormulario  in (select  f.idFormulario
											FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
											WHERE 
												 a.idAplicacion = (SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion')
												AND f.Ruta = CASE 
													WHEN ISNULL(@TipoFormulario, '') != ''
														THEN @TipoFormulario
													ELSE f.Ruta
													END)

							)
					
				) r ON c.idCampo = r.idCampo  and c.idFormulario=r.idFormulario
			) d
		PIVOT(MAX(Valor) FOR Nombre IN (
					InitialDate
					,FinalDate
					,FechaRecepcion
					,FechaModificacion
					,AssignedTo
					,TX_NOMBRE_ACREDITADO
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
					,DictamenBCN
					,DictamenSTM
					,DictamenSiAceptaSTM
					,DictamenDCP
					,Res_factorDCP
					,montoPromesa
					,IM_OPC_SELEC
					,Dictamenliquida
					)) piv
		WHERE idOrden > 0
		) t
	INNER JOIN dbo.Ordenes o WITH (NOLOCK) ON t.idOrden = o.idOrden
	INNER JOIN dbo.CatEstatusOrdenes cat WITH (NOLOCK) ON o.Estatus = cat.Codigo
	INNER JOIN dbo.Creditos l WITH (NOLOCK) ON o.num_Cred = l.CV_CREDITO
	INNER JOIN dbo.Usuario u WITH (NOLOCK) ON o.idUsuario = u.idUsuario
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
		) Dictamen ON Dictamen.idOrden = t.idOrden
		AND Dictamen.Fecha = t.Fecha
	LEFT JOIN (
		SELECT DISTINCT r.idOrden
			,r.Valor
		FROM (
			SELECT idOrden
				,idCampo
				,Valor
				,idFormulario
			FROM Respuestas
			WHERE Respuestas.idOrden IN (
					SELECT Ordenes.idOrden
					FROM Ordenes
					)
			) r
		INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo and r.idFormulario=c.idFormulario
		WHERE c.Nombre= 'montoPromesa' and r.idFormulario=c.idFormulario
		) montoPromesa ON montoPromesa.idOrden = o.idOrden
	LEFT JOIN CatEtiqueta etiq WITH (NOLOCK) ON l.CV_ETIQUETA = etiq.CV_ETIQUETA
	WHERE CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE CASE @reporte
			WHEN 0
				THEN '%%'
			ELSE '%' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + '%'
			END
		AND o.idUsuarioPadre = CASE @idUsuarioPadre
			WHEN 0
				THEN o.idUsuarioPadre
			ELSE @idUsuarioPadre
			END
		AND o.Estatus not in(6)
	 
	ORDER BY o.num_Cred
		,o.idVisita
END
