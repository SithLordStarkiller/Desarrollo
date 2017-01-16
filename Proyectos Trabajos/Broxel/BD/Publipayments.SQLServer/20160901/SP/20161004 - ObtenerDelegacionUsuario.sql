-- =============================================
-- Author:		Laura Dotor
-- Create date: 2016-10-04
-- Description:	Obtiene delagacion por usuario, se crea para quitar el select del código
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerDelegacionUsuario]
@idUsuario VARCHAR(10)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT RD.DELEGACION FROM USUARIO U WITH(NOLOCK) 
	LEFT JOIN RELACIONDELEGACIONES RD WITH(NOLOCK) ON RD.IDUSUARIO=U.IDUSUARIO
	WHERE U.IDUSUARIO = @idUsuario

END
GO
