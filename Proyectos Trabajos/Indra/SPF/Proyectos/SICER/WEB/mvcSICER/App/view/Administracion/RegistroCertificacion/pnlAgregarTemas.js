Ext.define('app.view.Administracion.RegistroCertificacion.pnlAgregarTemas', {
    extend: 'Ext.Panel',
    alias: 'widget.pnlAgregarTemas',
    id: 'pnlAgregarTemas',
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    bodyPadding: '10 10 10 10',
    initComponent: function () {
        Ext.apply(this, {
            title: 'Temas Certificaciones',
            layout: {
                type: 'vbox',
                align: 'center'
                //  columns: 2
            },
            items: [
                {
                    xtype: 'fieldset',
                    width: '100%',
                    padding: '20 20 20 20',
                    items: [
//                        {
//                            xtype: 'label',
//                            id: 'lblTemas1',
//                            html: '<h4 style="margin-left: 5px;margin-right: 5px;font-size: 160%;color: #4986D5;" width:=>Temas</h4>',
//                            //text: 'Temas',
//                            //colspan: 3
//                        },
////                        {   
//                            xtype: 'button',
//                            text: 'Agregar Tema',
//                            handler: function () {
                                // session();
//                                var window = new Ext.Window({
//                                    id: 'window',
//                                    title: 'Agregar o modificar Tema',
//                                    width: 1000,
//                                    resizable: false,
//                                    modal: true,
//                                    plain: true,
////                                    layout: {
////                                        type: 'vbox',
////                                        align: 'center'
////                                    },
//                                    bodyPadding: '10 10 10 10',
//                                    items: [
//                                        { 
//                                            xtype: 'panel',
//                                            title:'Temas',
//                                            bodyPadding: '10 10 10 10',
//                                            width:'100%',
//											layout: {
//                                                type: 'table',
//                                                columns: 3
//                                                //tdAttrs: { style: 'padding: 2px;' }
//                                            },
//                                            items: [
//                                                {
//                                                    xtype: 'label',
//                                                    id: 'lblTemas',
//                                                    html: '<h4 style="margin-left: 5px;margin-right: 5px;font-size: 160%;color: #4986D5;" width:=>Temas</h4>',
//                                                    //text: 'Temas',
//                                                    colspan: 3
//                                                },
                                                {
                                                    xtype: 'textarea',
                                                    id: 'txbNombreTema',
                                                    grow: true,
                                                    editable: true,
                                                    fieldLabel: 'Nombre Tema',
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    colspan: 2,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    msgTarget: 'side',
                                                    maskRe: /[A-Z a-zñÑ0-9\/.().+.*.'.¿.?.¡.!.:.;.,.-._.-]/,
                                                    maxLengthText: 'Máximo 500 caracteres, el resto sera omitido',
                                                    maxLength: 500,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase().substring(0, 500));
                                                        },
                                                        render: function () {
                                                            this.getEl().on('paste', function (e, t, eOpts) {
                                                                e.stopEvent();
                                                                Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                            });
                                                        }
                                                    }
                                                },
                                                {
                                                    xtype: 'checkbox',
                                                    boxLabel: 'Tema Activo',
                                                    name: 'TemaActivo',
                                                    allowBlank: false
                                                },
                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbCodigoTema',
                                                    grow: true,
                                                    editable: true,
                                                    fieldLabel: 'Código Tema',
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    msgTarget: 'side',
                                                    maskRe: /[A-Z a-zñÑ0-9\/.().+.*.'.¿.?.¡.!.:.;.,.-._.-]/,
                                                    maxLengthText: 'Máximo 30 caracteres, el resto sera omitido',
                                                    maxLength: 30,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase().substring(0, 30));
                                                        },
                                                        render: function () {
                                                            this.getEl().on('paste', function (e, t, eOpts) {
                                                                e.stopEvent();
                                                                Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                            });
                                                        }
                                                    }
                                                },
                                                {
                                                    xtype: 'combo',
                                                    id: 'cbOrdenTema',
                                                    //     store: '',
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    fieldLabel: "Orden del Tema",
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 350,
                                                    colspan: 2,
                                                    //         valueField: '',
                                                    //         displayField: '',
                                                    // autoSelect: true,
                                                    //  disabled: true,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                                    msgTarget: 'side'
                                                },

                                                // Falta hacer validación de este componente
        				                        {
        				                            xtype: 'textfield',
        				                            id: 'txbNoPreguntasAleatoriasTema',
        				                            grow: true,
        				                            editable: true,
        				                            fieldLabel: 'No. de Preguntas aleatorias por Tema',
        				                            labelAlign: 'right',
        				                            labelWidth: 200,
        				                            width: 500,
        				                            allowBlank: false,
        				                            blankText: 'Campo Obligatorio',
        				                            msgTarget: 'side',
        				                            maskRe: /[0-9]/,
        				                            maxLengthText: 'Máximo 30 caracteres, el resto sera omitido',
        				                            maxLength: 30,
        				                            listeners: {
        				                                change: function (field, newValue, oldValue) {
        				                                    field.setValue(newValue.toUpperCase().substring(0, 30));
        				                                },
        				                                render: function () {
        				                                    this.getEl().on('paste', function (e, t, eOpts) {
        				                                        e.stopEvent();
        				                                        Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
        				                                    });
        				                                }
        				                            }
                                                },

                                                // Falta hacer validación de este componente
        				                        {
        				                            xtype: 'textfield',
        				                            id: 'txbNoPreguntasCorrectasAprobarTema',
        				                            grow: true,
        				                            editable: true,
        				                            fieldLabel: 'Número de preguntas correctas para aprobar el Tema',
        				                            labelAlign: 'right',
        				                            labelWidth: 200,
        				                            width: 350,
        				                            colspan: 2,
        				                            allowBlank: false,
        				                            blankText: 'Campo Obligatorio',
        				                            msgTarget: 'side',
        				                            maskRe: /[0-9]/,
        				                            maxLengthText: 'Máximo 30 caracteres, el resto sera omitido',
        				                            maxLength: 30,
        				                            listeners: {
        				                                change: function (me, newValue, oldValue, eOpts) {
        				                                    if (newValue > Ext.getCmp("txbNoPreguntasAleatoriasFuncion").getValue()) {
        				                                        Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. De preguntas correctas para aprobar la Función</b> no debe ser mayor al campo <b>No. de preguntas aleatorias por Función</b>');
        				                                    }
        				                                },
        				                                render: function () {
        				                                    this.getEl().on('paste', function (e, t, eOpts) {
        				                                        e.stopEvent();
        				                                        Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
        				                                    });
        				                                }
        				                            }
                                                },
                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbTiempoExamenContestarTema',
                                                    grow: true,
                                                    editable: true,
                                                    fieldLabel: 'Tiempo de examen para contestar el Tema (minutos)',
                                                    labelAlign: 'right',
                                                    labelWidth: 200,
                                                    width: 500,
                                                    colspan: 3,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    msgTarget: 'side',
                                                    maskRe: /[0-9]/,
                                                    maxLengthText: 'Máximo 30 caracteres, el resto sera omitido',
                                                    maxLength: 30,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase().substring(0, 30));
                                                        },
                                                        render: function () {
                                                            this.getEl().on('paste', function (e, t, eOpts) {
                                                                e.stopEvent();
                                                                Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                            });
                                                        }
                                                    }
                                                },
                                                {
                                                    buttons: [
                                                        {
                                                            text: 'Nuevo', width: 100, handler: function () {
                                                                session();
                                                                win.destroy();
                                                            }
                                                        },
                                                        {
                                                            text: 'Guardar', width: 100, handler: function () {
                                                                session();
                                                                win.destroy();
                                                            }
                                                        },
                                                        {
                                                            text: 'Cancelar', width: 100, handler: function () {
                                                                session();
                                                                win.destroy();
                                                            }
                                                        }
                                                    ]
                                                }
//                                            ]
//                                        }
//                                    ]
//                                });
//                                window.show();
//                            }
//                        },


                        ///HASTA AQUI TERMINA EL BOTON DE AGREGAR TEMAS
//                        { xtype: 'gridTemas' }
                    ]  //CIERRA ITEMS
                } //CIERRA LLAVE ABAJO DE ITEMS
            ] //CIERRA SEGUNDO ITEMS (PRIMERO)
        });
        this.callParent(arguments);
    }
});