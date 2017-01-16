
/****** Object:  StoredProcedure [dbo].[ObtenerFiltros]    Script Date: 03/03/2016 11:17:00 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 27/01/2016
-- Description:	Obtiene los filtros para el controller filtros
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerFiltros]
	@idUsuario varchar(10),
	@delegacion  varchar(100),
	@despacho  varchar(100),
	@supervisor  varchar(100),
	@gestor  varchar(100)	
AS
BEGIN
	
	declare @rol int=0
	
	select @rol=idRol from Usuario with (nolock) where idUsuario =@idUsuario
	
	IF @rol in(2,3,4) 
	BEGIN
		select @despacho=idDominio from Usuario with (nolock) where idUsuario=@idUsuario 
	END
	
	IF @rol=3
	BEGIN
		set @supervisor=@idUsuario
	END
	
	select 'delegacion' TipoCampos,cast(Delegacion as varchar(100)) as Valor,cast(Descripcion as varchar(100)) as Descripcion from CatDelegaciones cd with (nolock) inner join (select distinct CV_Delegacion from Creditos with (nolock)) c on  c.CV_Delegacion=cd.Delegacion where 1=(case when @rol in(0,1,6) then 1 else 0 end)
	union all
	select 'despacho' TipoCampos,cast(d.idDominio as varchar(100)) as Valor,cast(d.nom_corto as varchar(20))+' - '+cast(d.NombreDominio as varchar(100)) as Descripcion from Dominio d with (nolock) inner join (select distinct TX_NOMBRE_DESPACHO from Creditos with (nolock) where (CV_DELEGACION =@delegacion or @delegacion='%')) c on  c.TX_NOMBRE_DESPACHO=d.nom_corto where 1=(case when @rol in(0,1,6,5) then 1 else 0 end)
	union all
	select 'supervisor' TipoCampos,cast(idUsuario as varchar(100)) as Valor,cast(Nombre as varchar(100)) as Descripcion from Usuario u with (nolock) inner join (select distinct idUsuarioPadre from Ordenes with (nolock) where idDominio=case when @despacho='%' then -2 else cast(@despacho as int) end) o on  o.idUsuarioPadre=u.idUsuario where 1=(case when @rol in(0,1,5,2) then 1 else 0 end)
	union all
	select 'gestor' TipoCampos,cast(u.idUsuario as varchar(100)) as Valor,cast(Nombre as varchar(100)) as Descripcion from Usuario u with (nolock) inner join (select distinct idUsuario from Ordenes with (nolock) where idUsuarioPadre=case when @supervisor='%' then -2 else cast(@supervisor as int) end) o on  o.idUsuario=u.idUsuario where 1=(case when @rol in(4,6) then 0 else 1 end) order by 3
	
	
	
END
