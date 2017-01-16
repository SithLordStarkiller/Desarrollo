/****** Object:  StoredProcedure [dbo].[ObtenerTablaReversos]    Script Date: 09/24/2015 13:17:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 11/02/2015
-- Description:	Obtiene tabla de ordenes para pantalla de reversos para los diferentes roles.
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerTablaReversos] 
	@Tipoformulario VARCHAR(10) = NULL, @idUsuario varchar(10) = NULL, @idRol  varchar(10) = NULL
AS
BEGIN
	DECLARE @SQL VARCHAR(max)

	SET @SQL = 'select o.idOrden,u.Usuario,u.Nombre,o.num_cred,case when o.Tipo=''S'' then ceo.Estado + '' SMS'' when o.Tipo=''C'' then ceo.Estado + '' CC'' when o.Tipo=''CS'' then ceo.Estado + '' SMS CC'' else ceo.Estado end as Estatus,
	o.idVisita,r.Valor as Dictamen,c.TX_COLONIA,
	c.TX_MUNICIPIO,CONVERT(varchar(19),o.FechaModificacion,121)as fechaModificacion,CONVERT(varchar(19),
	o.FechaRecepcion,121) as fechaRecepcion,CONVERT(varchar(19),o.FechaEnvio,121) as fechaEnvio,o.Auxiliar 
	from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred  left join Usuario u on u.idUsuario=o.idUsuario 
	left join CatEstatusOrdenes ceo on ceo.Codigo=o.Estatus left join Respuestas r on r.idOrden=o.idOrden 
	left join CamposRespuesta cr on cr.idCampo=r.idCampo where cr.Nombre like ''Dictamen%'' and o.Estatus in(4) 
	and cr.idFormulario in(select idFormulario from formulario where idAplicacion =(select Valor from CatalogoGeneral where id=3))
	and c.CV_RUTA= ''' + @Tipoformulario + ''' '

	IF @idRol = 3
	BEGIN
		SET @SQL = @SQL + ' and o.idUsuarioPadre=' + @idUsuario
	END

	EXECUTE sp_sqlexec @SQL
END
