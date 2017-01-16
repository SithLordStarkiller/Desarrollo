DECLARE 
	@maxId INT,
	@idFormulario INT

SELECT @maxId = MAX(idCampo) FROM CamposXML2

SET @maxId = @maxId + 1

SELECT @idFormulario = idFormulario FROM formulario WHERE Estatus = 1 and captura=1 and Ruta='rdst'

INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])VALUES(@maxId,'numPago','[Campo]NU_PAGO_MES_CORRIENTE',@maxId,1,@idFormulario)
SET @maxId = @maxId + 1

INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])VALUES(@maxId,'fechasPagos','[Campo]TX_PAGO_MES_CORRIENTE',@maxId,1,@idFormulario)

UPDATE camposxml2 SET Valor='[Campo]IM_PAGO_MES_CORRIENTE' where Nombre='TX_PAGO_MES_ACTUAL' and idformulario=@idFormulario

