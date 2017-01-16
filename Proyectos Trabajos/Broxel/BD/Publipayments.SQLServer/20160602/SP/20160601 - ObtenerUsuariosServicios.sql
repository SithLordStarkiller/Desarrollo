
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015-08-24
-- Description:	Obtiene los datos para conectar a servicios externos (email,sms)
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerUsuariosServicios] (@Tipo int,@idAplicacion int=0)
AS
BEGIN
	SET NOCOUNT ON;
OPEN SYMMETRIC KEY ServiciosPW_Key11
   DECRYPTION BY CERTIFICATE ServiciosPW;
 

 IF(@idAplicacion=0)
	select @idAplicacion= valor from catalogogeneral where llave ='idaplicacion'

SELECT Usuario,CONVERT(nvarchar,DecryptByKey(Password, 1 , HashBytes('SHA1', CONVERT(varbinary, idAplicacion)))) AS 'Password' ,Orden,CUST.tipo,CUST.Descripcion AS TipoElemento ,US.Nombre,US.Servidor,US.Extra,Us.Descripcion
 FROM usuariosServicios US  WITH (NOLOCK) inner join CatUsuariosServiciosTipo CUST  WITH (NOLOCK) on  US.Tipo=CUST.Tipo  where  idAplicacion=@idAplicacion and US.Tipo=@Tipo order by orden asc


END
