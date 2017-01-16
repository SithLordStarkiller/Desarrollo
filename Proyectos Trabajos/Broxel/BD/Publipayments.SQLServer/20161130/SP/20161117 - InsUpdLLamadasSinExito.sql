/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	13/09/2016
* Descripción:			Inserta/Actualiza los registros en la tabla de Llamadas sin exito CC
* Autor:Laura Anayeli Dotor Mejia
* Fecha: 01/11/2016
* Descripción:			Se agregan 2 parametros (Teléfono y resultado de la gestión)
*****************************************************************************/

ALTER PROCEDURE [dbo].[InsUpdLLamadasSinExito] (
	@PAIDARCHIVO INT
	,@PACREDITO VARCHAR(15)
	,@PAFECHALLAMADA DATETIME
	,@PATELEFONO VARCHAR(12)
	,@PARESULTADO INT
)

AS
BEGIN

	DECLARE 
		@IDUSUARIO INT
		,@IDDOMINIO INT
		,@DOMINIO VARCHAR(20)
	BEGIN TRY

		BEGIN TRAN RegistroLlamadas

			IF EXISTS (SELECT 1 FROM Creditos WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO)
			BEGIN
				SELECT @IDUSUARIO = ID_USUARIO FROM ArchivoXUsuario WITH(NOLOCK) WHERE ID_ARCHIVO = @PAIDARCHIVO
				SELECT @IDDOMINIO = IDDOMINIO FROM Usuario WITH(NOLOCK) WHERE IDUSUARIO = @IDUSUARIO
				SELECT @DOMINIO = nom_corto FROM Dominio WITH(NOLOCK) WHERE IDDOMINIO = @IDDOMINIO

				IF EXISTS (	SELECT 1 FROM Creditos WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO AND TX_NOMBRE_DESPACHO = @DOMINIO)
				BEGIN
					IF NOT EXISTS(SELECT 1 FROM Ordenes WITH(NOLOCK) WHERE NUM_CRED = @PACREDITO)
					BEGIN
						IF EXISTS (	SELECT 1 FROM LlamadasSinExito WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO)
						BEGIN
							IF EXISTS (	SELECT 1 FROM LlamadasSinExito WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO AND FECHA_LLAMADA = @PAFECHALLAMADA)
							BEGIN
								SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', este registro ha sido procesado previamente.' AS Descripcion;
							END
							ELSE
							BEGIN
								
								INSERT INTO [BitacoraLlamadasSinExito] ([ID_ARCHIVO], [CV_CREDITO], [FECHA_LLAMADA], [FECHA_ALTA], [FECHA_MODIFICACION], [TELEFONO], [ID_RESULTADO]) 
								SELECT 
									[ID_ARCHIVO],
									[CV_CREDITO],
									[FECHA_LLAMADA],
									[FECHA_ALTA],
									GETDATE(),
									[TELEFONO], 
									[ID_RESULTADO]
								FROM [LlamadasSinExito]
								WHERE [CV_CREDITO] = @PACREDITO

								UPDATE LlamadasSinExito SET ID_ARCHIVO = @PAIDARCHIVO, FECHA_LLAMADA = @PAFECHALLAMADA, FECHA_ALTA = GETDATE(), TELEFONO = @PATELEFONO, ID_RESULTADO = @PARESULTADO WHERE CV_CREDITO = @PACREDITO
								
								SELECT 0 AS Codigo, 'OK' AS Descripcion;

							END
						END
						ELSE
						BEGIN 
							INSERT INTO LlamadasSinExito ([ID_ARCHIVO], [CV_CREDITO], [FECHA_LLAMADA], [FECHA_ALTA], [TELEFONO], [ID_RESULTADO]) VALUES (@PAIDARCHIVO, @PACREDITO,@PAFECHALLAMADA, GETDATE(), @PATELEFONO, @PARESULTADO)

							SELECT 0 AS Codigo, 'OK' AS Descripcion;
						END
					END
					ELSE
					BEGIN
						SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', ya ha sido gestionado.' AS Descripcion;
					END
				END
				ELSE
				BEGIN
					SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', invalido para su carga.' AS Descripcion;
				END
			END
			ELSE
			BEGIN
				SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', invalido para su carga.' AS Descripcion;
			END
		COMMIT TRAN RegistroLlamadas
		END TRY
		BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
		ROLLBACK TRAN RegistroLlamadas
	END CATCH
END