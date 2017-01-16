/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	24/10/2016
* Descripción:			Actualiza la clave del nuevo dictamen
* Parametros:
*	@ID_NVODICTAMEN:	Id del nuevo dictamen
*****************************************************************************/
CREATE PROCEDURE [dbo].[ActualizaClaveDictamen] (
@ID_NVODICTAMEN INT
)
AS
BEGIN
	
	DECLARE 
		@Clave nvarchar(20),
		@Descripcion	nvarchar(100)

	BEGIN TRY
	
		SELECT @Descripcion = LOWER(Descripcion) FROM CatDictamen WITH(NOLOCK) WHERE IdCatalogo = @ID_NVODICTAMEN

		SELECT @Clave = Clave FROM CatDictamen WITH(NOLOCK) WHERE LOWER(Descripcion) like '%' + @Descripcion + '%' ORDER BY Clave OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY

		IF(@Clave != '')
		BEGIN

			UPDATE CatDictamen SET Clave = @Clave WHERE IdCatalogo = @ID_NVODICTAMEN

		END
			
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
	
	END CATCH
END