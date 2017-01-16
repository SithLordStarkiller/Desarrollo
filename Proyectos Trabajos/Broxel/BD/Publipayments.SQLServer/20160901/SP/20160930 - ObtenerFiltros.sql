-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 27/01/2016
-- Description:	Obtiene los filtros para el controller filtros
-- Modificación: JARO_2016/09/09_Se agrega la variable del idformulario para tener filtro por el formulario que se tiene seleccionado
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerFiltros]
	@idUsuario varchar(10),
	@delegacion  varchar(100),
	@despacho  varchar(100),
	@supervisor  varchar(100) = '%',
	@gestor  varchar(100),
	@idformulario varchar(3)='38'
AS
BEGIN
	
	declare @rol int=0,@CV_RUTA varchar(10)
	
	select @rol=idRol from Usuario with (nolock) where idUsuario =@idUsuario
	
	IF @rol in(2,3,4) 
	BEGIN
		select @despacho=idDominio from Usuario with (nolock) where idUsuario=@idUsuario 
	END
	
	IF @rol=3
	BEGIN
		set @supervisor=@idUsuario
	END

	SELECT @CV_RUTA = RUTA FROM formulario  WITH(NOLOCK) WHERE idformulario= CONVERT(INT, (CASE @idformulario WHEN '%' THEN '0' ELSE @idformulario END) )

	IF (@CV_RUTA = 'RDST')
	BEGIN
		select 'delegacion' TipoCampos,cast(Delegacion as varchar(100)) as Valor,cast(Descripcion as varchar(100)) as Descripcion from CatDelegaciones cd with (nolock) 
		inner join (select distinct CV_Delegacion from Creditos with (nolock) where CV_RUTA= CASE  WHEN @CV_RUTA IS NULL THEN CV_RUTA ELSE @CV_RUTA END) c on  c.CV_Delegacion=cd.Delegacion where 1=(case when @rol in(0,1,6) then 1 else 0 end)
			union all
		select 'despacho' TipoCampos,cast(d.idDominio as varchar(100)) as Valor,cast(d.nom_corto as varchar(20))+' - '+ cast(d.NombreDominio as varchar(100)) as Descripcion from Dominio d with (nolock) 
		inner join (select distinct TX_NOMBRE_DESPACHO from Creditos with (nolock) where (CV_DELEGACION = @delegacion or @delegacion='%') AND CV_RUTA= CASE  WHEN @CV_RUTA IS NULL THEN CV_RUTA ELSE @CV_RUTA END) c on  c.TX_NOMBRE_DESPACHO=d.nom_corto where 1=(case
	 when @rol in(0,1,6,5) then 1 else 0 end)
			union all
		select 'gestor' TipoCampos,cast(u.idUsuario as varchar(100)) as Valor,cast(Nombre as varchar(100)) as Descripcion from Usuario u with (nolock)
		inner join (select distinct idUsuario from Ordenes with (nolock) where idDominio=case when @despacho='%' then -2 else cast(@despacho as int) end AND CvRuta = CASE  WHEN @CV_RUTA IS NULL THEN CvRuta ELSE @CV_RUTA END) o 
		on  o.idUsuario=u.idUsuario where u.EsCallCenterOut = 1 and 1=(case when @rol in(4,6) then 0 else 1 end) order by 3
	END
	ELSE
	BEGIN
		select 'delegacion' TipoCampos,cast(Delegacion as varchar(100)) as Valor,cast(Descripcion as varchar(100)) as Descripcion from CatDelegaciones cd with (nolock) 
		inner join (select distinct CV_Delegacion from Creditos with (nolock) where CV_RUTA= CASE  WHEN @CV_RUTA IS NULL THEN CV_RUTA ELSE @CV_RUTA END) c on  c.CV_Delegacion=cd.Delegacion where 1=(case when @rol in(0,1,6) then 1 else 0 end)
			union all
		select 'despacho' TipoCampos,cast(d.idDominio as varchar(100)) as Valor,cast(d.nom_corto as varchar(20))+' - '+cast(d.NombreDominio as varchar(100)) as Descripcion from Dominio d with (nolock) 
		inner join (select distinct TX_NOMBRE_DESPACHO from Creditos with (nolock) where (CV_DELEGACION =@delegacion or @delegacion='%') AND CV_RUTA= CASE  WHEN @CV_RUTA IS NULL THEN CV_RUTA ELSE @CV_RUTA END) c on  c.TX_NOMBRE_DESPACHO=d.nom_corto where 1=(case
	 when @rol in(0,1,6,5) then 1 else 0 end)
			union all
		select 'supervisor' TipoCampos,cast(idUsuario as varchar(100)) as Valor,cast(Nombre as varchar(100)) as Descripcion from Usuario u with (nolock) 
		inner join (select distinct idUsuarioPadre from Ordenes with (nolock) where idDominio=case when @despacho='%' then -2 else cast(@despacho as int) end AND CvRuta= CASE  WHEN @CV_RUTA IS NULL THEN CvRuta ELSE @CV_RUTA END ) o 
		on  o.idUsuarioPadre=u.idUsuario where 1=(case when @rol in(0,1,5,2) then 1 else 0 end)
			union all
		select 'gestor' TipoCampos,cast(u.idUsuario as varchar(100)) as Valor,cast(Nombre as varchar(100)) as Descripcion from Usuario u with (nolock)
		inner join (select distinct idUsuario from Ordenes with (nolock) where idUsuarioPadre=case when @supervisor='%' then -2 else cast(@supervisor as int) end AND CvRuta= CASE  WHEN @CV_RUTA IS NULL THEN CvRuta ELSE @CV_RUTA END) o 
		on  o.idUsuario=u.idUsuario where 1=(case when @rol in(4,6) then 0 else 1 end) order by 3
	END
END
