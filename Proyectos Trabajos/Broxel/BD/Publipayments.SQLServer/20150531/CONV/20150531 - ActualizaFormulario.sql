exec ActualizaFormulario @idAplicacion =1,@nombre= 'CobSocial',@version='8.0'

Declare @campo int=0,@idFormulario int=0
select @campo=MAX(idCampo) from CamposXML2
select @idFormulario=idFormulario from Formulario where Version='8.0' and Nombre='CobSocial' and Estatus=1 and captura=1
set @campo=@campo+1
insert into CamposXML2
select @campo idCampo,'DictamenpromdepagoFPP' Nombre,'Promesa de pago FPP' Valor,@campo Orden,1 Activo,@idFormulario idFormulario
set @campo=@campo+1
insert into CamposXML2
select @campo idCampo,'AcreditadoRegCan' Nombre,'[Campo]IN_SOLICITAR_SERVICIO_EMPLEO' Valor,@campo Orden,1 Activo,@idFormulario idFormulario
set @campo=@campo+1
insert into CamposXML2
select @campo idCampo,'TX_PORTAFOLIO' Nombre,'[Campo]TX_PORTAFOLIO' Valor,@campo Orden,1 Activo,@idFormulario idFormulario
set @campo=@campo+1
insert into CamposXML2
select @campo idCampo,'DicAplicaMttoFPP' Nombre,'[Campo]DicAplicaMttoFPP' Valor,@campo Orden,1 Activo,@idFormulario idFormulario

delete from CamposXML2 where Nombre='AcreditadoRegCan' and idFormulario=@idFormulario and Valor='No'
