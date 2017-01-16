-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_GESTVISPROM]
	@idUsuario  int,
	@delegacion varchar(10),
	@despacho  varchar(10),
	@supervisor  varchar(10),
	@gestor  varchar(10),
	@tipoFormulario varchar(50),
	@callCenter varchar(6)='false'
AS
BEGIN

DECLARE @ruta varchar(10)='',@valor varchar(10)='',@porcentaje varchar(10)='',@parte varchar(10)='',@nombreDisplay varchar(100)='',@rol int,@contraPorcentaje int
select @ruta=Ruta from Formulario with (nolock) where idFormulario=@tipoFormulario
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones with (nolock) WHERE fc_Clave = 'DASH_GESTVISPROM' 
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
IF @rol in(0,1,2,3,5,6)
BEGIN
	SELECT @contraPorcentaje=COUNT(o.idOrden) 
	from dbo.Ordenes o with (nolock) 
	where  o.Estatus IN (3,4) AND o.cvRuta=@ruta   AND o.idUsuario <> 0 and o.idDominio>1
	and (@delegacion='%' or o.cvDelegacion=@delegacion) 
	and (@despacho='%' or o.idDominio=case when @despacho='%' then -99 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -99 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -99 else @gestor end)
	
	
SELECT @valor=(CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.idOrden), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END),@porcentaje= case when @contraPorcentaje<>0 then((CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.idOrden), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END)*100)/@contraPorcentaje else '0' end
	from  Ordenes o with (nolock) 
	where o.Estatus IN (3,4) AND o.cvRuta=@ruta   AND o.idUsuario <> 0 and o.idDominio>1
	and (@delegacion='%' or o.cvDelegacion=@delegacion)
	and (@despacho='%' or o.idDominio=case when @despacho='%' then -2 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -2 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -2 else @gestor end)		
END
IF @rol=4
BEGIN
SELECT @contraPorcentaje=COUNT(o.idOrden) 
	FROM dbo. Ordenes o with (nolock) 
	where o.Estatus IN (3,4) AND o.cvRuta=@ruta   AND o.idUsuario <> 0 and o.idDominio>1
	and (@delegacion='%' or o.cvDelegacion=@delegacion)
	and (@despacho='%' or o.idDominio=case when @despacho='%' then -99 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -99 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -99 else @gestor end)
	
	
SELECT @valor=(CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.idOrden), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END),@porcentaje= 100
	from  Ordenes o with (nolock) 
	where o.Estatus IN (3,4) AND o.cvRuta=@ruta   AND o.idUsuario <> 0 and o.idDominio>1
	and (@delegacion='%' or o.cvDelegacion=@delegacion)
	and (@despacho='%' or o.idDominio=case when @despacho='%' then -2 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -2 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -2 else @gestor end)	
END
END
ELSE
BEGIN
	select @valor=0,@porcentaje=0
END


	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay+'|'+@parte
END
