/****** Object:  StoredProcedure [dbo].[ObtenerUsuariosPorUsuario]    Script Date: 28/08/2015 11:01:40 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Rendon
-- Create date: 2015-08-28
-- Description:	Obtiene los datos del usuario por Usuario
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerUsuariosPorUsuario] @Usuario nvarchar(100) = ''
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Nombre],
		   [Email]
	FROM vUSuarios
	WHERE Usuario = @Usuario
END
