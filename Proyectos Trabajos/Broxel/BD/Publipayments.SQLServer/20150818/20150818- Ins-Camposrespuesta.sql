declare @idcampo int,@idformulario int
select @idcampo=max(idcampo) from camposrespuesta

select @idformulario=idformulario from formulario where estatus=1 and ruta='vsmp' and captura=1


set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'comentario_noLocalizo',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ComproAcreditado2',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenAcreditadoNoDisponible',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenAgendaCita',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenHabter',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenM1',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenM2',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenM4',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenM5',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenNoInteresa',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenNoInteresaVSM2',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Dictamentercvissincont',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenTraspaso',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Dictamenviviendailoc',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenVivInvadida',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FechaCita',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'foto_interior',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'foto_marco',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'foto_vecino',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NegativaAcConInf',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NegativaAcSinInf',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'OtrosNegAcNo',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'OtrosNegAcSI',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Parentesco',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'txt_Medidor',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'vec1',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'vec2',1,null,@idformulario)

GO


