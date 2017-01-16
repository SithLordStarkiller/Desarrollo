declare @idcampo int,@idformulario int
select @idcampo=max(idcampo) from camposrespuesta

select @idformulario=idformulario from formulario where estatus=1 and ruta='vsmp' and captura=1

set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ExternalType',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'AssignedTo',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'InitialDate',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FinalDate',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ResponseDate',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'InitialLatitude',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FinalLatitude',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'InitialLongitude',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FinalLongitude',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FormiikResponseSource',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'IM_PRIMA_SEG_DAN',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'IM_SALDO_SEG_DAN',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'gps_automatico',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'vivLoc',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Alguienviv',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'vivHab',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Dictamentercvissincont',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'comentario_noLocalizo',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Dictamenviviendailoc',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'locAcr',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'locAcrSol',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FotoAcreditadoSol',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'reqAct',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'IntCamVSM',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ContIntCamVSM',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FirmaMom',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenAceptacion',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'AcreditadoHabita',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenAcreditadoNoDisponible',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'AcreditadoPropietario',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ComproAcreditado2',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenTraspaso',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenNoInteresa',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NegativaAcSinInf',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ApellidoPaternoAcr',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'ApellidoMaternoAcr',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NombreAcr',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NU_TELEFONO_CASA_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NU_TELEFONO_CELULAR_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'NU_TELEFONO_TRABAJO_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_CALLE_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_NU_CALLE_INT_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_COLONIA_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_MUNICIPIO_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_ESTADO_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'CV_CODIGO_POSTAL_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_ENTRE_CALLE1_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_ENTRE_CALLE2_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FechaCita',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenAgendaCita',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'FamiliarDirecto',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenHabixfami',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_CORREO_ELECTRONICO_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_NU_CALLE_EXT_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'TX_EDIFICIO_ACT',2,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'selMuebles',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'selMtto',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'selVentanas',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'selGrafitt',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'selMedluz',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'foto_fachada',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'Fotomedidor',1,null,@idformulario)
set @idcampo=@idcampo+1
INSERT INTO [dbo].[CamposRespuesta]([idCampo],[Nombre],[Tipo],[Referencia],[idFormulario])VALUES(@idcampo,'DictamenM3',1,null,@idformulario)
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