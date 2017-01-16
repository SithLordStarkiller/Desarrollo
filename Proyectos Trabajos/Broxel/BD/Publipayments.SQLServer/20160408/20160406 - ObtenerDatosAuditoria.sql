
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/04/06
-- Description:	Obtiene informacion del resultado de la auditoria
-- =============================================
CREATE PROCEDURE ObtenerDatosAuditoria 
					@idAuditoriaImagenes INT
AS
BEGIN	
	SET NOCOUNT ON;
		SELECT idUsuario,num_credito,resultado FROM AuditoriaImagenes WITH(NOLOCK) WHERE idAuditoriaImagenes=@idAuditoriaImagenes
		SELECT imagen,resultado FROM AuditoriaCamposImagen WITH(NOLOCK) WHERE idAuditoriaImagenes=@idAuditoriaImagenes
END
GO
