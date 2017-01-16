
/****** Object:  StoredProcedure [dbo].[ObtenerIndDashDASH_GESTNVAVISITA]    Script Date: 04/04/2016 11:29:11 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_GESTNVAVISITA]
	@idUsuario  int,
	@delegacion varchar(10),
	@despacho  varchar(10),
	@supervisor  varchar(10),
	@gestor  varchar(10),
	@tipoFormulario varchar(50)
	,@contraPorcentaje int,
	@callCenter varchar(6)='false'
AS
BEGIN

DECLARE @ruta varchar(10)='',@valor varchar(10)='',@porcentaje varchar(10)='',@parte varchar(10)='',@nombreDisplay varchar(100)='',@rol int
select @ruta=Ruta from dbo.Formulario with (nolock) where idFormulario=@tipoFormulario
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones with (nolock) WHERE fc_Clave = 'DASH_GESTNVAVISITA' 
select @rol=idRol from dbo.Usuario with (nolock) where idUsuario=@idUsuario

IF @rol in(2,3,4)
Begin
	select @despacho=idDominio from dbo.Usuario with (nolock) where	idUsuario=@idUsuario
END

IF @rol in(3)
Begin
	select @supervisor=@idUsuario
END

IF @rol in(4)
Begin
	select @supervisor=idPadre from dbo.RelacionUsuarios with (nolock) where idHijo=@idUsuario
	select @gestor=@idUsuario
END

IF @rol in(5)
Begin
	select @delegacion=Delegacion from dbo.RelacionDelegaciones with (nolock) where idUsuario=@idUsuario
END

IF @callCenter='false'
BEGIN
IF @rol in(0,1,2,3,4,5,6)
BEGIN

DECLARE @tablaCredito TABLE (CV_CREDITO VARCHAR(50) PRIMARY KEY)
INSERT INTO @tablaCredito 
	SELECT o.num_Cred FROM dbo.Ordenes o with (nolock) 
	where o.Estatus in(1,11,12,15) and o.idVisita>1  
	and o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
	and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
	and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end
	
	
	SELECT @valor=COUNT(c.cv_credito) FROM  dbo.Creditos c WITH (NOLOCK)  
	inner join  dbo.Dominio d with (nolock)  on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	INNER JOIN @tablaCredito o ON c.cv_credito = o.CV_CREDITO
	WHERE 
	 d.idDominio=case when @despacho='%' then d.idDominio else @despacho end
	and d.idDominio>1
	AND c.CV_Ruta=@ruta 
	AND c.CV_DELEGACION = CASE WHEN @delegacion='%' THEN c.CV_DELEGACION ELSE @delegacion END
	

	--SELECT @valor=COUNT(c.cv_credito)
	--from  @tablaCredito c 
	--inner join  dbo.Ordenes o with (nolock) on o.num_Cred=c.CV_CREDITO 
	--where o.Estatus in(1,11,12,15) and o.idVisita>1  
	--and (@supervisor='%' OR o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end)
	--and (@gestor='%' OR o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end)
	
	select @porcentaje= case when @contraPorcentaje<>0 then(@valor*100)/@contraPorcentaje else '0' end
END
END
ELSE
BEGIN
	select @valor=0,@porcentaje=0
END
	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay + '|'+@parte
END
