-- =============================================
-- Author:		Laura Anayeli Dotor Mejia
-- Create date: 26/09/2016
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerIndDashRDST_MARCAJENOEXITOSO]
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
		@ruta  varchar(10)='',
		@valor varchar(10)='',
		@porcentaje varchar(10)='',
		@parte varchar(10)='',
		@nombreDisplay varchar(100)='',
		@rol int, 
		@dominio varchar(100)=''

	SELECT @ruta = Ruta FROM Formulario WITH (NOLOCK) WHERE idFormulario = @tipoFormulario

	SELECT @parte = fi_Parte, @nombreDisplay = fc_Descripcion FROM dbo.Utils_Descripciones WITH (NOLOCK) WHERE fc_Clave = 'RDST_MARCAJENOEXITOSO' 

	SELECT @rol = idRol FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario


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

	IF @callCenter = 'false'
	BEGIN
		IF @rol in(0,1,2,3,4,5,6)
		BEGIN	
			SELECT 
				@valor = COUNT(C.cv_credito)
			FROM LlamadasSinExito a WITH (NOLOCK)
			INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
			LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
			INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
			WHERE
			c.cv_Ruta = @ruta
			AND d.idDominio > 1 
			AND d.idDominio = 
				CASE 
					WHEN @despacho='%' THEN d.idDominio 
				ELSE @despacho 
				END
			AND (@delegacion='%' OR c.cv_Delegacion = @delegacion) 

			SELECT @porcentaje = CASE WHEN @contraPorcentaje <> 0 THEN(@valor*100)/@contraPorcentaje ELSE '0' END
	
		END
	END
	ELSE
	BEGIN
		SELECT @valor = 0,@porcentaje = 0
	END

	SELECT @valor + '|' + @porcentaje + '|' + @nombreDisplay + '|' + @parte
END
