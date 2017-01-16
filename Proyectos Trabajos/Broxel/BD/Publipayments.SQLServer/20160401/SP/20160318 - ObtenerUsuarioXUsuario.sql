
-- ==========================================================================================
-- Author:		Alberto Rojas
-- Create date: 2016/03/18
-- Description:	Obtiene la informacion de un usuario realizando la busqueda por usuario que tiene registrado en plataforma
-- ==========================================================================================
CREATE PROCEDURE ObtenerUsuarioXUsuario 
	@usuario NVARCHAR(50)
AS
BEGIN	
	SET NOCOUNT ON;

	SELECT u.idUsuario,u.idDominio,u.idRol,u.Usuario,u.Nombre,u.Email,u.Alta,u.UltimoLogin,u.Estatus,u.Intentos,u.Bloqueo,ru.idPadre
	FROM usuario u WITH(NOLOCK) LEFT JOIN  relacionusuarios ru WITH(NOLOCK) on u.idusuario=ru.idHijo 
	WHERE usuario=@usuario
END
GO




