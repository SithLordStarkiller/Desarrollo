
/****** Object:  StoredProcedure [dbo].[InsertaAutorizaciones]    Script Date: 02/07/2015 05:45:24 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/11
-- Description:	Inserta registro en tabla de AutorizacionSMS para enviar los SMS
-- =============================================
ALTER PROCEDURE [dbo].[InsertaAutorizaciones] 
AS
BEGIN

	declare @Telefonos TABLE(num_cred varchar(15),idorden INT,telefono char(10),Clave varchar(10),FechaAlta Datetime,fecharecepcion Datetime)
	
	insert into @Telefonos 
	Select O.num_Cred,O.idOrden,rr.valor as Telefono,RIGHT((select convert(bigint, convert (varbinary(8), NEWID(), 1))),8) as Clave,GETDATE() FechaAlta ,O.fecharecepcion
 from Ordenes O WITH (NOLOCK) right join 
 (
	select r.idOrden,r.valor ,r.idUsuarioPadre,r.idDominio from Respuestas r  WITH (NOLOCK) 
	inner join CamposRespuesta cr WITH (NOLOCK) on cr.idCampo=r.idCampo 
	where cr.Nombre like 'CelularSMS_%' and cr.Nombre != 'CelularSMS_Ant'
 )  rr
   on rr.idOrden=O.idOrden and rr.idUsuarioPadre=O.idUsuarioPadre and rr.idDominio=O.idDominio
   where not exists (select telefono from AutorizacionSMS A WITH (NOLOCK) where  A.Telefono=rr.valor ) 
   and 
   convert(char(2), tipo)='S'  
 
 
INSERT INTO [AutorizacionSMS]
				   ([num_Cred]
				   ,[idOrden]
				   ,[Telefono]
				   ,[Clave]
				   ,[FechaAlta]
				   )
	select num_cred,idorden,telefono,Clave,FechaAlta from @Telefonos  where idOrden  in (
		select  idorden from @Telefonos where telefono in (select telefono from @Telefonos group by telefono having count(telefono)=1)
		) or idorden in (
				select top 1 idorden from @Telefonos where telefono in (select telefono from @Telefonos group by telefono having count(telefono)>1) order by fecharecepcion desc
				)

END

