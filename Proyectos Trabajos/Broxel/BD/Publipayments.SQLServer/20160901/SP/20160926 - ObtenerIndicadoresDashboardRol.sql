-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 05/01/2016	
-- Description:	Obtiene Indicadores a mostrar para el rol
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndicadoresDashboardRol] 
	@parte INT, 
	@rol VARCHAR(25),
	@callCenter varchar(6)='false'
AS
BEGIN
	IF @parte = - 1
	BEGIN
		SELECT 
			fi_Id,
			fc_Clave,
			fc_Descripcion,
			fi_Parte,
			fc_Modulo,
			fi_Orden,
			fi_Activo,
			fc_Usuario,
			fd_FechaMod,
			fi_idPermisos,
			Ruta,
			idRol
		FROM dbo.Utils_Descripciones WITH (NOLOCK)
		WHERE fi_idPermisos IN (
				SELECT idPermiso
				FROM utils_Permisos
				WHERE permisoModulo = @rol
				)
			AND fi_Activo = 1
			and fc_Clave <>'DASH_CREDASIGPOOL'
			and (@callCenter='false' or (@callcenter='true' and @rol='Gestor' and fc_Clave not IN('DASH_GESTNVAVISITA','DASH_CREDSINASIG','DASH_CREDENMOVIL','DASH_CREDSINORD','DASH_GESTVISPROM','DASH_GESTREASIG','DASH_GESTSINAUTH','DASH_CREDCONSOLUCI')) or(@callcenter='true' and @rol<>'Gestor'))
			and ((@callCenter='false' and fc_Clave not IN('DASH_TOTALCC')) or @callCenter='true')
			AND Ruta IS NULL	
		ORDER BY fi_Parte, fi_Orden
	END
	ELSE
	BEGIN
		SELECT 
			fi_Id,
			fc_Clave,
			fc_Descripcion,
			fi_Parte,
			fc_Modulo,
			fi_Orden,
			fi_Activo,
			fc_Usuario,
			fd_FechaMod,
			fi_idPermisos,
			Ruta,
			idRol
		FROM dbo.Utils_Descripciones WITH (NOLOCK)
		WHERE fi_Parte = @parte
			AND fi_idPermisos IN (
				SELECT idPermiso
				FROM utils_Permisos
				WHERE permisoModulo = @rol
				)
			AND fi_Activo = 1
			and fc_Clave <>'DASH_CREDASIGPOOL'
			and (@callCenter='false' or (@callcenter='true' and @rol='Gestor' and fc_Clave not IN('DASH_GESTNVAVISITA','DASH_CREDSINASIG','DASH_CREDENMOVIL','DASH_CREDSINORD','DASH_GESTVISPROM','DASH_GESTREASIG','DASH_GESTSINAUTH','DASH_CREDCONSOLUCI')) or(@callcenter='true' and @rol<>'Gestor'))
			and ((@callCenter='false' and fc_Clave not IN('DASH_TOTALCC')) or @callCenter='true')
			AND Ruta IS NULL	
		ORDER BY fi_Parte, fi_Orden
	END
END
