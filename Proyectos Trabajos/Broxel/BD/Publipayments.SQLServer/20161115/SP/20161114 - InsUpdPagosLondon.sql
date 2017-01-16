/****** Object:  StoredProcedure [dbo].[InsUpdPagosLondon]    Script Date: 14/11/2016 12:33:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	14/11/2016
* Descripción:			Inserta/Actualiza los registros en la tabla de Pagos - London
*****************************************************************************/
CREATE PROCEDURE [dbo].[InsUpdPagosLondon] (
	@PAIDARCHIVO INT
	,@PACREDITO VARCHAR(15)
	,@PAIESTATUSPAGO INT
	,@PAINOPAGO INT
	,@PADMONTOPAGO NUMERIC(10,2)
	,@PACTXPAGO VARCHAR(150)
	)
AS
BEGIN

	BEGIN TRY

		BEGIN TRAN PagosLondon

			IF EXISTS (SELECT 1 FROM Creditos WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO)
			BEGIN
				IF EXISTS (	SELECT 1 FROM Pagos WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO)
				BEGIN
					IF EXISTS (	SELECT 1 FROM Pagos WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO AND CV_ESTATUS_PAGO = @PAIESTATUSPAGO AND NU_PAGO_MES_CORRIENTE = @PAINOPAGO )
					BEGIN
						SELECT -1 AS Codigo, 'Este registro ha sido procesado previamente.' AS Descripcion;
					END
					ELSE
					BEGIN
						INSERT INTO [BitacoraPagos] ([ID_ARCHIVO],[CV_CREDITO],[CV_ESTATUS_PAGO],[NU_PAGO_MES_CORRIENTE],[IM_PAGO_MES_CORRIENTE],[TX_PAGO_MES_CORRIENTE],[FECHA_ALTA],[FECHA]) 
						SELECT 
							[ID_ARCHIVO],
							[CV_CREDITO],
							[CV_ESTATUS_PAGO],
							[NU_PAGO_MES_CORRIENTE],
							[IM_PAGO_MES_CORRIENTE],
							[TX_PAGO_MES_CORRIENTE],
							[FECHA],
							GETDATE()
						FROM [Pagos]
						WHERE [CV_CREDITO] = @PACREDITO

						UPDATE Pagos SET ID_ARCHIVO = @PAIDARCHIVO, CV_ESTATUS_PAGO = @PAIESTATUSPAGO, NU_PAGO_MES_CORRIENTE = @PAINOPAGO, IM_PAGO_MES_CORRIENTE = @PADMONTOPAGO, TX_PAGO_MES_CORRIENTE = @PACTXPAGO, FECHA = GETDATE()  WHERE CV_CREDITO = @PACREDITO

						SELECT 0 AS Codigo, 'OK' AS Descripcion;
						
					END
				END
				ELSE
				BEGIN 
					INSERT INTO Pagos ([ID_ARCHIVO],[CV_CREDITO],[CV_ESTATUS_PAGO],[NU_PAGO_MES_CORRIENTE],[IM_PAGO_MES_CORRIENTE],[TX_PAGO_MES_CORRIENTE],[FECHA]) 
					VALUES (@PAIDARCHIVO, @PACREDITO, @PAIESTATUSPAGO, @PAINOPAGO, @PADMONTOPAGO, @PACTXPAGO, GETDATE())

					SELECT 0 AS Codigo, 'OK' AS Descripcion;
				END
			END
			ELSE
			BEGIN
				SELECT -1 AS Codigo, 'Revisar, credito no existe.' AS Descripcion;
			END
		
		COMMIT TRAN PagosLondon
		END TRY
		BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
		ROLLBACK TRAN PagosLondon
	END CATCH
END