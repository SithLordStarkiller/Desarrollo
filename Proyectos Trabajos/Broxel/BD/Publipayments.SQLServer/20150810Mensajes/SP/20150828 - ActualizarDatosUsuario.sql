
/****** Object:  StoredProcedure [dbo].[ActualizarDatosUsuario]    Script Date: 03/09/2015 01:17:11 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Pablo Rendon
* Fecha de creación:	03/09/2015
* Descripción:			Actualizar datos usuario por idUsuario
***************************************************************** *****/
CREATE PROCEDURE [dbo].[ActualizarDatosUsuario] (
	@idUsuario int,
	@nombre nvarchar(60),
	@email nvarchar(200),
	@password nvarchar(260),
	@Estatus int
	)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE usuario SET Nombre = @nombre,Email = @email,Password = @password,Estatus = @Estatus
	WHERE idUsuario = @idUsuario
END

GO


