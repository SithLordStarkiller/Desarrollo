/****** Object:  StoredProcedure [dbo].[ObtenerIndDashMaster]    Script Date: 01/28/2016 10:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 05/01/2016	
-- Description:	Obtiene Indicadores a mostrar
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerIndDashMaster] 
	@indicador VARCHAR(50),
	@idUsuario  int,
	@delegacion varchar(10),
	@despacho  varchar(10),
	@supervisor  varchar(10),
	@gestor  varchar(10),
	@tipoFormulario varchar(50)
AS
BEGIN
	IF @indicador = 'DASH_TOTALMES'
	BEGIN
		SELECT day(dateadd(mm,datediff(mm,-1,GETDATE()),-1)) Valor , 100 Porcentaje,
					ud.fc_Descripcion as NombreDisplay
					,ud.fi_Parte as Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_TOTALMES'					
	END

	IF @indicador = 'DASH_DIASREST'
	
	BEGIN
		SELECT day(dateadd(mm,datediff(mm,-1,GETDATE()),-1)) - DAY(getdate()) Valor, 
					abs(((day(dateadd(mm,datediff(mm,-1,GETDATE()),-1)) - DAY(getdate())) * 100) / datediff(day, GETDATE(), dateadd(month, 1, getdate()))) Porcentaje,
					 ud.fc_Descripcion as NombreDisplay
					,ud.fi_Parte as Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_DIASREST'
	END

	IF @indicador = 'DASH_CREDVISITADOS'
	BEGIN
		EXEC ObtenerIndDashDASH_CREDVISITADOS
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END

	IF @indicador = 'DASH_GESTNVAVISITA'
	BEGIN
		EXEC ObtenerIndDashDASH_GESTNVAVISITA 
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END

	IF @indicador = 'DASH_CREDSINASIG'
	BEGIN
		EXEC ObtenerIndDashDASH_CREDSINASIG
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END

	IF @indicador = 'DASH_CREDENMOVIL'
	BEGIN
		EXEC ObtenerIndDashDASH_CREDENMOVIL
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
	
	IF @indicador = 'DASH_CREDASIGPOOL'
	BEGIN
		/*++++++++ Creditos Asignados Pool ++++++++++*/
		EXEC ObtenerIndDashDASH_CREDASIGPOOL 
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
	
	IF @indicador = 'DASH_CREDASIGMOVIL'
	BEGIN
		/*++++++++ Creditos Asignados Pool ++++++++++*/
		EXEC ObtenerIndDashDASH_CREDASIGMOVIL
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
	
	IF @indicador = 'DASH_VISITADOSREAL'
	BEGIN
		EXEC ObtenerIndDashDASH_VISITADOSREAL
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
IF @indicador = 'DASH_CREDCONSOLUCI'
	BEGIN
		EXEC ObtenerIndDashDASH_CREDCONSOLUCI
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
IF @indicador = 'DASH_GESTSINAUTH'
	BEGIN
		EXEC ObtenerIndDashDASH_GESTSINAUTH
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
IF @indicador = 'DASH_GESTREASIG'
	BEGIN
		EXEC ObtenerIndDashDASH_GESTREASIG
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
IF @indicador = 'DASH_GESTVISPROM'
	BEGIN
		EXEC ObtenerIndDashDASH_GESTVISPROM
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
IF @indicador = 'DASH_GESTSINCRO'
	BEGIN
		EXEC ObtenerIndDashDASH_GESTSINCRO
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
	
	IF @indicador = 'DASH_CREDSINORD'
	BEGIN
		EXEC ObtenerIndDashDASH_CREDSINORD
			@idUsuario =@idUsuario,
			@delegacion =@delegacion,
			@despacho =@despacho,
			@supervisor =@supervisor,
			@gestor =@gestor,
			@tipoFormulario=@tipoFormulario
	END
	
	
END
