SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 11/02/2015
-- Description:	Obtiene tabla de ordenes para pantalla de reversos para los diferentes roles.
-- =============================================
ALTER PROCEDURE ObtenerTablaReversos 
	@Tipoformulario VARCHAR(10) = NULL, @idUsuario varchar(10) = NULL, @idRol  varchar(10) = NULL
AS
BEGIN
	DECLARE @SQL VARCHAR(max)

	SET @SQL = 'select o.idOrden,u.Usuario,u.Nombre,o.num_cred,ceo.Estado as Estatus,o.idVisita,r.Valor as Dictamen,c.TX_COLONIA,c.TX_MUNICIPIO,CONVERT(varchar(19),o.FechaModificacion,131)as fechaModificacion,CONVERT(varchar(19),o.FechaRecepcion,131) as fechaRecepcion,CONVERT(varchar(19),o.FechaEnvio,131) as fechaEnvio,o.Auxiliar from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred  left join Usuario u on u.idUsuario=o.idUsuario left join CatEstatusOrdenes ceo on ceo.Codigo=o.Estatus left join Respuestas r on r.idOrden=o.idOrden left join CamposRespuesta cr on cr.idCampo=r.idCampo where cr.Nombre like ''Dictamen%'' and o.Estatus in(4,47) and c.CV_RUTA= ''' + @Tipoformulario + ''' '

	IF @idRol = 3
	BEGIN
		SET @SQL = @SQL + ' and o.idUsuarioPadre=' + @idUsuario
	END

	EXECUTE sp_sqlexec @SQL
END
GO


