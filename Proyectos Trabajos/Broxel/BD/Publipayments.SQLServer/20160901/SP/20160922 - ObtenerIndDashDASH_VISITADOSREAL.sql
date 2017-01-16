-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_VISITADOSREAL]
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

DECLARE @ruta varchar(10)='',@valor varchar(10)='',@porcentaje varchar(10)='',@parte varchar(10)='',@nombreDisplay varchar(100)='',@rol int
select @ruta=Ruta from Formulario with (nolock) where idFormulario=@tipoFormulario
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones with (nolock) WHERE fc_Clave = 'DASH_VISITADOSREAL' 
select @rol=idRol from Usuario with (nolock) where idUsuario=@idUsuario

IF @rol in(2,3,4)
Begin
	select @despacho=idDominio from Usuario with (nolock) where	idUsuario=@idUsuario
END

IF @rol in(3)
Begin
	select @supervisor=@idUsuario
END

IF @rol in(4)
Begin
	select @supervisor=idPadre from RelacionUsuarios with (nolock) where idHijo=@idUsuario
	select @gestor=@idUsuario
END

IF @rol in(5)
Begin
	select @delegacion=Delegacion from RelacionDelegaciones with (nolock) where idUsuario=@idUsuario
END

IF @callCenter='false'
BEGIN
IF @rol in(0,1,2,3,4,5,6)
BEGIN	
	DECLARE @x int = 0
	SELECT @x += SUM(o.idVisita - 1) FROM dbo.Ordenes o with (nolock) 
	where o.Estatus IN (1, 11, 12, 15) AND o.cvRuta=@ruta and o.idDominio>1 AND o.idVisita > 1 
	and o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
	and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
	and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end
	AND (@delegacion='%' or o.cvDelegacion=@delegacion) 

	SELECT @x += SUM(o.idVisita) FROM dbo.Ordenes o with (nolock) 
	where o.Estatus IN (3,4) AND  o.cvRuta=@ruta and   o.idUsuario > 0 AND o.idDominio>1
	and o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
	and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
	and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end
	AND (@delegacion='%' or o.cvDelegacion=@delegacion) 

	select @valor=isnull(@x,0)

		select @porcentaje= case when @contraPorcentaje<>0 then ((@valor)*100)/@contraPorcentaje else '0' end
		
END
END
ELSE
BEGIN
	IF @rol in(0,1,2,3,4,5,6)
	BEGIN	
		select @valor=(isnull(t1.valor,0)+isnull(t2.valor,0))
		 from(
	SELECT isnull(sum(o.idVisita - 1), 0) AS Valor,'DatoTablasPart' as Dato
		from (select CV_CREDITO,TX_NOMBRE_DESPACHO,CV_Ruta,CV_DELEGACION,CC_DESPACHO from creditos with (nolock)) c 
		left join (select num_Cred,idUsuarioPadre,idUsuario,Estatus,idVisita from Ordenes with (nolock)) o on o.num_Cred=c.CV_CREDITO 
		left join (select idDominio,nom_corto from Dominio with (nolock)) d  on d.nom_corto=c.CC_DESPACHO 
		where CV_Ruta=@ruta and o.Estatus IN (1, 11, 12, 15) AND o.idVisita > 1 and d.idDominio>1
		)t1 
		full outer join (	
	SELECT isnull(sum(o.idVisita), 0) AS Valor,'DatoTablasPart' as Dato
		from (select CV_CREDITO,TX_NOMBRE_DESPACHO,CV_Ruta,CV_DELEGACION,CC_DESPACHO from creditos with (nolock)) c 
		left join (select num_Cred,idUsuarioPadre,idUsuario,Estatus,idVisita from Ordenes with (nolock)) o  on o.num_Cred=c.CV_CREDITO 
		left join (select idDominio,nom_corto from Dominio with (nolock)) d on d.nom_corto=c.CC_DESPACHO 
		where CV_Ruta=@ruta and o.Estatus IN (3,4) AND o.idUsuario <> 0  and d.idDominio>1
		) t2 on t1.Dato=t2.Dato
			
			select @porcentaje= case when @contraPorcentaje<>0 then ((@valor)*100)/@contraPorcentaje else '0' end
	END
END


	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay+'|'+@parte
END
