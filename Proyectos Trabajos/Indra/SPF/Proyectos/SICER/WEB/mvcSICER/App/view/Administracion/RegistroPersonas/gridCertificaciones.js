Ext.define('app.view.Administracion.RegistroPersonas.gridCertificaciones', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridCertificaciones',
    id: 'gridCertificaciones',
    initComponent: function () {
        session();
        Ext.getStore('catalogos.stCertificaciones').load();
        //        Ext.create('Ext.data.Store', {
        //            model: 'app.model.Administracion.RegistroCertificacion.mdFuncion',
        //            storeId: 'stFuncionesTemporal'
        //        });


        Ext.apply(this, {

            store: 'Administracion.RegistroPersonas.stCertificacionRegistro',

            //    storeId: 'Administracion.RegistroPersonas.stConsultaUbicacion',
            collapsible: false,
            loadMask: true,
            enableColumnResize: false,
            selModel:
            {
                pruneRemoved: false
            },
            multiSelect: true,
            viewConfig:
            {
                trackOver: false
            },
            columns: [
                 {
                     xtype: 'rownumberer',
                     width: '4%'
                 },
                 {
                     text: "Certificación",
                     dataIndex: 'certificacion',
                     width: '50%',
                     align: 'left',
                     hidden: false,
                     sortable: true
                 },
                 {
                     text: "Resultado",
                     dataIndex: 'evaluacionDesc',
                     width: '25%',
                     hidden: false,
                     align: 'center',
                     sortable: true
                 },
                 {
                     text: "Fecha de Examen",
                     dataIndex: 'crFechaExamen',
                     width: '25%',
                     hidden: false,
                     align: 'center',
                     sortable: true
                 }

            ]
             ,
            tbar: [

           {
               text: 'Agregar Certificación ',
               tooltip: 'Agregar Certificación',
               //id: 'btn',
               //icon: 'Imagenes/icon16/lupa_.png',
               handler: function () {
                   session();
                   var win = new Ext.Window({
                       id: 'win',
                       title: 'Agregar Certificación'
                    , width: 900
                    , resizable: false
                    , modal: true
                    , plain: true
                    , layout: {
                        type: 'vbox',
                        align: 'center'
                    }
                    , bodyPadding: '10 10 10 10'
                    , items: [
                    { xtype: 'panel',
                        title: 'Datos de Certificación'
                    , bodyPadding: '10 10 50 10'
                    , width: '100%'
                    , layout: {
                        type: 'table'
                    , columns: 2
                        //tdAttrs: { style: 'padding: 2px;' }
                    },
                        items: [

                    { xtype: 'hiddenfield', id: 'inserto', value: 'true' },
                        //{ xtype: 'hiddenfield', id: 'hfZona', value: '' },
                    {
                    xtype: 'combo',
                    id: 'ddlCertificacion',
                    store: 'catalogos.stCertificaciones',
                    emptyText: 'SELECCIONAR',
                    triggerAction: 'all',
                    queryMode: 'local',
                    grow: true,
                    editable: false,
                    allowBlank: false,
                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                    fieldLabel: 'Certificación',
                    labelAlign: 'right',
                    labelWidth: 200,
                    width: 500,
                    valueField: 'idCertificacion',
                    displayField: 'certificacion',
                    msgTarget: 'side'
                },

            {
                xtype: 'datefield',
                id: 'dfFechaAplicaExamen',
                format: 'd/m/Y',
                submitFormat: 'd/m/Y',
                altFormats: 'd/m/Y',
                //emptyText: 'DD/MM/AAAA',
                editable: false,
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                Value: new Date(),
                fieldLabel: "Fecha de Examen",
                labelAlign: 'right',
                labelWidth: 200,
                width: 300,
                msgTarget: 'side'
            },
            {
                xtype: 'combo',
                id: 'ddlInstAplicaExamen',
                store: Ext.getStore('catalogos.stInstAplicaExamen').load(),
                emptyText: 'SELECCIONAR',
                triggerAction: 'all',
                queryMode: 'local',
                grow: true,
                editable: false,
                fieldLabel: "Institución que Aplica el Examen",
                labelAlign: 'right',
                labelWidth: 200,
                width: 500,
                valueField: 'idInstitucionAplicaExamen',
                displayField: 'iaeDescripcion',
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                msgTarget: 'side'
            },
            {
                xtype: 'timefield',
                id: 'tfHoraExamen',
                fieldLabel: 'Hora de Examen',
                editable: false,
                maskRe: /[0-9:]/,
                format: 'H:i',
                altFormats: 'H:i'
            , increment: 15
            , fieldLabel: "Hora de Examen",
                labelAlign: 'right',
                labelWidth: 200,
                width: 300,
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                msgTarget: 'side'

            },
            {
                xtype: 'combo',
                id: 'ddlLugarAplicaExamen',
                store: Ext.getStore('catalogos.stLugarAplicacion').load(),
                emptyText: 'SELECCIONAR',
                triggerAction: 'all',
                queryMode: 'local',
                colspan: 2,
                grow: true,
                editable: false,
                fieldLabel: "Lugar de Aplicación del Examen",
                labelAlign: 'right',
                labelWidth: 200,
                width: 500,
                valueField: 'idLugarAplica',
                displayField: 'laDescripcion',
                // disabled: true,
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                msgTarget: 'side'
            , listeners:
            {
                select: function (field, newValue, oldValue) {

                    if (Ext.getCmp('ddlLugarAplicaExamen').getValue() != ("" || null)) {
                        if (!isNaN(Ext.getCmp('ddlLugarAplicaExamen').getValue())) {
                            Ext.getCmp('dfDomicilioLugarExamen').setValue(Ext.data.StoreManager.lookup('catalogos.stLugarAplicacion').findRecord('idLugarAplica', Ext.getCmp('ddlLugarAplicaExamen').getValue()).data.laDomicilio);
                        }
                    }

                }
            }
            },
            {
                xtype: 'combo',
                id: 'ddlEvaluador',
                store: Ext.getStore('catalogos.stEvaluador').load(),
                emptyText: 'SELECCIONAR',
                triggerAction: 'all',
                queryMode: 'local',
                grow: true,
                editable: false,
                fieldLabel: "Evaluador",
                labelAlign: 'right',
                labelWidth: 200,
                width: 500,
                colspan: 2,
                valueField: 'idEvaluador',
                displayField: 'evaDescripcion',
                //disabled: true,
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción'
            },



            {
                xtype: 'combo',
                id: 'ddlNivelSeguridad',
                store: Ext.getStore('catalogos.stNivelSeguridad').load(),
                emptyText: 'SELECCIONAR',
                triggerAction: 'all',
                queryMode: 'local',
                grow: true,
                editable: false,
                allowBlank: false,
                blankText: 'Campo Obligatorio',
                fieldLabel: "Nivel de Seguridad",
                labelAlign: 'right',
                labelWidth: 200,
                width: 500,
                colspan: 2,
                valueField: 'idNivelSeguridad',
                displayField: 'nsDescripcion',
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción'
            , listeners:
            {

                select: function (field, newValue, oldValue) {

                    if (Ext.getCmp('ddlNivelSeguridad').getValue() != ("" || null)) {
                        if (!isNaN(Ext.getCmp('ddlNivelSeguridad').getValue())) {
                            Ext.getCmp('ddlDependenciaExterna').clearValue();
                            Ext.getCmp('ddlDependenciaExterna').store.load({ params: { idNivelSeguridad: this.getValue()} });
                            Ext.getCmp('ddlDependenciaExterna').setDisabled(false);
                            Ext.getCmp('ddlDependenciaExterna').clearValue();
                            Ext.getCmp('ddlInstitucionExterna').clearValue();
                        }
                    }

                }
            }
            },
            {
                xtype: 'combo',
                id: 'ddlDependenciaExterna',
                store: 'catalogos.stDependenciaExterna',
                emptyText: 'SELECCIONAR',
                triggerAction: 'all',
                queryMode: 'local',
                grow: true,
                editable: false,
                allowBlank: false,
                blankText: 'Campo Obligatorio',
                fieldLabel: 'Dependencia O Institución',
                labelAlign: 'right',
                labelWidth: 200,
                width: 500,
                colspan: 2,
                valueField: 'idDependenciaExterna',
                displayField: 'deDescripcion',
                disabled: true,
                allowBlank: false,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción'
            , listeners:
            {
                select: function (field, newValue, oldValue) {

                    if (Ext.getCmp('ddlDependenciaExterna').getValue() != ("" || null)) {
                        if (!isNaN(Ext.getCmp('ddlDependenciaExterna').getValue())) {
                            Ext.getCmp('ddlInstitucionExterna').clearValue();
                            Ext.getCmp('ddlInstitucionExterna').store.load({ params: { idDependenciaExterna: this.getValue()} });
                            Ext.getCmp('ddlInstitucionExterna').setDisabled(false);
                            Ext.getCmp('ddlInstitucionExterna').clearValue();
                        }
                    }

                }
            }

            }, {
                xtype: 'combo',
                id: 'ddlInstitucionExterna',
                store: 'catalogos.stInstitucionExterna',
                emptyText: 'SELECCIONAR',
                triggerAction: 'all',
                queryMode: 'local',
                grow: true,
                editable: false,
                allowBlank: false,
                blankText: 'Campo Obligatorio',
                fieldLabel: 'Nombre de la Institución',
                labelAlign: 'right',
                labelWidth: 200,
                width: 500,
                colspan: 2,
                valueField: 'idInstitucionExterna',
                displayField: 'ieDescripcion',
                disabled: true,
                blankText: 'Campo Obligatorio, Por favor seleccione una opción'
            },


            {
                xtype: 'displayfield',
                id: 'dfDomicilioLugarExamen',
                value: '-',
                // store: '',
                // emptyText: 'SELECCIONAR',
                // triggerAction: 'all',
                // queryMode: 'local',
                colspan: 2,
                grow: true,
                // editable: false,
                fieldLabel: "Domicilio",
                labelAlign: 'right',
                labelWidth: 200,
                width: 500
                // valueField: 'idInstalacion',
                //  displayField: 'insNombre',
                //  disabled: true,

            }
                        /*
                        , {
                        xtype: 'displayfield',
                        id: 'dfZona',
                        store: 'app.store.Administracion.RegistroPersonas.stConsultaUbicacion',
                        //value: '-',
                        colspan: 2,
                        fieldLabel: "Zona",
                        labelAlign: 'right',
                        labelWidth: 200,
                        width: 500,
                        valueField: 'idZona',
                        displayField: 'ZonDescripcion'
                        }

                        , {
                        xtype: 'displayfield',
                        id: 'dfServicio',
                        store: 'catalogos.stServicio',
                        value: '-',
                        colspan: 2,
                        fieldLabel: "Servicio",
                        labelAlign: 'right',
                        labelWidth: 200,
                        width: 500,
                        valueField: 'idServicio',
                        displayField: 'serDescripcion'
                        }
                        , {
                        xtype: 'displayfield',
                        id: 'dfInstalacion',
                        store: 'catalogos.stInstalacion',
                        value: '-',
                        colspan: 2,
                        fieldLabel: " Instalación",
                        labelAlign: 'right',
                        labelWidth: 200,
                        width: 500,
                        valueField: 'idInstalacion',
                        displayField: 'insNombre'
                        }*/



            ]
                    },
            { xtype: 'panel',
                title: 'Resultados de Certificación'
            , bodyPadding: '10 10 10 10'
            , width: '100%'
            , layout: {
                type: 'table'
            , columns: 2
                //tdAttrs: { style: 'padding: 2px;' }
            },
                items: [

            {
                xtype: 'displayfield',
                id: 'dfResultadoCedula',
                value: '-',
                colspan: 1,
                // store: '',
                // emptyText: 'SELECCIONAR',
                // triggerAction: 'all',
                // queryMode: 'local',
                grow: true,
                // editable: false,
                fieldLabel: "Resultado de la Cédula",
                labelAlign: 'right',
                labelWidth: 200,
                width: 400
                // valueField: 'idInstalacion',
                //  displayField: 'insNombre',
                //  disabled: true,


            }
            ,
            {
                xtype: 'displayfield',
                id: 'dfFolio',
                value: '-',
                colspan: 1,
                // store: '',
                // emptyText: 'SELECCIONAR',
                // triggerAction: 'all',
                // queryMode: 'local',
                grow: true,
                // editable: false,
                fieldLabel: "Folio",
                labelAlign: 'right',
                labelWidth: 200,
                width: 400
                // valueField: 'idInstalacion',
                //  displayField: 'insNombre',
                //  disabled: true,
            },

            {
                xtype: 'displayfield',
                id: 'dfCalificacion',
                value: '-',
                colspan: 1,
                // store: '',
                // emptyText: 'SELECCIONAR',
                // triggerAction: 'all',
                // queryMode: 'local',
                grow: true,
                // editable: false,
                fieldLabel: "Calificación",
                labelAlign: 'right',
                labelWidth: 200,
                width: 450
                // valueField: 'idInstalacion',
                //  displayField: 'insNombre',
                //  disabled: true,


            },
            {
                xtype: 'displayfield',
                id: 'dfVigencia',
                value: '-',
                colspan: 2,
                // store: '',
                // emptyText: 'SELECCIONAR',
                // triggerAction: 'all',
                // queryMode: 'local',
                grow: true,
                // editable: false,
                fieldLabel: "Vigencia",
                labelAlign: 'right',
                labelWidth: 200,
                width: 400
                // valueField: 'idInstalacion',
                //  displayField: 'insNombre',
                //  disabled: true,

            }


                ////                                               ,{
                ////                                                xtype: 'image',
                ////                                                //src: '/path/to/img.png',
                ////                                                height: 50, // Specifying height/width ensures correct layout
                ////                                                width: 50,
                ////                                                listeners: {
                ////                                                render: function(c) {
                ////                                                c.getEl().on('click', function(e) {
                ////                                                alert('User clicked image');
                ////                                                }, c);
                ////                                                }
                ////                                                }
                ////                                                }



            ]
            }

            ],
                       buttons: [
            {
                text: 'Aceptar', handler: function () {
                    session();

                    if (Ext.getCmp('ddlCertificacion').getValue() == (0 || null)
                            || Ext.getCmp('dfFechaAplicaExamen').getRawValue() == ''
                            || Ext.getCmp('tfHoraExamen').getRawValue() == ''
                            || Ext.getCmp('ddlLugarAplicaExamen').getValue() == (0 || null)
                            || Ext.getCmp('ddlEvaluador').getValue() == (0 || null)
                            || Ext.getCmp('ddlInstAplicaExamen').getValue() == (0 || null)) {
                        Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                        return false;
                    }

                    if (Ext.getCmp('hfparticipante').value == 'E' &&
                            (Ext.getCmp('ddlNivelSeguridad').getValue() == (0 || null)
                            || Ext.getCmp('ddlDependenciaExterna').getValue() == (0 || null)
                            || Ext.getCmp('ddlInstitucionExterna').getValue() == (0 || null))) {
                        Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                        return false;
                    }

                    var alerta = false;

                    Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stCertificacionRegistro').each(function (subrec) {
                        if (subrec.data.certificacion.trim() == Ext.getCmp('ddlCertificacion').getRawValue()) {
                            alerta = true;
                        }
                    });

                    if (alerta == true) {
                        Ext.MessageBox.alert('SICER', '<b>Certificación existente</b></br> Favor de verificarlo');
                    }
                    else {
                        Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stCertificacionRegistro').add({
                            idCertificacion: Ext.getCmp('ddlCertificacion').getValue()
                            , certificacion: Ext.getCmp('ddlCertificacion').getRawValue()

                            , idNivelSeguridad: Ext.getCmp('ddlNivelSeguridad').getValue()
                            , nsDescripcion: Ext.getCmp('ddlNivelSeguridad').getRawValue()


                            , idDependenciaExterna: Ext.getCmp('ddlDependenciaExterna').getValue()
                            , deDescripcion: Ext.getCmp('ddlDependenciaExterna').getRawValue()

                            , idInstitucionExterna: Ext.getCmp('ddlInstitucionExterna').getValue()
                            , ieDescripcion: Ext.getCmp('ddlInstitucionExterna').getRawValue()
                            , dfDomicilioLugarExamen: Ext.getCmp('dfDomicilioLugarExamen').getValue()

                            , crFechaExamen: Ext.getCmp('dfFechaAplicaExamen').rawValue
                            , crHora: Ext.getCmp('tfHoraExamen').getRawValue()
                            , idLugarAplica: Ext.getCmp('ddlLugarAplicaExamen').getValue()
                            , idEvaluador: Ext.getCmp('ddlEvaluador').getValue()
                            , idInstitucionAplicaExamen: Ext.getCmp('ddlInstAplicaExamen').getValue()
                            , participante: Ext.getCmp('hfparticipante').getValue()

                            , laDescripcion: Ext.getCmp('ddlLugarAplicaExamen').getRawValue()////agregado
                            , iaeDescripcion: Ext.getCmp('ddlInstAplicaExamen').getValue()
                            , evaDescripcion: Ext.getCmp('ddlEvaluador').getValue()
                            , inserto: Ext.getCmp('inserto').getValue('true')
                            , evaluacionDesc: 'PENDIENTE'
                            , idCalificacion: 0
                            , actualiza: 1


                        });
                    } //fin else


                    win.destroy();

                }
            },
            {
                text: 'Cancelar', handler: function () {
                    session();
                    win.destroy();
                }
            }
            ]
                   });

                   if (Ext.getCmp('hfparticipante').value == 'E') {

                       Ext.getCmp('ddlNivelSeguridad').setVisible(true);
                       Ext.getCmp('ddlDependenciaExterna').setVisible(true);
                       Ext.getCmp('ddlInstitucionExterna').setVisible(true);
                       // Ext.getCmp('dfZona').setVisible(false);
                       // Ext.getCmp('dfServicio').setVisible(false);
                       // Ext.getCmp('dfInstalacion').setVisible(false);
                   } else {

                       // Ext.getCmp('dfZona').setVisible(true);
                       // Ext.getCmp('dfServicio').setVisible(true);
                       // Ext.getCmp('dfInstalacion').setVisible(true);

                       Ext.getCmp('ddlNivelSeguridad').setVisible(false);
                       Ext.getCmp('ddlDependenciaExterna').setVisible(false);
                       Ext.getCmp('ddlInstitucionExterna').setVisible(false);
                       /*
                       Ext.Ajax.request({
                       method: 'POST',
                       url: 'Home/ConsultaUbicacionInterno',
                       failure: function () {
                       Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                       },
                       params:
                       {
                       idEmpleado: Ext.getCmp('hfIdEmpleado').value
                       },
                       success: function (response) {

                       var retorno = Ext.decode(response.responseText);


                       Ext.getCmp('dfZona').setValue(retorno[0].zonDescripcion);
                       Ext.getCmp('dfServicio').setValue(retorno[0].serDescripcion);
                       Ext.getCmp('dfInstalacion').setValue(retorno[0].insNombre);
                       }
                       });*/

                   }

                   win.show();

               } //fin handler

           }//fin xtype
            , '-'

              , {
                  text: 'Modificar Certificación ',
                  tooltip: 'Modificar Certificación',
                  icon: 'Imagenes/icon16/lupa_.png',

                  // listeners: {
                  // click: function (gridView, htmlElement, columnIndex, dataRecord,rowIndex) {
                  handler: function () {
                      session();
                      if (Ext.getCmp('gridCertificaciones').getSelectionModel().hasSelection()) {//if (Ext.getCmp('gridCertificaciones').getSelectionModel().hasSelection()) {

                          var idNivSeguridad = Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idNivelSeguridad,
                              idDepExterna = Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idDependenciaExterna;

                          Ext.getStore('catalogos.stDependenciaExterna').load({ params: { idNivelSeguridad: idNivSeguridad} });
                          Ext.getStore('catalogos.stInstitucionExterna').load({ params: { idDependenciaExterna: idDepExterna} });


                          var win = new Ext.Window({
                              id: 'win',
                              title: 'Modificar Certificación'
                                        , width: 900
                                        , resizable: false
                                        , modal: true
                                        , plain: true
                                        , layout: {
                                            type: 'vbox',
                                            align: 'center'
                                        }
                                        , bodyPadding: '10 10 10 10'
                                        , items: [
                                            { xtype: 'panel',
                                                title: 'Datos de Dertificación'
                                             , bodyPadding: '10 10 10 10'
                                             , width: '100%'
											  , layout: {
											      type: 'table'
                                                    , columns: 2
											      //tdAttrs: { style: 'padding: 2px;' }
											  },
                                                items: [
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlCertificacion',
                                                    store: 'catalogos.stCertificaciones',
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    fieldLabel: 'Certificación',
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    valueField: 'idCertificacion',
                                                    displayField: 'certificacion',
                                                    disabled: true,
                                                    disabledCls: 'af-item-disabled',
                                                    msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCertificacion,
                                                    // value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.certificacion,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            //field.setValue(newValue.toUpperCase());
                                                            //Ext.getCmp('tfHoraExamen').setValue(gridView.getSelectionModel().getSelection()[0].data.idCertificacion);
                                                            //Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCertificacion;
                                                        }
                                                    }

                                                },
                                                {
                                                    xtype: 'datefield',
                                                    id: 'dfFechaAplicaExamen',
                                                    format: 'd/m/Y',
                                                    submitFormat: 'd/m/Y',
                                                    altFormats: 'd/m/Y',
                                                    //emptyText: 'DD/MM/AAAA',
                                                    //maxValue: new Date(),
                                                    editable: false,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    Value: new Date(),
                                                    fieldLabel: "Fecha de Examen",
                                                    labelAlign: 'right',
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    labelWidth: 200,
                                                    width: 300,
                                                    msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.crFechaExamen,
                                                    //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temDescripcion,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            //field.setValue(newValue.toUpperCase());
                                                            //Ext.getCmp('dfFechaAplicaExamen').setValue(gridView.getSelectionModel().getSelection()[0].data.crFechaExamen);
                                                            Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.crFechaExamen;
                                                        }
                                                    }

                                                },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlInstAplicaExamen',
                                                    store: Ext.getStore('catalogos.stInstAplicaExamen').load(),
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    fieldLabel: "Institución que Aplica el Examen",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    valueField: 'idInstitucionAplicaExamen',
                                                    displayField: 'iaeDescripcion',
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idInstitucionAplicaExamen,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            //field.setValue(newValue.toUpperCase());
                                                            Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idInstitucionAplicaExamen;
                                                        }
                                                    }

                                                },
                                                {
                                                    xtype: 'timefield',
                                                    id: 'tfHoraExamen',
                                                    fieldLabel: 'Hora de Examen',
                                                    editable: false,
                                                    maskRe: /[0-9:]/,
                                                    format: 'H:i',
                                                    altFormats: 'H:i',
                                                    increment: 15,
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 300,
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.crHora,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            // field.setValue(newValue.toUpperCase());
                                                            Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.crHora;
                                                        }
                                                    }

                                                },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlLugarAplicaExamen',
                                                    store: Ext.getStore('catalogos.stLugarAplicacion').load(),
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    colspan: 2,
                                                    grow: true,
                                                    editable: false,
                                                    fieldLabel: "Lugar de Aplicación del Examen",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    valueField: 'idLugarAplica',
                                                    displayField: 'laDescripcion',
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idLugarAplica,
                                                    listeners: {

                                                        //                                                        change: function () {
                                                        //                                                            Ext.getCmp('ddlLugarAplicaExamen').setValue(gridView.getSelectionModel().getSelection()[0].data.idLugarAplica);//dfDomicilioLugarExamen
                                                        //                                                            // Ext.getCmp('dfDomicilioLugarExamen').setValue(Ext.data.StoreManager.lookup('catalogos.stLugarAplicacion').findRecord('idLugarAplica', Ext.getCmp('ddlLugarAplicaExamen').getValue()).data.laDomicilio);
                                                        //                                                        },
                                                        change: function (field, newValue, oldValue) {
                                                            if (Ext.getCmp('ddlLugarAplicaExamen').getValue() != ("" || null)) {
                                                                if (!isNaN(Ext.getCmp('ddlLugarAplicaExamen').getValue())) {

                                                                    Ext.getCmp('dfDomicilioLugarExamen').setValue(Ext.data.StoreManager.lookup('catalogos.stLugarAplicacion').findRecord('idLugarAplica', Ext.getCmp('ddlLugarAplicaExamen').getValue()).data.laDomicilio);
                                                                }
                                                            }
                                                        }

                                                    }
                                                },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlEvaluador',
                                                    store: Ext.getStore('catalogos.stEvaluador').load(),
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    fieldLabel: "Evaluador",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    colspan: 2,
                                                    valueField: 'idEvaluador',
                                                    displayField: 'evaDescripcion',
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idEvaluador,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            // field.setValue(newValue.toUpperCase());
                                                        }
                                                    }
                                                },
                                                ///////////////////////////////////////////////////////////////
                                                {
                                                xtype: 'combo',
                                                id: 'ddlNivelSeguridad',
                                                store: Ext.getStore('catalogos.stNivelSeguridad').load(),
                                                emptyText: 'SELECCIONAR',
                                                triggerAction: 'all',
                                                queryMode: 'local',
                                                grow: true,
                                                editable: false,
                                                blankText: 'Campo Obligatorio',
                                                fieldLabel: "Nivel de Seguridad",
                                                labelAlign: 'right',
                                                labelWidth: 200,
                                                width: 500,
                                                colspan: 2,
                                                valueField: 'idNivelSeguridad',
                                                displayField: 'nsDescripcion',
                                                //disabled: banderaHabilitar,
                                                disabledCls: 'af-item-disabled',
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                msgTarget: 'side',
                                                value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idNivelSeguridad,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        if (Ext.getCmp('ddlNivelSeguridad').getValue() != ("" || null)) {
                                                            if (!isNaN(Ext.getCmp('ddlNivelSeguridad').getValue())) {

                                                                Ext.getCmp('ddlDependenciaExterna').store.load({ params: { idNivelSeguridad: this.getValue()} });
                                                                Ext.getCmp('ddlDependenciaExterna').clearValue();
                                                                Ext.getCmp('ddlInstitucionExterna').store.removeAll();
                                                                Ext.getCmp('ddlInstitucionExterna').clearValue();
                                                            }
                                                        }
                                                    }
                                                    //                                                    
                                                }

                                            },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlDependenciaExterna',
                                                    //store: Ext.getStore('catalogos.stDependenciaExterna').load({ params: { idNivelSeguridad: idNivSeguridad} }),
                                                    store: 'catalogos.stDependenciaExterna',
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    fieldLabel: 'Dependencia O Institución',
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    colspan: 2,
                                                    valueField: 'idDependenciaExterna',
                                                    displayField: 'deDescripcion',
                                                    //disabled: true,
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    //msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idDependenciaExterna,
                                                    //value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.deDescripcion,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            if (Ext.getCmp('ddlDependenciaExterna').getValue() != ("" || null)) {
                                                                if (!isNaN(Ext.getCmp('ddlDependenciaExterna').getValue())) {
                                                                    Ext.getCmp('ddlInstitucionExterna').clearValue();
                                                                    Ext.getCmp('ddlInstitucionExterna').store.load({ params: { idDependenciaExterna: this.getValue()} });
                                                                }
                                                            }

                                                        }

                                                    }

                                                },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlInstitucionExterna',
                                                    //store: Ext.getStore('catalogos.stInstitucionExterna').load({ params: { idDependenciaExterna: idDepExterna} }),
                                                    store: 'catalogos.stInstitucionExterna',
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    fieldLabel: 'Nombre de la Institución',
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    colspan: 2,
                                                    valueField: 'idInstitucionExterna',
                                                    displayField: 'ieDescripcion',
                                                    //disabled: true,
                                                    //disabled: banderaHabilitar,
                                                    disabledCls: 'af-item-disabled',
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    // msgTarget: 'side',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idInstitucionExterna
                                                    //value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.ieDescripcion
                                                },
                                                {
                                                    xtype: 'displayfield',
                                                    id: 'dfDomicilioLugarExamen',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.dfDomicilioLugarExamen,
                                                    colspan: 2,
                                                    grow: true,
                                                    fieldLabel: "Domicilio",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500
                                                }

                                            /*  
                                            , {
                                            xtype: 'displayfield',
                                            id: 'dfZona',
                                            store: 'catalogos.stZona',
                                            //value: '-',
                                            colspan: 2,
                                            fieldLabel: "Zona",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 500,
                                            valueField: 'idZona',
                                            displayField: 'ZonDescripcion',
                                            // value:  Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idZona// { xtype: 'hiddenfield', id: 'hfparticipante', value: 'E' },
                                            value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idZona
                                            // value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.ZonDescripcion
                                            }

                                            , {
                                            xtype: 'displayfield',
                                            id: 'dfServicio',
                                            store: 'catalogos.stServicio',
                                            //value: '-',
                                            colspan: 2,
                                            fieldLabel: "Servicio",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 500,
                                            valueField: 'idServicio',
                                            displayField: 'serDescripcion',
                                            value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idServicio
                                            //value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.serDescripcion
                                            }
                                            , {
                                            xtype: 'displayfield',
                                            id: 'dfInstalacion',
                                            store: 'catalogos.stInstalacion',
                                            //value: '-',
                                            colspan: 2,
                                            fieldLabel: "Instalación",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 500,
                                            valueField: 'idInstalacion',
                                            displayField: 'insNombre',
                                            value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idInstalacion
                                            //value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.insNombre
                                            }*/

                                                ]
                                        }
                                           ,


                              //#region PanelResultado
                                                {xtype: 'panel',
                                                title: 'Resultados de Certificación'
                                                , bodyPadding: '10 10 10 10'
                                                , width: '100%'
                                                , layout: {
                                                    type: 'table'
                                                , columns: 2
                                                    //tdAttrs: { style: 'padding: 2px;' }
                                                },
                                                items: [

                                                {
                                                    xtype: 'displayfield',
                                                    id: 'dfResultadoCedula',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.evaluacionDesc,
                                                    colspan: 1,
                                                    grow: true,
                                                    fieldLabel: "Resultado de la Cédula",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 400

                                                },
                                               {
                                                   xtype: 'button'
                                                   //,icon:'Imagenes/BotonCertificacionL.png'
                                                   //,cls: 'descargaCertificado'
                                                    , iconCls: 'descargaCertificado'
                                                    , rowspan: 3
                                                    , margin: '10 10 10 150'
                                                    , id: 'btnCert'
                                                    , width: 100
                                                    , height: 100
                                                   // ,html:'<span class="footDescCert">Descarga certificado</span>'
                                                    , handler: function () {

                                                        //                if (Ext.getCmp('dfResultadoCedula').getValue() == '-'
                                                        //                 || Ext.getCmp('dfFolio').getValue() == '-'
                                                        //                 || Ext.getCmp('dfCalificacion').getValue() == '-'
                                                        //                 || Ext.getCmp('dfVigencia').getValue() == '-') {
                                                        //                    Ext.MessageBox.alert('SICER - ERROR', 'Se debe realizar el examen de certificación antes de descargar la certificación.');
                                                        //                    return false;
                                                        //                }

                                                        var strDatosPersona = "";
                                                        var strDatosPDF = "";
                                                        var strDatos = "";

                                                        var idCertificacion = Ext.getCmp('ddlCertificacion').getValue();

                                                        Ext.data.StoreManager.lookup('Examen.stCertificacionPDF').load({ params: {

                                                            idCertificacion: idCertificacion
                                                        }
                                                        });





                                                        var taskPDF = Ext.TaskManager.start({
                                                            run: function () {
                                                                if (!Ext.data.StoreManager.lookup('Examen.stCertificacionPDF').isLoading()) {
                                                                    Ext.MessageBox.hide();
                                                                    Ext.TaskManager.stop(taskPDF);
                                                                    Ext.data.StoreManager.lookup('Examen.stCertificacionPDF').each(function (rec) {
                                                                        strDatosPDF = strDatosPDF
                                                                            + rec.data.cerNombre + '|' //10
                                                                            + rec.data.cerDescripcion + '|' //11
                                                                            + rec.data.cerPrimeraValidez + '|' //12
                                                                            + rec.data.cerRenovacionValidez + '|' //13
                                                                            + rec.data.eeDescripcion + '|' //14
                                                                            + rec.data.ecDescripcion; //15
                                                                    });

                                                                    //                Ext.Ajax.request({
                                                                    //                    method: 'POST',
                                                                    //                    url: 'Home/cargaImagenPDFExamen',
                                                                    //                    failure: function () {
                                                                    //                        Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                                                                    //                    },
                                                                    //                    success: function (response) {

                                                                    //                        var retImagen = Ext.decode(response.responseText);

                                                                    //                        Ext.getCmp('hfFotografia').setValue(retImagen);
                                                                    //                        Ext.getCmp('imgFoto').setSrc(retImagen.strImagen);
                                                                    //                        Ext.getCmp('imgFoto').setSrc('data:image/jpeg;base64,' + retImagen.strImagen);
                                                                    //                        //'data:image/jpeg;base64,' + data.strImagen


                                                                    //                    }
                                                                    //                });

                                                                    strDatos = strDatos + Ext.getCmp('txbPaterno').getValue() + '|'//0
                                                                                    + Ext.getCmp('txbMaterno').getValue() + '|'//1
                                                                                    + Ext.getCmp('txbNombre').getValue() + '|'//2
                                                                                    + Ext.getCmp('txbCURP').getValue() + '|'//3
                                                                                    + Ext.getCmp('txbCUIP').getValue() + '|' //4
                                                                                    + Ext.getCmp('dfFechaAplicaExamen').getRawValue() + '|' //5
                                                                                    + Ext.getCmp('hfNombreFotografia').getValue() + '|' //6
                                                                                    + Ext.getCmp('dfFolio').getValue() + '|' //7
                                                                                    + Ext.getCmp('txbNumEmpleado').getValue() + '|' //8
                                                                                    + Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCertificacionRegistro + '|';  //9

                                                                    strDatos = strDatos.toString();

                                                                    strDatosPersona = strDatos + strDatosPDF;

                                                                    //strDatosPersona = strDatosPersona.replace(/ /g, '%7E');  //El simbolo ¬ no lo reconoce y en el documento aparecen todas las palabras juntas

                                                                    strDatosPersona = encodeURI(strDatosPersona);

                                                                    var call = 'Generales/handlerCertificacionPDF.ashx?strDatosPersona=' + strDatosPersona + '';
                                                                    var frm = '<object type="text/html" data=' + call + ' style="float:left;width:100%;height:100%; background-image:url(../Imagenes/cargandoObjeto.gif); background-repeat:no-repeat; background-position:center; " />';
                                                                    var winPDF = new Ext.Window({
                                                                        title: 'Certificación'
                                                                        , plain: true
                                                                        , height: 700
                                                                        , width: 700
                                                                        , modal: true
                                                                        , layaout: 'fit'
                                                                        , autoScroll: true
                                                                        , html: frm
                                                                        , buttons: [
                                                                        //                                                            {
                                                                        //                                                                text: 'Enviar Correo Electronico',
                                                                        //                                                                handler: function () {

                                                                        //                                                                    var sender = "";
                                                                        //                                                                    var asunto = "";
                                                                        //                                                                    var texto = "";

                                                                        //                                                                    Ext.data.StoreManager.lookup('catalogos.stDatosCorreo').load();

                                                                        //                                                                    var tiempoCarga = Ext.TaskManager.start({
                                                                        //                                                                        run: function () {
                                                                        //                                                                            if (!Ext.data.StoreManager.lookup('catalogos.stDatosCorreo').isLoading()) {
                                                                        //                                                                                Ext.TaskManager.stop(tiempoCarga);
                                                                        //                                                                                Ext.data.StoreManager.lookup('catalogos.stDatosCorreo').each(function (rec) {

                                                                        //                                                                                    sender = rec.data.sender;
                                                                        //                                                                                    asunto = rec.data.asunto;
                                                                        //                                                                                    texto = rec.data.texto;
                                                                        //                                                                                });

                                                                        //                                                                                var archName = Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCertificacionRegistro + '_'
                                                                        //                                                                                   + Ext.getCmp('txbCURP').getValue() + '.pdf';
                                                                        //                                                                                var nombreAtt = 'Certificado';

                                                                        //                                                                                var recep = 'usuario';

                                                                        //                                                                                Ext.Ajax.request({
                                                                        //                                                                                    method: 'POST',
                                                                        //                                                                                    url: 'Home/enviaCorreo',
                                                                        //                                                                                    params: { pdfName: archName, nameAttach: nombreAtt, receptor: recep, sender: sender, subject: asunto, text: texto },

                                                                        //                                                                                    failure: function () {
                                                                        //                                                                                        Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                                                                        //                                                                                    },
                                                                        //                                                                                    success: function (response) {
                                                                        //                                                                                        var retorno = Ext.decode(response.responseText);

                                                                        //                                                                                        if (retorno.alerta == "") {
                                                                        //                                                                                            Ext.MessageBox.alert('SICER', 'El correo ha sido enviado correctamente.');
                                                                        //                                                                                        } else {
                                                                        //                                                                                            Ext.MessageBox.alert('SICER', 'Error al enviar el correo: ' + retorno.alerta);
                                                                        //                                                                                        }

                                                                        //                                                                                    }
                                                                        //                                                                                });
                                                                        //                                                                            }
                                                                        //                                                                        }
                                                                        //                                                                        , interval: 500
                                                                        //                                                                    });
                                                                        //                                                                }
                                                                        //                                                            },
                                                            {
                                                            text: 'Salir',
                                                            handler: function () {
                                                                winPDF.destroy();
                                                            }
                                                        }


                                                        ]
                                                                    });
                                                                    winPDF.show();


                                                                }


                                                            },
                                                            interval: 500
                                                        });


                                                    }


                                               },
                                                {
                                                    xtype: 'displayfield',
                                                    id: 'dfFolio',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.folio,
                                                    colspan: 1,
                                                    // store: '',
                                                    // emptyText: 'SELECCIONAR',
                                                    // triggerAction: 'all',
                                                    // queryMode: 'local',
                                                    grow: true,
                                                    // editable: false,
                                                    fieldLabel: "Folio",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 400
                                                    // valueField: 'idInstalacion',
                                                    //  displayField: 'insNombre',
                                                    //  disabled: true,
                                                },

                                                {
                                                    xtype: 'displayfield',
                                                    id: 'dfCalificacion',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.calificacionDesc,
                                                    colspan: 1,
                                                    // store: '',
                                                    // emptyText: 'SELECCIONAR',
                                                    // triggerAction: 'all',
                                                    // queryMode: 'local',
                                                    grow: true,
                                                    // editable: false,
                                                    fieldLabel: "Calificación",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 450
                                                    // valueField: 'idInstalacion',
                                                    //  displayField: 'insNombre',
                                                    //  disabled: true,


                                                },
                                                {
                                                    xtype: 'displayfield',
                                                    id: 'dfVigencia',
                                                    value: Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.vigenciaDesc,
                                                    colspan: 2,
                                                    grow: true,
                                                    fieldLabel: "Vigencia",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 400
                                                }

                                                ]
                                            }
                              //#endregion PanelResultado
                                        ],
                              buttons: [
                                                      {
                                                          text: 'Aceptar',
                                                          // disabled: banderaHabilitar,
                                                          handler: function () {
                                                              session();


                                                              if (Ext.getCmp('ddlCertificacion').getValue() == (0 || null)
                                                                    || Ext.getCmp('dfFechaAplicaExamen').getRawValue() == ''
                                                                    || Ext.getCmp('tfHoraExamen').getRawValue() == ''
                                                                    || Ext.getCmp('ddlLugarAplicaExamen').getValue() == (0 || null)
                                                                    || Ext.getCmp('ddlEvaluador').getValue() == (0 || null)
                                                                    || Ext.getCmp('ddlInstAplicaExamen').getValue() == (0 || null)) {
                                                                  Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                                                                  return false;
                                                              }

                                                              if (Ext.getCmp('hfparticipante').value == 'E' &&
                                                                                (Ext.getCmp('ddlNivelSeguridad').getValue() == (0 || null)
                                                                                || Ext.getCmp('ddlDependenciaExterna').getValue() == (0 || null)
                                                                                || Ext.getCmp('ddlInstitucionExterna').getValue() == (0 || null))) {
                                                                  Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                                                                  return false;
                                                              }

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCertificacion = Ext.getCmp('ddlCertificacion').getValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.certificacion = Ext.getCmp('ddlCertificacion').getRawValue();

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idNivelSeguridad = Ext.getCmp('ddlNivelSeguridad').getValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.nsDescripcion = Ext.getCmp('ddlNivelSeguridad').getRawValue();

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idDependenciaExterna = Ext.getCmp('ddlDependenciaExterna').getValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.deDescripcion = Ext.getCmp('ddlDependenciaExterna').getRawValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.dfDomicilioLugarExamen = Ext.getCmp('dfDomicilioLugarExamen').getValue();

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idInstitucionExterna = Ext.getCmp('ddlInstitucionExterna').getValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.ieDescripcion = Ext.getCmp('ddlInstitucionExterna').getRawValue();

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.crFechaExamen = Ext.getCmp('dfFechaAplicaExamen').getRawValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.crHora = Ext.getCmp('tfHoraExamen').getRawValue();

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idLugarAplica = Ext.getCmp('ddlLugarAplicaExamen').getValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idEvaluador = Ext.getCmp('ddlEvaluador').getValue();
                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idInstitucionAplicaExamen = Ext.getCmp('ddlInstAplicaExamen').getValue();

                                                              Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.actualiza = 1;
                                                              Ext.getCmp('gridCertificaciones').getView().refresh(true);
                                                              win.destroy();
                                                          }
                                                      },
                                                    {
                                                        text: 'Cancelar', handler: function () {
                                                            session();
                                                            win.destroy();
                                                        }
                                                    }
                                         ]
                          });


                          /* if (Ext.getCmp('hfIdEmpleado').value != 0) {

                          Ext.getCmp('ddlNivelSeguridad').setVisible(false);
                          Ext.getCmp('ddlDependenciaExterna').setVisible(false);
                          Ext.getCmp('ddlInstitucionExterna').setVisible(false);
                          } else {

                          Ext.getCmp('dfZona').setVisible(true);
                          Ext.getCmp('dfServicio').setVisible(true);
                          Ext.getCmp('dfInstalacion').setVisible(true);
                          }*/

                          if (Ext.getCmp('hfparticipante').value == 'I') {

                              //Ext.getCmp('dfZona').setVisible(true);
                              //Ext.getCmp('dfServicio').setVisible(true);
                              //Ext.getCmp('dfInstalacion').setVisible(true);
                              Ext.getCmp('ddlNivelSeguridad').setVisible(false);
                              Ext.getCmp('ddlDependenciaExterna').setVisible(false);
                              Ext.getCmp('ddlInstitucionExterna').setVisible(false);

                          } else {

                              Ext.getCmp('ddlNivelSeguridad').setVisible(true);
                              Ext.getCmp('ddlDependenciaExterna').setVisible(true);
                              Ext.getCmp('ddlInstitucionExterna').setVisible(true);
                              //   Ext.getCmp('dfZona').setVisible(false);
                              //   Ext.getCmp('dfServicio').setVisible(false);
                              //   Ext.getCmp('dfInstalacion').setVisible(false);
                          }


                          Ext.getCmp('btnCert').setVisible(false);

                          switch (Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCalificacion) {
                              case 1:
                                  Ext.getCmp('btnCert').setVisible(true);
                              case 2:
                                  //Ext.getCmp('ddlCertificacion').setDisabled(true);
                                  Ext.getCmp('dfFechaAplicaExamen').setDisabled(true);
                                  Ext.getCmp('ddlInstAplicaExamen').setDisabled(true);
                                  Ext.getCmp('tfHoraExamen').setDisabled(true);
                                  Ext.getCmp('ddlLugarAplicaExamen').setDisabled(true);
                                  Ext.getCmp('ddlEvaluador').setDisabled(true);
                                  Ext.getCmp('ddlNivelSeguridad').setDisabled(true);
                                  Ext.getCmp('ddlDependenciaExterna').setDisabled(true);
                                  Ext.getCmp('ddlInstitucionExterna').setDisabled(true);
                          }

                          win.show();

                      }
                      else {
                          Ext.MessageBox.alert('SICER', 'Debes seleccionar una certificación.');
                      }

                  } //fin handler

                  // }///fin listener
              }
            //fin text




              , {
                  text: 'Eliminar Certificación ',
                  tooltip: 'Eliminar Certificación',
                  icon: 'Imagenes/icon16/cancelar.png',

                  // listeners: {
                  // click: function (gridView, htmlElement, columnIndex, dataRecord,rowIndex) {
                  handler: function () {

                      session();
                      //                if (Ext.getCmp('gridCertificaciones').getSelectionModel().hasSelection()) {

                      //                    Ext.getCmp('gridCertificaciones').store.remove(gridView.getSelectionModel().getSelection()[0]);

                      //                }

                      if (Ext.getCmp('gridCertificaciones').getSelectionModel().hasSelection()) {

                          if (Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCalificacion != 1
                              && Ext.getCmp('gridCertificaciones').getSelectionModel().getSelection()[0].data.idCalificacion != 2) {
                              var stElim = Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stEliminar');
                              var sm = Ext.getCmp('gridCertificaciones').getSelectionModel();

                              //if (elimino == 1) {
                              stElim.add({
                                  idRegistro: Ext.getCmp('hfIdRegistro').getValue(),
                                  idCertificacionRegistro: sm.getSelection()[0].data.idCertificacionRegistro,
                                  idCertificacion: sm.getSelection()[0].data.idCertificacion, elimino: 1
                              });
                              //   }

                              Ext.getCmp('gridCertificaciones').store.remove(sm.getSelection()[0]);
                          } else {
                              Ext.MessageBox.alert('SICER', 'No es posible eliminar un registro que ya tiene evaluación');
                          }
                      } else {
                          Ext.MessageBox.alert('SICER', 'Debes seleccionar una certificación');
                      }
                  }
              }

          ]


        })
        this.callParent(arguments);
    }
});
