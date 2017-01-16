
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/09/01
-- Description:	Obtiene los usuarios que se encuentren en un dominio en especifico
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerUsuariosPorDominio] (@idsDominio varchar(100))
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE  @sql varchar(1000)='';

	set @sql=@sql + 'SELECT [idUsuario]'
	set @sql=@sql +',[idDominio]'
	set @sql=@sql +',[idRol]'
	set @sql=@sql +',[Usuario]'
	set @sql=@sql +',[Nombre]'
	set @sql=@sql +',[Email]'
	set @sql=@sql +',[Password]'
	set @sql=@sql +',[Alta]'
	set @sql=@sql +',[UltimoLogin]'
	set @sql=@sql +',[Estatus]'
	set @sql=@sql +',[Intentos]'
	set @sql=@sql +',[Bloqueo]'
	set @sql=@sql + ' FROM Usuario'
	set @sql=@sql + ' WHERE idDominio in ('+@idsDominio+')'
	set @sql=@sql + ' AND Estatus != 0'
	EXEC (@sql)
	
END


GO
