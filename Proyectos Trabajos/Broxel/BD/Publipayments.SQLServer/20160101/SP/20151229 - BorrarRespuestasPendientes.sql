
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/21/29
-- Description:	Borra el registro de una respuesta que ha sido procesada
-- =============================================
CREATE PROCEDURE BorrarRespuestasPendientes
				@idorden INT
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE  FROM respuestaspendientes  WHERE idorden=@idorden
	SELECT COUNT(idOrden) as resultado FROM respuestaspendientes WITH(NOLOCK) WHERE idorden=@idorden
END
GO
