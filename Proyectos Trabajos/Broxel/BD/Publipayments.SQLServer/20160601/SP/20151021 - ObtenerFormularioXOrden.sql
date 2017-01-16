
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/10/21
-- Description:	Obtiene el nombre del formulario correspondiente a la orden a procesar
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerFormularioXOrden]
	@idorden int
	,@idusuario int
	,@Captura int = 0
AS
BEGIN
	SET NOCOUNT ON;

		DECLARE @iddominio INT=0

		IF(@idusuario>0)
		SELECT  @iddominio =iddominio FROM usuario WITH(NOLOCK) WHERE idusuario=@idusuario
		
		SELECT 
		f.idFormulario
		,f.idAplicacion
		,f.Nombre
		,f.Version
		,f.Captura
		,f.Ruta
		,f.Descripcion
		,f.FechaAlta
		,f.Estatus
		 FROM ordenes o WITH(NOLOCK) inner join creditos c WITH(NOLOCK) ON o.num_cred=c.CV_CREDITO
		inner join AsignacionFormularios aF WITH(NOLOCK) ON c.CV_RUTA=aF.CV_RUTA
		inner join formulario f WITH(NOLOCK) ON f.nombre=aF.nombreformulario
		where o.idorden=@idorden  
		and f.estatus = 1
		and f.captura= CASE @Captura WHEN 0 THEN f.captura ELSE @Captura  END
		and aF.iddominio = CASE  aF.iddominio WHEN 0 THEN aF.iddominio ELSE @iddominio END
		and aF.Orden_Estatus = CASE  aF.Orden_Estatus WHEN 0 THEN aF.Orden_Estatus ELSE o.estatus END
		and aF.Orden_idVisita = CASE  aF.Orden_idVisita WHEN 0 THEN aF.Orden_idVisita ELSE o.idvisita END
		and aF.Orden_Tipo = CASE  aF.Orden_Tipo WHEN ' ' THEN aF.Orden_Tipo ELSE o.Tipo END

	
END
