Ext.define('app.view.Administracion.RegistroCertificacion.gridPreguntas', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridPreguntas',
    id: 'gridPreguntas',
    initComponent: function () {
        session();

        var contadorRespuestas = 0;
        var positivo = true;

        Ext.create('Ext.data.Store', {
            model: 'app.model.Administracion.RegistroCertificacion.mdRespuesta',
            storeId: 'stRespuestasTemporal'
        });

        Ext.apply(this, {
            // store: '',
            collapsible: false,
            loadMask: true,
            selModel:
            {
                pruneRemoved: false,
                preserveScrollOnRefresh: true
            },
            multiSelect: true,
            viewConfig:
            {
                trackOver: false
            },
            listeners: {
                rowclick: function (gridView) {
                    session();

                    var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                    if (gridView.getSelectionModel().getSelection()[0].data.idPregunta == 0) {
                        ///////////////////////Se actualiza GRID//////////////////////////////
                        Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();
                        var idPreguntaTemporal = gridView.getSelectionModel().getSelection()[0].data.idPreguntaTemporal;
                        var respuestasTemporal = [];
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                            if (rec.data.idPreguntaTemporal == idPreguntaTemporal)
                                respuestasTemporal.push(rec.copy());
                        });
                        Ext.data.StoreManager.lookup('stRespuestasTemporal').add(respuestasTemporal);
                        Ext.getCmp('gridRespuestas').reconfigure(Ext.data.StoreManager.lookup('stRespuestasTemporal'));
                        // Ext.getCmp('gridRespuestas').focus();
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }

                    else {
                        ///////////////////////Se actualiza GRID//////////////////////////////
                        Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();
                        var idPregunta = gridView.getSelectionModel().getSelection()[0].data.idPregunta;
                        var respuestasTemporal = [];
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                            if (rec.data.idPregunta == idPregunta)
                                respuestasTemporal.push(rec.copy());
                        });
                        Ext.data.StoreManager.lookup('stRespuestasTemporal').add(respuestasTemporal);
                        Ext.getCmp('gridRespuestas').reconfigure(Ext.data.StoreManager.lookup('stRespuestasTemporal'));
                        //Ext.getCmp('gridRespuestas').focus();
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    }
                    Ext.getCmp('panelRespuestasCertificaciones').setTitle('<span style="font-size: 160%;margin-top:20px;">Respuestas de la pregunta: ' + gridView.getSelectionModel().getSelection()[0].data.preCodigo + '</span>');

                    Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                }
            },
            columns: [
                {
                    xtype: 'rownumberer',
                    text: 'Orden',
                    width: '4%'
                },
                 {
                     text: "Código",
                     dataIndex: 'preCodigo',
                     width: '15%',
                     hidden: false,
                     align: 'center',
                     sortable: false
                 },
                {
                    text: "Pregunta",
                    dataIndex: 'preDescripcion',
                    width: '70%',
                    align: 'left',
                    hidden: false,
                    sortable: false
                },
                {
                    xtype: 'checkcolumn',
                    header: "Obligatoria",
                    dataIndex: 'preObligatoria',
                    width: '5%',
                    hidden: false,
                    align: 'center',
                    sortable: false,
                    stopSelection: false,
                    listeners: {
                        checkchange: function (column, recordIndex, checked, rowIndex) {

                            var //idFuncion = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idFuncion,
                            //idFuncionTemporal = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idFuncionTemporal,
                            idPregunta = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idPregunta,
                            idPreguntaTemporal = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idPreguntaTemporal;


                            ////////////////////////ACTUALIZO LA FUNCIÓN DENTRO DEL STORE//////////////////////////////////
                            if (idPregunta == 0) {
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                        .findRecord('idPreguntaTemporal', idPreguntaTemporal, 0, false, true, true).data.preObligatoria = checked;
                            } else {
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                        .findRecord('idPregunta', idPregunta, 0, false, true, true).data.preObligatoria = checked;
                            }
                            ///////////////////////////////////////////////////////////////////////////////
                        }
                    }
                },
                {
                    xtype: 'checkcolumn',
                    header: "Activa",
                    dataIndex: 'preActiva',
                    width: '5%',
                    hidden: false,
                    align: 'center',
                    sortable: false,
                    stopSelection: false,
                    listeners: {
                        checkchange: function (column, recordIndex, checked, rowIndex) {
                            var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                            var idFuncion = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idFuncion,
                            idFuncionTemporal = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idFuncionTemporal,
                            idPregunta = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idPregunta,
                            idPreguntaTemporal = Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.idPreguntaTemporal;


                            var respuestasPregunta = 0, respuestasCorrectas = 0;
                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                                if (rec.data.resActiva == true
                                    && rec.data.idPregunta == idPregunta
                                    && rec.data.idPreguntaTemporal == idPreguntaTemporal) {
                                    respuestasPregunta++;
                                    if (rec.data.resCorrecta)
                                        respuestasCorrectas++;
                                }
                            });
                            if (checked == true && (respuestasPregunta < 2 || respuestasCorrectas == 0)) {
                                Ext.MessageBox.alert('SICER', 'Es necesario que la pregunta tenga por lo menos dos respuestas activas y una correcta para ser activada');
                                Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.preActiva = false;
                                Ext.getCmp('gridPreguntas').getView().refresh(true);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                return;
                            }

                            ////////////////////////PREGUNTAS ACTIVAS - NO ACTIVAS///////////////////////////////////////////////////////////

                            var idCert = Ext.getCmp('hfIdCertificacion').getValue();

                            Ext.Ajax.request({
                                method: 'POST',
                                url: 'Catalogos/consultarCalificacion',
                                params: { idCertificacion: idCert },
                                failure: function () { },
                                success: function (response) {
                                    var regreso = Ext.decode(response.responseText);

                                    if (!regreso) {

                                        ////////////////////////CERTIFICACIÓN NO RESUELTA - PREGUNTA NO ACTIVA//////////////////////////

                                        ////////////////////////ACTUALIZO LA FUNCIÓN DENTRO DEL STORE//////////////////////////////////
                                        if (idPregunta == 0) {
                                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                                .findRecord('idPreguntaTemporal', idPreguntaTemporal, 0, false, true, true).data.preActiva = checked;
                                        } else {
                                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                                .findRecord('idPregunta', idPregunta, 0, false, true, true).data.preActiva = checked;
                                        }
                                        ///////////////////////////////////////////////////////////////////////////////
                                        desactivaFuncion(idFuncion, idFuncionTemporal);

                                        Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);

                                        positivo = false;
                                    }
                                }
                            });

                            if (positivo) {

                                ////////////////////////CERTIFICACIÓN RESUELTA - PREGUNTA ACTIVA///////////////////////////////////

                                var preguntas = 0;
                                Ext.getCmp('gridPreguntas').store.each(function (rec) {
                                    if (!rec.data.preActiva) {
                                        preguntas += 0;
                                    } else {
                                        preguntas++;
                                    }
                                });

                                if (checked == false && preguntas == 0) {
                                    Ext.MessageBox.alert('SICER', 'No se pueden desactivar todas las preguntas de la función.');
                                    Ext.getCmp('gridPreguntas').store.getAt(recordIndex).data.preActiva = true;
                                    Ext.getCmp('gridPreguntas').getView().refresh(true);

                                    Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                    return;
                                }

                                ////////////////////////ACTUALIZO LA FUNCIÓN DENTRO DEL STORE//////////////////////////////////
                                if (idPregunta == 0) {
                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                    .findRecord('idPreguntaTemporal', idPreguntaTemporal, 0, false, true, true).data.preActiva = checked;
                                } else {
                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                    .findRecord('idPregunta', idPregunta, 0, false, true, true).data.preActiva = checked;
                                }
                                ///////////////////////////////////////////////////////////////////////////////
                                desactivaFuncion(idFuncion, idFuncionTemporal);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                            }

                            ////////////////////////FIN PREGUNTAS ACTIVAS - NO ACTIVAS///////////////////////////////////////////////////////

//                            ////////////////////////ACTUALIZO LA FUNCIÓN DENTRO DEL STORE//////////////////////////////////
//                            if (idPregunta == 0) {
//                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
//                                    .findRecord('idPreguntaTemporal', idPreguntaTemporal, 0, false, true, true).data.preActiva = checked;
//                            } else {
//                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
//                                    .findRecord('idPregunta', idPregunta, 0, false, true, true).data.preActiva = checked;
//                            }
//                            ///////////////////////////////////////////////////////////////////////////////
//                            desactivaFuncion(idFuncion, idFuncionTemporal);

//                            Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                        }
                    }
                }
             ],
            tbar: [
                {
                    text: 'Modificar Pregunta',
                    tooltip: 'Modifica los datos de la pregunta seleccionada'
                    , handler: function () {
                        session();
                        if (Ext.getCmp('gridPreguntas').getSelectionModel().hasSelection()) {
                            var preguntaSelec = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0];


                            var window = new Ext.Window({
                                //id: 'window',
                                title: 'Modificar Pregunta',
                                resizable: false,
                                modal: true,
                                plain: true,
                                layout: {
                                    type: 'vbox',
                                    align: 'center'
                                },
                                bodyPadding: '10 10 10 10',
                                items: [
                                        {
                                            xtype: 'panel',

                                            bodyPadding: '10 10 10 10',
                                            //width: '100%',
                                            layout: {
                                                type: 'table',
                                                columns: 2
                                                //tdAttrs: { style: 'padding: 2px;' }
                                            },
                                            items: [

                                            /*{
                                            xtype: 'hiddenfield',
                                            id: 'hfImagenAgPre'
                                            , value: preguntaSelec.data.imagen
                                            },*/
                                                {
                                                xtype: 'hiddenfield',
                                                id: 'hfTipoArchivo',
                                                value: preguntaSelec.data.preTipoArchivo
                                            },
                                                {
                                                    xtype: 'hiddenfield',
                                                    id: 'hfIdentificador',
                                                    value: preguntaSelec.data.identificadorImagen
                                                },

                                                {
                                                    xtype: 'form',
                                                    id: 'formfotoAgPre',
                                                    border: false,
                                                    fileUpload: true,
                                                    //width: 200,
                                                    align: 'center',
                                                    margin: '0 20 0 30',
                                                    rowspan: 2,
                                                    layout: {
                                                        type: 'vbox',
                                                        align: 'center'
                                                        // ,pack: 'middle'
                                                    },
                                                    items: [
                                                        {
                                                            xtype: 'image',
                                                            id: 'imgFotoAgPre',
                                                            src: preguntaSelec.data.preTipoArchivo == 'I' ? cargaImagen(preguntaSelec.data.preNombreArchivo, preguntaSelec.data.identificadorImagen)
                                                                    : preguntaSelec.data.preTipoArchivo == 'P' ? 'Imagenes/adobePDF.png' : '',
                                                            height: 150, // Specifying height/width ensures correct layout
                                                            width: 150,
                                                            border: 1,
                                                            style: {
                                                                borderColor: 'black',
                                                                borderStyle: 'solid'
                                                            },
                                                            //margin: '20 50 5 50',
                                                            listeners: {
                                                                render: function (c) {
                                                                    c.getEl().on('click', function (e) {


                                                                        if (Ext.getCmp('hfTipoArchivo').getValue() == 'P') {
                                                                            if (Ext.getCmp('hfIdentificador').getValue() == '') {
                                                                                //CASO: hay un documento almacenado anteriormente y es un PDF
                                                                                var call = 'Generales/handlerPDF.ashx?nombreArchivo=' + preguntaSelec.data.preNombreArchivo + '';
                                                                                var frm = '<object type="text/html" data=' + call + ' style="float:left;width:100%;height:100%; background-image:url(../Imagenes/cargandoObjeto.gif); background-repeat:no-repeat; background-position:center; " />';
                                                                                var win = new Ext.Window({
                                                                                    title: 'Documento'
                                                                                    , plain: true
                                                                                    , height: 700
                                                                                    , width: 700
                                                                                    , constrain: true
                                                                                    , layaout: 'fit'
                                                                                    , autoScroll: true
                                                                                    , html: frm
                                                                                    , buttons: [
                                                                                                {
                                                                                                    text: 'Cerrar Documento', handler: function () { win.destroy(); }
                                                                                                }
                                                                                            ]
                                                                                });
                                                                                win.show();
                                                                            }
                                                                            else {
                                                                                //CASO: hay un documento en memoria
                                                                                var call = 'Generales/handlerPDFRegCert.ashx?identificador=' + Ext.getCmp('hfIdentificador').getValue() + '';
                                                                                var frm = '<object type="text/html" data=' + call + ' style="float:left;width:100%;height:100%; background-image:url(../Imagenes/cargandoObjeto.gif); background-repeat:no-repeat; background-position:center; " />';
                                                                                var win = new Ext.Window({
                                                                                    title: 'Documento'
                                                                                , plain: true
                                                                                , height: 700
                                                                                , width: 700
                                                                                , constrain: true
                                                                                , layaout: 'fit'
                                                                                , autoScroll: true
                                                                                , html: frm
                                                                                , buttons: [
                                                                                    {
                                                                                        text: 'Cerrar Documento', handler: function () { win.destroy(); }
                                                                                    }
                                                                                        ]
                                                                                });
                                                                                win.show();
                                                                            }
                                                                        }


                                                                    }, c);
                                                                }
                                                            }
                                                        },
                                                        {
                                                            xtype: 'filefield',
                                                            id: 'fotografiaAgPre',
                                                            emptyText: 'Selecciona un archivo',
                                                            buttonOnly: true,
                                                            hideLabel: true,
                                                            allowBlank: true,
                                                            //margin: '0 0 10 80',
                                                            name: 'FotoAgPre',
                                                            regex: /(.)+((\.png)|(\.jpg)|(\.jpeg)|(\.pdf)(\w)?)$/i,
                                                            regexText: 'Solo formatos PNG, JPG y PDF son aceptados.',
                                                            buttonText: 'Cargar Imagen/Documento',
                                                            uploadConfig: {
                                                                maxFileSize: 1000,
                                                                maxQueueLength: 100
                                                            },
                                                            listeners: {
                                                                'change': {
                                                                    fn: function (field, e) {
                                                                        session();
                                                                        //var form = field.up('form').getForm();
                                                                        var form = Ext.getCmp('formfotoAgPre').getForm();

                                                                        if (Ext.getCmp('fotografiaAgPre').validate()) {
                                                                            if (form.isValid()) {
                                                                                form.submit
                                                                                ({
                                                                                    url: 'Home/cargaImagenPDFExamen',
                                                                                    method: 'post',
                                                                                    success: function (form, action) {
                                                                                        /*var data = Ext.decode(action.response.responseText, action.response.message);
                                                                                        if (data != null) {
                                                                                        if (data.success == false) {
                                                                                        Ext.MessageBox.alert('SICER', '<b>' + data.response + '</b>');
                                                                                        } else {
                                                                                        Ext.getCmp('imgFotoAgPre').setSrc('data:image/jpeg;base64,' + data.strImagen);
                                                                                        Ext.getCmp('hfImagenAgPre').setValue(data.strImagen);
                                                                                        }
                                                                                        } else {
                                                                                        Ext.MessageBox.alert('SICER', '<b>Error al cargar la imagen</b>');
                                                                                        }*/

                                                                                    },
                                                                                    failure: function (form, action) {
                                                                                        //TRAMPA! siempre entra en el failure

                                                                                        var data = Ext.decode(action.response.responseText, action.response.message);

                                                                                        if (data.response != "") {
                                                                                            Ext.MessageBox.alert('SICER', data.response);
                                                                                        } else {
                                                                                            if (data.tipo == 'I') {
                                                                                                Ext.getCmp('imgFotoAgPre').setSrc('data:image/jpeg;base64,' + data.strImagen);
                                                                                                //Ext.getCmp('hfImagenAgPre').setValue(data.strImagen);
                                                                                                Ext.getCmp('hfTipoArchivo').setValue('I');
                                                                                                Ext.getCmp('hfIdentificador').setValue(data.identificador);
                                                                                            } else {
                                                                                                Ext.getCmp('imgFotoAgPre').setSrc('Imagenes/adobePDF.png');
                                                                                                //Ext.getCmp('hfImagenAgPre').setValue('');
                                                                                                Ext.getCmp('hfTipoArchivo').setValue('P');
                                                                                                Ext.getCmp('hfIdentificador').setValue(data.identificador);
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                });
                                                                            } else {
                                                                                Ext.Msg.alert("SICER", "Solo formatos PNG, JPG y PDF son aceptados.");
                                                                            }
                                                                        } else {
                                                                            Ext.Msg.alert("SICER", "Solo formatos PNG, JPG y PDF son aceptados.");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    ]
                                                },
                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbCodigoPregunta',
                                                    fieldLabel: 'Código Pregunta',
                                                    labelAlign: 'right',
                                                    labelWidth: 90,
                                                    width: 320,
                                                    //colspan:2,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[A-Za-zñÑ0-9-]/,
                                                    maxLength: 30,
                                                    enforceMaxLength: true,
                                                    value: Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.preCodigo,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase());
                                                        }
                                                    }
                                                },
                                                {
                                                    xtype: 'textarea',
                                                    id: 'txbTextoPregunta',
                                                    fieldLabel: 'Texto pregunta',
                                                    labelAlign: 'right',
                                                    labelWidth: 90,
                                                    width: 500,
                                                    height: 150,
                                                    //colspan: 2,
                                                    maxLength: 1000000,
                                                    enforceMaxLength: true,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_¿?¡!\r\n ]/,
                                                    value: Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.preDescripcion
                                                   /* ,listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase());
                                                        }
                                                    }*/
                                                }
                                            ],
                                            buttons: [
                                                        {
                                                            text: 'Aceptar', width: 100, handler: function () {
                                                                session();

                                                                if (Ext.getCmp('txbTextoPregunta').getRawValue() == (null || '')
                                                                  || Ext.getCmp('txbCodigoPregunta').getRawValue() == (null || '')
                                                                  ) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario ingresar un código y un texto a la Pregunta.');
                                                                    return false;
                                                                }



                                                                var idFuncionTemporal = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.idFuncionTemporal;
                                                                var idFuncion = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.idFuncion;
                                                                var idPregunta = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.idPregunta;
                                                                var idPreguntaTemporal = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.idPreguntaTemporal;


                                                                var retorno = false;
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
                                                                    if (!(rec.data.idPregunta == idPregunta && rec.data.idPreguntaTemporal == idPreguntaTemporal) &&
                                                                             Ext.getCmp('txbCodigoPregunta').getValue() == rec.data.preCodigo)
                                                                    { retorno = true; return; }
                                                                });
                                                                if (retorno) {
                                                                    Ext.MessageBox.alert('SICER', 'Alguna pregunta tiene el mismo código de pregunta');
                                                                    return;
                                                                }

                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();


                                                                if (idPregunta == 0) {
                                                                    var preguntaRecord = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                                                            .findRecord('idPreguntaTemporal', idPreguntaTemporal, 0, false, true, true);
                                                                }
                                                                else {
                                                                    var preguntaRecord = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas')
                                                                            .findRecord('idPregunta', idPregunta, 0, false, true, true);
                                                                }

                                                                preguntaRecord.set('preDescripcion', Ext.getCmp('txbTextoPregunta').getValue());
                                                                preguntaRecord.set('preCodigo', Ext.getCmp('txbCodigoPregunta').getValue());
                                                                preguntaRecord.set('preTipoArchivo', Ext.getCmp('hfTipoArchivo').getValue());
                                                                //preguntaRecord.set('imagen', Ext.getCmp('hfImagenAgPre').getValue());
                                                                preguntaRecord.set('identificadorImagen', Ext.getCmp('hfIdentificador').getValue());


                                                                ///////////////////////Se actualiza GRID//////////////////////////////
                                                                Ext.data.StoreManager.lookup('stPreguntasTemporal').removeAll();
                                                                var preguntasTemporal = [];
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
                                                                    if (rec.data.idFuncionTemporal == idFuncionTemporal && rec.data.idFuncion == idFuncion)
                                                                        preguntasTemporal.push(rec.copy());
                                                                });
                                                                Ext.data.StoreManager.lookup('stPreguntasTemporal').add(preguntasTemporal);
                                                                Ext.getCmp('gridPreguntas').reconfigure(Ext.data.StoreManager.lookup('stPreguntasTemporal'));
                                                                ////////////////////////////////////////////////////////////////////////


                                                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);

                                                                window.destroy();
                                                            }
                                                        },

                                                        {
                                                            text: 'Cancelar', width: 100, handler: function () {
                                                                session();
                                                                window.destroy();
                                                            }
                                                        }
                                                    ]
                                        }


                                 ]
                            });
                            window.show();

                        }
                        else {
                            Ext.MessageBox.alert('SICER', 'Debes seleccionar una pregunta');
                        }

                    }
                },
                 '-',
                { text: 'Agregar Respuesta',
                    tooltip: 'Agrega una Respuesta a la pregunta seleccionada'
                    , handler: function () {
                        session();
                        if (Ext.getCmp('gridPreguntas').getSelectionModel().hasSelection()) {
                            var window = new Ext.Window({
                                //id: 'window',
                                title: 'Agregar Respuesta',
                                resizable: false,
                                modal: true,
                                plain: true,
                                layout: {
                                    type: 'vbox',
                                    align: 'center'
                                },
                                bodyPadding: '10 10 10 10',
                                items: [
                                        {
                                            xtype: 'panel',

                                            bodyPadding: '10 10 10 10',
                                            //width: '100%',
                                            layout: {
                                                type: 'table',
                                                columns: 2
                                                //tdAttrs: { style: 'padding: 2px;' }
                                            },
                                            items: [

                                            /* {
                                            xtype: 'hiddenfield',
                                            id: 'hfImagenAgRes'
                                            },*/
                                                {
                                                xtype: 'hiddenfield',
                                                id: 'hfTipoArchivo',
                                                value: ''
                                            },
                                                {
                                                    xtype: 'hiddenfield',
                                                    id: 'hfIdentificador',
                                                    value: ''
                                                },

                                                {
                                                    xtype: 'form',
                                                    id: 'formfotoAgRes',
                                                    border: false,
                                                    fileUpload: true,
                                                    //width: 200,
                                                    align: 'center',
                                                    margin: '0 20 0 30',
                                                    rowspan: 2,
                                                    layout: {
                                                        type: 'vbox',
                                                        align: 'center'
                                                        // ,pack: 'middle'
                                                    },
                                                    items: [
                                                        {
                                                            xtype: 'image',
                                                            id: 'imgFotoAgRes',
                                                            //src: 'Imagenes/UserFoto.png',
                                                            height: 150, // Specifying height/width ensures correct layout
                                                            width: 150,
                                                            border: 1,
                                                            style: {
                                                                borderColor: 'black',
                                                                borderStyle: 'solid'
                                                            },
                                                            //margin: '20 50 5 50',
                                                            listeners: {
                                                                render: function (c) {
                                                                    c.getEl().on('click', function (e) {
                                                                        // alert('User clicked image');
                                                                        if (Ext.getCmp('hfTipoArchivo').getValue() == 'P') {
                                                                            var call = 'Generales/handlerPDFRegCert.ashx?identificador=' + Ext.getCmp('hfIdentificador').getValue() + '';
                                                                            var frm = '<object type="text/html" data=' + call + ' style="float:left;width:100%;height:100%; background-image:url(../Imagenes/cargandoObjeto.gif); background-repeat:no-repeat; background-position:center; " />';
                                                                            var win = new Ext.Window({
                                                                                title: 'Documento'
                                                                                , plain: true
                                                                                , height: 700
                                                                                , width: 700
                                                                                , constrain: true
                                                                                , layaout: 'fit'
                                                                                , autoScroll: true
                                                                                , html: frm
                                                                                , buttons: [
                                                                                            {
                                                                                                text: 'Cerrar Documento', handler: function () { win.destroy(); }
                                                                                            }
                                                                                        ]
                                                                            });
                                                                            win.show();
                                                                        }


                                                                    }, c);
                                                                }
                                                            }
                                                        },
                                                        {
                                                            xtype: 'filefield',
                                                            id: 'fotografiaAgRes',
                                                            emptyText: 'Selecciona un archivo',
                                                            buttonOnly: true,
                                                            hideLabel: true,
                                                            allowBlank: true,
                                                            //margin: '0 0 10 80',
                                                            name: 'FotoAgRes',
                                                            regex: /(.)+((\.png)|(\.jpg)|(\.jpeg)|(\.pdf)(\w)?)$/i,
                                                            regexText: 'Solo formatos PNG, JPG y PDF son aceptados.',
                                                            buttonText: 'Cargar Imagen/Documento',
                                                            uploadConfig: {
                                                                maxFileSize: 1000,
                                                                maxQueueLength: 100
                                                            },
                                                            listeners: {
                                                                'change': {
                                                                    fn: function (field, e) {
                                                                        session();
                                                                        //var form = field.up('form').getForm();
                                                                        var form = Ext.getCmp('formfotoAgRes').getForm();

                                                                        if (Ext.getCmp('fotografiaAgRes').validate()) {
                                                                            if (form.isValid()) {
                                                                                form.submit
                                                                                ({
                                                                                    url: 'Home/cargaImagenPDFExamen',
                                                                                    method: 'post',
                                                                                    success: function (form, action) {
                                                                                        /*var data = Ext.decode(action.response.responseText, action.response.message);
                                                                                        if (data != null) {
                                                                                        if (data.success == false) {
                                                                                        Ext.MessageBox.alert('SICER', '<b>' + data.response + '</b>');
                                                                                        } else {
                                                                                        Ext.getCmp('imgFotoAgPre').setSrc('data:image/jpeg;base64,' + data.strImagen);
                                                                                        Ext.getCmp('hfImagenAgPre').setValue(data.strImagen);
                                                                                        }
                                                                                        } else {
                                                                                        Ext.MessageBox.alert('SICER', '<b>Error al cargar la imagen</b>');
                                                                                        }*/

                                                                                    },
                                                                                    failure: function (form, action) {
                                                                                        //TRAMPA! siempre entra en el failure

                                                                                        var data = Ext.decode(action.response.responseText, action.response.message);

                                                                                        if (data.response != "") {
                                                                                            Ext.MessageBox.alert('SICER', data.response);
                                                                                        } else {
                                                                                            if (data.tipo == 'I') {
                                                                                                Ext.getCmp('imgFotoAgRes').setSrc('data:image/jpeg;base64,' + data.strImagen);
                                                                                                //Ext.getCmp('hfImagenAgRes').setValue(data.strImagen);
                                                                                                Ext.getCmp('hfTipoArchivo').setValue('I');
                                                                                                Ext.getCmp('hfIdentificador').setValue(data.identificador);
                                                                                            } else {
                                                                                                Ext.getCmp('imgFotoAgRes').setSrc('Imagenes/adobePDF.png');
                                                                                                //Ext.getCmp('hfImagenAgRes').setValue('');
                                                                                                Ext.getCmp('hfTipoArchivo').setValue('P');
                                                                                                Ext.getCmp('hfIdentificador').setValue(data.identificador);
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                });
                                                                            } else {
                                                                                Ext.Msg.alert("SICER", "Solo formatos PNG, JPG y PDF son aceptados.");
                                                                            }
                                                                        } else {
                                                                            Ext.Msg.alert("SICER", "Solo formatos PNG, JPG y PDF son aceptados.");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    ]
                                                },
                                                {
                                                    xtype: 'textarea',
                                                    id: 'txbTextoRespuesta',
                                                    fieldLabel: 'Texto Respuesta',
                                                    labelAlign: 'right',
                                                    labelWidth: 90,
                                                    width: 500,
                                                    //colspan:2,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_¿?¡!\r\n ]/,
                                                    maxLength: 1000000,
                                                    enforceMaxLength: true
                                                    /*,listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase());
                                                        }
                                                    }*/
                                                },
                                                {
                                                    xtype: 'textarea',
                                                    id: 'txbExpRespuesta',
                                                    fieldLabel: 'Explicación de la Respuesta',
                                                    labelAlign: 'right',
                                                    labelWidth: 90,
                                                    width: 500,
                                                    height: 150,
                                                    //colspan: 2,
                                                    maxLength: 1000000,
                                                    enforceMaxLength: true,
                                                    //allowBlank: false,
                                                    //blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_¿?¡!\r\n ]/
                                                    /*,listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase());
                                                        }
                                                    }*/
                                                }
                                            ],
                                            buttons: [
                                                        {
                                                            text: 'Aceptar', width: 100, handler: function () {
                                                                session();

                                                                if (Ext.getCmp('txbTextoRespuesta').getRawValue() == (null || '')
                                                                //|| Ext.getCmp('txbCodigoRespuesta').getRawValue() == (null || '')
                                                                  ) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario ingresar un texto a la Respuesta.');
                                                                    return false;
                                                                }


                                                                /*
                                                                var idFuncionTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncionTemporal;
                                                                var idF = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncion;

                                                                var idTemaTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idTemaTemporal;
                                                                var idTema = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idTema;
                                                                
                                                                var retorno = false;
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                                                                if (//!(rec.data.idPregunta == idP && rec.data.idFuncionTemporal == idFuncionTemporal) &&
                                                                Ext.getCmp('txbTextoRespuesta').getValue() == rec.data.preCodigo)
                                                                retorno = true;
                                                                });
                                                                if (retorno) {
                                                                Ext.MessageBox.alert('SICER', 'Alguna Respuesta tiene el mismo código de respuesta');
                                                                return;
                                                                }
                                                                */

                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();


                                                                contadorRespuestas = contadorRespuestas + 1;

                                                                var idPreguntaTemporal = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.idPreguntaTemporal;
                                                                var idPregunta = Ext.getCmp('gridPreguntas').getSelectionModel().getSelection()[0].data.idPregunta;

                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').add({
                                                                    resDescripcion: Ext.getCmp('txbTextoRespuesta').getValue(),
                                                                    resExplicacion: Ext.getCmp('txbExpRespuesta').getValue(),
                                                                    resActiva: 0,
                                                                    resCorrecta: 0,
                                                                    resNombreArchivo: '',
                                                                    resTipoArchivo: Ext.getCmp('hfTipoArchivo').getValue(),
                                                                    //imagen: Ext.getCmp('hfImagenAgRes').getValue(),
                                                                    identificadorImagen: Ext.getCmp('hfIdentificador').getValue(),
                                                                    idPreguntaTemporal: idPreguntaTemporal,
                                                                    idPregunta: idPregunta,
                                                                    idRespuestaTemporal: contadorRespuestas
                                                                });

                                                                ///////////////////////Se actualiza GRID//////////////////////////////
                                                                Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();
                                                                var respuestasTemporal = [];
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                                                                    if (rec.data.idPreguntaTemporal == idPreguntaTemporal && rec.data.idPregunta == idPregunta)
                                                                        respuestasTemporal.push(rec.copy());
                                                                });
                                                                Ext.data.StoreManager.lookup('stRespuestasTemporal').add(respuestasTemporal);
                                                                Ext.getCmp('gridRespuestas').reconfigure(Ext.data.StoreManager.lookup('stRespuestasTemporal'));
                                                                ////////////////////////////////////////////////////////////////////////


                                                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                                                window.destroy();
                                                            }
                                                        },

                                                        {
                                                            text: 'Cancelar', width: 100, handler: function () {
                                                                session();
                                                                window.destroy();
                                                            }
                                                        }
                                                    ]
                                        }


                                 ]
                            });
                            window.show();

                        }
                        else {
                            Ext.MessageBox.alert('SICER', 'Debes seleccionar una función');
                        }
                    }
                }

                ]

        })
        this.callParent(arguments);
    }
});


function cargaImagen(nombreArchivo,identificador) {

    var imagen = 'data:image/jpeg;base64,';
    Ext.Ajax.request({
        method: 'POST',
        url: 'Home/cargaImagen',
        params: { nombreArchivo: nombreArchivo, identificador: identificador },
        failure: function () {
            imagen = 'Imagenes/imgNoDisponible.png';
            if (Ext.getCmp('imgFotoAgPre')!=undefined)
                Ext.getCmp('imgFotoAgPre').setSrc(imagen);
            if (Ext.getCmp('imgFotoAgRes') != undefined)
                Ext.getCmp('imgFotoAgRes').setSrc(imagen);
        },
        success: function (response) {
            var retorno = Ext.decode(response.responseText);

            if (retorno.response == '') {
                imagen += retorno.strImagen;
            } else {
                imagen='Imagenes/imgNoDisponible.png';
            }
            if (Ext.getCmp('imgFotoAgPre') != undefined)
                Ext.getCmp('imgFotoAgPre').setSrc(imagen);

            if (Ext.getCmp('imgFotoAgRes') != undefined)
                Ext.getCmp('imgFotoAgRes').setSrc(imagen);
        }
    });



    return 'Imagenes/cargandoObjeto.gif';

}
