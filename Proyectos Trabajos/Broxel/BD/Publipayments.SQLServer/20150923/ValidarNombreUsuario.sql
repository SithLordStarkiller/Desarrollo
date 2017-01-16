-- =============================================
-- Proyecto:    London-PubliPayments-Formiik
-- Author:		Pablo Rendon
-- Create date: 17/09/2015
-- Description:	Revisa si el usuario esta registrado
-- =============================================
CREATE PROCEDURE [dbo].[ValidarNombreUsuario](
				 @Usuario nvarchar(50)
				)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE
			@idUsuario INT

	SELECT 
		@idUsuario = u.idUsuario
	FROM Usuario u
	WHERE 
	rtrim(ltrim(upper(u.Usuario))) = rtrim(ltrim(upper(@Usuario)))

	IF @idUsuario IS NOT NULL
		BEGIN
			SELECT @idUsuario idUsuario
		END
	ELSE
		BEGIN
			SELECT -1 idUsuario
		END
END
