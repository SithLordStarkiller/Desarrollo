DECLARE 
	@maxId INT,
	@idFormulario INT

SELECT @maxId = MAX(idCampo) FROM CamposXML2

SET @maxId = @maxId + 1

SELECT @idFormulario = idFormulario FROM formulario WHERE Estatus = 1 and captura=1 and Ruta='CI'

INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Nombre','[Campo]TX_NOMBRE_ACREDITADO',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Calle','[Campo]TX_CALLE',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Ciudad','[Campo]TX_MUNICIPIO',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Codigo Postal','[Campo]CV_CODIGO_POSTAL',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Colonia','[Campo]TX_COLONIA',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'CredNum','[Campo]CV_CREDITO',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Estado','[Campo]TX_DELEGACION',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Evento','Inspección de casa',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Municipio','[Campo]TX_MUNICIPIO',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'Acreditado','[Campo]TX_NOMBRE_ACREDITADO',@maxId,1,@idFormulario)

SET @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'NumExtInt','',@maxId,1,@idFormulario)

