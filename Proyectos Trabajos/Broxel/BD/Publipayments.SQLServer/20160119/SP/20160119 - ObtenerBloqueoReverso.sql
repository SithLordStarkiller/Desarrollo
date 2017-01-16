
/****** Object:  StoredProcedure [dbo].[ObtenerBloqueoReverso]    Script Date: 19/01/2016 11:05:22 a.m. ******/
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
-- Modification 2016/01/19 - JARO - Se cambia para hacer llamada al SP que realiza los filtros
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerBloqueoReverso] 
	@idOrden INT
AS
BEGIN
	
	DECLARE @TextoOrdenesFiltrados	VARCHAR(10)=''

EXEC FiltrarEstatusUsuarioOrdenes @idOrden
			,@TextoOrdenesFiltrados OUTPUT

SELECT  CASE WHEN LEN(@TextoOrdenesFiltrados)>0 THEN  1 ELSE 0 END  AS Bloqueo
END
