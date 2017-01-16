
GO
/****** Object:  StoredProcedure [dbo].[ActualizarResultadoBitacoraEnvio]    Script Date: 15/02/2015 07:53:57 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 20140930
-- Description:	Actuliza Bitacora Envio
-- Modificó:	Alberto Rojas
-- Fecha modificación: 20150215
-- Description:	Se agrega estatus de SMS
-- =============================================
ALTER PROCEDURE [dbo].[ActualizarResultadoBitacoraEnvio]
	@ids varchar(5000),
	@Resultado varchar(100) 
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @sql VARCHAR(8000)
	SET @sql = 'UPDATE BitacoraEnvio SET FechaEnvio = GETDATE(), RespuestaEnvio = ISNULL(RespuestaEnvio,'''') +''' + @Resultado
	SET @sql += ''' WHERE id IN (' + @ids + ')' 

	EXECUTE sp_sqlexec @sql
	
	SET NOCOUNT OFF;

	SET @sql = 'UPDATE o SET o.Estatus = case b.EstatusEnvio when  17 then 17  when 57 then 17 when 27 then 17  else 1 end FROM Ordenes o INNER JOIN BitacoraEnvio b ON o.idOrden = b.idOrden'
	SET @sql += ' WHERE b.id IN (' + @ids + ') AND o.Estatus NOT IN (1,2,3,4,5)'

	EXECUTE sp_sqlexec @sql
END
