-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/05/13
-- Description:	Se encarga de insertar o actualizar un registro de filtro de un usuario en especifico
-- =============================================
CREATE PROCEDURE InsertaFiltroAplicacion	(@idUsuario INT, @Filtro NVARCHAR(50),@Valor NVARCHAR(100) )

AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE @idFiltro INT 

	SELECT @idFiltro=id FROM CatFiltrosAplicacion WITH(NOLOCK) WHERE Descripcion=@Filtro
  
	IF EXISTS(SELECT 1 FROM FiltrosAplicacion WITH(NOLOCK) WHERE idusuario=@idUsuario AND idFiltro=@idFiltro)
	BEGIN 
			UPDATE FiltrosAplicacion SET Valor=@Valor 
			WHERE idusuario=@idUsuario AND idFiltro=@idFiltro
	END
	ELSE
		BEGIN
			INSERT INTO FiltrosAplicacion
           (idUsuario
           ,idFiltro
           ,Valor)
     VALUES
           (@idUsuario
           ,@idFiltro
           ,@Valor)
		END

END
GO
