
/****** Object:  StoredProcedure [dbo].[ObtenerIndDashRDST_CREDSOLUCIONADOS]    Script Date: 14/11/2016 12:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Maximiliano Silva	
-- Create date: 20160927
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashRDST_CREDSOLUCIONADOS] @idUsuario INT, @delegacion VARCHAR(10), @despacho VARCHAR(10), @supervisor VARCHAR(10), @gestor VARCHAR(10), @tipoFormulario VARCHAR(50), @contraPorcentaje INT, @callCenter VARCHAR(6) = 'false'

AS
BEGIN
	DECLARE @ruta VARCHAR(10) = '', @valor VARCHAR(10) = '', @porcentaje VARCHAR(10) = '', @parte VARCHAR(10) = '', @nombreDisplay VARCHAR(100) = '', @rol INT

	SELECT @ruta = Ruta
	FROM Formulario WITH (NOLOCK)
	WHERE idFormulario = @tipoFormulario

	SELECT @parte = fi_Parte, @nombreDisplay = fc_Descripcion
	FROM dbo.Utils_Descripciones WITH (NOLOCK)
	WHERE fc_Clave = 'RDST_CREDSOLUCIONADOS'

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

	IF @callCenter = 'false'
	BEGIN
		IF @rol IN (0, 1, 2, 3, 4, 5, 6)
		BEGIN
			SELECT @valor = COUNT(o.idOrden)
			FROM Ordenes o WITH (NOLOCK)
			WHERE o.Estatus IN (3,4)
				AND o.cvRuta = @ruta
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND o.idUsuario = CASE 
					WHEN @gestor = '%'
						THEN o.idUsuario
					ELSE @gestor
					END
				AND o.idDominio = CASE 
					WHEN @despacho = '%'
						THEN o.idDominio
					ELSE @despacho
					END
				AND (
					@delegacion = '%'
					OR o.cvDelegacion = @delegacion
					)
				AND o.idDictamen IN (
					SELECT idCatalogo
					FROM CatDictamen WITH (NOLOCK)
					WHERE Nombre IN ('DictamenPromesaPago','DictamenPPFPP','DictamenLiquidacion','DictamenCONFPRParcial','DictamenProrrogaP','DictamenFPP') 
					AND CV_RUTA = @ruta
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
