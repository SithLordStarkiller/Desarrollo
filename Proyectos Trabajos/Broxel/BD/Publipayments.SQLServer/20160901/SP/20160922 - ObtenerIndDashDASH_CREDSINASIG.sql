-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_CREDSINASIG]
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
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones with (nolock) WHERE fc_Clave = 'DASH_CREDSINASIG' 
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

	SELECT @valor = COUNT(o.num_Cred) FROM dbo.Ordenes o with (nolock) 
	where o.Estatus in(1,11,12,15) AND o.cvRuta =@ruta and o.idUsuario=0 AND o.idDominio>1
	and o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
	and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
	and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end
	and (@delegacion='%' or o.cvDelegacion=@delegacion) 

	select @porcentaje= case when @contraPorcentaje<>0 then (@valor*100)/@contraPorcentaje else '0' end
	
END
END
ELSE
BEGIN
	select @valor=0,@porcentaje=0
END

	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay+'|'+@parte
END
