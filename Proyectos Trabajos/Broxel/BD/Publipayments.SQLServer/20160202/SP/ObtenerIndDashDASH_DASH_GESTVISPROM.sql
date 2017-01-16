SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
create PROCEDURE ObtenerIndDashDASH_GESTVISPROM
	@idUsuario  int,
	@delegacion varchar(10),
	@despacho  varchar(10),
	@supervisor  varchar(10),
	@gestor  varchar(10),
	@tipoFormulario varchar(50)
AS
BEGIN

DECLARE @ruta varchar(10)='',@valor varchar(10)='',@porcentaje varchar(10)='',@parte varchar(10)='',@nombreDisplay varchar(100)='',@rol int,@contraPorcentaje int
select @ruta=Ruta from Formulario where idFormulario=@tipoFormulario
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones WHERE fc_Clave = 'DASH_GESTVISPROM' 
select @rol=idRol from Usuario where idUsuario=@idUsuario

IF @rol in(2,3,4)
Begin
	select @despacho=idDominio from Usuario where	idUsuario=@idUsuario
END

IF @rol in(3)
Begin
	select @supervisor=@idUsuario
END

IF @rol in(4)
Begin
	select @supervisor=idPadre from RelacionUsuarios where idHijo=@idUsuario
	select @gestor=@idUsuario
END


IF @rol in(5)
Begin
	select @delegacion=Delegacion from RelacionDelegaciones where idUsuario=@idUsuario
END

IF @rol in(0,1,2,3,4,5,6)
BEGIN
	SELECT @contraPorcentaje=COUNT(*) 
	from Creditos c 
	left join Ordenes o on o.num_Cred=c.CV_CREDITO
	left join Dominio d on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	where CV_Ruta=@ruta  and d.idDominio>1
	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
	and (@despacho='%' or d.idDominio=case when @despacho='%' then -99 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -99 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -99 else @gestor end)
	
	
SELECT @valor=(CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.idOrden), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END),@porcentaje= case when @contraPorcentaje<>0 then(CASE WHEN isnull(COUNT(DISTINCT o.idUsuario), 0) = 0
							THEN '0'
						ELSE isnull(count(o.idOrden), 0) / isnull(COUNT(DISTINCT o.idUsuario), 0)
						END)/@contraPorcentaje else '0' end
	from Creditos c 
	left join Ordenes o on o.num_Cred=c.CV_CREDITO 
	left join Dominio d on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	where CV_Ruta=@ruta and o.Estatus IN (3,4) AND o.idUsuario <> 0 and d.idDominio>1
	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
	and (@despacho='%' or d.idDominio=case when @despacho='%' then -2 else @despacho end)
	and (@supervisor='%' or o.idUsuarioPadre=case when @supervisor='%' then -2 else @supervisor end)
	and (@gestor='%' or o.idUsuario=case when @gestor='%' then -2 else @gestor end)		
END


	SELECT @valor Valor,@porcentaje as Porcentaje,@nombreDisplay NombreDisplay,@parte Parte 
END
GO
