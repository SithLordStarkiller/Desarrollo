/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	13/09/2016
* Descripción:			Inserta/Actualiza los registros en la tabla de Llamadas sin exito CC
*****************************************************************************/
CREATE PROCEDURE [dbo].[InsUpdLLamadasSinExito] (
	@PAIDARCHIVO INT
	,@PACREDITO VARCHAR(15)
	,@PAFECHALLAMADA DATETIME	
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
								IF NOT EXISTS (	SELECT 1 FROM BitacoraLlamadasSinExito WITH(NOLOCK) WHERE CV_CREDITO = @PACREDITO AND FECHA_LLAMADA = @PAFECHALLAMADA)
								BEGIN
									INSERT INTO [BitacoraLlamadasSinExito] ([ID_ARCHIVO], [CV_CREDITO], [FECHA_LLAMADA], [FECHA_ALTA], [FECHA_MODIFICACION]) 
									SELECT 
										[ID_ARCHIVO],
										[CV_CREDITO],
										[FECHA_LLAMADA],
										[FECHA_ALTA],
										GETDATE()
									FROM [LlamadasSinExito]
									WHERE [CV_CREDITO] = @PACREDITO

									UPDATE LlamadasSinExito SET FECHA_LLAMADA = @PAFECHALLAMADA, FECHA_ALTA = GETDATE() WHERE CV_CREDITO = @PACREDITO

									SELECT 0 AS Codigo, 'OK' AS Descripcion;
								END
								ELSE
								BEGIN
									SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', este registro ha sido procesado previamente.' AS Descripcion;
								END	
							END
						END
						ELSE
						BEGIN 
							INSERT INTO LlamadasSinExito ([ID_ARCHIVO], [CV_CREDITO], [FECHA_LLAMADA], [FECHA_ALTA]) VALUES (@PAIDARCHIVO, @PACREDITO,@PAFECHALLAMADA, GETDATE())

							SELECT 0 AS Codigo, 'OK' AS Descripcion;
						END
					END
					ELSE
					BEGIN
						SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', ya ha sigo gestionado.' AS Descripcion;
					END
				END
				ELSE
				BEGIN
					SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', no pertenece a su despacho.' AS Descripcion;
				END
			END
			ELSE
			BEGIN
				SELECT -1 AS Codigo, 'Revisar credito: ' + @PACREDITO + ', no existe.' AS Descripcion;
			END
		COMMIT TRAN RegistroLlamadas
		END TRY
		BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
		ROLLBACK TRAN RegistroLlamadas
	END CATCH
END