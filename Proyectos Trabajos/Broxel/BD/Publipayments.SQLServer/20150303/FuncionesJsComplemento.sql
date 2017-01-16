
GO
/****** Object:  StoredProcedure [dbo].[FuncionesJsComplemento]    Script Date: 03/03/2015 10:37:54 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2014-11-26
-- Description:	separa los datos a validar y las acciones a realizar para la creacion de los javascript ; genera nombre  a las funciones
-- Cambios: Se agrega la funcion del calculo de DCP y STM
-- Actualizacion: Alberto Rojas
-- Fecha : 2015-01-31
-- Cambios: Se agrego el dar de baja el formulario anterior y se agrega formato de fecha en los nuevos campos del formulario version 5.0
-- =============================================
ALTER PROCEDURE [dbo].[FuncionesJsComplemento]
AS
BEGIN
	DECLARE @idFormulario INT
	DECLARE @funcionSTM NVARCHAR(4000) = 'var pagoPuede = $("#PagoQuePuede").val();var i = 4;while (!isNaN(parseInt($("#IM_OPC" + i + "_STM").val(), 10))){if(pagoPuede >= parseInt($("#IM_OPC" + i + "_STM").val(), 10)){ i--; } else {i++;break;}}i = i == 0 ? 1 : i > 4? 4: i;OPC_STM = "IM_OPC" + (i) + "_STM";$("#IM_OPC_SELEC").val(OPC_STM);$("#IM_PRIMER_PAGO_SELEC").val($("#IM_OPC" + (i) + "_STM_PRIMER_PAGO").val());$("#IM_SELEC_STM_PAGO_SUBSEC").val($("#IM_OPC" + (i) + "_STM_PAGO_SUBSEC").val());$("#IM_BENSELEC_STM").val($("#IM_BEN" + (i) + "_STM").val());'
	DECLARE @funcionDCP NVARCHAR(4000) = 'ingtotalesDcp = parseFloat($("#IngtotalesDCP").val()); gastos = parseFloat($("#gastos").val()); fm = parseFloat($("#FM").val()); ft = parseFloat($("#FT").val()); salarioMinimo = parseFloat($("#salarioMinimo").val()); segDanAct = parseFloat($("#IM_PRIMA_SEG_DAN").val()); segDanOms = parseFloat($("#IM_SALDO_SEG_DAN").val()); pagotemp = 0; factorFinal = 0; pagotemp = (ingtotalesDcp >= gastos) ? (gastos * 0.15) : (ingtotalesDcp * 0.15); if (pagotemp <= fm) { factorFinal = (fm >= (salarioMinimo * 6.8)) ? (fm + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } else { if (pagotemp >= ft) { factorFinal = (ft >= (salarioMinimo * 6.8)) ? (ft + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } else { if ((pagotemp * 1.20) > ft) { factorFinal = (pagotemp >= (salarioMinimo * 6.8)) ? (pagotemp + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } else { $("#QPagoTempHeader").html("¿Acepta la cantidad de " + pagotemp * 1.20 + " (sin seguro) para el pago de los siguientes meses?"); window.QPagoTemp.Show(); } } }}function QPagoTempSi() { factorFinal = ((pagotemp * 1.20) >= (salarioMinimo * 6.8)) ? ((pagotemp * 1.20) + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); }function QPagoTempNo() { factorFinal = (pagotemp >= (salarioMinimo * 6.8)) ? (pagotemp + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } function finalizarQPagoTemp() {window.QPagoTemp.Hide(); $("#Res_factorDCP").val(factorFinal.toFixed(4)); $("#factorSinFee").val((factorFinal - segDanOms).toFixed(4));'
	DECLARE @funcionTelefonosSi NVARCHAR(4000)='Mostrar("TelTipoAgT1",true);Mostrar("AgTelefonoT1",true);Mostrar("TelTipoAgT2",true);Mostrar("AgTelefonoT2",true);Mostrar("TelTipoAgT3",true);Mostrar("AgTelefonoT3",true);Mostrar("TelTipoAgT4",true);Mostrar("AgTelefonoT4",true);'
	DECLARE @funcionTelefonosNo NVARCHAR(4000)='Mostrar("TelTipoAgT1",false);Mostrar("AgTelefonoT1",false);Mostrar("TelTipoAgT2",false);Mostrar("AgTelefonoT2",false);Mostrar("TelTipoAgT3",false);Mostrar("AgTelefonoT3",false);Mostrar("TelTipoAgT4",false);Mostrar("AgTelefonoT4",false);'
	DECLARE @ValidacionTelefonos NVARCHAR(1000)='if((ValC("reqAct", "Si") && ValC("AgTelefonos", "Si")))'
	
	DECLARE @ValidacionDicAplicaSMSSi NVARCHAR(4000)='if((ValC("DicAplicaSMS", "No")))'
	DECLARE @funcionDicAplicaSMSSi NVARCHAR(4000)='pcFormularios.GetTabByName("Actualizacion").SetEnabled(true);pcFormularios.GetTabByName("GestVisita").SetEnabled(true);'
	DECLARE @funcionDicAplicaSMSNo NVARCHAR(4000)='pcFormularios.GetTabByName("Actualizacion").SetEnabled(false);pcFormularios.GetTabByName("GestVisita").SetEnabled(false);'
	

	SELECT @idFormulario = MAX(idFormulario)
	FROM [dbo].Formulario

	-- ========================================================================
	-- obtiene las condiciones a validar (condicionales)
	-- ========================================================================
	UPDATE [dbo].CatFuncionesJS
	SET Validacion = SUBSTRING(FuncionSI, 0, CHARINDEX('{', FuncionSI, 0))
	FROM CatFuncionesJS
	WHERE idFormulario = @idFormulario
		AND FuncionSI LIKE 'if%'

	UPDATE [dbo].CatFuncionesJS
	SET Validacion = SUBSTRING(FuncionSI, 19, CHARINDEX(';', FuncionSI, 0) - 20)
	FROM CatFuncionesJS
	WHERE idFormulario = @idFormulario
		AND FuncionSI LIKE '$(%'

	-- =====================================================================
	-- obtiene las funciones a ejecutar si la validacion no es satidfactoria     
	-- =====================================================================
	UPDATE [dbo].CatFuncionesJS
	SET FuncionNo = SUBSTRING(FuncionSI, CHARINDEX('else', FuncionSI, 0) + 5, len(FuncionSI) - CHARINDEX('else', FuncionSI, 0) - 5)
	FROM CatFuncionesJS
	WHERE idFormulario = @idFormulario
		AND FuncionSI LIKE 'if%'

	-- ========================================================================
	-- obtiene las funciones a ejecutar si la validacion cumple con los valores
	-- ========================================================================
	UPDATE [dbo].CatFuncionesJS
	SET FuncionSI = SUBSTRING(FuncionSI, CHARINDEX('{', FuncionSI, 0) + 1, CHARINDEX('}', FuncionSI, 0) - CHARINDEX('{', FuncionSI, 0) - 1)
	FROM CatFuncionesJS
	WHERE idFormulario = @idFormulario
		AND FuncionSI LIKE 'if%'

	-- ========================================================================
	-- Agrega el valor a los dictamenes que se cargan con valor nulo
	-- ========================================================================
	UPDATE [dbo].CamposXSubFormulario
	SET Valor = '[Tabla]' + NombreCampo
	WHERE idSubFormulario IN (
			SELECT idSubFormulario
			FROM SubFormulario
			WHERE idFormulario = @idFormulario
			)
		AND NombreCampo LIKE 'Dictamen%'
		AND Valor IS NULL

	-- =====================================================================
	--Genera el nombre de las funciones     
	-- =====================================================================
	UPDATE cc
	SET cc.Nombre = ff.NombreFuncion + '_' + convert(VARCHAR(5), ff.idSubFormulario)
	FROM [dbo].CatFuncionesJS cc
	INNER JOIN (
		SELECT 'func_' + convert(VARCHAR(5), row_number() OVER (
					ORDER BY f.Validacion
					)) NombreFuncion
			,f.Validacion
			,c.idsubformulario
		FROM [dbo].CamposXSubFormulario c
		INNER JOIN [dbo].FuncionesXCampos fc ON c.idCampoFormulario = fc.idCampoFormulario
		INNER JOIN [dbo].CatFuncionesJS f ON fc.idFuncionJS = f.idFuncionJS
		WHERE f.idFormulario = @idFormulario
		GROUP BY f.Validacion
			,c.idsubformulario
		) ff ON cc.Validacion = ff.Validacion
	WHERE idFormulario = @idFormulario

	-- =====================================================================
	-- Se limpia el campo de validacion, solo se utilizo para nombrar la funcion
	-- =====================================================================
	UPDATE [dbo].CatFuncionesJS
	SET Validacion = NULL
	FROM CatFuncionesJS
	WHERE idFormulario = @idFormulario
		AND FuncionSI LIKE '$(%'

	-- =====================================================================
	-- Se oculta un elemento que no jamaz debe mostrarse
	-- =====================================================================
	UPDATE [dbo].[CatFuncionesJS]
	SET FuncionSI = REPLACE(FuncionSI, 'Mostrar(''DicAplicaConvenio'',true)', 'Mostrar(''DicAplicaConvenio'',false)')
	WHERE FuncionSI LIKE '%Mostrar(''DicAplicaConvenio'',true)%'
		AND idFormulario = @idFormulario

	-- =======================================================================================
	-- Se limpia el valor que trae inicializado, por que son valores calculados al vuelo
	-- =======================================================================================
	UPDATE [dbo].CamposXSubFormulario
	SET Valor=''
	WHERE idSubFormulario IN (
			SELECT idSubFormulario
			FROM SubFormulario
			WHERE idFormulario = @idFormulario
			)
		AND NombreCampo in ('MejorOpcConSdoSegDanos','IM_PRIMER_PAGO_SELEC','MEJOR_OPC_PRIMER_PAGO','MEJOR_OPC_PAGO_SUBSEC','IM_SELEC_STM_PAGO_SUBSEC','MEJOR_OPC_BEN','IM_BENSELEC_STM','segDanAct','segDanOms')
	
	-- =====================================================================
	-- Se aplica validación en email
	-- =====================================================================	
	UPDATE [dbo].CamposXSubFormulario
	SET validacion = '[a-zA-Z0-9._-]+?@[a-zA-Z0-9_-]+?\.[a-zA-Z.]{2,4}'
	WHERE idSubFormulario IN (
			SELECT idSubFormulario
			FROM SubFormulario
			WHERE idFormulario = @idFormulario
			)
		AND NombreCampo = 'TX_CORREO_ELECTRONICO_ACT'
	
	-- =====================================================================
	-- Se cargan las restricciones de las fechas
	-- =====================================================================	
	UPDATE [dbo].CamposXSubFormulario
	SET valor = 'Edad,FechaActual,FechaActual'
	WHERE idSubFormulario IN (
			SELECT idSubFormulario
			FROM SubFormulario
			WHERE idFormulario = @idFormulario
			)
		AND NombreCampo in  ('fechaIngAcla3'
							,'Viudodesdecuan'
							,'Divordesdecuan'
							,'fechaInicioCurso1'
							,'fechaFinCurso1'
							,'fechaInicioCurso2'
							,'fechaFinCurso2'
							,'fechaInicioCurso3'
							,'fechaFinCurso3'
							,'fechaInicioEmp'
							,'fechaFinEmp'
							,'comenzarBuscar')
		
		UPDATE [dbo].CamposXSubFormulario
	SET valor = 'InicioMesActual,FinMesActual,FechaActual'
	WHERE idSubFormulario IN (
			SELECT idSubFormulario
			FROM SubFormulario
			WHERE idFormulario = @idFormulario
			)
		AND NombreCampo in  ('fechaPagoAcla')
		
	UPDATE [dbo].CamposXSubFormulario
	SET valor = 'InicioMesAnterior,InicioMesActual,FechaActual'
	WHERE idSubFormulario IN (
			SELECT idSubFormulario
			FROM SubFormulario
			WHERE idFormulario = @idFormulario
			)
		AND NombreCampo in  ('fechaPagoAcla2')
	-- =====================================================================
	-- Se carga la funcion que realiza el calculo del boton de STM
	-- =====================================================================	
		INSERT INTO [CatFuncionesJS]
           ([Nombre]
           ,[Validacion]
           ,[FuncionSI]
           ,[FuncionNo]
           ,[idFormulario])
		SELECT Valor as Nombre,null as validacion,@funcionSTM as FuncionSI,null as FuncionNo,@idFormulario as  idFormulario from CamposXSubFormulario where idTipoCampo=11  and idSubFormulario in (
		SELECT idSubFormulario from SubFormulario where Clase='GestVisita' and idFormulario=@idFormulario)
	 
	 -- =====================================================================
	-- Se carga la funcion que realiza el calculo del boton de DCP
	-- =====================================================================	
		INSERT INTO [CatFuncionesJS]
           ([Nombre]
           ,[Validacion]
           ,[FuncionSI]
           ,[FuncionNo]
           ,[idFormulario])
		SELECT Valor as Nombre,null as validacion,@funcionDCP as FuncionSI,null as FuncionNo,@idFormulario as  idFormulario from CamposXSubFormulario where idTipoCampo=11  and idSubFormulario in (
		SELECT idSubFormulario from SubFormulario where Clase='SubFormDCP' and idFormulario=@idFormulario)
 
    -- =====================================================================================================================================
	-- se inserta la funcion que controlara el despliegue de los telefonos adicionales, en la pestaña de actualizar datos
	-- =====================================================================================================================================
	DECLARE @idCampoFAgTelefonos INT
	DECLARE @idFuncionJs INT
	SELECT @idCampoFAgTelefonos=idCampoFormulario from CamposXSubFormulario where NombreCampo='AgTelefonos' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	
	INSERT INTO [dbo].[CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])VALUES('FuncMTelefonos',@ValidacionTelefonos,@funcionTelefonosSi,@funcionTelefonosNo,@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) select @idFuncionJs,idCampoFormulario from CamposXSubFormulario where NombreCampo='reqAct' and idTipoCampo!=1 and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampoFAgTelefonos)
	 -- =====================================================================================================================================
	-- se da de baja el formulario anterior
	-- =====================================================================================================================================
	
   update formulario set Estatus=0 where captura=2 and idAplicacion =(select idAplicacion from Formulario where idFormulario=@idFormulario) and idFormulario not in (@idFormulario)
	
	 -- =====================================================================================================================================
	-- Ajustes Formulario V6 SMS
	-- =====================================================================================================================================
	-- se ajusta una funcion para que no muestre una linea 
	 update catfuncionesjs set FuncionSi=FuncionNo where FuncionSi like '%ContinuarGestion%' and idformulario=@idFormulario
	--Se limpia el valor 
	 update CamposXSubFormulario set valor='' where NombreCampo='CelularSMS_Actualizado' and idSubFormulario in (select idsubformulario from subformulario where idformulario=@idFormulario)

	-- Se carga la funcion que muestra pestañas de gestion cuando no se busca un codigo SMS
		DECLARE @idCampoDicAplicaSMS INT
		DECLARE @idFuncionJsSMS INT
		SELECT @idCampoDicAplicaSMS=idCampoFormulario from CamposXSubFormulario where NombreCampo='DicAplicaSMS' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
		INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])values ('DicAplicaSMS',@ValidacionDicAplicaSMSSi,@funcionDicAplicaSMSSi,@funcionDicAplicaSMSNo,@idFormulario)
		SELECT @idFuncionJsSMS = SCOPE_IDENTITY()
		INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJsSMS,@idCampoDicAplicaSMS)
END




