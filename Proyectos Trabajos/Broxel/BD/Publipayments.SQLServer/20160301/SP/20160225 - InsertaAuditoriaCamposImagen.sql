
/****** Object:  StoredProcedure [dbo].[InsertaAuditoriaCamposImagen]    Script Date: 25/02/2016 05:43:08 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 26/01/2015
-- Description:	Guarda detalle de autorizacion por imagen
-- =============================================
ALTER PROCEDURE [dbo].[InsertaAuditoriaCamposImagen] 
	@idAuditoriaImagenes INT, 
	@imagen VARCHAR(max), 
	@resultado INT
AS
BEGIN
	IF EXISTS (
			SELECT imagen
			FROM AuditoriaCamposImagen WITH(NOLOCK)
			WHERE idAuditoriaImagenes = @idAuditoriaImagenes
				AND imagen = @imagen
			)
	BEGIN
		UPDATE AuditoriaCamposImagen
		SET resultado = @resultado
		WHERE idAuditoriaImagenes = @idAuditoriaImagenes
			AND imagen = @imagen
	END
	ELSE
	BEGIN
		INSERT INTO AuditoriaCamposImagen
		VALUES (@idAuditoriaImagenes, @imagen, @resultado)
	END
END
