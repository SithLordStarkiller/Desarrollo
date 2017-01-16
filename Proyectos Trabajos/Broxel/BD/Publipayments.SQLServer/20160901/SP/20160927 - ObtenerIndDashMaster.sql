-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 05/01/2016	
-- Description:	Obtiene Indicadores a mostrar
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashMaster] @indicador VARCHAR(50), @idUsuario INT, @delegacion VARCHAR(10), @despacho VARCHAR(10), @supervisor VARCHAR(10), @gestor VARCHAR(10), @tipoFormulario VARCHAR(50), @contraPorcentaje INT = 0, @callCenter VARCHAR(6) = 'false'
AS
BEGIN
	IF @indicador = 'DASH_TOTALMES'
	BEGIN
		SELECT cast(day(dateadd(mm, datediff(mm, - 1, GETDATE()), - 1)) AS VARCHAR(10)) + '|' + cast(100 AS VARCHAR(10)) + '|' + ud.fc_Descripcion + '|' + cast(ud.fi_Parte AS VARCHAR(10))
		FROM [Utils_Descripciones] ud WITH (NOLOCK)
		WHERE ud.fc_Clave = 'DASH_TOTALMES'
	END
	ELSE IF @indicador = 'DASH_DIASREST'
	BEGIN
		SELECT cast(day(dateadd(mm, datediff(mm, - 1, GETDATE()), - 1)) - DAY(getdate()) AS VARCHAR(10)) + '|' + cast(abs(((day(dateadd(mm, datediff(mm, - 1, GETDATE()), - 1)) - DAY(getdate())) * 100) / datediff(day, GETDATE(), dateadd(month, 1, getdate()))) AS VARCHAR(10)) + '|' + ud.fc_Descripcion + '|' + cast(ud.fi_Parte AS VARCHAR(10))
		FROM [Utils_Descripciones] ud WITH (NOLOCK)
		WHERE ud.fc_Clave = 'DASH_DIASREST'
	END
	ELSE IF @indicador = 'DASH_CREDASIGPOOL'
	BEGIN
		/*++++++++ Creditos Asignados Pool ++++++++++*/
		EXEC ObtenerIndDashDASH_CREDASIGPOOL @idUsuario = @idUsuario, @delegacion = @delegacion, @despacho = @despacho, @supervisor = @supervisor, @gestor = @gestor, @tipoFormulario = @tipoFormulario, @callCenter = @callCenter
	END
	ELSE IF @indicador = 'DASH_GESTVISPROM'
	BEGIN
		EXEC ObtenerIndDashDASH_GESTVISPROM @idUsuario = @idUsuario, @delegacion = @delegacion, @despacho = @despacho, @supervisor = @supervisor, @gestor = @gestor, @tipoFormulario = @tipoFormulario, @callCenter = @callCenter
	END
	ELSE
	BEGIN
		DECLARE @sql NVARCHAR(2000)
		DECLARE @ParmDefinition NVARCHAR(2000) = N'@idUsuario  int,	@delegacion varchar(10),	@despacho  varchar(10),	@supervisor  varchar(10),	@gestor  varchar(10),	@tipoFormulario varchar(50)	,@contraPorcentaje int,	@callCenter varchar(6)'

		SET @sql = CONCAT ('ObtenerIndDash', @indicador, ' @idUsuario = @idUsuario, @delegacion = @delegacion, @despacho = @despacho, @supervisor = @supervisor, @gestor = @gestor, @tipoFormulario = @tipoFormulario, @contraPorcentaje = @contraPorcentaje, @callCenter = @callCenter')

		EXECUTE sp_executesql @sql, @ParmDefinition, @idUsuario = @idUsuario, @delegacion = @delegacion, @despacho = @despacho, @supervisor = @supervisor, @gestor = @gestor, @tipoFormulario = @tipoFormulario, @contraPorcentaje = @contraPorcentaje, @callCenter = @callCenter
	END
END