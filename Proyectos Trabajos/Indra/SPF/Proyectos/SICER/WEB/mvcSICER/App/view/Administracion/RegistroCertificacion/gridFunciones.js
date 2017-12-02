Ext.define('app.view.Administracion.RegistroCertificacion.gridFunciones', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridFunciones',
    id: 'gridFunciones',
    initComponent: function () {
        session();
        var contadorPreguntas = 0;
        var pregTema = 0;
        var tiemTema = 0;
        var contTiemFuncion = 0;
        var contPregFuncion = 0;

        Ext.create('Ext.data.Store', {
            model: 'app.model.Administracion.RegistroCertificacion.mdPregunta',
            storeId: 'stPreguntasTemporal'
        });

        Ext.apply(this, {
            //store: 'stFuncionesTemporal',
            collapsible: false,
            loadMask: true,
            selModel:
            {
                pruneRemoved: false
            },
            multiSelect: true,
            viewConfig:
            {
                trackOver: false
                //,preserveScrollOnRefresh: true
            },

            listeners: {
                rowclick: function (gridView) {
                    session();
                    /*Ext.MessageBox.show({
                    msg: 'Actualizando preguntas de función...',
                    progressText: 'Procesando...',
                    width: 200,
                    wait: true,
                    icon: 'ext-mb-download'
                    });*/
                    var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                    if (gridView.getSelectionModel().getSelection()[0].data.idFuncion == 0) {
                        ///////////////////////Se actualiza GRID//////////////////////////////
                        Ext.data.StoreManager.lookup('stPreguntasTemporal').removeAll();
                        var idFuncionTemporal = gridView.getSelectionModel().getSelection()[0].data.idFuncionTemporal;
                        var preguntasTemporal = [];
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
                            if (rec.data.idFuncionTemporal == idFuncionTemporal)
                                preguntasTemporal.push(rec.copy());
                        });
                        Ext.data.StoreManager.lookup('stPreguntasTemporal').add(preguntasTemporal);
                        Ext.getCmp('gridPreguntas').reconfigure(Ext.data.StoreManager.lookup('stPreguntasTemporal'));
                        Ext.getCmp('gridRespuestas').store.removeAll();
                        //Ext.getCmp('gridRespuestas').focus();

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }

                    else {
                        ///////////////////////Se actualiza GRID//////////////////////////////
                        Ext.data.StoreManager.lookup('stPreguntasTemporal').removeAll();
                        var idFuncion = gridView.getSelectionModel().getSelection()[0].data.idFuncion;
                        var preguntasTemporal = [];
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
                            if (rec.data.idFuncion == idFuncion)
                                preguntasTemporal.push(rec.copy());
                        });
                        Ext.data.StoreManager.lookup('stPreguntasTemporal').add(preguntasTemporal);
                        Ext.getCmp('gridPreguntas').reconfigure(Ext.data.StoreManager.lookup('stPreguntasTemporal'));
                        Ext.getCmp('gridRespuestas').store.removeAll();
                        //Ext.getCmp('gridRespuestas').focus();

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    }

                    Ext.getCmp('panelPreguntasCertificaciones').setTitle('<span style="font-size: 160%;margin-top:20px;">Preguntas de la función: ' + gridView.getSelectionModel().getSelection()[0].data.funCodigo + '</span>');
                    Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);


                    //Ext.MessageBox.hide();
                    /*
                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').each(function (rec) {
                    pregTema = rec.data.ctAleatorias;
                    tiemTema = rec.data.ctTiempo;
                    });

                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {

                    var funciones = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones');
                    //                   //     for (i = 0; i < funciones.getCount(); i++) {
                    //                            if (rec.data.tfActivo) {
                    //                                contPregFuncion = contPregFuncion + rec.data.funAleatorias;
                    //                                contTiemFuncion = contTiemFuncion + rec.data.funTiempo;
                    //                            }
                    //                   //     }

                    //     for (i = 0; i < funciones.getCount(); i++) {
                    if (rec.data.tfActivo) {
                    contPregFuncion = contPregFuncion + rec.data.funAleatorias;
                    contTiemFuncion = contTiemFuncion + rec.data.funTiempo;
                    }
                    //     }

                    if (contPregFuncion > pregTema) {
                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'Las preguntas totales de las Funciones no debe exceder a las del campo <b>No. de Preguntas aleatorias por Tema</b>.');
                    contPregFuncion = 0;
                    }

                    if (contTiemFuncion > tiemTema) {
                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'El tiempo total (minutos) de las Funciones no debe exceder a los del campo <b>Tiempo de examen para contestar el Tema (minutos)</b>.');
                    contTiemFuncion = 0;
                    }
                    });*/
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
                    dataIndex: 'funCodigo',
                    width: '15%',
                    hidden: false,
                    align: 'center',
                    sortable: false
                },
                {
                    text: "Función",
                    dataIndex: 'funNombre',
                    width: '59%',
                    align: 'left',
                    hidden: false,
                    sortable: false
                },
                {
                    text: "Preguntas",
                    dataIndex: 'funAleatorias',
                    width: '5%',
                    align: 'center',
                    hidden: false,
                    sortable: false
                },
                {
                    text: "Preg. Correctas",
                    dataIndex: 'funCorrectas',
                    width: '6%',
                    hidden: false,
                    align: 'center',
                    sortable: false
                },
            //                {
            //                    text: "Activa",
            //                    dataIndex: 'tfActivo',
            //                    width: '10%',
            //                    hidden: false,
            //                    align: 'center',
            //                    sortable: true
            //                }
                {
                xtype: 'checkcolumn',
                header: 'Activa',
                dataIndex: 'tfActivo',
                id: 'checkColFunciones',
                //itemId: 'checkcolumnId',
                width: '10%',
                sortable: false,
                stopSelection: false,
                listeners: {
                    checkchange: function (column, recordIndex, checked, rowIndex) {

                        var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();
                        var idFuncion = Ext.getCmp('gridFunciones').store.getAt(recordIndex).data.idFuncion,
                            idFuncionTemporal = Ext.getCmp('gridFunciones').store.getAt(recordIndex).data.idFuncionTemporal,
                            idTema = Ext.getCmp('gridFunciones').store.getAt(recordIndex).data.idTema,
                            idTemaTemporal = Ext.getCmp('gridFunciones').store.getAt(recordIndex).data.idTemaTemporal,
                            preguntasAleatorias = Ext.getCmp('gridFunciones').store.getAt(recordIndex).data.funAleatorias;

                        var preguntasFuncionActivas = 0;
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
                            if (rec.data.preActiva == true
                            && rec.data.idFuncion == idFuncion
                            && rec.data.idFuncionTemporal == idFuncionTemporal)
                                preguntasFuncionActivas++;
                        });
                        if (checked == true && preguntasFuncionActivas < preguntasAleatorias) {
                            Ext.MessageBox.alert('SICER', 'Es necesario que la función tenga por lo menos ' + preguntasAleatorias + ' pregunta(s) activa(s) para ser activada');
                            Ext.getCmp('gridFunciones').store.getAt(recordIndex).data.tfActivo = false;
                            Ext.getCmp('gridFunciones').getView().refresh(true);

                            Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                            return;
                        }


                        ////////////////////////ACTUALIZO LA FUNCIÓN DENTRO DEL STORE//////////////////////////////////
                        if (idFuncion == 0) {
                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones')
                                    .findRecord('idFuncionTemporal', idFuncionTemporal, 0, false, true, true).data.tfActivo = checked;
                        } else {
                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones')
                                    .findRecord('idFuncion', idFuncion, 0, false, true, true).data.tfActivo = checked;
                        }
                        ///////////////////////////////////////////////////////////////////////////////

                        //////////////////////ACTUALIZO PREGUNTAS DEL TEMA DE LA FUNCIÓN//////////////////////////
                        var preguntas = 0,
                        preguntasCorrectas = 0;
                        Ext.getCmp('gridFunciones').store.each(function (rec) {
                            if (rec.data.tfActivo) {
                                //tiempo += rec.data.funTiempo;
                                preguntas += rec.data.funAleatorias;
                                preguntasCorrectas += rec.data.funCorrectas;
                            }
                        });

                        Ext.getCmp('gridTemas').store.each(function (rec) {
                            if (rec.data.idTema == idTema && rec.data.idTematemporal == idTemaTemporal) {
                                rec.data.ctAleatorias = preguntas;
                                rec.data.ctCorrectas = preguntasCorrectas;
                                return;
                            }
                        });
                        Ext.getCmp('gridTemas').getView().refresh(true);
                        ///////////////////////////////////////////////////////////////////////////////


                        ////////////////////ACTUALIZO DATOS DE CERTIFICACION///////////////////////////////////
                        preguntas = 0;
                        preguntasCorrectas = 0;
                        Ext.getCmp('gridTemas').store.each(function (recTema) {
                            if (recTema.data.ctActivo) {
                                preguntas += recTema.data.ctAleatorias;
                                preguntasCorrectas += recTema.data.ctCorrectas;
                            }
                        });
                        Ext.getCmp('txbNumeroDePreguntasExamen').setValue(preguntas);
                        Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').setValue(preguntasCorrectas);
                        ///////////////////////////////////////////////////////////////////////////////

                        desactivaTema(idTema, idTemaTemporal);

                        Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                    }
                }
            }
            ],
            tbar: [{ text: 'Modificar Función',
                tooltip: 'Modifica los datos de la función seleccionada',
                id: 'tbarModFun',
                handler: function () {
                         session();
                         if (Ext.getCmp('gridFunciones').getSelectionModel().hasSelection()) {
                             var window = new Ext.Window({
                                 //id: 'window',
                                 title: 'Modificar Función',
                                 //width: 1000,
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
                                                columns: 3
                                                //tdAttrs: { style: 'padding: 2px;' }
                                            },
                                            items: [
                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbNombreFuncion',
                                                    fieldLabel: 'Nombre Función',
                                                    labelAlign: 'right',
                                                    labelWidth: 150,
                                                    width: 570,
                                                    colspan: 2,
                                                    maxLength: 500,
                                                    enforceMaxLength: true,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_ ]/,
                                                    value: Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.funNombre,
                                                    listeners: {
                                                        blur: function (field) {
                                                            field.setValue(field.getValue().toUpperCase());
                                                        }
                                                    }
                                                },
                                            /*{
                                            xtype: 'checkbox',
                                            boxLabel: 'Función Activa',
                                            id: 'FuncionActiva',
                                            //margin: '20 200 20 110',
                                            name: 'FuncionActiva',
                                            allowBlank: true
                                            },*/
                                                {
                                                xtype: 'textfield',
                                                id: 'txbCodigoFuncion',
                                                fieldLabel: 'Código Función',
                                                labelAlign: 'right',
                                                labelWidth: 130,
                                                width: 320,
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio',
                                                maskRe: /[A-Za-zñÑ0-9-]/,
                                                maxLength: 30,
                                                enforceMaxLength: true,
                                                value: Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.funCodigo,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        field.setValue(newValue.toUpperCase());
                                                    }
                                                }
                                            },
                                            /*{
                                            xtype: 'combo',
                                            id: 'cbOrdenFuncion',
                                            //store: Ext.getStore('Administracion.RegistroCertificacion.stOrdenTema').load(),
                                            emptyText: 'SELECCIONAR',
                                            triggerAction: 'all',
                                            queryMode: 'local',
                                            fieldLabel: "Orden de la Función",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 350,
                                            colspan: 2,
                                            valueField: 'idTema',
                                            displayField: 'ordenTema',
                                            // autoSelect: true,
                                            //  disabled: true,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                            msgTarget: 'side'
                                            },*/

        				                        {
        				                        xtype: 'textfield',
        				                        id: 'txbNoPreguntasAleatoriasFuncion',
        				                        fieldLabel: 'Núm. de <b>preguntas aleatorias</b> por Función',
        				                        labelAlign: 'right',
        				                        labelWidth: 150,
        				                        width: 250,
        				                        allowBlank: false,
        				                        blankText: 'Campo Obligatorio',
        				                        maskRe: /[0-9]/,
        				                        maxLength: 4,
        				                        enforceMaxLength: true,
        				                        value: Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.funAleatorias,
        				                        listeners: {
        				                            change: function (me, newValue, oldValue, eOpts) {
        				                                /*if (newValue > gridView.getSelectionModel().getSelection()[0].data.ctAleatorias) {
        				                                Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. de preguntas aleatorias por Función</b> no debe ser mayor al campo <b>No. de Preguntas Aleatorias por Tema</b>');
        				                                }*/
        				                            }
        				                        }
        				                    },


        				                    {
        				                        xtype: 'textfield',
        				                        id: 'txbNoPreguntasCorrectasAprobarFuncion',
        				                        fieldLabel: 'Núm. <b>preguntas correctas</b> para aprobar la Función',
        				                        labelAlign: 'right',
        				                        labelWidth: 200,
        				                        width: 300,
        				                        //colspan: 2,
        				                        allowBlank: false,
        				                        blankText: 'Campo Obligatorio',
        				                        maskRe: /[0-9. ]/,
        				                        margin: '0 0 0 20',
        				                        maxLength: 4,
        				                        enforceMaxLength: true,
        				                        value: Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.funCorrectas,
        				                        listeners: {
        				                            change: function (me, newValue, oldValue, eOpts) {
        				                                /* if (newValue > Ext.getCmp("txbNoPreguntasAleatoriasFuncion").getValue()) {
        				                                Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. De preguntas correctas para aprobar la Función</b> no debe ser mayor al campo <b>No. de preguntas aleatorias por Función</b>');
        				                                }*/
        				                            }
        				                        }
        				                    }
                                            /*,{
                                            xtype: 'textfield',
                                            id: 'txbTiempoExamenContestarFuncion',
                                            fieldLabel: 'Tiempo de examen para contestar la Función (minutos)',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 300,
                                            colspan: 3,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maskRe: /[0-9. ]/,
                                            margin: '0 0 0 20',
                                            maxLength: 4,
                                            enforceMaxLength: true,
                                            listeners: {
                                            change: function (me, newValue, oldValue, eOpts) {
                                            /*   if (newValue > gridView.getSelectionModel().getSelection()[0].data.ctTiempo) {
                                            Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>Tiempo de examen para contestar la Función (minutos)</b> no debe ser mayor al campo <b>Tiempo de examen para contestar el Tema (minutos)</b>');
                                            }* /
                                            }
                                            }
                                            }*/
                                            ]
                                        }
                                    ],
                                 buttons: [
                                                        {
                                                            text: 'Aceptar', width: 100, handler: function () {
                                                                session();

                                                                if (Ext.getCmp('txbNombreFuncion').getRawValue() == (null || '')
                                                                || Ext.getCmp('txbCodigoFuncion').getRawValue() == (null || '')
                                                                // || Ext.getCmp('cbOrdenFuncion').getRawValue() == 0
                                                                || Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getRawValue() == 0
                                                                || Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getRawValue() == 0
                                                                //|| Ext.getCmp('txbTiempoExamenContestarFuncion').getRawValue() == 0
                                                                ) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                                                                    return;
                                                                }

                                                                if (parseInt(Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getRawValue()) < parseInt(Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getRawValue())) {
                                                                    Ext.MessageBox.alert('SICER', 'El número de preguntas que se mostrarán en el examen no debe </br>sobrepasar al número de preguntas correctas para aprobar.');
                                                                    return;
                                                                }

                                                                var idFuncionTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncionTemporal;
                                                                var idTemaTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idTemaTemporal;
                                                                var idTema = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idTema;
                                                                var idF = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncion;


                                                                var retorno = false;
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                                                                    if (!(rec.data.idFuncion == idF && rec.data.idFuncionTemporal == idFuncionTemporal) &&
                                                                         Ext.getCmp('txbCodigoFuncion').getRawValue() == rec.data.funCodigo)
                                                                        retorno = true;
                                                                });
                                                                if (retorno) {
                                                                    Ext.MessageBox.alert('SICER', 'Alguna función tiene el mismo código de función');
                                                                    return;
                                                                }


                                                                //var id = 'stFuncionesTema' + gridView.getSelectionModel().getSelection()[0].data.idTemporal;
                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();


                                                                //var idFuncion = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncion;
                                                                //var id = 'stFuncionesTema' + gridView.getSelectionModel().getSelection()[0].data.idTemporal;

                                                                if (idFuncionTemporal != 0) {
                                                                    var acuerdosRecord = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones')
                                                                            .findRecord('idFuncionTemporal', idFuncionTemporal, 0, false, true, true); //Exact Match
                                                                }
                                                                else {
                                                                    var acuerdosRecord = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones')
                                                                            .findRecord('idFuncion', idF, 0, false, true, true); //Exact Match
                                                                }

                                                                acuerdosRecord.set('funNombre', Ext.getCmp('txbNombreFuncion').getValue());
                                                                acuerdosRecord.set('funAleatorias', Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getValue());
                                                                acuerdosRecord.set('funCorrectas', Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getValue());
                                                                //acuerdosRecord.set('funTiempo', Ext.getCmp('txbTiempoExamenContestarFuncion').getValue());
                                                                //acuerdosRecord.set('funOrden', Ext.getCmp('cbOrdenFuncion').getRawValue());
                                                                acuerdosRecord.set('funCodigo', Ext.getCmp('txbCodigoFuncion').getValue());
                                                                //acuerdosRecord.set('tfActivo', Ext.getCmp('FuncionActiva').getValue());

                                                                //////////Se actualiza GRID//////////////////////////////
                                                                Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                                                                var funcionesTemporal = [];
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                                                                    if (rec.data.idTema == idTema && rec.data.idTemaTemporal == idTemaTemporal)
                                                                        funcionesTemporal.push(rec.copy());
                                                                });
                                                                Ext.data.StoreManager.lookup('stFuncionesTemporal').add(funcionesTemporal);
                                                                Ext.getCmp('gridFunciones').reconfigure(Ext.data.StoreManager.lookup('stFuncionesTemporal'));
                                                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                                                                //////////////////////ACTUALIZO PREGUNTAS DEL TEMA DE LA FUNCIÓN//////////////////////////

                                                                var preguntas = 0,
                                                                preguntasCorrectas = 0;
                                                                Ext.getCmp('gridFunciones').store.each(function (rec) {
                                                                    if (rec.data.tfActivo) {
                                                                        //tiempo += rec.data.funTiempo;
                                                                        preguntas += rec.data.funAleatorias;
                                                                        preguntasCorrectas += rec.data.funCorrectas;
                                                                    }
                                                                });

                                                                Ext.getCmp('gridTemas').store.each(function (rec) {
                                                                    if (rec.data.idTema == idTema && rec.data.idTematemporal == idTemaTemporal) {
                                                                        rec.data.ctAleatorias = preguntas;
                                                                        rec.data.ctCorrectas = preguntasCorrectas;
                                                                        return;
                                                                    }
                                                                });
                                                                Ext.getCmp('gridTemas').getView().refresh(true);
                                                                ///////////////////////////////////////////////////////////////////////////////


                                                                ////////////////////ACTUALIZO DATOS DE CERTIFICACION///////////////////////////////////
                                                                preguntas = 0;
                                                                preguntasCorrectas = 0;
                                                                Ext.getCmp('gridTemas').store.each(function (recTema) {
                                                                    if (recTema.data.ctActivo) {
                                                                        preguntas += recTema.data.ctAleatorias;
                                                                        preguntasCorrectas += recTema.data.ctCorrectas;
                                                                    }
                                                                });
                                                                Ext.getCmp('txbNumeroDePreguntasExamen').setValue(preguntas);
                                                                Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').setValue(preguntasCorrectas);
                                                                ///////////////////////////////////////////////////////////////////////////////


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
                             });
                             window.show();
                         }
                         else {
                             Ext.MessageBox.alert('SICER', 'Debes seleccionar una función');
                         }
                     }
            },
                   '-',
                   { text: 'Agregar Pregunta',
                       tooltip: 'Agrega una Pregunta a la función seleccionada'
                     , handler: function () {
                         session();
                         if (Ext.getCmp('gridFunciones').getSelectionModel().hasSelection()) {
                             var window = new Ext.Window({
                                 //id: 'window',
                                 title: 'Agregar Pregunta',
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
                                            },*/
                                                {
                                                xtype: 'hiddenfield',
                                                id: 'hfTipoArchivo'
                                            },
                                                {
                                                    xtype: 'hiddenfield',
                                                    id: 'hfIdentificador',
                                                    value: ''
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

                                                                if (Ext.getCmp('txbTextoPregunta').getRawValue() == (null || '')
                                                                  || Ext.getCmp('txbCodigoPregunta').getRawValue() == (null || '')
                                                                  ) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario ingresar un código y un texto a la Pregunta.');
                                                                    return false;
                                                                }


                                                                /*
                                                                var idFuncionTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncionTemporal;
                                                                var idF = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncion;

                                                                var idTemaTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idTemaTemporal;
                                                                var idTema = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idTema;
                                                                */
                                                                var retorno = false;
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
                                                                    if (//!(rec.data.idPregunta == idP && rec.data.idFuncionTemporal == idFuncionTemporal) &&
                                                                             Ext.getCmp('txbCodigoPregunta').getValue() == rec.data.preCodigo)
                                                                        retorno = true;
                                                                });
                                                                if (retorno) {
                                                                    Ext.MessageBox.alert('SICER', 'Alguna pregunta tiene el mismo código de pregunta');
                                                                    return;
                                                                }


                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();


                                                                contadorPreguntas = contadorPreguntas + 1;

                                                                var idFuncionTemporal = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncionTemporal;
                                                                var idFuncion = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].data.idFuncion;

                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').add({
                                                                    preDescripcion: Ext.getCmp('txbTextoPregunta').getValue(),
                                                                    preCodigo: Ext.getCmp('txbCodigoPregunta').getValue(),
                                                                    //preActiva: Ext.getCmp('chkActiva').getValue(),
                                                                    preActiva: 0,
                                                                    preObligatoria: 0,
                                                                    preTipoArchivo: Ext.getCmp('hfTipoArchivo').getValue(),
                                                                    preNombreArchivo: '',
                                                                    //imagen: Ext.getCmp('hfImagenAgPre').getValue(),
                                                                    identificadorImagen: Ext.getCmp('hfIdentificador').getValue(),
                                                                    idFuncionTemporal: idFuncionTemporal,
                                                                    idFuncion: idFuncion,
                                                                    idPreguntaTemporal: contadorPreguntas
                                                                });

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
                             Ext.MessageBox.alert('SICER', 'Debes seleccionar una función');
                         }
                     }
                   },
                   '-',
                   { text: 'Subir',
                       tooltip: 'Cambia el orden en que aparecerá la función seleccionada en el examen'
                        , handler: function () {
                            session();
                            if (Ext.getCmp('gridFunciones').getSelectionModel().hasSelection()) {
                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                                var recordFuncion = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0];

                                if (!recordFuncion) {
                                    Ext.MessageBox.alert('SICER', 'Debes seleccionar un tema');
                                    return;
                                }
                                var index = Ext.getCmp('gridFunciones').store.indexOf(recordFuncion);

                                index--;
                                if (index < 0) {
                                    return;
                                }

                                Ext.getCmp('gridFunciones').store.remove(recordFuncion);
                                Ext.getCmp('gridFunciones').store.insert(index, recordFuncion);
                                Ext.getCmp('gridFunciones').getSelectionModel().select(index, true);

                                var orden = 0;
                                Ext.getCmp('gridFunciones').store.each(function (rec) {
                                    orden++;
                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones')
                                            .findRecord('funCodigo', rec.data.funCodigo, 0, false, true, true).data.funOrden = orden;
                                });
                                Ext.getCmp('gridFunciones').getView().refresh(true);

                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                            }
                            else {
                                Ext.MessageBox.alert('SICER', 'Debes seleccionar una función');
                            }

                        }
                   },
                   '-',
                   { text: 'Bajar',
                       tooltip: 'Cambia el orden en que aparecerá la función seleccionada en el examen'
                    , handler: function () {
                        session();
                        if (Ext.getCmp('gridFunciones').getSelectionModel().hasSelection()) {
                            var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();
                            var recordFuncion = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0];

                            if (!recordFuncion) {
                                Ext.MessageBox.alert('SICER', 'Debes seleccionar un tema');
                                return;
                            }
                            var index = Ext.getCmp('gridFunciones').store.indexOf(recordFuncion);
                            index++;
                            if (index >= Ext.getCmp('gridFunciones').store.getCount()) {
                                return;
                            }
                            Ext.getCmp('gridFunciones').store.remove(recordFuncion);
                            Ext.getCmp('gridFunciones').store.insert(index, recordFuncion);
                            Ext.getCmp('gridFunciones').getSelectionModel().select(index, true);

                            var orden = 0;
                            Ext.getCmp('gridFunciones').store.each(function (rec) {
                                orden++;
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones')
                                            .findRecord('funCodigo', rec.data.funCodigo, 0, false, true, true).data.funOrden = orden;
                            });
                            Ext.getCmp('gridFunciones').getView().refresh(true);

                            Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                        }
                        else {
                            Ext.MessageBox.alert('SICER', 'Debes seleccionar una función');
                        }

                    }
                   }]
        });
        this.callParent(arguments);
    }
});
