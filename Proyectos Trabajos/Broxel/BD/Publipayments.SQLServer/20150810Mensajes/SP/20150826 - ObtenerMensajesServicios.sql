
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/08/26
-- Description:	Obtiene los datos relacionados a la clave que se recibe
-- =============================================
CREATE PROCEDURE ObtenerMensajesServicios  @Clave varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 Titulo,Mensaje,Clave,Descripcion,EsHtml,Tipo FROM MensajesServicios WITH (NOLOCK) WHERE Clave=@Clave AND (idaplicacion = (SELECT valor FROM catalogogeneral WHERE id=3) or idaplicacion=0) order by idaplicacion desc 
	
END

