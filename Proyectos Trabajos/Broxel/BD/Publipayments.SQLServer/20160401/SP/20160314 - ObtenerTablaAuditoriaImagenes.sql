
/****** Object:  StoredProcedure [dbo].[ObtenerTablaAuditoriaImagenes]    Script Date: 25/02/2016 05:44:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 26/01/2015
-- Description:	Crea tabla para auditoria de imagenes
-- Modificacion: JARO 2016/02/29 -  Se agrega el id del usuario que se encuentra haciendo la solicitud de la informacion 
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerTablaAuditoriaImagenes] 
	 @delegacion VARCHAR(4)
	,@despacho VARCHAR(4)
	,@supervisor VARCHAR(4)
	,@gestor VARCHAR(4)
	,@dictamen VARCHAR(150)
	,@status VARCHAR(4)
	,@autoriza VARCHAR(4)
	,@TipoFormulario VARCHAR(10)
	,@valorOcr VARCHAR(150)
	,@idUsuarioAuditoria VARCHAR(4)
AS
BEGIN
	DECLARE @SQL VARCHAR(max)

	SET @SQL = 
		'SELECT idOrden, num_Cred, Nombre, Valor, NombreDominio, Supervisor, Gestor, Delegacion, resultadoGeneral, resultadoImagen,' 
		+'isnull(idAuditoriaImagenes, 0) idAuditoriaImagenes,tipoAutorizado FROM ( 	SELECT d.NombreDominio, us.Nombre AS Supervisor,'
		+'ug.Nombre AS Gestor, o.idOrden, o.num_Cred, cr.Nombre, r.Valor, CASE 	WHEN cr.Nombre LIKE ''Dictamen%'''				
		+'THEN 1 ELSE 2	END AS orden, cd.Descripcion AS Delegacion, ai.resultado AS resultadoGeneral, '
		+'aci.resultado AS resultadoImagen, ai.idAuditoriaImagenes,case when o.Tipo=''S'' then ceo.Estado + '' SMS'' when o.Tipo=''C'' then ceo.Estado + '' Call Center'' else ceo.Estado end as tipoAutorizado	FROM Respuestas r WITH(NOLOCK)	LEFT JOIN Ordenes '
		+'o WITH(NOLOCK) ON o.idOrden = r.idOrden	left join CatEstatusOrdenes ceo WITH(NOLOCK) on ceo.Codigo=o.Estatus LEFT JOIN Creditos c WITH(NOLOCK) ON c.CV_CREDITO = o.num_Cred	LEFT JOIN CatDelegaciones cd WITH(NOLOCK) ON cd.Delegacion = c.CV_DELEGACION	LEFT JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = r.idCampo	LEFT JOIN Dominio d WITH(NOLOCK) ON d.idDominio = o.idDominio	LEFT JOIN Usuario ug WITH(NOLOCK) ON ug.idUsuario = o.idUsuario	LEFT JOIN Usuario us WITH(NOLOCK) ON us.idUsuario = o.idUsuarioPadre	LEFT JOIN AuditoriaImagenes ai WITH(NOLOCK) ON ai.num_credito = o.num_Cred	LEFT JOIN AuditoriaCamposImagen aci WITH(NOLOCK) ON aci.idAuditoriaImagenes = ai.idAuditoriaImagenes AND aci.imagen = r.Valor '

	IF @dictamen <> ''
		AND @dictamen <> '9999'
	BEGIN
		SET @SQL = @SQL + 'inner join (select o.idOrden from Respuestas r WITH(NOLOCK) inner join Ordenes o WITH(NOLOCK) on o.idOrden=r.idOrden where o.Estatus=4 and r.idCampo in(select idCampo from CatDictamenRespuesta WITH(NOLOCK) where Valor=''' + @dictamen + ''')) nnn on nnn.idOrden=o.idOrden '
	END

	IF @valorOcr <> ''
		AND @valorOcr <> '9999'
	BEGIN
		SET @SQL = @SQL + ' inner join (select o.idOrden from Respuestas r WITH(NOLOCK) inner join Ordenes o WITH(NOLOCK) on o.idOrden=r.idOrden where o.Estatus=4 AND r.valor ='''+@valorOcr+''') zzz on zzz.idOrden=o.idOrden '
	END

	SET @SQL = @SQL + ' WHERE o.Estatus in (4) '

	IF @delegacion <> ''
		AND @delegacion <> '9999'
	BEGIN
		SET @SQL = @SQL + ' and c.CV_DELEGACION=''' + @delegacion + ''' '
	END

	IF @despacho <> ''
		AND @despacho <> '9999'
	BEGIN
		SET @SQL = @SQL + ' and o.idDominio=''' + @despacho + ''' '
	END

	IF @supervisor <> ''
		AND @supervisor <> '9999'
	BEGIN
		SET @SQL = @SQL + ' and o.idUsuarioPadre=''' + @supervisor + ''' '
	END

	IF @gestor <> ''
		AND @gestor <> '9999'
	BEGIN
		SET @SQL = @SQL + ' and o.idUsuario=''' + @gestor + ''' '
	END

	IF @status <> ''
		AND @status <> '9999'
	BEGIN
		IF @status = '2'
		BEGIN
			SET @SQL = @SQL + 'and o.num_Cred not in (select num_credito from AuditoriaImagenes WITH(NOLOCK)) '
		END
		ELSE
		BEGIN
			SET @SQL = @SQL + 'and o.num_Cred in (select num_credito from AuditoriaImagenes WITH(NOLOCK) where resultado=''' + @status + ''') '
		END
	END

	IF @autoriza <> ''
		AND @autoriza <> '9999'
	BEGIN
		SET @SQL = @SQL + 'and CAST(o.Estatus as varchar(2)) + o.Tipo =''' + @autoriza + ''' '
	END

	SET @SQL = @SQL + ' and(cr.Nombre like ''Dictamen%'' or r.Valor like ''https:/%'') and c.CV_RUTA=''' + @TipoFormulario + ''' ) as fre order by idOrden,orden'


	EXECUTE sp_sqlexec @SQL
	
END
