
/****** Object:  StoredProcedure [dbo].[GeneraOrdenXML3]    Script Date: 07/12/2015 11:02:22 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Modificó: Maximiliano Silva
Fecha Modificación: 19/08/2014
Modificación: Se agrega estatus 11,12,15,3
Modificacion: Se agreaga filtro para buscar por idFormulario al que pertenece en tabla CamposXML2
Modificacion: Se agrega consulta para obtener el dominio original del credito para CC
Modificacion: Se agrega consulta para saber si viene de CC y se actualiza la orden, en vez de crearse
Modificación: 20151207 - MJNS - Se agrega manejo de transacción para inserción de orden 
*/
ALTER PROCEDURE [dbo].[GeneraOrdenXML3] (
	@idPool INT, 
	@Credito NVARCHAR(50), 
	@idUsuario INT = - 1, 
	@idUsuarioPadre INT = - 1, 
	@idUsuarioAlta INT = - 1, 
	@idDominio INT = 0, 
	@idOrden INT = - 1
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idVisita INT = 1, @usuario VARCHAR(50), @estatus INT = - 1, @Ruta VARCHAR(10),@vieneCC int=0

	IF (@idDominio = - 1)
	BEGIN
		SELECT @idDominio = idDominio
		FROM creditos cr
		INNER JOIN dominio dm ON cr.TX_NOMBRE_DESPACHO = dm.nom_corto
		WHERE cv_Credito = @Credito
	END
	
	select @vieneCC=1,@idOrden=idOrden from Ordenes
	where num_Cred=@Credito and idUsuarioPadre in('-110')

	--SELECT @idVisita = MAX([idVisita]) + 1
	--FROM Ordenes
	--WHERE IdOrden = @Credito
	--	AND Estatus = 3;
	--SET @idVisita = 1; --ISNULL(@idVisita, 1);
	IF (@idOrden > 0)
	BEGIN
		UPDATE Ordenes
		SET 
			idUsuario = case when @vieneCC=1 then idUsuario else @idUsuario end ,
			idUsuarioPadre=case when @vieneCC=1 then @idUsuarioPadre else idUsuarioPadre end ,
			[FechaModificacion] = GETDATE()
		WHERE idOrden = @idOrden
		
		update Respuestas
		set idUsuarioPadre=case when @vieneCC=1 then @idUsuarioPadre else idUsuarioPadre end
		WHERE idOrden = @idOrden
		
	END
	ELSE
	BEGIN
		BEGIN TRY
		BEGIN TRANSACTION InsertarOrden
		
		SELECT TOP 1 @idOrden = idOrden, @estatus = Estatus
		FROM Ordenes WITH (ROWLOCK)
		WHERE [num_Cred] = @Credito
			AND [idDominio] = @idDominio
		ORDER BY idOrden DESC

		IF (
				@estatus != 1
				AND @estatus != 11
				AND @estatus != 12
				AND @estatus != 15
				AND @estatus != 3
				)
		BEGIN
			INSERT INTO Ordenes ([idPool], [num_Cred], [idUsuario], [idUsuarioPadre], [idUsuarioAlta], [idDominio], [idVisita], [FechaAlta], [Estatus], [FechaModificacion])
			VALUES (@idPool, @Credito, @idUsuario, @idUsuarioPadre, @idUsuarioAlta, @idDominio, @idVisita, GETDATE(), 1, GETDATE()) -- 1 = Asignada

			SET @idOrden = SCOPE_IDENTITY()
		END
		COMMIT TRANSACTION InsertarOrden
		END TRY
		BEGIN CATCH
			IF (@@TRANCOUNT  > 0)
				ROLLBACK TRANSACTION InsertarOrden;
			THROW;
		END CATCH
	END

	SELECT TOP 1 @Ruta = CV_RUTA
	FROM creditos
	WHERE CV_CREDITO = @Credito

	SELECT @usuario = u.Usuario, @idVisita = o.[idVisita]
	FROM USUARIO u
	INNER JOIN Ordenes o ON o.idUsuario = u.idUsuario
	WHERE idOrden = @idOrden

	SELECT TOP 1 @idOrden idOrden, @usuario Usuario, c.*, @idVisita idVisita, d.Descripcion TX_DELEGACION, ISNULL(e.TX_DESCRIPCION_ETIQUETA, 'No encontrada') TX_DESCRIPCION_ETIQUETA
	FROM dbo.CREDITOS c
	INNER JOIN CatDelegaciones d ON c.CV_DELEGACION = d.Delegacion
	LEFT JOIN dbo.CatEtiqueta e ON c.CV_ETIQUETA = e.CV_ETIQUETA
	WHERE c.CV_CREDITO = @Credito
	ORDER BY ID_ARCHIVO DESC

	SELECT cxml.*
	FROM CamposXML2 cxml
	INNER JOIN Formulario f ON f.idFormulario = cxml.idFormulario
	INNER JOIN Aplicacion a ON f.idAplicacion = a.idAplicacion
	WHERE f.Estatus = 1
		AND a.idAplicacion = (
			SELECT valor
			FROM [CatalogoGeneral]
			WHERE Llave = 'idAplicacion'
			)
		AND f.Ruta = @Ruta
	ORDER BY cxml.Orden
END
