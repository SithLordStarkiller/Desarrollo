
/****** Object:  StoredProcedure [dbo].[FuncionesJsComplemento]    Script Date: 27/06/2016 10:34:00 a.m. ******/
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
-- 2016/05/12_JARO_Cambios: se agregan funciones para formulario de Call  Center Salida
-- =============================================
ALTER PROCEDURE [dbo].[FuncionesJsComplemento]  (@idFormulario int)
AS
BEGIN
	DECLARE @funcionSTM NVARCHAR(4000) = 'var pagoPuede = $("#PagoQuePuede").val();var i = 4;while (!isNaN(parseInt($("#IM_OPC" + i + "_STM").val(), 10))){if(pagoPuede >= parseInt($("#IM_OPC" + i + "_STM").val(), 10)){ i--; } else {i++;break;}}i = i == 0 ? 1 : i > 4? 4: i;OPC_STM = "IM_OPC" + (i) + "_STM";$("#IM_OPC_SELEC").val(OPC_STM);$("#IM_PRIMER_PAGO_SELEC").val($("#IM_OPC" + (i) + "_STM_PRIMER_PAGO").val());$("#IM_SELEC_STM_PAGO_SUBSEC").val($("#IM_OPC" + (i) + "_STM_PAGO_SUBSEC").val());$("#IM_BENSELEC_STM").val($("#IM_BEN" + (i) + "_STM").val());'
	DECLARE @funcionDCP NVARCHAR(4000) = 'var continuar = true; $("#FM,#FT,#IM_PRIMA_SEG_DAN,#IM_SALDO_SEG_DAN").each(function () { if (!$(this).val()) { alert("El campo " + $(this).attr("id") + " no fue proporcionado por el Instituto, favor de informar a soporte "); continuar = false; } }); if (continuar) { ingtotalesDcp = parseFloat($("#IngtotalesDCP").val()); gastos = parseFloat($("#gastos").val()); fm = parseFloat($("#FM").val()); ft = parseFloat($("#FT").val()); salarioMinimo = parseFloat($("#salarioMinimo").val()); segDanAct = parseFloat($("#IM_PRIMA_SEG_DAN").val()); segDanOms = parseFloat($("#IM_SALDO_SEG_DAN").val()); pagotemp = 0; factorFinal = 0; pagotemp = (ingtotalesDcp >= gastos) ? (gastos * 0.15) : (ingtotalesDcp * 0.15); if (pagotemp <= fm) { factorFinal = (fm >= (salarioMinimo * 6.8)) ? (fm + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } else { if (pagotemp >= ft) { factorFinal = (ft >= (salarioMinimo * 6.8)) ? (ft + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } else { if ((pagotemp * 1.20) > ft) { factorFinal = (pagotemp >= (salarioMinimo * 6.8)) ? (pagotemp + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } else { $("#QPagoTempHeader").html("¿Acepta la cantidad de " + pagotemp * 1.20 + " (sin seguro) para el pago de los siguientes meses?"); window.QPagoTemp.Show(); } } } } } function QPagoTempSi() { factorFinal = ((pagotemp * 1.20) >= (salarioMinimo * 6.8)) ? ((pagotemp * 1.20) + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } function QPagoTempNo() { factorFinal = (pagotemp >= (salarioMinimo * 6.8)) ? (pagotemp + segDanAct + segDanOms) : ((salarioMinimo * 6.8) + segDanAct + segDanOms); finalizarQPagoTemp(); } function finalizarQPagoTemp() { window.QPagoTemp.Hide(); $("#Res_factorDCP").val(factorFinal.toFixed(4)); $("#factorSinFee").val((factorFinal - segDanOms).toFixed(4));'
	DECLARE @funcionTelefonosSi NVARCHAR(4000)='Mostrar("TelTipoAgT1",true);Mostrar("AgTelefonoT1",true);Mostrar("TelTipoAgT2",true);Mostrar("AgTelefonoT2",true);Mostrar("TelTipoAgT3",true);Mostrar("AgTelefonoT3",true);Mostrar("TelTipoAgT4",true);Mostrar("AgTelefonoT4",true);'
	DECLARE @funcionTelefonosNo NVARCHAR(4000)='Mostrar("TelTipoAgT1",false);Mostrar("AgTelefonoT1",false);Mostrar("TelTipoAgT2",false);Mostrar("AgTelefonoT2",false);Mostrar("TelTipoAgT3",false);Mostrar("AgTelefonoT3",false);Mostrar("TelTipoAgT4",false);Mostrar("AgTelefonoT4",false);'
	DECLARE @ValidacionTelefonos NVARCHAR(1000)='if((ValC("reqAct", "Si","==") && ValC("AgTelefonos", "Si","==")))'
	
	DECLARE @ValidacionDicAplicaSMSSi NVARCHAR(4000)='if((ValC("DicAplicaSMS", "No","==")))'
	DECLARE @funcionDicAplicaSMSSi NVARCHAR(4000)='pcFormularios.GetTabByName("Actualizacion").SetEnabled(true);pcFormularios.GetTabByName("GestVisita").SetEnabled(true);'
	DECLARE @funcionDicAplicaSMSNo NVARCHAR(4000)='pcFormularios.GetTabByName("Actualizacion").SetEnabled(false);pcFormularios.GetTabByName("GestVisita").SetEnabled(false);'
	
		
	DECLARE @formularioTbTemp TABLE
	(
	 idFormulario int,
	 idAplicacion int,
	 Nombre varchar(50),
	 Estatus char(1),
	 Captura int ,
	 Ruta varchar(10)
	)
	
	INSERT INTO @formularioTbTemp (idFormulario,idAplicacion,Nombre,Estatus,Captura,Ruta) select f.idFormulario,f.idAplicacion,f.Nombre,f.Estatus,f.Captura,f.Ruta  from formulario f  where idformulario=@idFormulario

	-- 2015/09/23------------------SELECT @idFormulario = MAX(idFormulario)
	-- 2015/09/23------------------FROM [dbo].Formulario

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
	
	update f set f.estatus=0  from formulario f inner join @formularioTbTemp ft on f.nombre=ft.nombre where f.ruta=ft.ruta and f.captura=ft.captura and  f.estatus=1 and f.idformulario  not in (ft.idformulario) 
	
	-- =====================================================================================================================================
	-- Ajustes Formulario V6 SMS
	-- =====================================================================================================================================
	-- se ajusta una funcion para que no muestre una linea 
	 update catfuncionesjs set FuncionSi=FuncionNo where FuncionSi like '%ContinuarGestion%' and idformulario=@idFormulario
	--Se limpia el valor 
	 update CamposXSubFormulario set valor='' where NombreCampo='CelularSMS_Actualizado' and idSubFormulario in (select idsubformulario from subformulario where idformulario=@idFormulario)


	 IF((select 1 from formulario where idformulario=@idformulario and nombre like '%CobSocial%')=1)
			BEGIN 
				-- Se carga la funcion que muestra pestañas de gestion cuando no se busca un codigo SMS
				DECLARE @idCampoDicAplicaSMS INT
				DECLARE @idFuncionJsSMS INT
				SELECT @idCampoDicAplicaSMS=idCampoFormulario from CamposXSubFormulario where NombreCampo='DicAplicaSMS' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
				INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])values ('DicAplicaSMS',@ValidacionDicAplicaSMSSi,@funcionDicAplicaSMSSi,@funcionDicAplicaSMSNo,@idFormulario)
				SELECT @idFuncionJsSMS = SCOPE_IDENTITY()
				INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJsSMS,@idCampoDicAplicaSMS)
			END
	

	-- =====================================================================================================================================
	-- Ajustes Formulario V7 SMS
	-- =====================================================================================================================================
	--se igualan funciones para que siempre ejecute el no y no sea visible el campo de la funcion
	update catfuncionesjs set FuncionSi=funcionNo where funcionSi like '%CodRecSMS%'   and FuncionNo is not null  and idformulario=@idFormulario
	-- se da nombre a la funcion, es la funcion para comparar los numeros de codigo SMS; se agreaga una validacion ; se relaciona la funcion ;se manda a ejecutar funcion para hacer valida la validacion
	update catfuncionesjs set nombre='validaSMS' where FuncionSi like '%#ValCodSMS%' and FuncionSi like '%=%' and idformulario=@idFormulario
	update camposxsubformulario set validacion='^Correcto' where nombreCampo='valCodSMS' and idsubformulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES ((select idFuncionJS from catfuncionesjs  where FuncionSi like '%#ValCodSMS%' and FuncionSi like '%=%' and idformulario=@idformulario) ,(select  idCampoFormulario from camposxsubformulario where nombreCampo = 'CodSMS' and idsubformulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)))
	update catfuncionesjs set FuncionSI=FuncionSi+'ValidaFormato($("#ValCodSMS"))'  where FuncionSi like '%#ValCodSMS%' and FuncionSi like '%=%' and idformulario=@idformulario
	--se nombran las funciones pertenecientes a la doble validacion de celular capturado
	update  catfuncionesjs set nombre='func'+SUBSTRING(FuncionSi, CHARINDEX('#',FuncionSi,0)+1, CHARINDEX(')',FuncionSi,0)-6)   
	   where FuncionSi like '%VALSMS%' and FuncionNo is null and idformulario=@idformulario
	--se crea la validacion en el campo para realizar la validacion correcta
	update camposxsubformulario set validacion='^Correcto' where nombreCampo like '%VALSMS%' and idsubformulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)
	-- se insertan las relaciones de las funciones antes renombradas con el campo que se acaba de poner la validacion
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) 
	 select cat.idfuncionjs,cs.idCampoFormulario from
	 (select idfuncionjs,SUBSTRING(FuncionSi, CHARINDEX('=',FuncionSi,0)+2, ((CHARINDEX(';',FuncionSi,0)-6)-CHARINDEX('=(',FuncionSi,0))) as 'NombreCampo' from catfuncionesjs
	   where FuncionSi like '%VALSMS%' and FuncionNo is null and idformulario=@idformulario
	) as cat   inner join 
	(select idCampoFormulario,NombreCampo from CamposXSubFormulario where  nombreCampo like '%ConfirmeSMS%'  and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)) as cs  on cat.NombreCampo=cs.NombreCampo
	--se agrega la funcion que se ejecutara para realizar la validacion de los telefonos 
	-- 2015/09/23------------------update  catfuncionesjs set FuncionSI=FuncionSi
	-- 2015/09/23------------------where FuncionSi like '%VALSMS%' and FuncionNo is null and idformulario=@idformulario
	update  catfuncionesjs set FuncionSI=FuncionSi+'ValidaFormato($("#'+ SUBSTRING(FuncionSi, CHARINDEX('#',FuncionSi,0)+1, CHARINDEX(')',FuncionSi,0)-6) +'"));'
		where FuncionSi like '%VALSMS%' and FuncionNo is null and idformulario=@idformulario
	-- se cambia el texto
	update CamposXSubFormulario set Texto='El siguiente campo indica si el dato capturado es correcto' where NombreCampo like 'Val%'  and NombreCampo like '%SMS%'   and idtipocampo=2  and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)
	-- se actualiza una funcion par que funcione tanto para idvisita1 como para idvisita2
	--update  catfuncionesJs set validacion=REPLACE(validacion,'(ValC(''IDVISITA'', ''2''))','((ValC(''IDVISITA'', ''1''))||(ValC(''IDVISITA'', ''2'')))')  where nombre in (
	--select Nombre from catfuncionesJs where idformulario=@idformulario and FuncionSi like '%fotoMediacionFinal%' )
	-- =====================================================================================================================================
	-- Ajustes Formulario V8 
	-- =====================================================================================================================================
	
	-- se actualiza una funcion par que funcione tanto para idvisita1 como para idvisita2
	-- 2015/09/23------------------update  catfuncionesJs set validacion=REPLACE(validacion,'(ValC(''IDVISITA'', ''2''))','((ValC(''IDVISITA'', ''1''))||(ValC(''IDVISITA'', ''2'')))')  where nombre in (
	-- 2015/09/23------------------select Nombre from catfuncionesJs where idformulario=@idformulario and FuncionSi like '%Lbl_SinContacto%' )
	--  se cambia la validacion para que siempre se muestre el boton para calcular el DCP
	update catfuncionesjs set FuncionNo=FuncionSi where FuncionSi like '%CalcularDCP%' and idformulario=@idformulario
	---  se igualan las funciones para que el campo no se muestre de ninguna forma
	update catfuncionesjs set FuncionSi=FuncionNo where FuncionSI like '%AcreditadoRegCan%' and idformulario=@idformulario

	-- =====================================================================================================================================
	-- Ajustes Formulario V10 
	-- =====================================================================================================================================

	-- 2015/09/23------------------se agregan las validaciones para las funciones de menor o igual que 
	-- 2015/09/23------------------update catfuncionesjs set validacion= replace(validacion,'ValC(''CantidadPagos'', ''1'')','ValC(''CantidadPagos'', ''1'')||ValC(''CantidadPagos'', ''2'')||ValC(''CantidadPagos'', ''3'')||ValC(''CantidadPagos'', ''4'')||ValC(''CantidadPagos'', ''5'')||ValC(''CantidadPagos'', ''6'')')
	-- 2015/09/23------------------where idfuncionJS in (select  idfuncionJS from catfuncionesjs where FuncionSi like '%TX_PAGA1%'  ) and idformulario=@idformulario and FuncionSi like '%TX_PAGA1%'

	-- 2015/09/23------------------update catfuncionesjs set validacion= replace(validacion,'ValC(''CantidadPagos'', ''2'')','ValC(''CantidadPagos'', ''2'')||ValC(''CantidadPagos'', ''3'')||ValC(''CantidadPagos'', ''4'')||ValC(''CantidadPagos'', ''5'')||ValC(''CantidadPagos'', ''6'')')
	-- 2015/09/23------------------where idfuncionJS in (select  idfuncionJS from catfuncionesjs where FuncionSi like '%TX_PAGA2%'  ) and idformulario=@idformulario and FuncionSi like '%TX_PAGA2%'

	
	-- 2015/09/23------------------update catfuncionesjs set validacion= replace(validacion,'ValC(''CantidadPagos'', ''3'')','ValC(''CantidadPagos'', ''3'')||ValC(''CantidadPagos'', ''4'')||ValC(''CantidadPagos'', ''5'')||ValC(''CantidadPagos'', ''6'')')
	-- 2015/09/23------------------where idfuncionJS in (select  idfuncionJS from catfuncionesjs where FuncionSi like '%TX_PAGA3%'  ) and idformulario=@idformulario and FuncionSi like '%TX_PAGA3%'

	-- 2015/09/23------------------update catfuncionesjs set validacion= replace(validacion,'ValC(''CantidadPagos'', ''4'')','ValC(''CantidadPagos'', ''4'')||ValC(''CantidadPagos'', ''5'')||ValC(''CantidadPagos'', ''6'')')
	-- 2015/09/23------------------where idfuncionJS in (select  idfuncionJS from catfuncionesjs where FuncionSi like '%TX_PAGA4%'  ) and idformulario=@idformulario and FuncionSi like '%TX_PAGA4%'

	-- 2015/09/23------------------update catfuncionesjs set validacion= replace(validacion,'ValC(''CantidadPagos'', ''5'')','ValC(''CantidadPagos'', ''5'')||ValC(''CantidadPagos'', ''6'')')
	-- 2015/09/23------------------where idfuncionJS in (select  idfuncionJS from catfuncionesjs where FuncionSi like '%TX_PAGA5%'  ) and idformulario=@idformulario and FuncionSi like '%TX_PAGA5%'

	
	--se realiza el cambio de las funciones 
	--update catfuncionesjs set  FuncionSi=FuncionNo,FuncionNo=FuncionSi where idFuncionJS in (select idFuncionJS from catfuncionesjs where validacion like '%AceptaFPP1%' and idformulario=@idformulario)

	--Se cambia nombre a funciones para ejecutarse aparte
	update catfuncionesjs set nombre=(nombre+'xxx') where funcionSi like '%AceptaFPP%' and idformulario=@idformulario
	
	--se oculta un campo de configuracion de pagos fpp
	update  CamposXSubFormulario set  ClasesLinea=ClasesLinea+' CWHidden' where nombrecampo='CantidadPagos'  and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)

	-- se agrega la actualizacion para poder procesar otra funcion para el calendario
	-- 2015/09/23------------------update camposxsubformulario set valor='FechaActual,FechaActual+dias,FechaActual',Validacion='0,10' where  idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) and  idtipocampo=7 and NombreCampo in ('FH_PROMESA_PAGO','FechaPromesaSTM','FechaPromesaSiSTM','FH_PROMESA_PAGOFPP','FH_PROMESA_PAGOPR5050')

	-- =====================================================================================================================================
	-- Ajustes Formulario V11 
	-- =====================================================================================================================================

	-- Se quita el valor por defecto que tiene que por el momento no se esta mandando
	update camposxsubformulario set Valor='' where  idsubformulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)  and nombrecampo ='Otrarazon'

	-- Se ponen como readOnly y requeridos los campos finales necesarios para la gestion DCP
	update camposxsubformulario set ClasesLinea=ClasesLinea+' CWRec',ClasesValor='CWAncho300 CWReadonly' where  idsubformulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario)  and nombrecampo in ('Res_factorDCP','factorSinFee') 



	-- Cambios en textos para callcenter, solo aplica si el formulario tiene nombre de callcenter 


		IF((select 1 from formulario where idformulario=@idformulario and nombre like '%Callcenter%')=1)
			BEGIN 
				update subformulario set Subformulario='Gestión de la Llamada' where idsubformulario in (select idsubformulario from  subformulario  where idformulario= @idformulario and clase='GestVisita')
				update subformulario set Subformulario='Observaciones' where idsubformulario in (select idsubformulario from  subformulario  where idformulario= @idformulario and clase='Coordyobs')
				update camposxsubformulario set texto='Instrucciones: Continuar en la pestaña de Observaciones' where idsubformulario in (select idsubformulario from  subformulario  where idformulario= @idformulario) and nombrecampo='Instrco' 
				update camposxsubformulario set texto='Instrucciones: Agradezca al Acreditado y guarde la forma.' where idsubformulario in (select idsubformulario from  subformulario  where idformulario= @idformulario) and nombrecampo='Instfingest' 
			-- Cambio para que no se muestre este elemento
				--------------update catfuncionesjs set FuncionSi=FuncionNo where funcionSI like '%foto_Retencion%' and idformulario in  (select idformulario from formulario where nombre like 'CallCenter%' and estatus=1 )
				--------------update camposxsubformulario set NombreCampo='DictamenEnviaVisitaDCP',valor='[Tabla]DictamenEnviaVisitaDCP' where idsubformulario in (select idsubformulario from  subformulario  where idformulario= @idformulario) and nombrecampo = 'DictamenDCP'
				--------------update catfuncionesjs set funcionSI= replace(funcionSI,'DictamenDCP','DictamenEnviaVisitaDCP'), funcionNO=replace(funcionNO,'DictamenDCP','DictamenEnviaVisitaDCP')  where funcionSi like '%DictamenDCP%'and idformulario = @idformulario
				update camposxsubformulario set valor='No acepta Solución' where idsubformulario in (select idsubformulario from subformulario where idformulario=@idformulario) and nombrecampo='DictamenVisAdicionalNoESE'
			END
		

		
		IF((select 1 from formulario where idformulario=@idformulario and Ruta='RDST')=1)
			BEGIN 
						
			DECLARE @idCampo INT
	
	
			--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='Acreditado' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('Acreditado','if(ValC("Acreditado", "No","=="))'
						,'$("#AcreditadoFin").val("1");$("#AcreditadoFin").trigger("blur");'
						,'$("#AcreditadoFin").val("5");$("#AcreditadoFin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
			--SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='VerificarDatos' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			--INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
			--			values ('VerificarDatos','if(ValC("VerificarDatos", "No","=="))'
			--			,'$("#VerificarDatosFin").val("0");$("#VerificarDatosFin").trigger("blur");'
			--			,'$("#VerificarDatosFin").val("2");$("#VerificarDatosFin").trigger("blur");',@idFormulario)
			--SELECT @idFuncionJs = SCOPE_IDENTITY()
			--INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
			--SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='DatosCorrectos' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			--INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
			--			values ('DatosCorrectos','if(ValC("DatosCorrectos", "No","=="))'
			--			,'$("#DatosCorrectosFin").val("5");$("#DatosCorrectosFin").trigger("blur");'
			--			,'$("#DatosCorrectosFin").val("1");$("#DatosCorrectosFin").trigger("blur");',@idFormulario)
			--SELECT @idFuncionJs = SCOPE_IDENTITY()
			--INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
				SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='ActitudHostil' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('ActitudHostil','if(ValC("ActitudHostil", "No","=="))'
						,'$("#ActitudHostilFin").val("1");$("#ActitudHostilFin").trigger("blur");'
						,'$("#ActitudHostilFin").val("3");$("#ActitudHostilFin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AplicaFPP' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AplicaFPP','if(ValC("AplicaFPP", "No","=="))'
						,'$("#AplicaFPPFin").val("2");$("#AplicaFPPFin").trigger("blur");'
						,'$("#AplicaFPPFin").val("0");$("#AplicaFPPFin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP1' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP1','if(ValC("AceptaFPP1", "No","=="))'
						,'$("#AceptaFPP1Fin").val("0");$("#AceptaFPP1Fin").trigger("blur");'
						,'$("#AceptaFPP1Fin").val("1");$("#AceptaFPP1Fin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
						SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP2' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP2','if(ValC("AceptaFPP2", "No","=="))'
						,'$("#AceptaFPP2Fin").val("0");$("#AceptaFPP2Fin").trigger("blur");'
						,'$("#AceptaFPP2Fin").val("1");$("#AceptaFPP2Fin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP3' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP3','if(ValC("AceptaFPP3", "No","=="))'
						,'$("#AceptaFPP3Fin").val("0");$("#AceptaFPP3Fin").trigger("blur");'
						,'$("#AceptaFPP3Fin").val("1");$("#AceptaFPP3Fin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP4' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP4','if(ValC("AceptaFPP4", "No","=="))'
						,'$("#AceptaFPP4Fin").val("0");$("#AceptaFPP4Fin").trigger("blur");'
						,'$("#AceptaFPP4Fin").val("1");$("#AceptaFPP4Fin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP5' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP5','if(ValC("AceptaFPP5", "No","=="))'
						,'$("#AceptaFPP5Fin").val("0");$("#AceptaFPP5Fin").trigger("blur");'
						,'$("#AceptaFPP5Fin").val("1");$("#AceptaFPP5Fin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP6' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP6','if(ValC("AceptaFPP6", "No","=="))'
						,'$("#AceptaFPP6Fin").val("0");$("#AceptaFPP6Fin").trigger("blur");'
						,'$("#AceptaFPP6Fin").val("1");$("#AceptaFPP6Fin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaFPP','if(ValC("AceptaFPP", "No","=="))'
						,'$("#AceptaFPPFin").val("2");$("#AceptaFPPFin").trigger("blur");'
						,'$("#AceptaFPPFin").val("0");$("#AceptaFPPFin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			
				
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AplicaConfPr' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AplicaConfPr','if(ValC("AplicaConfPr", "No","=="))'
						,'$("#AplicaConfPrFin").val("2");$("#AplicaConfPrFin").trigger("blur");'
						,'$("#AplicaConfPrFin").val("0");$("#AplicaConfPrFin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	
			--**********************--
					SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaConfPr' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('AceptaConfPr','if(ValC("AceptaConfPr", "No","=="))'
						,'$("#AceptaConfPrFin").val("2");$("#AceptaConfPrFin").trigger("blur");'
						,'$("#AceptaConfPrFin").val("0");$("#AceptaConfPrFin").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			
			--********************** FUNCIONES QUE SE APLICAN PARA QUE SEAN EJECUTADAS AL MOMENTO DE REALIZAR EL CALCULO QUE SE APLICA EN EL FORMULARIO ****************  - 
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('TotalizaA','if(true)'
						,'$("#TotalizaA").trigger("blur");'
						,'$("#TotalizaA").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AcreditadoFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='ActitudHostilFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='DatosCorrectosFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			--INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			--SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='ActitudHostilFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			--INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			
			--**********************-
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('TotalizaB','if(true)'
						,'$("#TotalizaB").trigger("blur");'
						,'$("#TotalizaB").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AplicaFPPFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPPFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			
			--**********************-
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('TotalizaC','if(true)'
						,'$("#TotalizaC").trigger("blur");'
						,'$("#TotalizaC").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP1Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP2Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP3Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP4Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP5Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP6Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			
			--**********************-
			INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
						values ('TotalizaD','if(true)'
						,'$("#TotalizaD").trigger("blur");'
						,'$("#TotalizaD").trigger("blur");',@idFormulario)
			SELECT @idFuncionJs = SCOPE_IDENTITY()
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AplicaConfPrFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaConfPrFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
			INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	
	

			update camposxsubformulario set ClasesLinea=ClasesLinea+' CWVariables', ClasesValor=ClasesValor + ' CWVariables' where nombrecampo in (
				'Acreditado'
				,'AcreditadoFin'
				--,'VerificarDatos'
				--,'VerificarDatosFin'
				--,'DatosCorrectos'
				--,'DatosCorrectosFin'
				,'ActitudHostil'
				,'ActitudHostilFin'
				,'AplicaFPP'
				,'AplicaFPPFin'
				,'AceptaFPP1'
				,'AceptaFPP1Fin'
				,'AceptaFPP2'
				,'AceptaFPP2Fin'
				,'AceptaFPP3'
				,'AceptaFPP3Fin'
				,'AceptaFPP4'
				,'AceptaFPP4Fin'
				,'AceptaFPP5'
				,'AceptaFPP5Fin'
				,'AceptaFPP6'
				,'AceptaFPP6Fin'
				,'AceptaFPP'
				,'AceptaFPPFin'
				,'TotalizaA'
				,'TotalizaC'
				,'TotalizaB'
				,'TotalizaD'
				,'AplicaConfPrFin'
				,'AplicaConfPr'
				) and idtipocampo=2
				and idsubformulario in (select  idsubformulario from subformulario  where idFormulario =(select idformulario from formulario where estatus=1 and captura=2 and Ruta='RDST'))

	
			END
END





