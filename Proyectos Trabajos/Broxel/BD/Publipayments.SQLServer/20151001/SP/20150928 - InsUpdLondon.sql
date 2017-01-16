
/****** Object:  StoredProcedure [dbo].[InsUpdLondon]    Script Date: 28/09/2015 12:37:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Maximiliano Silva
* Fecha de creación:	16/05/2014
* Descripción:			Inserta/Actualiza los registros en la tabla de London
* Fecha modificación:	01/08/2014
* Modificación:			20150203 Maximiliano Silva Se agregaron los campos que faltavan en [BitacoraCreditos] (TX_SCRIPT y CV_RUTA)
* Modificación			20150531 JARO - Se agregan los campos nuevos de creditos
* Modificación:			20150601 MJNS - Se agrega el campo de TX_ETIQUETA
* Modificación:			20150928 JARO - Se agrega el campo de CC_DESPACHO
*****************************************************************************/
ALTER PROCEDURE [dbo].[InsUpdLondon] (
	@idArchivo INT
	,@Credito VARCHAR(15)
	,@Campos NVARCHAR(max)
	,@Valores NVARCHAR(max)
	,@Tipo INT
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @sql NVARCHAR(max)
		,@campo NVARCHAR(500)
		,@valor NVARCHAR(500)

	IF NOT EXISTS (
			SELECT 1
			FROM Creditos
			WHERE CV_CREDITO = @Credito
			)
	BEGIN
		SET @sql = 'INSERT INTO Creditos (ID_ARCHIVO,' + @campos + ')' + 'VALUES (' + CONVERT(NVARCHAR(15), @idArchivo) + ',' + @Valores + ')'

		IF (@Tipo = 3)
		BEGIN
			SELECT 1088 AS Codigo
				,'EL CREDITO ' + @Credito + ' NO SE ENCUENTRA Y NO SE PUEDE ACTUALIZAR' AS Descripcion;

			RETURN - 1;
		END
	END
	ELSE
	BEGIN
		BEGIN TRY
			INSERT INTO [BitacoraCreditos]
			SELECT ID_ARCHIVO
				,CV_DELEGACION
				,CV_ZONA
				,CV_CREDITO
				,FH_OTORGAMIENTO
				,CV_REGIMEN
				,CV_ESTATUS_CONTABLE
				,IM_SALDO
				,PC_DESCUENTO_LIQUIDACION
				,IM_MONTO_DESCUENTO
				,IM_MONTO_LIQUIDAR_CON_DESCUENTO
				,CT_FACTOR_REA_ORIGINAL
				,CT_FACTOR_REA
				,CT_FACTOR_ROA
				,IM_PAGO_MENSUAL_SIN_SEG_DAN
				,IM_PRIMA_SEG_DAN
				,IM_PAGO_MENSUAL
				,IM_SALDO_SEG_DAN
				,CV_ETIQUETA
				,TX_SOLUCIONES
				,NU_MESES_RECUPERAR
				,IM_MONTO_RECUPERAR
				,IM_PAGO_MINIMO
				,IM_PAGO_RECOMENDADO
				,IM_PAGO_TOPE
				,IM_MINIMO_TEORICO
				,IM_PAGO_FPP
				,IM_BENEFICIO_FPP
				,NU_MESES_DISP_FPP
				,NU_MESES_FIN_PRORROGA
				,IN_FPP
				,IN_PPAR
				,IM_PAGO_PPAR
				,FH_INI_PRORROGA
				,FH_FIN_PRORROGA
				,FH_ASIGNACION
				,TX_NOMBRE_ACREDITADO
				,CV_RFC
				,FH_NACIMIENTO
				,CV_NSS
				,TX_CALLE
				,TX_COLONIA
				,TX_MUNICIPIO
				,CV_CODIGO_POSTAL
				,NU_TELEFONO_CASA
				,NU_TELEFONO_CELULAR
				,GP_LONGITUD
				,GP_LATITUD
				,GP_ALTITUD
				,CV_CANAL
				,TX_NOMBRE_DESPACHO
				,CV_DESPACHO
				,TX_ULTIMA_GESTION_4MESES
				,TX_ULTIMA_GESTION_3MESES
				,TX_ULTIMA_GESTION_2MESES
				,TX_ULTIMA_GESTION_1MES
				,TX_VECTOR_PAGOS
				,TX_PAGO_4MESES
				,TX_PAGO_3MESES
				,TX_PAGO_2MESES
				,TX_PAGO_1MES
				,GETDATE() AS MOVIMIENTO
				,IM_OPC1_STM
				,IM_OPC2_STM
				,IM_OPC3_STM
				,IM_OPC4_STM
				,IM_OPC1_STM_PRIMER_PAGO
				,IM_OPC2_STM_PRIMER_PAGO
				,IM_OPC3_STM_PRIMER_PAGO
				,IM_OPC4_STM_PRIMER_PAGO
				,IM_OPC1_STM_PAGO_SUBSEC
				,IM_OPC2_STM_PAGO_SUBSEC
				,IM_OPC3_STM_PAGO_SUBSEC
				,IM_OPC4_STM_PAGO_SUBSEC
				,IM_BEN1_STM
				,IM_BEN2_STM
				,IM_BEN3_STM
				,IM_BEN4_STM
				,IM_PAGO_TOPE_PESOS
				,CV_PROVEEDOR
				,CV_CONTRATO
				,TX_SCRIPT
				,CV_RUTA
				,IN_SOLICITAR_CEL
				,IN_SOLICITAR_SERVICIO_EMPLEO
				,TX_PORTAFOLIO
				,TX_ETIQUETA
				,IN_OPCION_JUDICIAL
				,TX_PAGA1
				,TX_PAGA2
				,TX_PAGA3
				,TX_PAGA4
				,TX_PAGA5
				,TX_PAGA6
				,CC_DESPACHO
			FROM [Creditos]
			WHERE [CV_CREDITO] = @Credito
		END TRY

		BEGIN CATCH
			SELECT ERROR_NUMBER() AS Codigo
				,'Revisar campos en este stored. '+ERROR_MESSAGE() AS Descripcion;

			RETURN - 1;
		END CATCH

		SET @sql = 'UPDATE Creditos WITH (ROWLOCK) SET '

		WHILE LEN(@Campos) > 0
		BEGIN
			IF PATINDEX('%,%', @Campos) > 0
			BEGIN
				SET @campo = SUBSTRING(@Campos, 0, PATINDEX('%,%', @Campos))
				SET @Campos = SUBSTRING(@Campos, LEN(@campo + ',') + 1, LEN(@Campos))
				SET @Valor = SUBSTRING(@Valores, 0, PATINDEX('%,%', @Valores))
				SET @Valores = SUBSTRING(@Valores, LEN(@Valor + ',') + 1, LEN(@Valores))
			END
			ELSE
			BEGIN
				SET @campo = @Campos
				SET @Campos = NULL
				SET @Valor = @Valores
				SET @Valores = NULL
			END

			SET @sql += @campo + '=' + @valor + ', '
		END

		SET @sql += ' ID_ARCHIVO=' + CONVERT(NVARCHAR(10), @idArchivo) + ' WHERE CV_CREDITO = ''' + @Credito + ''''
	END

	BEGIN TRY
		EXECUTE sp_executesql @sql
	END TRY

	BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo
			,ERROR_MESSAGE() AS Descripcion;

		DELETE FROM [BitacoraCreditos] WHERE CV_CREDITO=@Credito and ID_ARCHIVO IN (SELECT TOP 1 ID_ARCHIVO FROM CREDITOS WITH (NOLOCK) WHERE [CV_CREDITO] = @Credito)

		RETURN - 1;
	END CATCH

	--UPDATE Archivos
	--SET Registros = Registros + 1
	--WHERE id = @idArchivo
	SELECT 0 AS Codigo
		,'OK' AS Descripcion;
END
