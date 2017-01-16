
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioXUsuario]    Script Date: 02/09/2016 13:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ==========================================================================================
-- Author:		Alberto Rojas
-- Create date: 2016/03/18
-- Description:	Obtiene la informacion de un usuario realizando la busqueda por usuario que tiene registrado en plataforma
-- 20160902_JARO_ Se agrega el despliegue de la nueva columna usuario de Call Center Out
-- ==========================================================================================
ALTER PROCEDURE [dbo].[ObtenerUsuarioXUsuario] 
	@usuario NVARCHAR(50)
AS
BEGIN	
	SET NOCOUNT ON;

	SELECT u.idUsuario,u.idDominio,u.idRol,u.Usuario,u.Nombre,u.Email,u.Alta,u.UltimoLogin,u.Estatus,u.Intentos,u.Bloqueo,ru.idPadre,u.EsCallCenterOut
	FROM usuario u WITH(NOLOCK) LEFT JOIN  relacionusuarios ru WITH(NOLOCK) on u.idusuario=ru.idHijo 
	WHERE usuario=@usuario
END
