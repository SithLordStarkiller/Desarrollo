
/****** Object:  StoredProcedure [dbo].[ObtenerRespuestas]    Script Date: 30/08/2016 20:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Maximiliano Silva
* Fecha de creación:	02/04/2014
* Descripción:			Obtiene las respuestas de las ordenes asignadas
	@tipo		Cantidad de campos, 0 = todos
	@idOrden	Numero de la orden o parte
	@num_Cred	Numero del credito
	@fechaAlta	Fecha de la asignacion
	
* Modificado: Pablo Eduardo Jaimes Vargas
* Fecha de Modificacion: 17/03/2015
* Descripcion: Se agrega validacion por roles para carga de respuestas
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerRespuestas] (
	@tipo INT = 0
	,@idOrden VARCHAR(20) = ''
	,@reporte INT = 0
	,@idUsuarioPadre INT = 0
	,@restriccion BIT =1
	)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @campos NVARCHAR(max)
		,@sql NVARCHAR(max)
		,@idFormulario INT
		,@idRol INT,
		@esCallCenterOut bit

	DECLARE @TablaRespuestasUsuario VARCHAR(50) = ''
	SET @TablaRespuestasUsuario = '##tablaTemporal' + CONVERT(VARCHAR(10), @idUsuarioPadre);
	IF OBJECT_ID('tempdb.dbo.' + @TablaRespuestasUsuario) IS NOT NULL
	BEGIN
		SET @sql = 'DROP TABLE ' + @TablaRespuestasUsuario;
		EXEC (@sql)
	END

	
	SELECT top 1 @idFormulario = idFormulario
	FROM Respuestas WITH(NOLOCK)
	WHERE  idOrden=@idOrden
	
	SELECT top 1 @idRol = idRol,@esCallCenterOut= EsCallCenterOut
	FROM Usuario WITH(NOLOCK)
	WHERE  idUsuario=@idUsuarioPadre


	IF  @esCallCenterOut IS NOT NULL 
		SET @restriccion=0

	SELECT @campos = isnull(+ @campos + ', ', '') + '[' + Nombre + ']'
	FROM CamposRespuesta  WITH(NOLOCK)
	WHERE  Tipo >= @tipo  and idFormulario=@idFormulario

	SET @sql = 'SELECT idOrden, ' + @campos + ' '
	SET @sql += 'INTO ' + @TablaRespuestasUsuario + ' '
	SET @sql += 'FROM( SELECT r.idOrden,c.Nombre,r.Valor '
	SET @sql += 'FROM [CamposRespuesta] c WITH(NOLOCK) '
	SET @sql += 'LEFT JOIN [Respuestas] r  WITH(NOLOCK)'
	SET @sql += 'ON c.idCampo = r.idCampo and c.idFormulario=r.idFormulario '

	IF (@idOrden > 0)
		SET @sql += 'WHERE CONVERT(VARCHAR(12), r.idOrden) LIKE ''%' + @idOrden + '%'''
	SET @sql += ') d PIVOT ('
	SET @sql += 'MAX(Valor) '
	SET @sql += 'FOR Nombre IN '
	SET @sql += '( ' + @campos
	SET @sql += ') ) piv '
	SET @sql += 'WHERE idOrden > 0; '

	EXEC (@sql)
	
		SET @sql='SELECT l.ID_ARCHIVO id_Carga'
		SET @sql+=',l.CV_CREDITO num_cred'
		SET @sql+=',l.CV_ETIQUETA desc_etiq'
		SET @sql+=',l.TX_SOLUCIONES soluciones'
		SET @sql+=',l.TX_NOMBRE_ACREDITADO nombre'
		SET @sql+=',l.TX_CALLE calle'
		SET @sql+=',l.TX_COLONIA colonia'
		SET @sql+=',l.TX_MUNICIPIO municipio'
		SET @sql+=',l.CV_CODIGO_POSTAL cp'
		SET @sql+=',cd.Descripcion estado'
		SET @sql+=',l.CV_DESPACHO nom_corto'
		SET @sql+=',u.Usuario'
		SET @sql+=',o.idVisita'
		SET @sql+=',o.Estatus'
		SET @sql+=',o.FechaAlta'
		SET @sql+=',o.num_Cred'
		SET @sql+=',o.FechaModificacion'
		SET @sql+=',o.FechaEnvio'
		SET @sql+=',ISNULL(Dictamen.Valor, ''Sin dictamen'') AS Dictamen'
		SET @sql+=',t.* '
		SET @sql+='FROM '+ @TablaRespuestasUsuario +' t '
		SET @sql+='INNER JOIN dbo.Ordenes o WITH(NOLOCK) ON t.idOrden = o.idOrden '
		SET @sql+='INNER JOIN dbo.Creditos l WITH(NOLOCK) ON o.num_Cred = l.CV_CREDITO '
		SET @sql+='INNER JOIN dbo.Usuario u WITH(NOLOCK) ON o.idUsuario = u.idUsuario '
		SET @sql+='INNER JOIN CatDelegaciones cd WITH(NOLOCK) ON l.CV_DELEGACION = cd.Delegacion '
		SET @sql+='LEFT JOIN ( '
		SET @sql+='SELECT r.idOrden '
		SET @sql+=',r.Valor '
		SET @sql+='FROM Respuestas r WITH(NOLOCK) '
		SET @sql+='INNER JOIN CamposRespuesta c WITH(NOLOCK) ON r.idCampo = c.idCampo and r.idFormulario=r.idFormulario '
		SET @sql+='WHERE c.Nombre LIKE ''dictamen%'''
		SET @sql+=') Dictamen ON Dictamen.idOrden = o.idOrden '
		SET @sql+='WHERE o.Estatus IN (3,4) '
		IF(@restriccion=1)	
			SET @sql+=' AND o.idUsuarioPadre='+ CONVERT(NVARCHAR(10), @idUsuarioPadre)
		ELSE
			SET @sql+=' AND o.idUsuarioPadre= o.idUsuarioPadre '

	EXEC (@sql)
	
	IF OBJECT_ID('tempdb.dbo.' + @TablaRespuestasUsuario) IS NOT NULL
	BEGIN
		SET @sql = 'DROP TABLE ' + @TablaRespuestasUsuario;
		EXEC (@sql)
	END
END
