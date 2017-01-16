
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 05/02/2015	
-- Description:	Recupera Bloqueo Para reversos
-- =============================================
CREATE PROCEDURE ObtenerBloqueoReverso
	@idOrden int
AS
BEGIN
	select cdr.Bloqueo from Respuestas r 
	inner join CatDictamenRespuesta cdr on cdr.idCampo=r.idCampo 
	where r.idOrden=@idOrden	
END
GO
