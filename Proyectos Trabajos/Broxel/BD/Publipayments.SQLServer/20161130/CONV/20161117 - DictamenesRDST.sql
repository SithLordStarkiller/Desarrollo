DECLARE 
	@maxId INT,
	@idFormulario INT

SELECT @maxId = MAX(idCampo) FROM CamposXML2

SET @maxId = @maxId + 1

SELECT @idFormulario = idFormulario FROM formulario WHERE Estatus = 1 and captura=1 and Ruta='RDST'

INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenLiquidacion','Liquidación',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenPPFPP','Promesa de Pago FPP',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenROA','Crédito ROA',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenProrrogaP','Prórroga Parcial',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenProrrogaT','Prórroga Total',@maxId,1,@idFormulario)

			
			
	