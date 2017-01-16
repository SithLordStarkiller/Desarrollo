
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/12/29
-- Description:	Obtiene los datos de las respuestas pendientes
-- =============================================
CREATE PROCEDURE ObtenerDatosRespuestasPendientes
		@idorden int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT idorden,NombreCampo,valor,idUsuario,Fecha FROM respuestaspendientes WITH(NOLOCK) WHERE idorden=@idorden
END
GO
