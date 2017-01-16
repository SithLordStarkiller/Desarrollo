-- =============================================
-- Author:		Laura Anayeli Dotor Mejia
-- Create date: 28/09/2016
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerIndDashRDST_CREDGEST] @idUsuario INT, @delegacion VARCHAR(10), @despacho VARCHAR(10), @supervisor VARCHAR(10), @gestor VARCHAR(10), @tipoFormulario VARCHAR(50), @contraPorcentaje INT, @callCenter VARCHAR(6) = 'false'
AS
BEGIN
	DECLARE @ruta VARCHAR(10) = '', @valor VARCHAR(10) = '', @porcentaje VARCHAR(10) = '', @parte VARCHAR(10) = '', @nombreDisplay VARCHAR(100) = '', @rol INT

	SELECT @ruta = Ruta
	FROM Formulario WITH (NOLOCK)
	WHERE idFormulario = @tipoFormulario

	SELECT @parte = fi_Parte, @nombreDisplay = fc_Descripcion
	FROM dbo.Utils_Descripciones WITH (NOLOCK)
	WHERE fc_Clave = 'RDST_CREDGEST'

	SELECT @rol = idRol
	FROM Usuario WITH (NOLOCK)
	WHERE idUsuario = @idUsuario

	IF @rol IN (2, 3, 4)
	BEGIN
		SELECT @despacho = idDominio
		FROM Usuario WITH (NOLOCK)
		WHERE idUsuario = @idUsuario
	END

	IF @rol IN (3)
	BEGIN
		SELECT @supervisor = @idUsuario
	END

	IF @rol IN (4)
	BEGIN
		SELECT @supervisor = idPadre
		FROM RelacionUsuarios WITH (NOLOCK)
		WHERE idHijo = @idUsuario

		SELECT @gestor = @idUsuario
	END

	IF @rol IN (5)
	BEGIN
		SELECT @delegacion = Delegacion
		FROM RelacionDelegaciones WITH (NOLOCK)
		WHERE idUsuario = @idUsuario
	END

	IF (@ruta = 'RDST')
	BEGIN
		SET @supervisor = '%'
	END

	IF @callCenter = 'false'
	BEGIN
		IF @rol IN (0, 1, 2, 3, 4, 5, 6)
		BEGIN
			SELECT @valor = COUNT(c.num_cred)
			FROM Ordenes c WITH (NOLOCK)
			WHERE c.cvruta = @ruta
				AND (
					C.Estatus IN (3, 4)
					OR C.idVisita > 1
					)
				AND c.idUsuarioPadre = CASE 
					WHEN @supervisor = '%'
						THEN c.idUsuarioPadre
					ELSE @supervisor
					END
				AND c.idUsuario = CASE 
					WHEN @gestor = '%'
						THEN c.idUsuario
					ELSE @gestor
					END
				AND c.idDominio = CASE 
					WHEN @despacho = '%'
						THEN c.idDominio
					ELSE @despacho
					END
				AND (
					@delegacion = '%'
					OR c.cvDelegacion = @delegacion
					)

			SELECT @porcentaje = CASE 
					WHEN @contraPorcentaje <> 0
						THEN (@valor * 100) / @contraPorcentaje
					ELSE '0'
					END
		END
	END
	ELSE
	BEGIN
		SELECT @valor = 0, @porcentaje = 0
	END

	SELECT @valor + '|' + @porcentaje + '|' + @nombreDisplay + '|' + @parte
END
