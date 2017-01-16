
/****** Object:  StoredProcedure [dbo].[InsertaAutorizaciones]    Script Date: 14/05/2015 01:15:04 p.m. ******/
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

UPDATE ordenes set tipo=' ' WHERE idorden in (
	SELECT   TOP 1 o.idorden  from respuestas r WITH (NOLOCK) inner join ordenes o  WITH (NOLOCK) ON r.idorden=o.idorden  WHERE valor  in (
		SELECT  rr.valor as Telefono
			 FROM Ordenes O WITH (NOLOCK) right join 
			 (
				SELECT r.idOrden,r.valor ,r.idUsuarioPadre,r.idDominio FROM Respuestas r WITH (NOLOCK)  inner join CamposRespuesta cr WITH (NOLOCK)
				ON cr.idCampo=r.idCampo and cr.idFormulario=r.idFormulario 
				where cr.Nombre like 'CelularSMS_%' and cr.Nombre != 'CelularSMS_Ant'
			 )  rr ON rr.idOrden=O.idOrden and rr.idUsuarioPadre=O.idUsuarioPadre and rr.idDominio=O.idDominio
			WHERE not exists (select * from AutorizacionSMS A WITH (NOLOCK) where  A.Telefono=rr.valor ) and O.tipo='S'  GROUP BY rr.valor HAVING count(*) > 1
		)  and o.idusuario!=0 and o.estatus=3 and o.tipo='S' ORDER by fecharecepcion DESC
)


INSERT INTO [AutorizacionSMS]
				   ([num_Cred]
				   ,[idOrden]
				   ,[Telefono]
				   ,[Clave]
				   ,[FechaAlta]
				   )
	Select O.num_Cred,O.idOrden,rr.valor as Telefono,RIGHT((select convert(bigint, convert (varbinary(8), NEWID(), 1))),8) as Clave,GETDATE() FechaAlta 
 from Ordenes O WITH (NOLOCK) right join 
 (
	select r.idOrden,r.valor ,r.idUsuarioPadre,r.idDominio from Respuestas r  WITH (NOLOCK) 
	inner join CamposRespuesta cr WITH (NOLOCK) on cr.idCampo=r.idCampo and cr.idFormulario=r.idFormulario 
	where cr.Nombre like 'CelularSMS_%' and cr.Nombre != 'CelularSMS_Ant'
 )  rr on rr.idOrden=O.idOrden and rr.idUsuarioPadre=O.idUsuarioPadre and rr.idDominio=O.idDominio
   where not exists (select * from AutorizacionSMS A WITH (NOLOCK) where  A.Telefono=rr.valor ) and tipo='S'  
END

