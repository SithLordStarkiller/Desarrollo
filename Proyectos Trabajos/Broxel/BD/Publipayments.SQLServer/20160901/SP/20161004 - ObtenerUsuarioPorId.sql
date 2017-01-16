SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/10/04
-- Description:	Obtiene la informacion de un usuario  por el Id
-- =============================================
CREATE PROCEDURE ObtenerUsuarioPorId  @idusuario INT
AS
BEGIN
	SET NOCOUNT ON;


	SELECT d.NombreDominio,d.nom_corto AS 'dominio',r.NombreRol,u.Usuario,u.Nombre,u.Email,u.idUsuario,u.idDominio,u.idRol,u.Password,u.Alta,u.UltimoLogin,u.Estatus,u.Intentos,u.Bloqueo,ru.idPadre,u.EsCallCenterOut
	FROM usuario u WITH(NOLOCK) 
	INNER JOIN  relacionusuarios ru WITH(NOLOCK) on u.idusuario=ru.idHijo 
	INNER JOIN dominio d WITH(NOLOCK) ON d.iddominio=u.iddominio
	INNER JOIN roles r WITH(NOLOCK) ON r.idrol=u.idRol
	WHERE u.idusuario=@idusuario
END
GO
	
