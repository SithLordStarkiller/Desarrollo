-- =============================================
-- Author:		Laura Anayeli Dotor Mejia
-- Create date: 26/09/2016
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashRDST_CREDPAGOS]
	@idUsuario INT,
	@delegacion VARCHAR(10),
	@despacho VARCHAR(10),
	@supervisor VARCHAR(10),
	@gestor VARCHAR(10),
	@tipoFormulario VARCHAR(50),
	@contraPorcentaje INT,
	@callCenter VARCHAR(6)='false'
AS
BEGIN

	DECLARE 
	@RUTA VARCHAR(10)='',
	@TOTAL_REGISTROS VARCHAR(10)='',
	@PORCENTAJE VARCHAR(10)='',
	@PARTE VARCHAR(10)='',
	@NOMBRE_DISPLAY VARCHAR(100)='',
	@ID_ROL INT

	SELECT @RUTA = Ruta FROM Formulario WITH (NOLOCK) WHERE idFormulario = @tipoFormulario

	SELECT @PARTE = fi_Parte, @NOMBRE_DISPLAY = fc_Descripcion FROM dbo.Utils_Descripciones WITH (NOLOCK) WHERE fc_Clave = 'RDST_CREDPAGOS' 

	SELECT @ID_ROL = idRol FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario

	IF @ID_ROL IN(2,3,4)
	BEGIN
		SELECT @despacho = idDominio FROM Usuario WITH (NOLOCK) WHERE idUsuario = @idUsuario
	END

	IF @ID_ROL IN(5)
	BEGIN
		SELECT @delegacion = Delegacion FROM RelacionDelegaciones WITH (NOLOCK) WHERE idUsuario = @idUsuario
	END
	
	SELECT 
		@TOTAL_REGISTROS = COUNT(c.cv_credito)
	FROM Pagos a WITH (NOLOCK)
	INNER JOIN CatEstatusPagos cat WITH (NOLOCK) ON a.CV_ESTATUS_PAGO = cat.ID_ESTATUS
	INNER JOIN Creditos c WITH (NOLOCK) ON a.CV_CREDITO = c.CV_CREDITO 
	LEFT JOIN Ordenes o WITH (NOLOCK) ON a.CV_CREDITO  = o.num_Cred
	INNER JOIN Dominio d WITH (NOLOCK) ON d.nom_corto = c.TX_NOMBRE_DESPACHO
	WHERE
	c.cv_Ruta = @RUTA
	AND d.idDominio > 1 
	AND d.idDominio = 
		CASE 
			WHEN @despacho='%' THEN d.idDominio 
		ELSE @despacho 
		END
	AND (@delegacion='%' OR c.cv_Delegacion = @delegacion) 

	SELECT @PORCENTAJE = CASE WHEN @contraPorcentaje <> 0 THEN(@TOTAL_REGISTROS*100)/@contraPorcentaje ELSE '0' END
	
	SELECT @TOTAL_REGISTROS + '|' + @PORCENTAJE + '|' + @NOMBRE_DISPLAY + '|' + @PARTE
END
