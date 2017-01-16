USE [SistemasCobranzaDesarrollo]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerRankingIndicadoresMaster]    Script Date: 02/10/2015 05:08:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 27/10/2014
-- Description:	Controla y direcciona a los distintos SP del Ranking por indicador
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerRankingIndicadoresMaster] 
	 @Master VARCHAR(100) = NULL, 
	 @fc_DashBoard VARCHAR(100) = NULL, 
	 @Indicador VARCHAR(100) = NULL, 
	 @fc_Despacho VARCHAR(100) = NULL, 
	 @idUsuarioPadre VARCHAR(100) = NULL, 
	 @valorSuperior INT = 1, 
	 @fc_Delegacion VARCHAR(100) = NULL,
	 @TipoFormulario varchar(10) =NULL
	 
AS
BEGIN
	--ObtenerRankInd
	/*++++++++ Creditos Asignados Pool ++++++++++*/
	IF @Indicador = 'DASH_CREDASIGPOOL'
	BEGIN
		EXEC ObtenerRankIndDASH_CREDASIGPOOL 
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
		
	END

	/*++++++++ Creditos Asignados A Un Movil +++++++++++++++*/
	IF @Indicador = 'DASH_CREDASIGMOVIL'
	BEGIN
		EXEC ObtenerRankIndDASH_CREDASIGMOVIL
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

	/*++++++++ Creditos Visitados +++++++++++++++*/
	IF @Indicador = 'DASH_CREDVISITADOS'
	BEGIN
		EXEC ObtenerRankIndDASH_CREDVISITADOS
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

	/*++++++++ Creditos por Volver a Visitar +++++++++++++++*/
	IF @Indicador = 'DASH_GESTNVAVISITA'
	BEGIN
		EXEC ObtenerRankIndDASH_GESTNVAVISITA
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

	/*++++++++ Créditos sin asignar a movil ++++++++++++++++++++++++*/
	IF @Indicador = 'DASH_CREDSINASIG'
	BEGIN
		EXEC ObtenerRankIndDASH_CREDSINASIG
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

/*++++++++ Créditos sin asignar a supervisor  ++++++++++++++++++++++++*/
	IF @Indicador = 'DASH_CREDSINORD'
	BEGIN
		IF @fc_DashBoard <> 'Supervisor'
			AND @fc_DashBoard <> 'Gestor'
		BEGIN
			EXEC ObtenerRankIndDASH_CREDSINORD
				@Master=@Master,
				@fc_DashBoard=@fc_DashBoard,
				@fc_Despacho=@fc_Despacho,
				@idUsuarioPadre=@idUsuarioPadre,
				@valorSuperior=@valorSuperior,
				@fc_Delegacion=@fc_Delegacion,
				@TipoFormulario=@TipoFormulario
		END
	END
	
/*++++++++ Créditos en móvil ++++++++++++++++++++++++*/
	IF @Indicador = 'DASH_CREDENMOVIL'
	BEGIN
		EXEC ObtenerRankIndDASH_CREDENMOVIL
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

/*++++++++ Visitas Realizadas +++++++++++++++*/
	IF @Indicador = 'DASH_VISITADOSREAL'
	BEGIN
		EXEC ObtenerRankIndDASH_VISITADOSREAL
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

/*+++++++ Gestiones autorizadas +++++++++++++++++++++++*/
	IF @Indicador = 'DASH_CREDCONSOLUCI'
	BEGIN
		EXEC ObtenerRankIndDASH_CREDCONSOLUCI
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

/*++++++++ Gestiones No Autorizadas ++++++++++*/
	IF @Indicador = 'DASH_GESTSINAUTH'
	BEGIN
		EXEC ObtenerRankIndDASH_GESTSINAUTH
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

/*++++++++ Gestiones por reasignar ++++++++++*/
	IF @Indicador = 'DASH_GESTREASIG'
	BEGIN
		EXEC ObtenerRankIndDASH_GESTREASIG
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END

/*++++++++ Gestiones visitadas promedio +++++++++++++++*/
	IF @Indicador = 'DASH_GESTVISPROM'
	BEGIN
		EXEC ObtenerRankIndDASH_GESTVISPROM
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
	END
	
	/*++++++++ Creditos Asignados Pool ++++++++++*/
	IF @Indicador = 'DASH_GESTSINCRO'
	BEGIN
		EXEC ObtenerRankIndDASH_GESTSINCRO 
			@Master=@Master,
			@fc_DashBoard=@fc_DashBoard,
			@fc_Despacho=@fc_Despacho,
			@idUsuarioPadre=@idUsuarioPadre,
			@valorSuperior=@valorSuperior,
			@fc_Delegacion=@fc_Delegacion,
			@TipoFormulario=@TipoFormulario
		
	END
END
