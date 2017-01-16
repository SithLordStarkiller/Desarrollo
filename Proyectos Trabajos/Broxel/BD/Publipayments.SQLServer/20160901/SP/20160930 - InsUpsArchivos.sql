/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	20/09/2016
* Descripción:			Inserta/Actualiza los registros en la tabla Archivos y ArchivoXUsuario
*****************************************************************************/
CREATE PROCEDURE [dbo].[InsUpsArchivos] (
	@PAIDARCHIVO INT
	,@PANOMBREARCHIVO VARCHAR(100)
	,@PAESTATUSARCHIVO VARCHAR(20)
	,@PAREGISTROS INT
	,@PAEXTARCHIVO VARCHAR(5)
	,@PATIPOARCHIVO INT
	,@PAIDUSUARIO INT
	)
AS
BEGIN
	DECLARE 
	@ID_ARCHIVO INT 
	,@FECHAINICIO DATETIME
	,@FECHAFIN DATETIME = GETDATE()

	BEGIN TRY
		BEGIN TRAN RegistroArchivo
			
			IF NOT EXISTS(SELECT 1 FROM Archivos WITH (NOLOCK) WHERE ID = @PAIDARCHIVO)
			BEGIN
			
				INSERT Archivos ([Archivo], [Tipo], [Registros], [Tiempo], [Fecha], [Estatus], [FechaAlta], [tipoArchivo]) VALUES (@PANOMBREARCHIVO, @PAEXTARCHIVO, @PAREGISTROS, 0, GETDATE(), @PAESTATUSARCHIVO, GETDATE(), @PATIPOARCHIVO)

				SELECT @ID_ARCHIVO = SCOPE_IDENTITY()

				INSERT INTO ArchivoXUsuario ([ID_ARCHIVO], [ID_USUARIO]) VALUES (@ID_ARCHIVO, @PAIDUSUARIO)
			
				SELECT 0 AS Codigo, 'Se guardó archivo' AS Descripcion;

				SELECT [Id], [Fecha] FROM Archivos WHERE ID = @ID_ARCHIVO
			END
			ELSE
			BEGIN
			SELECT @FECHAINICIO = [FechaAlta] FROM Archivos WITH (NOLOCK) WHERE ID = @PAIDARCHIVO

				UPDATE Archivos SET [Registros] = @PAREGISTROS, [Tiempo] = DATEDIFF(S, @FECHAINICIO, @FECHAFIN), [Estatus] = @PAESTATUSARCHIVO, [Fecha] = @FECHAFIN WHERE ID = @PAIDARCHIVO
				SELECT 0 AS Codigo, 'Se actualizó archivo' AS Descripcion;
			END		
				
		COMMIT TRAN RegistroArchivo
		END TRY
		BEGIN CATCH
			SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
			ROLLBACK TRAN RegistroArchivo
		END CATCH
	END