/****** Object:  StoredProcedure [dbo].[ValidarUsuario]    Script Date: 30/08/2016 22:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Maximiliano Silva
* Fecha de creación:	26/03/2014
* Descripción:			Autentifica a los usuarios
**********************************************************************/
ALTER PROCEDURE [dbo].[ValidarUsuario] (
	@Dominio NVARCHAR(50)
	,@Usuario NVARCHAR(50)
	,@Password NVARCHAR(130)
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idUsuario INT
		,@idRol INT = - 1
		,@Rol VARCHAR(30)
		,@idDominio INT = - 1
		,@nombreDominio VARCHAR(50)
		,@alta DATETIME = '2014-01-01'
		,@ultimoLogin DATETIME = '2013-01-01'
		,@nombreUsuario nvarchar(30) = ''
		,@Estatus int=-1
		,@FUltimoReset DATETIME='2014-01-01'
		,@Email nvarchar(100)
		,@EsCallCenterOut BIT

	SELECT @idUsuario = u.idUsuario
		,@idRol = u.idRol
		,@idDominio = d.idDominio
		,@nombreDominio = d.NombreDominio
		,@alta = u.Alta
		,@ultimoLogin = u.UltimoLogin
		,@nombreUsuario = u.Nombre
		,@Estatus=u.Estatus
		,@Email=u.Email
		,@EsCallCenterOut=u.EsCallCenterOut
	FROM Usuario u  WITH (NOLOCK)
	INNER JOIN Dominio d  WITH (NOLOCK) ON u.idDominio = d.idDominio
	WHERE u.[Usuario] = @Usuario
		AND u.[Password] = @Password
		AND d.[nom_corto] = @Dominio
		AND u.Estatus in (1,2,3)
		AND d.Estatus = 1
		AND ISNULL(bloqueo, GETDATE()) <= GETDATE()

	SELECT @Rol = NombreRol
	FROM Roles WITH(NOLOCK)
	WHERE idRol = @idRol

	IF @idUsuario IS NOT NULL
	BEGIN
	
	SELECT @FUltimoReset=max(Fecha)+60 from BitacoraPasswords WITH(NOLOCK) where idUsuario=@idUsuario
	
	IF(@alta = @ultimoLogin or @FUltimoReset<GETDATE() or @Estatus=3)	
	BEGIN
	    UPDATE Usuario
		SET UltimoLogin = GETDATE()
			,Estatus=3
		WHERE idUsuario = @idUsuario
		SET @Estatus=3;
	END
	ELSE
	BEGIN 
			UPDATE Usuario
		SET UltimoLogin = GETDATE()
			,Intentos=0
			,Estatus = 1
			,Bloqueo=NULL
		WHERE idUsuario = @idUsuario
		SET @Estatus=1;
	END
	SELECT @idUsuario idUsuario
			,@idRol idRol
			,@Rol NombreRol
			,@idDominio idDominio
			,@Dominio Dominio
			,@nombreDominio NombreDominio
			,@alta Alta
			,@ultimoLogin UltimoLogin
			,@nombreUsuario NombreUsuario
			,@Email Email
			,@Estatus Estatus
			,@EsCallCenterOut EsCallCenterOut
	END
	ELSE
	BEGIN
		SELECT - 1 idUsuario
			,@idRol idRol
			,@Rol NombreRol
			,@idDominio idDominio
			,@Dominio Dominio
			,@nombreDominio NombreDominio
			,@alta Alta
			,@ultimoLogin UltimoLogin
			,@nombreUsuario NombreUsuario
			,@Email Email
	END
END
