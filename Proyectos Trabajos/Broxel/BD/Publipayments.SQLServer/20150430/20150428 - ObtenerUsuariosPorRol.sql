SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2015-04-28
-- Description:	Obtiene los usuarios por rol
-- =============================================
ALTER PROCEDURE ObtenerUsuariosPorRol @idRol INT = - 1
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [idUsuario]
		,[idDominio]
		,[idRol]
		,[Usuario]
		,[Nombre]
		,[Email]
		,[Password]
		,[Alta]
		,[UltimoLogin]
		,[Estatus]
		,[Intentos]
		,[Bloqueo]
	FROM Usuario
	WHERE idRol = @idRol
	AND Estatus != 0
END
GO


--ObtenerUsuariosPorRol 3

