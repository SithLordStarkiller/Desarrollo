
/****** Object:  StoredProcedure [dbo].[ObtenerIndDashDASH_DASH_TOTALCC]    Script Date: 04/04/2016 11:43:42 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_DASH_TOTALCC]
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
SELECT @parte=fi_Parte,@nombreDisplay=fc_Descripcion FROM dbo.Utils_Descripciones with (nolock) WHERE fc_Clave = 'DASH_TOTALCC' 
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
	SELECT @valor=0,@porcentaje= 0	
END
END
ELSE
BEGIN
	IF @rol in(0,1,2,3,4,5,6)
	BEGIN
	
	DECLARE @tablaCredito TABLE (CV_CREDITO VARCHAR(50) PRIMARY KEY)
	INSERT INTO @tablaCredito 
	SELECT o.num_Cred FROM dbo.Ordenes o with (nolock) 
	where o.Tipo like'%C%'
	
	
		SELECT @valor=COUNT(c.CV_CREDITO)
		from Creditos c with (nolock)  
		inner join Dominio d with (nolock) on d.nom_corto=c.CC_DESPACHO 
		inner join @tablaCredito o on o.CV_CREDITO=c.CV_CREDITO 		
		where CV_Ruta=@ruta  and d.idDominio>1		
		
		select @porcentaje= case when @contraPorcentaje<>0 then (@valor*100)/@contraPorcentaje else '0' end
				
	END
END


	SELECT @valor+'|'+@porcentaje+'|'+@nombreDisplay+'|'+@parte
END
