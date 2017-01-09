-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 25/12/2016
-- Description:	Obtener el usuario por usuario y contraseña
-- =============================================

CREATE PROCEDURE dbo.Usp_ObtenerUsuario
@Usuario varchar(50),
@Contrasena varchar(20)
AS
BEGIN
SELECT * FROM USUSUARIOS WHERE USUARIO = @Usuario AND @Contrasena = @Contrasena
END