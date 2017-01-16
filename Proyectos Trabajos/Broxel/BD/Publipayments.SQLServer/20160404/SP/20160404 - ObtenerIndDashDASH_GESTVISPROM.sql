
/****** Object:  StoredProcedure [dbo].[ObtenerIndDashDASH_GESTVISPROM]    Script Date: 04/04/2016 11:59:05 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
declare @tablaCredito TABLE (numCredito VARCHAR(50) PRIMARY KEY,idUsuario int)
	insert into @tablaCredito 
	select o.num_Cred,idUsuario from Ordenes o with (nolock)
	where o.idUsuario <> 0 AND o.Estatus IN (3,4)  
	and o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
	and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
	and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end

	SELECT @contraPorcentaje=COUNT(c.CV_CREDITO) 
	from Creditos c with (nolock)
	left join @tablaCredito o  on o.numCredito=c.CV_CREDITO 
	left join Dominio d with (nolock) on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	where CV_Ruta=@ruta and d.idDominio>1
	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
	and d.idDominio=case when @despacho='%' then d.idDominio else @despacho end

	SELECT @valor=(CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.numCredito), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END)						
	from Creditos c with (nolock)
	left join @tablaCredito o on o.numCredito=c.CV_CREDITO 
	left join Dominio d with (nolock) on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	where CV_Ruta=@ruta and d.idDominio>1
	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
	and d.idDominio=case when @despacho='%' then d.idDominio else @despacho end
	
	select @porcentaje= case when @contraPorcentaje<>0 then(@valor*100)/@contraPorcentaje else '0' end
END
IF @rol=4
BEGIN
--SELECT @contraPorcentaje=COUNT(*) 
--	from Creditos c  with (nolock)
--	left join Ordenes o with (nolock) on o.num_Cred=c.CV_CREDITO 
--	left join Dominio d with (nolock) on d.nom_corto=c.TX_NOMBRE_DESPACHO 
--	where CV_Ruta=@ruta  and o.Estatus IN (3,4) AND o.idUsuario <> 0 and d.idDominio>1
--	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
--	and (@despacho='%' or d.idDominio=case when @despacho='%' then -99 else @despacho end)
--	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -99 else @supervisor end)
--	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -99 else @gestor end)
	
	
SELECT @valor=(CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.idOrden), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END)
	from (select CV_CREDITO,TX_NOMBRE_DESPACHO,CV_Ruta,CV_DELEGACION from Creditos with (nolock)) c  
	left join (select Estatus,idUsuario,idUsuarioPadre,idOrden,num_Cred from Ordenes with (nolock)) o on o.num_Cred=c.CV_CREDITO 
	left join (select idDominio,nom_corto from Dominio with (nolock)) d on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	where CV_Ruta=@ruta and o.Estatus IN (3,4) AND o.idUsuario <> 0 and d.idDominio>1
	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
	and (@despacho='%' or d.idDominio=case when @despacho='%' then -2 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -2 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -2 else @gestor end)	
	
	select @porcentaje= 100
END
END
ELSE
BEGIN
	select @valor=0,@porcentaje=0
END


	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay+'|'+@parte
END
