-- =============================================
-- Author:		Laura Anayeli Dotor Mejia
-- Create date: 28/09/2016
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerIndDashRDST_GESTPROMASESOR]
	@idUsuario  int,
	@delegacion varchar(10),
	@despacho  varchar(10),
	@supervisor  varchar(10),
	@gestor  varchar(10),
	@tipoFormulario varchar(50),
	@contraPorcentaje int,
	@callCenter varchar(6)='false'
AS
BEGIN

	DECLARE 
		@cantidadGestoresCC INT = 0,
		@valorGestiones INT = 0,
		@ruta varchar(10)='',
		@valor varchar(10)='',
		@porcentaje varchar(10)='',
		@parte varchar(10)='',
		@nombreDisplay varchar(100)='',
		@rol int,
		@dominio int = 0
	
	SELECT @ruta = Ruta FROM Formulario WITH (NOLOCK) WHERE idFormulario = @tipoFormulario

	SELECT @parte = fi_Parte, @nombreDisplay = fc_Descripcion FROM dbo.Utils_Descripciones WITH (NOLOCK) WHERE fc_Clave = 'RDST_GESTPROMASESOR' 

	SELECT @rol = idRol FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario

	SELECT @dominio = idDominio FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario

	IF @rol in(2,3,4)
	BEGIN
		SELECT @despacho = idDominio FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario
	END

	IF @rol in(3)
	BEGIN
		SELECT @supervisor = @idUsuario
	END

	IF @rol in(4)
	BEGIN
		SELECT @supervisor = idPadre FROM RelacionUsuarios WITH (NOLOCK) WHERE idHijo = @idUsuario
		SELECT @gestor = @idUsuario
	END

	IF @rol in(5)
	BEGIN
		SELECT @delegacion = Delegacion FROM RelacionDelegaciones WITH (NOLOCK) WHERE idUsuario = @idUsuario
	END

	SELECT @cantidadGestoresCC = COUNT(idUsuario) FROM USUARIO WITH (NOLOCK) WHERE idRol = 4 AND EsCallCenterOut = 1 AND idDominio = CASE  WHEN @despacho = '%' THEN idDominio ELSE @despacho END 
	
	IF(@cantidadGestoresCC > 0)
	BEGIN
		IF @callCenter = 'false'
		BEGIN
			IF @rol in(0,1,2,3,4,5,6)
			BEGIN	
				SELECT 
					@valorGestiones = COUNT(c.cv_credito)
				FROM Creditos c WITH (NOLOCK)
				INNER JOIN Dominio d WITH (NOLOCK) ON d.nom_corto = c.TX_NOMBRE_DESPACHO
				INNER JOIN Ordenes o WITH (NOLOCK) ON o.num_Cred = c.cv_credito
				WHERE c.CV_Ruta = @ruta
				AND ( o.Estatus IN (3, 4) OR o.idVisita > 1	)
				AND o.idUsuario = CASE WHEN @gestor = '%' THEN o.idUsuario ELSE @gestor END
				AND (@delegacion = '%' OR c.CV_DELEGACION = @delegacion )
				AND d.idDominio > 1
				AND d.idDominio = CASE  WHEN @despacho = '%' THEN d.idDominio ELSE @despacho END 

				SELECT @valor = CAST ((@valorGestiones / @cantidadGestoresCC) AS VARCHAR (10))

				SELECT @porcentaje = CASE WHEN @contraPorcentaje <> 0 THEN(@valor*100)/@contraPorcentaje ELSE '0' END	
			END
		END
		ELSE
		BEGIN
			SELECT @valor = 0,@porcentaje = 0
		END
	END
	ELSE
	BEGIN
		SELECT @valor = 0,@porcentaje = 0
	END

	SELECT @valor + '|' + @porcentaje + '|' + @nombreDisplay + '|' + @parte
END