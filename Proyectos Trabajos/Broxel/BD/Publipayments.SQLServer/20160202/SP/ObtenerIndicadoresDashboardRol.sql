-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 05/01/2016	
-- Description:	Obtiene Indicadores a mostrar para el rol
-- =============================================
CREATE PROCEDURE ObtenerIndicadoresDashboardRol 
	@parte INT, 
	@rol VARCHAR(25)
AS
BEGIN
	IF @parte = - 1
	BEGIN
		SELECT *
		FROM dbo.Utils_Descripciones
		WHERE fi_idPermisos IN (
				SELECT idPermiso
				FROM utils_Permisos
				WHERE permisoModulo = @rol
				)
			AND fi_Activo = 1
		ORDER BY fi_Parte, fi_Orden
	END
	ELSE
	BEGIN
		SELECT *
		FROM dbo.Utils_Descripciones
		WHERE fi_Parte = @parte
			AND fi_idPermisos IN (
				SELECT idPermiso
				FROM utils_Permisos
				WHERE permisoModulo = @rol
				)
			AND fi_Activo = 1
		ORDER BY fi_Parte, fi_Orden
	END
END
GO


