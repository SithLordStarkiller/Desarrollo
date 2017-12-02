Ext.define('app.view.Administracion.RegistroCertificacion.gridRespuestas', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridRespuestas',
    id: 'gridRespuestas',
    initComponent: function () {
        session();

        Ext.apply(this, {
            // store: '',
            collapsible: false,
            loadMask: true,
            selModel:
            {
                pruneRemoved: false
            },
            multiSelect: true,
            viewConfig:
            {
                trackOver: false,
                preserveScrollOnRefresh: true
            },

            columns: [
                {
                    xtype: 'rownumberer',
                    text: 'Orden',
                    width: '4%'
                },
                {
                    text: "Respuestas",
                    dataIndex: 'resDescripcion',
                    width: '85%',
                    align: 'left',
                    hidden: false,
                    sortable: false
                },
                {
                    xtype: 'checkcolumn',
                    header: "Correcta",
                    dataIndex: 'resCorrecta',
                    width: '5%',
                    hidden: false,
                    align: 'center',
                    stopSelection: false,
                    sortable: false,
                    listeners: {
                        checkchange: function (column, recordIndex, checked, rowIndex) {
                            var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();
                            var 
                            idPregunta = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idPregunta,
                            idPreguntaTemporal = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idPreguntaTemporal,
                            idRespuesta = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idRespuesta,
                            idRespuestaTemporal = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idRespuestaTemporal;

                            ////////////////////////RESPUESTAS CORRECTAS//////////////////////////////////////////
                            var respuestasCorrectas = 0, respuestaCorrectaActiva = true;
                            Ext.getCmp('gridRespuestas').store.each(function (rec) {
                                if (!rec.data.resCorrecta) {
                                    respuestasCorrectas += 0;
                                } else {
                                    respuestasCorrectas++;
                                }

                                if (rec.data.resActiva == false
                                    && rec.data.idRespuesta == idRespuesta
                                    && rec.data.idRespuestaTemporal == idRespuestaTemporal) {

                                    respuestaCorrectaActiva = false;
                                }
                            });

                            if (checked == true && respuestaCorrectaActiva == false) {
                                Ext.MessageBox.alert('SICER', 'No se puede marcar como correcta porque la respuesta no esta activa.');
                                Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.resCorrecta = false;
                                Ext.getCmp('gridRespuestas').getView().refresh(true);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                return;
                            }

                            if (checked == false && respuestasCorrectas == 0) {
                                Ext.MessageBox.alert('SICER', 'Es necesario que la pregunta tenga por lo menos una respuesta correcta.');
                                Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.resCorrecta = true;
                                Ext.getCmp('gridRespuestas').getView().refresh(true);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                return;
                            }

                            /////////////////////////////////////////////////////////////////////////////////////////////////

                            ////////////////////////ACTUALIZO LA RESPUESTA DENTRO DEL STORE//////////////////////////////////
                            if (idRespuesta == 0) {
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas')
                                        .findRecord('idRespuestaTemporal', idRespuestaTemporal, 0, false, true, true).data.resCorrecta = checked;
                            } else {
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas')
                                        .findRecord('idRespuesta', idRespuesta, 0, false, true, true).data.resCorrecta = checked;
                            }
                            ///////////////////////////////////////////////////////////////////////////////

                            desactivaPregunta(idPregunta, idPreguntaTemporal);
                            Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);

                        }
                    }
                },
                {
                    xtype: 'checkcolumn',
                    header: "Activa",
                    dataIndex: 'resActiva',
                    width: '5%',
                    hidden: false,
                    align: 'center',
                    stopSelection: false,
                    sortable: false,
                    listeners: {
                        checkchange: function (column, recordIndex, checked, rowIndex) {
                            var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();
                            var 
                            idPregunta = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idPregunta,
                            idPreguntaTemporal = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idPreguntaTemporal,
                            idRespuesta = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idRespuesta,
                            idRespuestaTemporal = Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.idRespuestaTemporal;

                            ////////////////////////RESPUESTAS ACTIVAS - NO ACTIVAS//////////////////////////////////////////
                            var respuestas = 0, respuestaCorrecta = false;
                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                                if (rec.data.resActiva == true
                                    && rec.data.idPregunta == idPregunta
                                    && rec.data.idPreguntaTemporal == idPreguntaTemporal) {
                                    respuestas++;
                                }

                                if (rec.data.resActiva == true
                                    && rec.data.idRespuesta == idRespuesta
                                    && rec.data.idRespuestaTemporal == idRespuestaTemporal
                                    && rec.data.resCorrecta == true) {

                                    respuestaCorrecta = true;
                                }
                            });

                            if (checked == false && respuestaCorrecta == true) {
                                Ext.MessageBox.alert('SICER', 'No se puede desactivar la respuesta porque es una respuesta correcta');
                                Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.resActiva = true;
                                Ext.getCmp('gridRespuestas').getView().refresh(true);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                return;
                            }

                            if (checked == false && respuestas < 3) {
                                Ext.MessageBox.alert('SICER', 'Es necesario que la pregunta tenga por lo menos dos respuestas activas');
                                Ext.getCmp('gridRespuestas').store.getAt(recordIndex).data.resActiva = true;
                                Ext.getCmp('gridRespuestas').getView().refresh(true);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                return;
                            }
                            ///////////////////////////////////////////////////////////////////////////////////////////////

                            ////////////////////////ACTUALIZO LA RESPUESTA DENTRO DEL STORE//////////////////////////////////
                            if (idRespuesta == 0) {
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas')
                                        .findRecord('idRespuestaTemporal', idRespuestaTemporal, 0, false, true, true).data.resActiva = checked;
                            } else {
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas')
                                        .findRecord('idRespuesta', idRespuesta, 0, false, true, true).data.resActiva = checked;
                            }
                            ///////////////////////////////////////////////////////////////////////////////
                            desactivaPregunta(idPregunta, idPreguntaTemporal);
                            Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                        }
                    }
                }

            ],
            tbar: [
                {
                    text: 'Modificar Respuesta',
                    tooltip: 'Modifica los datos de la Respuesta seleccionada'
                    , handler: function () {
                        session();
                        if (Ext.getCmp('gridRespuestas').getSelectionModel().hasSelection()) {

                            var respuestaSelec = Ext.getCmp('gridRespuestas').getSelectionModel().getSelection()[0];

                            var window = new Ext.Window({
                                //id: 'window',
                                title: 'Modificar Respuesta',
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
                                            id: 'hfImagenAgRes',
                                            value: respuestaSelec.data.imagen
                                            },*/
                                                {
                                                xtype: 'hiddenfield',
                                                id: 'hfTipoArchivo',
                                                value: respuestaSelec.data.resTipoArchivo
                                            },
                                                {
                                                    xtype: 'hiddenfield',
                                                    id: 'hfIdentificador',
                                                    value: respuestaSelec.data.identificadorImagen
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
                                                            src: respuestaSelec.data.resTipoArchivo == 'I' ? cargaImagen(respuestaSelec.data.resNombreArchivo, respuestaSelec.data.identificadorImagen)
                                                            : respuestaSelec.data.resTipoArchivo == 'P' ? 'Imagenes/adobePDF.png' : '',
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
                                                                            if (Ext.getCmp('hfIdentificador').getValue() == '') {
                                                                                var call = 'Generales/handlerPDF.ashx?nombreArchivo=' + respuestaSelec.data.resNombreArchivo + '';
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

                                                                            } else {

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
                                                                                                // Ext.getCmp('hfImagenAgRes').setValue('');
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
                                                    enforceMaxLength: true,
                                                    value: respuestaSelec.data.resDescripcion
                                                   /* ,listeners: {
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
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_¿?¡!\r\n ]/,
                                                    value: respuestaSelec.data.resExplicacion
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
                                                                //|| Ext.getCmp('txbExpRespuesta').getRawValue() == (null || '')
                                                                  ) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario ingresar un texto a la Respuesta.');
                                                                    return false;
                                                                }

                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                                                                var idRespuestaTemporal = Ext.getCmp('gridRespuestas').getSelectionModel().getSelection()[0].data.idRespuestaTemporal;
                                                                var idRespuesta = Ext.getCmp('gridRespuestas').getSelectionModel().getSelection()[0].data.idRespuesta;
                                                                var idPreguntaTemporal = Ext.getCmp('gridRespuestas').getSelectionModel().getSelection()[0].data.idPreguntaTemporal;
                                                                var idPregunta = Ext.getCmp('gridRespuestas').getSelectionModel().getSelection()[0].data.idPregunta;
                                                                //var id = 'stFuncionesTema' + gridView.getSelectionModel().getSelection()[0].data.idTemporal;

                                                                if (idRespuestaTemporal != 0) {
                                                                    var respuestaRecord = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas')
                                                                            .findRecord('idRespuestaTemporal', idRespuestaTemporal, 0, false, true, true);
                                                                }
                                                                else {
                                                                    var respuestaRecord = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas')
                                                                            .findRecord('idRespuesta', idRespuesta, 0, false, true, true);

                                                                }


                                                                respuestaRecord.set('resDescripcion', Ext.getCmp('txbTextoRespuesta').getValue());
                                                                respuestaRecord.set('resExplicacion', Ext.getCmp('txbExpRespuesta').getValue());
                                                                respuestaRecord.set('resTipoArchivo', Ext.getCmp('hfTipoArchivo').getValue());
                                                                // respuestaRecord.set('imagen', Ext.getCmp('hfImagenAgRes').getValue());
                                                                respuestaRecord.set('identificadorImagen', Ext.getCmp('hfIdentificador').getValue());

                                                                //////////Se actualiza GRID//////////////////////////////
                                                                Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();
                                                                var respuestasTemporal = [];
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
                                                                    if (rec.data.idPreguntaTemporal == idPreguntaTemporal && rec.data.idPregunta == idPregunta)
                                                                        respuestasTemporal.push(rec.copy());
                                                                });
                                                                Ext.data.StoreManager.lookup('stRespuestasTemporal').add(respuestasTemporal);
                                                                Ext.getCmp('gridRespuestas').reconfigure(Ext.data.StoreManager.lookup('stRespuestasTemporal'));
                                                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



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
                        } else {
                            Ext.MessageBox.alert('SICER', 'Debes seleccionar una respuesta');
                        }
                    }
                }
           ]
        })
        this.callParent(arguments);
    }
});