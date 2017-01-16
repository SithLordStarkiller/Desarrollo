-- ===============================================================================
-- Author:		ALBERTO ROJAS
-- Create date: 2016/02/19
-- Description:	Inserta un nuevo registro registro en la tabla UsuariosServicios
-- ===============================================================================
CREATE PROCEDURE InsUsuariosServicios
				@idAplicacion INT,
				@Usuario NVARCHAR(50),
				@Password VARCHAR(25),
				@Orden INT,
				@Tipo INT,
				@Nombre VARCHAR(500) =null,
				@Servidor VARCHAR(2000) =null,
				@Extra VARCHAR(2000) =null,
				@Descripcion VARCHAR(500)=null
AS
BEGIN
	OPEN SYMMETRIC KEY ServiciosPW_Key11
    DECRYPTION BY CERTIFICATE ServiciosPW;

	INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo],[Nombre],[Servidor],[Extra],[Descripcion]) 
	VALUES (@idAplicacion,
	@Usuario,
	EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),@Password) , 1, HashBytes('SHA1', CONVERT( varbinary, @idAplicacion))),
	@Orden,
	@Tipo,
	@Nombre,
	@Servidor,
	@Extra,
	@Descripcion)

    
END
GO


