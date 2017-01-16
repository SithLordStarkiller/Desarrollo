/****** Object:  StoredProcedure [dbo].[ObtenerRespuestasBitacora]    Script Date: 09/24/2015 13:51:56 ******/
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
* Fecha Modificación: 17/02/2015
* Descripción: Se agrega el anexo de tipo, para identificar si el formulario tiene alguna variacion ej SMS
* 2015/04/24 - Modificacion se agrego filtro solo estatus 3,4 se cambio un formateo en funcion case , se cambia Res_factorDCP por factorSinFee para emparejar con resporte a London
* Modificó: Pablo Jaimes
* Fecha Modificación: 24/09/2015
* Descripción: Se agrega tipo Call Center
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
		,
		CASE 
			WHEN o.Tipo='S'
			THEN cat.Estado+' SMS'
			WHEN o.Tipo='C'
			THEN cat.Estado+' CC'
			WHEN o.Tipo='CS'
			THEN cat.Estado+' SMS CC'
 			ELSE cat.Estado 
			END
		AS Estatus
		,RTRIM(CONVERT(VARCHAR(2),cat.Codigo)+o.Tipo) AS EstatusCodigo
		,f.Descripcion as TIPO_FORMULARIO
		,o.idVisita
		,o.FechaAlta
		,o.num_Cred
		,o.FechaModificacion
		,o.FechaEnvio
		,o.FechaRecepcion
		,CASE 
			WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN 	
							CASE t.montoPromesa
						WHEN 'MEN'
							THEN l.IM_PAGO_MENSUAL
						WHEN 'TOM'
							THEN l.IM_MONTO_RECUPERAR
						WHEN  'LIQ'
							THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
							END
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
						t.IM_PRIMER_PAGO_SELEC
					  END				
			 AS IM_MONTO_MENSUALIDAD_PESOS
			,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN   t.montoPromesa
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
		,ISNULL(Dictamen.Valor, 'Sin dictamen') AS Dictamen
		,ISNULL(TX_DESCRIPCION_ETIQUETA, 'No encontrada') AS Etiqueta
		,l.CV_CANAL
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
			,factorSinFee
			,montoPromesa
			,IM_OPC_SELEC
			,Dictamenliquida
			,IM_SELEC_STM_PAGO_SUBSEC
			,IM_PRIMER_PAGO_SELEC
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
					WHERE
					rp.idFormulario  in (select  f.idFormulario
											FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
											WHERE 
												 a.idAplicacion = (SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion')
												AND f.Ruta = CASE 
													WHEN ISNULL(@TipoFormulario, '') != ''
														THEN @TipoFormulario
													ELSE f.Ruta
													END)
							and rp.idUsuarioPadre=@idUsuarioPadre
						
				
				UNION ALL
			
					SELECT br.idOrden
						,br.idCampo
						,br.Valor
						,br.Fecha
						,idFormulario
					FROM BitacoraRespuestas br WITH (NOLOCK)
					WHERE
						 br.idFormulario  in (select  f.idFormulario
											FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
											WHERE 
												 a.idAplicacion = (SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion')
												AND f.Ruta = CASE 
													WHEN ISNULL(@TipoFormulario, '') != ''
														THEN @TipoFormulario
													ELSE f.Ruta
													END)
						AND br.idUsuarioPadre=@idUsuarioPadre
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
					,factorSinFee
					,montoPromesa
					,IM_OPC_SELEC
					,Dictamenliquida
					,IM_SELEC_STM_PAGO_SUBSEC
					,IM_PRIMER_PAGO_SELEC
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
			,r.idformulario
		FROM (
			SELECT idOrden
				,idCampo
				,Valor
				,idformulario
				,CONVERT(DATETIME, '3000-01-01') Fecha
			FROM Respuestas WITH (NOLOCK)
			WHERE idUsuarioPadre = @idUsuarioPadre
			UNION
			
			SELECT idOrden
				,idCampo
				,Valor
				,idformulario
				,Fecha
			FROM BitacoraRespuestas WITH (NOLOCK)
			WHERE idUsuarioPadre = @idUsuarioPadre		
		
			) r
		INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo
		WHERE c.Nombre LIKE 'dictamen%'
		) Dictamen ON Dictamen.idOrden = t.idOrden
		AND Dictamen.Fecha = t.Fecha
		inner join formulario f on f.idformulario=Dictamen.idformulario
	LEFT JOIN (
		SELECT DISTINCT r.idOrden
			,r.Valor
		FROM (
			SELECT idOrden
				,idCampo
				,Valor
				,idFormulario
			FROM Respuestas WITH (NOLOCK)
			WHERE Respuestas.idOrden IN (
					SELECT Ordenes.idOrden
					FROM Ordenes
					)
			) r
		INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo and r.idFormulario=c.idFormulario
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
			AND o.Estatus  in(3,4)
	ORDER BY o.num_Cred
		,o.idVisita
END
