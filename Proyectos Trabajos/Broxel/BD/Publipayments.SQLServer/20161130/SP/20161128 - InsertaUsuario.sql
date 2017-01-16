/****** Object:  StoredProcedure [dbo].[InsertaUsuario]    Script Date: 28/11/2016 05:25:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Proyecto:					portal.publipayments.com
* Descripcion:				Inserta nuevo usuario.
* Modificacion:				Laura Anayeli Dotor Mejia
* Fecha de modificacion:	28/11/2016
* Descripción modificacion:	Se inserta parametro EsCallCenter
*****************************************************************************/
ALTER PROCEDURE [dbo].[InsertaUsuario]( 
	@idPadre INT, 
	@idDominio INT, 
	@idRol INT, 
	@Usuario NVARCHAR (50),
	@Nombre NVARCHAR (50),
	@Password NVARCHAR (130),
	@Email NVARCHAR (50),
	@EsCallCenter BIT
)
 
AS
BEGIN
	DECLARE
		@idNuevoUsuario INT, 
		@EstatusUsuario INT
	
	IF @idRol = 4
	BEGIN
		SET @EstatusUsuario = 1
	END
	ELSE
	BEGIN 
		SET @EstatusUsuario = 3
	END
	
	INSERT INTO dbo.Usuario 
	(
		idDominio,
		idRol,
		Usuario,
		Nombre,
		Password,
		Email,
		Alta,
		UltimoLogin,
		Estatus,
		EsCallCenterOut
	)
	VALUES
	(
		@idDominio,
		@idRol,
		@Usuario,
		@Nombre,
		@Password,
		@Email,
		GETDATE(),
		GETDATE(),
		@EstatusUsuario,
		@EsCallCenter
	)
	
	SELECT @idNuevoUsuario = SCOPE_IDENTITY()

	INSERT INTO [BitacoraPasswords]
	(
		[idUsuario],
		[PassWord],
		[Fecha]
	)
	VALUES
	(
		@idNuevoUsuario,
		@Password,
		GETDATE()
	)

	INSERT INTO dbo.RelacionUsuarios
	(
		idPadre,
		idHijo,
		Estatus
	)
	VALUES
	(
		@idPadre,
		@idNuevoUsuario,
		1
	)
	
	SELECT @idDominio idDominio, @idNuevoUsuario idUsuario, @idRol idRol 

END

