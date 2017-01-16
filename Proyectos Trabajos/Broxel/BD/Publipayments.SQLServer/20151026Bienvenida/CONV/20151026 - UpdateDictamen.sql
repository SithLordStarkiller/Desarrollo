declare @idcampo int,@idformulario int
select @idcampo=max(idcampo) from [CamposXML2]

select @idformulario=idformulario from formulario where Nombre='Bienvenida' and captura=1 and estatus=1


update camposxml2 set valor='Realizar nueva visita (En 7 u 8 días)' where idformulario=@idformulario and nombre='DictamenAgendarNuevaVisita1'
update camposxml2  set valor='Habitada por tercero con negativa de pago'  where idformulario=@idformulario and nombre ='DictamenHabTerceroPagTerceroSinTraslado'
update camposxml2  set valor='Habitada por tercero con localizacion acreditado'  where idformulario=@idformulario and nombre ='DictamenHabTerceroPagTercero'
update camposxml2  set valor='Habitada por familiar con negativa de pago'  where idformulario=@idformulario and nombre ='DictamenHabFamiliarSinPago'
update camposxml2  set valor='Habitada por familiar sin localizacion acreditado'  where idformulario=@idformulario and nombre ='DictamenHabFamPagFamSinTraslado'
update camposxml2  set valor='Habitada por familiar con localizacion acreditado'  where idformulario=@idformulario and nombre ='DictamenHabFamPagFam'

set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposXML2] ([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
VALUES (@idcampo,'DictamenNoAtendio','No atendió',@idcampo,1, @idformulario)

set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposXML2] ([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES (@idcampo,'DictamenViviendaIlocalizable','Vivienda Ilocalizable',@idcampo,1, @idformulario)


set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposXML2] ([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES (@idcampo,'DictamenrecoEscriVivienda','Cuenta con escrituras',@idcampo,1, @idformulario)

set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposXML2] ([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES (@idcampo,'DictamenVisAdicional','Genera visita adicional',@idcampo,1, @idformulario)

	 
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposXML2] ([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES (@idcampo,'DictamenFinGestion','Fin de gestion',@idcampo,1, @idformulario)





	 