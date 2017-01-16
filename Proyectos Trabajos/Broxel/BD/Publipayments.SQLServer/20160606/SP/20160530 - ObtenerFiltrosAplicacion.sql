
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/05/30
-- Description:	Obtiene el filtro que tenga registrado el usuario
-- =============================================
CREATE PROCEDURE ObtenerFiltrosAplicacion (@idUsuario INT, @Filtro NVARCHAR(50))
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idFiltro INT 

	SELECT @idFiltro=id FROM CatFiltrosAplicacion WITH(NOLOCK) WHERE Descripcion=@Filtro
  
	SELECT Valor FROM FiltrosAplicacion WITH(NOLOCK) WHERE idusuario=@idUsuario AND idFiltro=@idFiltro
END
GO
