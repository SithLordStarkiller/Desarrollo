/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	20/09/2016
* Descripción:			Inserta/Actualiza los registros en la tabla ArchivosError
*****************************************************************************/
CREATE PROCEDURE [dbo].[InsArchivosError] (
	@PAIDARCHIVO INT	
	,@PAERROR VARCHAR(MAX)
	)
AS
BEGIN
	DECLARE @ID_ARCHIVO INT 

	BEGIN TRY
		BEGIN TRAN RegistroArchivoError

			INSERT ArchivosError ([id_archivo], [Error]) VALUES (@PAIDARCHIVO, @PAERROR)

			SELECT 0 AS Codigo, 'Se guardó registro' AS Descripcion;			
				
		COMMIT TRAN RegistroArchivoError
		END TRY
		BEGIN CATCH
			SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
			ROLLBACK TRAN RegistroArchivoError
		END CATCH
	END