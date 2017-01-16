GO
/****** Object:  StoredProcedure [dbo].[InsertaAutorizaciones]    Script Date: 27/02/2015 07:51:57 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/11
-- Description:	Inserta registro en tabla de AutorizacionSMS para enviar los SMS
-- =============================================
CREATE PROCEDURE [dbo].[InsertaAutorizaciones] 
AS
BEGIN

INSERT INTO [AutorizacionSMS]
				   ([num_Cred]
				   ,[idOrden]
				   ,[Telefono]
				   ,[Clave]
				   ,[FechaAlta]
				   )
	Select O.num_Cred,O.idOrden,rr.valor as Telefono,RIGHT((select convert(bigint, convert (varbinary(8), NEWID(), 1))),8) as Clave,GETDATE() FechaAlta 
 from Ordenes O right join 
 (
 select r.idOrden,r.valor ,r.idUsuarioPadre,r.idDominio from Respuestas r  inner join CamposRespuesta cr on cr.idCampo=r.idCampo and cr.idFormulario=r.idFormulario where cr.Nombre like 'CelularSMS_%' and cr.Nombre != 'CelularSMS_Ant'
 )  rr on rr.idOrden=O.idOrden and rr.idUsuarioPadre=O.idUsuarioPadre and rr.idDominio=O.idDominio
   where not exists (select Telefono from AutorizacionSMS A where A.idOrden = O.idOrden and A.num_Cred=O.num_Cred ) and tipo='S'  
END

