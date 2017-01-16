SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/05/02
-- Description:	Obtiene numero de ordenes que se encuentran respondidas por el usuario 0 (no asignada)
-- =============================================
CREATE PROCEDURE ObtenerGestionesUsuarioNoAsignado
AS
BEGIN

	SET NOCOUNT ON;
	SELECT  COUNT(idorden) as ordenes FROM ordenes WITH(NOLOCK) WHERE estatus in (3,4) and idusuario=0
END
GO
