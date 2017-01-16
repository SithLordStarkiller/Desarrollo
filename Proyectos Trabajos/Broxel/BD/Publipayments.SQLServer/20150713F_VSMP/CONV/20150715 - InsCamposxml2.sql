declare @maxId int
declare @idFormulario int
select @maxId=max(idCampo)  from CamposXML2
set @maxId=@maxId+1
select  @idFormulario = idFormulario from formulario where Estatus=1 and captura=1 and Ruta='CSP'

INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Calle','[Campo]TX_CALLE',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Ciudad','[Campo]TX_MUNICIPIO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'CodigoPostal','[Campo]CV_CODIGO_POSTAL',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Colonia','[Campo]TX_COLONIA',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'CV_CODIGO_POSTAL','[Campo]CV_CODIGO_POSTAL',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'CV_CREDITO','[Campo]CV_CREDITO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'CV_DELEGACION','[Campo]CV_DELEGACION',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'CV_DESPACHO','[Campo]CV_DESPACHO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'CV_RFC','[Campo]CV_RFC',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenAceptacion','DictamenAceptacion',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenAcreditadoNoDisponible','Acreditado No Disponible',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenAgendaCita','DictamenAgendaCita',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenHabixfami','Habitada por familia',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenHabter','Habitada por tercero',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenM1','Casa Abandonada',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenM2','Casa Deshabitada',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenM3','Casa Habitada',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenM4','Casa Vandalizada',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenNoInteresa','DictamenNoInteresa',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Dictamentercvissincont','Visita sin contacto',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenTraspaso','Traspaso',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Dictamenviviendailoc','Vivienda ilocalizable',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'DictamenVivInvadida','Vivienda Invadida',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Estado','[Campo]CV_DELEGACION',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'FH_NACIMIENTO','[Campo]FH_NACIMIENTO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'FH_OTORGAMIENTO','[Campo]FH_OTORGAMIENTO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'IDVISITA','[Campo]idVisita',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'IM_MONTO_RECUPERAR','[Campo]IM_MONTO_RECUPERAR',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'IM_PAGO_MENSUAL','[Campo]IM_PAGO_MENSUAL',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'IM_PRIMA_SEG_DAN','[Campo]IM_PRIMA_SEG_DAN',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'IM_SALDO','[Campo]IM_SALDO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'IM_SALDO_SEG_DAN','[Campo]IM_SALDO_SEG_DAN',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Municipio','[Campo]TX_MUNICIPIO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'Nombre','[Campo]TX_NOMBRE_ACREDITADO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'NU_MESES_RECUPERAR','[Campo]NU_MESES_RECUPERAR',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'NU_TELEFONO_CASA','[Campo]NU_TELEFONO_CASA',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'NU_TELEFONO_CELULAR','[Campo]NU_TELEFONO_CELULAR',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_CALLE','[Campo]TX_CALLE',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_CANAL','[Campo]CV_CANAL',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_COLONIA','[Campo]TX_COLONIA',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_CORREO_ELECTRONICO',' ',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_DESCRIPCION_ETIQUETA','[Campo]TX_DESCRIPCION_ETIQUETA',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_MUNICIPIO','[Campo]TX_MUNICIPIO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_NOMBRE_ACREDITADO','[Campo]TX_NOMBRE_ACREDITADO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_NOMBRE_DESPACHO','[Campo]TX_NOMBRE_DESPACHO',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_PAGO_1MES','[Campo]TX_PAGO_1MES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_PAGO_2MESES','[Campo]TX_PAGO_2MESES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_PAGO_3MESES','[Campo]TX_PAGO_3MESES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_PAGO_4MESES','[Campo]TX_PAGO_4MESES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_PAGO_MES_ACTUAL','0',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_SOLUCIONES','[Campo]TX_SOLUCIONES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_ULTIMA_GESTION_1MES','[Campo]TX_ULTIMA_GESTION_1MES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_ULTIMA_GESTION_2MESES','[Campo]TX_ULTIMA_GESTION_2MESES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_ULTIMA_GESTION_3MESES','[Campo]TX_ULTIMA_GESTION_3MESES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_ULTIMA_GESTION_4MESES','[Campo]TX_ULTIMA_GESTION_4MESES',@maxId,1,@idFormulario)
set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario]) VALUES (@maxId,'TX_VECTOR_PAGOS','[Campo]TX_VECTOR_PAGOS',@maxId,1,@idFormulario)
set @maxId=@maxId+1
GO
