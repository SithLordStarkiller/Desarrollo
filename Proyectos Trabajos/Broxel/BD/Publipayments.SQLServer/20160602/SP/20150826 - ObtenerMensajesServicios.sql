
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/08/26
-- Description:	Obtiene los datos relacionados a la clave que se recibe
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerMensajesServicios]  @Clave varchar(50),@idAplicacion int=0
AS
BEGIN
	SET NOCOUNT ON;
	
 IF(@idAplicacion=0)
	select @idAplicacion= valor from catalogogeneral where llave ='idaplicacion'

	SELECT TOP 1 Titulo,Mensaje,Clave,Descripcion,EsHtml,Tipo FROM MensajesServicios WITH (NOLOCK) WHERE Clave=@Clave AND idaplicacion = CASE idaplicacion WHEN 0 THEN  idaplicacion ELSE @idAplicacion END  and activo=1 order by idaplicacion desc 
	
END