/****** Object:  StoredProcedure [dbo].[ActualizarDatosUsuario]    Script Date: 29/11/2016 09:46:46 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************
* Proyecto:					London-PubliPayments-Formiik
* Autor:					Pablo Rendon
* Fecha de creación:		03/09/2015
* Descripción:				Actualizar datos usuario por idUsuario
* Modificación:				Laura Anayeli Dotor Mejía 
* Fecha de modificación:	29/11/2016 
* Descripción modificacion: Se agrega el parametro de modificacion 
*							EsCallCenterOut
**********************************************************************/
ALTER PROCEDURE [dbo].[ActualizarDatosUsuario] (
	@idUsuario INT,
	@nombre NVARCHAR(60),
	@email NVARCHAR(200),
	@password NVARCHAR(260),
	@Estatus INT,
	@EsCallCenter BIT
)
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE usuario SET Nombre = @nombre, Email = @email, Password = @password, Estatus = @Estatus, EsCallCenterOut = @EsCallCenter
	WHERE idUsuario = @idUsuario
END

