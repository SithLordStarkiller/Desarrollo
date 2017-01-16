/****** Object:  StoredProcedure [dbo].[ObtenerIndDashRDST_CREDSINSOLUCION]    Script Date: 30/11/2016 06:13:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Laura Anayeli Dotor Mejia
-- Create date: 26/09/2016
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashRDST_CREDSINSOLUCION] 
@idUsuario INT, 
@delegacion VARCHAR(10), 
@despacho VARCHAR(10), 
@supervisor VARCHAR(10), 
@gestor VARCHAR(10), 
@tipoFormulario VARCHAR(50), 
@contraPorcentaje INT, 
@callCenter VARCHAR(6) = 'false'

AS
BEGIN
	DECLARE 
		@ruta VARCHAR(10) = '', 
		@valorTotal INT, 
		@valorGestion INT, 
		@valor VARCHAR(10) = '',
		@porcentaje VARCHAR(10) = '', 
		@parte VARCHAR(10) = '', 
		@nombreDisplay VARCHAR(100) = '', 
		@rol INT

	SELECT @ruta = Ruta FROM Formulario WITH (NOLOCK) WHERE idFormulario = @tipoFormulario

	SELECT @parte = fi_Parte, @nombreDisplay = fc_Descripcion FROM dbo.Utils_Descripciones WITH (NOLOCK)
	WHERE fc_Clave = 'RDST_CREDSINSOLUCION'

	SELECT @rol = idRol FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario

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

		SELECT @gestor = '%'
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

	SELECT 
		@valorTotal = COUNT(c.CV_CREDITO), 
		@porcentaje = 100
	FROM dbo.Creditos c with (NOLOCK)
	INNER JOIN dbo.Dominio d with (nolock) ON d.nom_corto = c.TX_NOMBRE_DESPACHO			
	WHERE c.CV_Ruta = @ruta
	AND (@delegacion = '%' OR c.CV_DELEGACION = @delegacion )
	AND d.idDominio > 1
	AND d.idDominio = CASE  WHEN @despacho = '%' THEN d.idDominio ELSE @despacho END 
			
	SELECT @valorGestion = COUNT(c.num_cred)
	FROM Ordenes c WITH (NOLOCK)
	WHERE c.cvruta = @ruta
	AND ( C.Estatus IN (3, 4) OR C.idVisita > 1	)
	AND c.idUsuarioPadre = 
		CASE 
			WHEN @supervisor = '%' THEN c.idUsuarioPadre
		ELSE @supervisor
		END
	AND c.idUsuario = 
		CASE 
			WHEN @gestor = '%' THEN c.idUsuario
		ELSE @gestor
		END
	AND c.idDominio = 
		CASE 
			WHEN @despacho = '%' THEN c.idDominio
		ELSE @despacho
		END
	AND ( @delegacion = '%'	OR c.cvDelegacion = @delegacion	)
			
	
	SELECT @valor = @valorTotal - @valorGestion

	SELECT @porcentaje = 
		CASE 
			WHEN @contraPorcentaje <> 0 THEN (@valor * 100) / @contraPorcentaje
		ELSE '0'
		END
		
	SELECT @valor + '|' + @porcentaje + '|' + @nombreDisplay + '|' + @parte
END
