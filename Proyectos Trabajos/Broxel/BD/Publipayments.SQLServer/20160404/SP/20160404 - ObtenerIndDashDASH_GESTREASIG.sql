
/****** Object:  StoredProcedure [dbo].[ObtenerIndDashDASH_GESTREASIG]    Script Date: 04/04/2016 11:25:48 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_GESTREASIG]
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
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones with (nolock) WHERE fc_Clave = 'DASH_GESTREASIG' 
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

DECLARE @tablaCredito TABLE (CV_CREDITO VARCHAR(50) PRIMARY KEY,idOrden int)
	insert into @tablaCredito
	select o.num_Cred,o.idOrden 
	from Ordenes o with (nolock)
	where o.Estatus=3 and o.idUsuario<>0 
	and o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
	and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
	and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end
	
	DECLARE @tablaRespuestas TABLE (idOrden int)
	insert into  @tablaRespuestas
	select idOrden from CamposRespuesta r with (nolock) 
	inner join Respuestas cr with (nolock) on r.idCampo=cr.idCampo
	where r.Nombre IN ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible')  

	SELECT @valor=COUNT(c.CV_CREDITO)
	from Creditos c with (nolock)  
	inner join Dominio d with (nolock) on d.nom_corto=c.TX_NOMBRE_DESPACHO 
	inner join @tablaCredito o  on o.CV_CREDITO=c.CV_CREDITO 	
	inner join @tablaRespuestas r on r.idOrden=o.idOrden
	where CV_Ruta=@ruta and d.idDominio>1
	and (@delegacion='%' or c.CV_DELEGACION=@delegacion) 
	and d.idDominio=case when @despacho='%' then d.idDominio else @despacho end
	
	
	select @porcentaje= case when @contraPorcentaje<>0 then(@valor*100)/@contraPorcentaje else '0' end
	
	
END
END
ELSE
BEGIN
	select @valor=0,@porcentaje=0
END


	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay+'|'+@parte
END
