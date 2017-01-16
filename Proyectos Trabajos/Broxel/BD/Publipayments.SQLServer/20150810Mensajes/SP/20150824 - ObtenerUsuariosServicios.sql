
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015-08-24
-- Description:	Obtiene los datos para conectar a servicios externos (email,sms)
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerUsuariosServicios] (@Tipo int)
AS
BEGIN
	SET NOCOUNT ON;
OPEN SYMMETRIC KEY ServiciosPW_Key11
   DECRYPTION BY CERTIFICATE ServiciosPW;
SELECT Usuario,CONVERT(nvarchar,DecryptByKey(Password, 1 , HashBytes('SHA1', CONVERT(varbinary, idAplicacion)))) AS 'Password' ,Orden,CUST.tipo,CUST.Descripcion
 FROM usuariosServicios US  WITH (NOLOCK) inner join CatUsuariosServiciosTipo CUST  WITH (NOLOCK) on  US.Tipo=CUST.Tipo  where  idAplicacion=(select valor from catalogogeneral where id=3) and US.Tipo=@Tipo order by orden asc


END
