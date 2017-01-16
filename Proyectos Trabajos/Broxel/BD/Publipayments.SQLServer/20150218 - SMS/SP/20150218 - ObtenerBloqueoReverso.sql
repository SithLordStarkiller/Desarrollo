
GO
/****** Object:  StoredProcedure [dbo].[ObtenerBloqueoReverso]    Script Date: 02/25/2015 11:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 05/02/2015	
-- Description:	Recupera Bloqueo Para reversos
-- Modification Date: 17/02/2014
-- Modification Description: Ordenes con tipo SMS bloqueadas
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerBloqueoReverso] 
	@idOrden INT
AS
BEGIN
	IF EXISTS (
			SELECT 1
			FROM Ordenes
			WHERE idOrden = @idOrden
				AND Tipo = 'S'
			)
	BEGIN
		SELECT '0' as Bloqueo
	END
	ELSE
	BEGIN
		SELECT cdr.Bloqueo
		FROM Respuestas r
		INNER JOIN CatDictamenRespuesta cdr ON cdr.idCampo = r.idCampo
		WHERE r.idOrden = @idOrden
	END
END
