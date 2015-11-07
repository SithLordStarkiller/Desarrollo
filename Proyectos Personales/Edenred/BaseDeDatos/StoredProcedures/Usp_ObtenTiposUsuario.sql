SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 05/11/2015
-- Description:	Se encargara de obtener los registros de la tabla US_CAT_TIPO_USUARIO
-- =============================================
CREATE PROCEDURE Usp_ObtenTiposUsuario 
@IdTipoUsuario int
AS
BEGIN
	SELECT * FROM US_CAT_TIPOS_USUARIOS
END
GO
