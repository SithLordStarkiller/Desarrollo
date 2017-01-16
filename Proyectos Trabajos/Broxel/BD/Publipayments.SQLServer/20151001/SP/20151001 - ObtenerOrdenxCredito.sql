
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	   Alberto Rojas
-- Create date: 2015/10/01
-- Description:	Obtiene los datos de una orden relacionada al credito
-- =============================================
CREATE PROCEDURE ObtenerOrdenxCredito (@credito varchar(15))
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT TOP 1 o.idOrden IdOrden, o.num_Cred NumCred,o.idusuario,o.idusuarioPadre,o.idusuarioAlta,o.iddominio,o.idvisita,o.fechaAlta,o.estatus,o.fechaModificacion,o.fechaEnvio,o.fechaRecepcion,o.auxiliar,o.idusuarioAnterior,o.tipo,u.Usuario Usuario, 
    CASE ua.idUsuario WHEN 0 THEN u.Usuario ELSE ua.Usuario END UsuarioAnterior, c.CV_RUTA Ruta 
    FROM Ordenes o WITH(NOLOCK) INNER JOIN Usuario u WITH(NOLOCK) on o.idUsuario = u.idUsuario 
    INNER JOIN Usuario ua WITH(NOLOCK) on o.idUsuarioAnterior = ua.idUsuario 
    INNER JOIN creditos c WITH(NOLOCK) ON c.CV_CREDITO=o.num_Cred 
    WHERE o.num_Cred=@credito order by o.fechaAlta asc

END
GO