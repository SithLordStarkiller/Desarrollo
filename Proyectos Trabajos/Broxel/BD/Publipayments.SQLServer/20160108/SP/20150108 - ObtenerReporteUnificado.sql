
/****** Object:  StoredProcedure [dbo].[ObtenerReporteUnificado]    Script Date: 08/01/2016 04:49:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2014-10-07
-- Description:	obtiene informacion para crear archivo REPORTE_UNIFICADO.TXT para ser enviado a London
-- Fecha Modificacion:   01/02/2015
-- Modifico:				Alberto Rojas
-- Modificacion:			se agrega los filtros para no repetir los campos de camposRespuesta
-- Modificacion: 2015-04-13  JARO: Se incluyen los campos de celular SMS para reportar a London estos casos
-- 2015/04/24 - JARO  se cambio un formateo en funcion case 
-- 2015/07/15 - JARO  Se agrega un filtro para no tomar en cuenta la ruta CSP
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerReporteUnificado]
AS
BEGIN
	
	DECLARE @fecha VARCHAR(25) = '%' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + '%'
    DECLARE @RutaNA VARCHAR (10) ='vsmp';

	
SELECT CV_CREDITO,CV_DELEGACION,CV_PROVEEDOR,TX_NOMBRE_DESPACHO,CV_MOVIL,CV_RAMA,CV_ESTATUS_FINAL,TX_ESTATUS_FINAL,FH_PROMESA_PAGO,IM_PROMESA_PAGO,CV_APROBACION,FH_INICIO_GESTION,FH_FIN_GESTION,FH_CREACION,FH_CARGA,CV_DESPACHO,CV_RUTA,GP_DOMICILIO,IM_MONTO_MENSUALIDAD_PESOS,CV_PRODUCTO_CONVENIO,CV_TIPO_CONVENIO,NU_VISITA,FH_INFORMACION,TEL_CELULAR_SMS,TIPO_CELULAR_SMS,CORROBORO_CELULAR_SMS,FH_DCP as FECHA_CITA,Hora_DCP as HORA_CITA
	from
	(
	SELECT l.CV_CREDITO
			,l.CV_RUTA as RutaCredito
			,l.CV_DELEGACION
			,l.CV_PROVEEDOR
			,l.TX_NOMBRE_DESPACHO
			,t.AssignedTo AS CV_MOVIL
			,CASE 
				WHEN o.Estatus = 4
					THEN 'bmp'
				ELSE t.AssignedTo
				END AS CV_RAMA
			,ISNULL(Dictamen.Clave, '') AS CV_ESTATUS_FINAL
			,ISNULL(Dictamen.Valor, '') AS 	TX_ESTATUS_FINAL 
			,ISNULL(CONVERT(VARCHAR(10),CONVERT(datetime, t.FH_PROMESA_PAGO,121 ),121), CONVERT(VARCHAR(10),CONVERT(datetime, t.FH_PROMESA_PAGOC,121 ),121)) AS FH_PROMESA_PAGO
			,CASE t.montoPromesa
				WHEN 'MEN'
					THEN l.IM_PAGO_MENSUAL
				WHEN 'TOM'
					THEN l.IM_MONTO_RECUPERAR
				WHEN  'LIQ'
					THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
			END  AS IM_PROMESA_PAGO
			,CASE 
				WHEN o.Estatus=4
					THEN 'Valida aprobada'
				WHEN t.Fecha != '3000-01-01 00:00:00.000'
					THEN 'Valida no aprobada'	
				ELSE  'Valida'
			END AS CV_APROBACION
			, CONVERT(VARCHAR(10),CONVERT(datetime, t.FinalDate,121 ),121) + ' ' + CONVERT(VARCHAR(30),CONVERT(datetime, t.InitialDate,121 ),8) AS FH_INICIO_GESTION
			, CONVERT(VARCHAR(10),CONVERT(datetime, t.FinalDate,121 ),121) + ' ' + CONVERT(VARCHAR(30),CONVERT(datetime, t.FinalDate,121 ),8) AS FH_FIN_GESTION
			, CONVERT(VARCHAR(10),CONVERT(datetime, o.FechaEnvio,121 ),121) + ' ' + CONVERT(VARCHAR(30),CONVERT(datetime, o.FechaEnvio,121 ),8) AS FH_CREACION
			, CONVERT(VARCHAR(30),CONVERT(datetime, o.FechaRecepcion,121 ),100)  AS FH_CARGA
			,l.CV_DESPACHO
			,CASE 
				WHEN o.tipo like '%C%'
				THEN 
					'CC'
				ELSE
					'RDS'
				END AS CV_RUTA
			,SUBSTRING(t.gps_automatico,5,CHARINDEX('Lon:',t.gps_automatico)-5)+','+ SUBSTRING(t.gps_automatico,CHARINDEX('Lon:',t.gps_automatico)+4,10)+',0' AS GP_DOMICILIO
			,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN l.IM_PAGO_MENSUAL
			WHEN  t.DictamenpromdepagoTOM IS NOT NULL   
					THEN  l.IM_MONTO_RECUPERAR
			WHEN  t.Dictamenliquida IS NOT NULL   
					THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
			WHEN  t.DictamenBCN IS NOT NULL OR t.DictamenBCNPrivado  IS NOT NULL OR t.DictamenBCNJudicial IS NOT NULL 
					THEN l.IM_PAGO_MENSUAL_SIN_SEG_DAN
			WHEN  t.DictamenDCP IS NOT NULL OR DictamenDCPPrivado IS NOT NULL OR  DictamenDCPJudicial IS NOT NULL 
					THEN
						CASE
							WHEN 
								t.factorSinFee = ''
								THEN
									null	
								ELSE
									 t.factorSinFee
							END
			WHEN  t.DictamenSTM IS NOT NULL  OR t.DictamenSiAceptaSTM IS NOT NULL OR t.DictamenSTMPrivado IS NOT NULL OR t.DictamenSTMJudicial IS NOT NULL   
					THEN 	
							CASE t.IM_OPC_SELEC
						WHEN 'IM_OPC1_STM'
							THEN l.IM_OPC1_STM_PAGO_SUBSEC
						WHEN 'IM_OPC2_STM'
							THEN l.IM_OPC2_STM_PAGO_SUBSEC
						WHEN  'IM_OPC3_STM'
							THEN l.IM_OPC3_STM_PAGO_SUBSEC
						WHEN  'IM_OPC4_STM'
							THEN l.IM_OPC4_STM_PAGO_SUBSEC
							else
								l.IM_OPC1_STM_PAGO_SUBSEC 
							END				
					END				
			 AS IM_MONTO_MENSUALIDAD_PESOS
			  ,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN   t.montoPromesa
				WHEN  t.DICTAMENPROMDEPAGOTOM IS NOT NULL   
					THEN   'TOM'
				WHEN  t.Dictamenliquida IS NOT NULL   
					THEN   'LIQ'
				WHEN  t.DictamenBCN IS NOT NULL OR t.DictamenBCNPrivado  IS NOT NULL OR t.DictamenBCNJudicial IS NOT NULL 
					THEN 'BCN'
				WHEN  t.DictamenDCP IS NOT NULL OR DictamenDCPPrivado IS NOT NULL OR  DictamenDCPJudicial IS NOT NULL 
					THEN 'DCP'	
				WHEN  t.DictamenSTM IS NOT NULL  OR t.DictamenSiAceptaSTM IS NOT NULL OR t.DictamenSTMPrivado IS NOT NULL OR t.DictamenSTMJudicial IS NOT NULL
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
				WHEN  t.DictamenFPP IS NOT NULL  
					THEN	
						CASE 
							WHEN  t.AceptaFPP1 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP1,1)		
							WHEN  t.AceptaFPP2 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP2,1)
							WHEN  t.AceptaFPP3 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP3,1)
							WHEN  t.AceptaFPP4 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP4,1)
							WHEN  t.AceptaFPP5 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP5,1)
							WHEN  t.AceptaFPP6 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP6,1)
						END
			END AS CV_PRODUCTO_CONVENIO
			, CASE 
					WHEN t.DictamenBCNJudicial IS NOT NULL  OR t.DictamenSTMJudicial IS NOT NULL  OR t.DictamenDCPJudicial IS NOT NULL 
					THEN 'J'
					ELSE
						'P'
					END 
					 AS CV_TIPO_CONVENIO
			,o.idVisita AS NU_VISITA
			,CONVERT(varchar, GETDATE(),112) as FH_INFORMACION
			,celSMS.Valor as TEL_CELULAR_SMS
			,CelSMSProp.valor as TIPO_CELULAR_SMS
			,CASE o.tipo
				WHEN 'S'
				THEN 
					CASE o.estatus
						WHEN 4
							THEN 'Si'
						ELSE 'No'
					END
				ELSE
				NULL
				END as CORROBORO_CELULAR_SMS
				,t.FH_DCP
				,t.Hora_DCP
		FROM (
			SELECT idOrden
				,ISNULL(Fecha, CONVERT(DATETIME, '3000-01-01')) Fecha
				,InitialDate
				,FinalDate
				,ResponseDate
				,AssignedTo
				,FH_PROMESA_PAGOC
				,FH_PROMESA_PAGO
				,gps_automatico
				,DICTAMENPROMDEPAGO
				,DictamenpromdepagoTOM
				,DictamenBCN
				,DictamenBCNPrivado
				,DictamenBCNJudicial
				,DictamenSTM
				,DictamenSTMPrivado
				,DictamenSTMJudicial
				,DictamenSiAceptaSTM
				,DictamenDCP
				,DictamenDCPPrivado
				,DictamenDCPJudicial
				,factorSinFee
				,montoPromesa
				,IM_OPC_SELEC
				,Dictamenliquida
				,IM_SELEC_STM_PAGO_SUBSEC
				,DictamenFPP
				,AceptaFPP1
				,AceptaFPP2
				,AceptaFPP3
				,AceptaFPP4
				,AceptaFPP5
				,AceptaFPP6
				,FH_DCP
				,Hora_DCP
			FROM (
				SELECT r.idOrden
					,c.Nombre
					,r.Valor
					,r.Fecha
				FROM [CamposRespuesta] c WITH (NOLOCK)
				LEFT JOIN (
					(
						SELECT rp.idOrden
							,rp.idCampo
							,rp.Valor
							,NULL AS Fecha
							,idFormulario
						FROM Respuestas rp WITH (NOLOCK)
						WHERE rp.idOrden IN (
								SELECT o.idOrden
								FROM Ordenes o WITH (NOLOCK)
								WHERE CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE @fecha
								)
						)
					) r ON c.idCampo = r.idCampo and c.idFormulario = r.idFormulario
				) d
			PIVOT(MAX(Valor) FOR Nombre IN (
						InitialDate
						,FinalDate
						,ResponseDate
						,AssignedTo
						,gps_automatico
						,FH_PROMESA_PAGOC
						,FH_PROMESA_PAGO
						,DICTAMENPROMDEPAGO
						,DictamenpromdepagoTOM
						,DictamenBCN
						,DictamenBCNPrivado
						,DictamenBCNJudicial
						,DictamenSTM
						,DictamenSTMPrivado
						,DictamenSTMJudicial
						,DictamenSiAceptaSTM
						,DictamenDCP
						,DictamenDCPPrivado
						,DictamenDCPJudicial
						,factorSinFee
						,montoPromesa
						,IM_OPC_SELEC
						,Dictamenliquida
						,IM_SELEC_STM_PAGO_SUBSEC
						,DictamenFPP
						,AceptaFPP1
						,AceptaFPP2
						,AceptaFPP3
						,AceptaFPP4
						,AceptaFPP5
						,AceptaFPP6
						,FH_DCP
						,Hora_DCP
						)) piv
			WHERE idOrden > 0
			) t
		INNER JOIN dbo.Ordenes o WITH (NOLOCK) ON t.idOrden = o.idOrden
		INNER JOIN dbo.Creditos l WITH (NOLOCK) ON o.num_Cred = l.CV_CREDITO
		LEFT JOIN (
			SELECT DISTINCT r.idOrden
				,r.Valor
				,r.Fecha
				,r.idCampo
				,CD.Clave
				,CD.CV_RUTA
			FROM (
				SELECT idOrden
					,idCampo
					,Valor
					,CONVERT(DATETIME, '3000-01-01') Fecha
					,idFormulario
				FROM Respuestas WITH (NOLOCK)
				WHERE Respuestas.idOrden IN (
						SELECT Ordenes.idOrden
						FROM Ordenes WITH (NOLOCK)
						WHERE CONVERT(VARCHAR(20), Ordenes.FechaModificacion, 120) LIKE @fecha
						)
				) r
			INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo and r.idFormulario= c.idFormulario
			LEFT JOIN CatDictamen CD WITH (NOLOCK) ON C.Nombre = CD.Nombre
			WHERE c.Nombre LIKE 'dictamen%'
			) Dictamen ON Dictamen.idOrden = t.idOrden
			LEFT JOIN (select r.valor,cr.idcampo,r.idorden from respuestas r left join camposrespuesta cr on r.idcampo=cr.idcampo where cr.Nombre like 'CelularSMS_%' and cr.Nombre != 'CelularSMS_Ant') CelSMS
			on CelSMS.idorden = t.idOrden
			LEFT JOIN  (select r.valor,cr.idcampo,r.idorden from respuestas r left join camposrespuesta cr on r.idcampo=cr.idcampo where cr.Nombre like 'ParCelularSMS_%' and cr.Nombre != 'CelularSMS_Ant') CelSMSProp
			on CelSMSProp.idOrden= t.idOrden
			AND Dictamen.Fecha = t.Fecha 
			and Dictamen.CV_RUTA=l.CV_RUTA
			) respuestas1 where respuestas1.RutaCredito!=@RutaNA


	UNION

	SELECT  CV_CREDITO
			, CV_DELEGACION
			,CV_PROVEEDOR
			,TX_NOMBRE_DESPACHO
			, CV_MOVIL
			,CV_RAMA
			, CV_ESTATUS_FINAL
			, TX_ESTATUS_FINAL 
			, FH_PROMESA_PAGO
			, IM_PROMESA_PAGO
			, CV_APROBACION
			, FH_INICIO_GESTION
			,FH_FIN_GESTION
			, FH_CREACION
			,FH_CARGA
			,CV_DESPACHO
			, CV_RUTA
			,GP_DOMICILIO
			,IM_MONTO_MENSUALIDAD_PESOS
			, CV_PRODUCTO_CONVENIO
			, CV_TIPO_CONVENIO
			,NU_VISITA
			,FH_INFORMACION
			,TEL_CELULAR_SMS
			,TIPO_CELULAR_SMS
			,CORROBORO_CELULAR_SMS
			,FH_DCP
			,Hora_DCP
			from
	(
		SELECT l.CV_CREDITO
			,l.CV_RUTA as RutaCredito
			,l.CV_DELEGACION
			,l.CV_PROVEEDOR
			,l.TX_NOMBRE_DESPACHO
			,t.AssignedTo AS CV_MOVIL
			,CASE 
				WHEN o.Estatus = 4
					THEN 'bmp'
				ELSE t.AssignedTo
				END AS CV_RAMA
			,ISNULL(Dictamen.Clave, '') AS CV_ESTATUS_FINAL
			,ISNULL(Dictamen.Valor, '') AS 	TX_ESTATUS_FINAL 
			,ISNULL(CONVERT(VARCHAR(10),CONVERT(datetime, t.FH_PROMESA_PAGO,121 ),121), CONVERT(VARCHAR(10),CONVERT(datetime, t.FH_PROMESA_PAGOC,121 ),121)) AS FH_PROMESA_PAGO
			,CASE t.montoPromesa
				WHEN 'MEN'
					THEN l.IM_PAGO_MENSUAL
				WHEN 'TOM'
					THEN l.IM_MONTO_RECUPERAR
				WHEN  'LIQ'
					THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
			END  AS IM_PROMESA_PAGO
			,CASE 
				WHEN o.Estatus=4
					THEN 'Valida aprobada'
				WHEN t.Fecha != '3000-01-01 00:00:00.000'
					THEN 'Valida no aprobada'	
				ELSE  'Valida'
			END AS CV_APROBACION
			, CONVERT(VARCHAR(10),CONVERT(datetime, t.FinalDate,121 ),121) + ' ' + CONVERT(VARCHAR(30),CONVERT(datetime, t.InitialDate,121 ),8) AS FH_INICIO_GESTION
			, CONVERT(VARCHAR(10),CONVERT(datetime, t.FinalDate,121 ),121) + ' ' + CONVERT(VARCHAR(30),CONVERT(datetime, t.FinalDate,121 ),8) AS FH_FIN_GESTION
			, CONVERT(VARCHAR(10),CONVERT(datetime, o.FechaEnvio,121 ),121) + ' ' + CONVERT(VARCHAR(30),CONVERT(datetime, o.FechaEnvio,121 ),8) AS FH_CREACION
			, CONVERT(VARCHAR(30),CONVERT(datetime, o.FechaRecepcion,121 ),100)  AS FH_CARGA
			,l.CV_DESPACHO
			,CASE 
				WHEN o.tipo like '%C%'
				THEN 
					'CC'
				ELSE
					'RDS'
				END AS CV_RUTA
			,SUBSTRING(t.gps_automatico,5,CHARINDEX('Lon:',t.gps_automatico)-5)+','+ SUBSTRING(t.gps_automatico,CHARINDEX('Lon:',t.gps_automatico)+4,10)+',0' AS GP_DOMICILIO
			,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN l.IM_PAGO_MENSUAL
			WHEN  t.DictamenpromdepagoTOM IS NOT NULL   
					THEN  l.IM_MONTO_RECUPERAR
			WHEN  t.Dictamenliquida IS NOT NULL   
					THEN l.IM_MONTO_LIQUIDAR_CON_DESCUENTO
			WHEN  t.DictamenBCN IS NOT NULL OR t.DictamenBCNPrivado  IS NOT NULL OR t.DictamenBCNJudicial IS NOT NULL 
					THEN l.IM_PAGO_MENSUAL_SIN_SEG_DAN
			WHEN  t.DictamenDCP IS NOT NULL OR DictamenDCPPrivado IS NOT NULL OR  DictamenDCPJudicial IS NOT NULL 
					THEN
						CASE
							WHEN 
								t.factorSinFee = ''
								THEN
									null	
								ELSE
									 t.factorSinFee
							END
			WHEN  t.DictamenSTM IS NOT NULL  OR t.DictamenSiAceptaSTM IS NOT NULL OR t.DictamenSTMPrivado IS NOT NULL OR t.DictamenSTMJudicial IS NOT NULL   
					THEN 	
							CASE t.IM_OPC_SELEC
						WHEN 'IM_OPC1_STM'
							THEN l.IM_OPC1_STM_PAGO_SUBSEC

						WHEN 'IM_OPC2_STM'
							THEN l.IM_OPC2_STM_PAGO_SUBSEC
						WHEN  'IM_OPC3_STM'
							THEN l.IM_OPC3_STM_PAGO_SUBSEC
						WHEN  'IM_OPC4_STM'
							THEN l.IM_OPC4_STM_PAGO_SUBSEC
							else
								l.IM_OPC1_STM_PAGO_SUBSEC 
							END				
					END				
			 AS IM_MONTO_MENSUALIDAD_PESOS
			,CASE 
				WHEN  t.DICTAMENPROMDEPAGO IS NOT NULL   
					THEN   t.montoPromesa
				WHEN  t.Dictamenliquida IS NOT NULL   
					THEN   'LIQ'
				WHEN  t.DictamenBCN IS NOT NULL OR t.DictamenBCNPrivado  IS NOT NULL OR t.DictamenBCNJudicial IS NOT NULL 
					THEN 'BCN'
				WHEN  t.DictamenDCP IS NOT NULL OR DictamenDCPPrivado IS NOT NULL OR  DictamenDCPJudicial IS NOT NULL 
					THEN 'DCP'	
				WHEN  t.DictamenSTM IS NOT NULL  OR t.DictamenSiAceptaSTM IS NOT NULL OR t.DictamenSTMPrivado IS NOT NULL OR t.DictamenSTMJudicial IS NOT NULL
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
			WHEN  t.DictamenFPP IS NOT NULL  
					THEN	
						CASE 
							WHEN  t.AceptaFPP1 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP1,1)		
							WHEN  t.AceptaFPP2 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP2,1)
							WHEN  t.AceptaFPP3 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP3,1)
							WHEN  t.AceptaFPP4 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP4,1)
							WHEN  t.AceptaFPP5 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP5,1)
							WHEN  t.AceptaFPP6 IS NOT NULL  
								THEN 'FPP'+right(t.AceptaFPP6,1)
						END
			END AS CV_PRODUCTO_CONVENIO
			,CASE 
					WHEN t.DictamenBCNJudicial IS NOT NULL  OR t.DictamenSTMJudicial IS NOT NULL  OR t.DictamenDCPJudicial IS NOT NULL 
					THEN 'J'
					ELSE
						'P'
					END 
					 AS CV_TIPO_CONVENIO
			,tt2.idVisita AS NU_VISITA
			,CONVERT(varchar, GETDATE(),112) as FH_INFORMACION
		    ,celSMS.Valor as TEL_CELULAR_SMS
			,CelSMSProp.valor as TIPO_CELULAR_SMS
			,NULL as CORROBORO_CELULAR_SMS
			,t.FH_DCP
			,t.Hora_DCP
		FROM (
			SELECT idOrden
				,ISNULL(Fecha, CONVERT(DATETIME, '3000-01-01')) Fecha
				,InitialDate
				,FinalDate
				,ResponseDate
				,AssignedTo
				,FH_PROMESA_PAGOC
				,FH_PROMESA_PAGO
				,gps_automatico
				,DICTAMENPROMDEPAGO
				,DictamenpromdepagoTOM
				,DictamenBCN
				,DictamenBCNPrivado
				,DictamenBCNJudicial
				,DictamenSTM
				,DictamenSTMPrivado
				,DictamenSTMJudicial
				,DictamenSiAceptaSTM
				,DictamenDCP
				,DictamenDCPPrivado
				,DictamenDCPJudicial
				,factorSinFee
				,montoPromesa
				,IM_OPC_SELEC
				,Dictamenliquida
				,IM_SELEC_STM_PAGO_SUBSEC
				,DictamenFPP
				,AceptaFPP1
				,AceptaFPP2
				,AceptaFPP3
				,AceptaFPP4
				,AceptaFPP5
				,AceptaFPP6
				,FH_DCP
				,Hora_DCP
			FROM (
				SELECT r.idOrden
					,c.Nombre
					,r.Valor
					,r.Fecha
				FROM [CamposRespuesta] c WITH (NOLOCK)
				LEFT JOIN (
					(
						SELECT br.idOrden
							,br.idCampo
							,br.Valor
							,br.Fecha
							,br.idFormulario
						FROM BitacoraRespuestas br WITH (NOLOCK)
						WHERE br.idOrden IN (
								SELECT o.idOrden
								FROM Ordenes o WITH (NOLOCK)
								)
							AND CONVERT(VARCHAR(20), br.Fecha, 120) LIKE @fecha
						)
					) r ON c.idCampo = r.idCampo and c.idFormulario= r.idFormulario
				) d
			PIVOT(MAX(Valor) FOR Nombre IN (
						InitialDate
						,FinalDate
						,ResponseDate
						,AssignedTo
						,gps_automatico
						,FH_PROMESA_PAGOC
						,FH_PROMESA_PAGO
						,DICTAMENPROMDEPAGO
						,DictamenpromdepagoTOM
						,DictamenBCN
						,DictamenBCNPrivado
						,DictamenBCNJudicial
						,DictamenSTM
						,DictamenSTMPrivado
						,DictamenSTMJudicial
						,DictamenSiAceptaSTM
						,DictamenDCP
						,DictamenDCPPrivado
						,DictamenDCPJudicial
						,factorSinFee
						,montoPromesa
						,IM_OPC_SELEC
						,Dictamenliquida
						,IM_SELEC_STM_PAGO_SUBSEC
						,DictamenFPP
						,AceptaFPP1
						,AceptaFPP2
						,AceptaFPP3
						,AceptaFPP4
						,AceptaFPP5
						,AceptaFPP6
						,FH_DCP
						,Hora_DCP
						)) piv
			WHERE idOrden > 0
			) t
		INNER JOIN dbo.Ordenes o WITH (NOLOCK) ON t.idOrden = o.idOrden
		INNER JOIN dbo.Creditos l WITH (NOLOCK) ON o.num_Cred = l.CV_CREDITO
		LEFT JOIN (
			SELECT DISTINCT r.idOrden
				,r.Valor
				,r.Fecha
				,r.idCampo
				,CD.Clave
				,CD.CV_RUTA
			FROM (
				SELECT idOrden
					,idCampo
					,Valor
					,Fecha
					,idFormulario
				FROM BitacoraRespuestas WITH (NOLOCK)
				WHERE BitacoraRespuestas.idOrden IN (
						SELECT Ordenes.idOrden
						FROM Ordenes WITH (NOLOCK)
						)
					AND CONVERT(VARCHAR(20), BitacoraRespuestas.Fecha, 120) LIKE @fecha
				) r
			INNER JOIN CamposRespuesta c WITH (NOLOCK) ON r.idCampo = c.idCampo and r.idFormulario=c.idFormulario
			LEFT JOIN CatDictamen CD WITH (NOLOCK) ON C.Nombre = CD.Nombre
			WHERE c.Nombre LIKE 'dictamen%'
			) Dictamen ON Dictamen.idOrden = t.idOrden 
			LEFT JOIN (select r.valor,cr.idcampo,r.idorden from Bitacorarespuestas r left join camposrespuesta cr on r.idcampo=cr.idcampo where cr.Nombre like 'CelularSMS_%' and cr.Nombre != 'CelularSMS_Ant') CelSMS
			on CelSMS.idorden = t.idOrden
			LEFT JOIN  (select r.valor,cr.idcampo,r.idorden from Bitacorarespuestas r left join camposrespuesta cr on r.idcampo=cr.idcampo where cr.Nombre like 'ParCelularSMS_%' and cr.Nombre != 'CelularSMS_Ant') CelSMSProp
			on CelSMSProp.idOrden= t.idOrden
					inner join
			(SELECT idOrden, ROW_NUMBER() OVER (PARTITION BY idOrden ORDER BY Fecha ASC) AS idVisita, Fecha
				FROM bitacorarespuestas
				GROUP BY idOrden, Fecha) tt2 ON o.idOrden=tt2.idOrden	
				where  CONVERT(VARCHAR(20), tt2.Fecha, 120) LIKE @fecha
				and t.Fecha=tt2.fecha
			AND Dictamen.Fecha = t.Fecha and Dictamen.CV_RUTA=l.CV_RUTA
		)
			respuestas2 where respuestas2.RutaCredito!=@RutaNA

	UNION
		SELECT o.num_Cred AS CV_CREDITO
			,c.CV_DELEGACION AS CV_DELEGACION
			,c.CV_PROVEEDOR
			,c.TX_NOMBRE_DESPACHO
			,u.Usuario AS CV_MOVIL
			,NULL AS CV_RAMA
			,NULL AS CV_ESTATUS_FINAL
			,NULL AS TX_ESTATUS_FINAL 
			,NULL AS FH_PROMESA_PAGO
			,NULL AS IM_PROMESA_PAGO
			,'MOVIL' AS CV_APROBACION
			,null AS FH_INICIO_GESTION
			,null AS FH_FIN_GESTION
			,null AS FH_CREACION
			,null AS FH_CARGA
			,c.CV_DESPACHO
			,CASE 
				WHEN o.tipo like '%C%'
				THEN 
					'CC'
				ELSE
					'RDS'
				END AS CV_RUTA
			,null AS GP_DOMICILIO
			,null AS IM_MONTO_MENSUALIDAD_PESOS
			,null AS CV_PRODUCTO_CONVENIO
			,'P' AS CV_TIPO_CONVENIO
			,o.idVisita AS NU_VISITA
			,CONVERT(varchar, GETDATE(),112) as FH_INFORMACION
			,NULL as TEL_CELULAR_SMS
			,NULL as TIPO_CELULAR_SMS
			,NULL as CORROBORO_CELULAR_SMS
			,NULL as FH_DCP
			,NULL as Hora_DCP
		FROM Ordenes o
		INNER JOIN Creditos c ON c.CV_CREDITO = o.num_Cred
		INNER JOIN Usuario u ON u.idUsuario = o.idUsuario
		WHERE CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE @fecha
			AND o.Estatus = 1
			AND o.idUsuario != 0
			AND c.CV_RUTA NOT IN (@RutaNA)
END
