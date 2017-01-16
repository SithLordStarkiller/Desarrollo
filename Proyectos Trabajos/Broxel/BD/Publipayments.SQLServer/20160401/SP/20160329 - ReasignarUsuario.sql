
/****** Object:  StoredProcedure [dbo].[ReasignarUsuario]    Script Date: 29/03/2016 09:57:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Alberto Rojas
* Fecha de creación:	05/04/2014
* Descripción:			Reasigna el usuario a otro usuario padre
Modificacion :			2016/03/09  JARO :  Se agrega la actualizacion a la tabla respuestas y se controla los errores
**********************************************************************/
ALTER PROCEDURE [dbo].[ReasignarUsuario](
				 @idPadreViejo INT,
			     @idPadreNuevo INT,
			     @idHijo INT
		)
AS
BEGIN

	DECLARE 
		@NOrdenes INT=0
	
	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE RelacionUsuarios SET idPadre=@idPadreNuevo WHERE idHijo=@idHijo
			SELECT @NOrdenes=count(idOrden) FROM Ordenes  WITH(NOLOCK) WHERE idUsuarioPadre=@idPadreViejo AND idUsuario=@idHijo
			IF (@NOrdenes>0 AND @idHijo>0)
				BEGIN
					UPDATE respuestas SET idUsuarioPadre=@idPadreNuevo 
					WHERE idUsuarioPadre=@idPadreViejo AND idorden IN (SELECT  idorden FROM ordenes WITH(NOLOCK) WHERE estatus in (3,4) AND idUsuarioPadre=@idPadreViejo AND  idusuario=@idHijo)
					UPDATE bitacorarespuestas SET idUsuarioPadre=@idPadreNuevo WHERE idUsuarioPadre=@idPadreViejo
					UPDATE Ordenes SET idUsuarioPadre=@idPadreNuevo WHERE idUsuarioPadre=@idPadreViejo AND idUsuario=@idHijo
				END
		
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
		SELECT 'OK'

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
