
/****** Object:  StoredProcedure [dbo].[ValidarUsuarioParcial]    Script Date: 27/08/2015 06:15:51 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Proyecto:    London-PubliPayments-Formiik
-- Author:		Alberto Ortiz
-- Create date: 29/94/2014
-- Description:	Revisa si e-mail, usuario y dominio son correctos
-- =============================================
ALTER PROCEDURE [dbo].[ValidarUsuarioParcial](
				 @Dominio nvarchar(50),
				 @Usuario nvarchar(50)
				)
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE
			@idUsuario INT,
			@Email nvarchar(100)

	SELECT 
		@idUsuario = u.idUsuario, @Email = u.Email
	FROM Usuario u  WITH (NOLOCK)
	INNER JOIN Dominio d  WITH (NOLOCK)
	ON u.idDominio = d.idDominio
	WHERE u.[Usuario] = @Usuario
	AND d.[nom_corto] = @Dominio
	AND u.Estatus != 0
	AND d.Estatus = 1

	IF @idUsuario IS NOT NULL
	BEGIN
		SELECT @idUsuario idUsuario, @Email Email
	END
	ELSE
	BEGIN
		SELECT -1 idUsuario
	END
END
