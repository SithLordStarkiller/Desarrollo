Ext.define('app.view.Administracion.RegistroCertificacion.panDatosCertificacion', {
    extend: 'Ext.Panel',
    alias: 'widget.panelDatosCertificacion',
    autoScroll: true,
    initComponent: function () {



        Ext.apply(this, {
            xtype: 'panel',
            title: '<span style="font-size: 160%;margin-top:20px;">Datos de la certificación</span>',
            split: true,
            autoScroll: true,
            layout: {
                type: 'table',
                align: 'center',
                columns: 4
            },
            bodyPadding: '10 10 10 10',
            width: '100%',

            items: [
										{
										    xtype: 'textfield',
										    id: 'txbNombreCertificacion',
										    fieldLabel: 'Nombre de la Certificación',
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 700,
										    colspan: 3,
										    allowBlank: false,
										    blankText: 'Campo Obligatorio',
										    disabledCls: 'af-item-disabled',
										    enforceMaxLength: true,
										    maxLength: 100,
										    margin: '5 0 15 0',
										    maskRe: /[áéíóúÁÉÍÓÚA-Z a-zñÑ0-9.-]/,
										    listeners: {
										        change: function (field, newValue, oldValue) {
										            field.setValue(newValue.toUpperCase());
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
                                            boxLabel: 'Certificación <b>Activa</b>',
                                            name: 'CertificacionActiva',
                                            disabledCls: 'af-item-disabled',
                                            id: 'CertificacionActiva',
                                            checked: false,
                                            listeners: {
                                                change: function () {
                                                    if (this.getValue()==true) {
                                                        var temaCertificacion = 0;
                                                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').each(function (rec) {
                                                            if (rec.data.ctActivo == true)
                                                                temaCertificacion++;
                                                        });

                                                        if (temaCertificacion == 0) {
                                                            this.setValue(false);
                                                            Ext.MessageBox.alert('SICER - Alerta', 'La Certificación solo puede ser activada solo si tiene uno o más temas activos');
                                                        }
                                                    }
                                                    /*
                                                    if (!this.getValue()) {
                                                    Ext.MessageBox.alert('SICER - Alerta', 'La Certificación esta desactivada');
                                                    } else {
                                                    Ext.MessageBox.alert('SICER - Alerta', 'La Certificación esta Activada');
                                                    }*/
                                                }
                                            }
                                        },

                                        {
                                            xtype: 'textfield',
                                            id: 'txbSiglas',
                                            grow: true,
                                            fieldLabel: 'Siglas',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 300,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            disabledCls: 'af-item-disabled',
                                            //msgTarget: 'side',
                                            maskRe: /[A-Z a-zñÑ0-9.-]/,
                                            maxLength: 30,
                                            enforceMaxLength: true,
                                            listeners: {
                                                change: function (field, newValue, oldValue) {
                                                    field.setValue(newValue.toUpperCase());
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
                                            id: 'txbTiempoValidezCertificacionPrimeraVez',
                                            fieldLabel: 'Tiempo Validez Certificación <b>PRIMERA VEZ</b> (años)',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 300,
                                            allowBlank: false,
                                            disabledCls: 'af-item-disabled',
                                            blankText: 'Campo Obligatorio',
                                            maxLength: 2,
                                            enforceMaxLength: true,
                                            maskRe: /[0-9]/,
                                            listeners: {
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
										    id: 'txbTiempoValidezCertificacionRenovacion',
										    grow: true,
										    editable: true,
										    fieldLabel: 'Tiempo Validez Certificación <b>RENOVACIÓN</b> (años)',
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 300,
										    disabledCls: 'af-item-disabled',
										    allowBlank: false,
										    maxLength: 2,
										    enforceMaxLength: true,
										    blankText: 'Campo Obligatorio',
										    //msgTarget: 'side',
										    maskRe: /[0-9]/,
										    listeners: {
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
                                            id: 'txbTiempoParaNuevoIntento',
                                            grow: true,
                                            editable: true,
                                            fieldLabel: 'Tiempo Para <b>Nuevo Intento</b> (meses)',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 300,
                                            disabledCls: 'af-item-disabled',
                                            allowBlank: true,
                                            //   blankText: 'Campo Obligatorio',
                                            maskRe: /[0-9]/,
                                            maxLength: 3,
                                            enforceMaxLength: true,
                                            margin: '0 0 0 20',
                                            listeners: {
                                                change: function (field, newValue, oldValue) {
                                                    if (field.getValue() > 250)
                                                        field.setValue('250');
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
										    xtype: 'datefield',
										    id: 'dfFechaDeAlta',
										    format: 'd/m/Y',
										    submitFormat: 'd/m/Y',
										    editable: false,
										    altFormats: 'd/m/Y',
										    emptyText: 'DD/MM/AAAA',
										    Value: new Date(),
										    disabledCls: 'af-item-disabled',
										    maxValue: new Date(),
										    fieldLabel: "Fecha de <b>Alta</b>",
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 300,
										    allowBlank: false,
										    blankText: 'Campo Obligatorio, Por favor seleccione una opción'
										},
            /*{
            xtype: 'datefield',
            id: 'dfFechaDeBaja',
            editable: false,
            format: 'd/m/Y',
            submitFormat: 'd/m/Y',
            altFormats: 'd/m/Y',
            emptyText: 'DD/MM/AAAA',
            Value: new Date(),
            maxValue: new Date(),
            fieldLabel: "Fecha de <b>Baja</b>",
            labelAlign: 'right',
            labelWidth: 200,
            width: 300,
            allowBlank: true
            , listeners: {
            render: function (c) {

            Ext.QuickTips.register({
            target: c.getEl(),
            text: 'Fecha en que esta certificación se dará de baja',
            enabled: true,
            showDelay: 100,
            trackMouse: true,
            autoShow: true
            });
                                                
            }
            }

            },*/
                                        {
                                        //xtype: 'textfield',
                                        xtype: 'displayfield',
                                        id: 'txbTiempoTotalExamen',
                                        fieldLabel: 'Tiempo total de Examen (<b>minutos</b>)',
                                        labelAlign: 'right',
                                        labelWidth: 200,
                                        width: 300,
                                        allowBlank: false,
                                        blankText: 'Campo Obligatorio',
                                        maskRe: /[0-9]/,
                                        value: 0,
                                        //disabled: true,
                                        //disabledCls: 'af-item-disabled',
                                        //maxLength: 4,
                                        //enforceMaxLength: true,
                                        listeners: {
                                            render: function (c) {
                                                Ext.QuickTips.register({
                                                    target: c.getEl(),
                                                    text: 'Muestra la duración total (en minutos) del examen. </br> Suma duración de los temas <b>activos</b>',
                                                    enabled: true,
                                                    showDelay: 100,
                                                    trackMouse: true,
                                                    autoShow: true
                                                });
                                            }
                                        }
                                    },
										{
										    //xtype: 'textfield',
										    xtype: 'displayfield',
										    id: 'txbNumeroDePreguntasExamen',
										    fieldLabel: 'Número total de <b>Preguntas</b>',
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 300,
										    allowBlank: false,
										    blankText: 'Campo Obligatorio',
										    maskRe: /[0-9]/,
										    value: 0,
										    //disabled: true,
										    //disabledCls: 'af-item-disabled',
										    //maxLength: 4,
										    //enforceMaxLength: true,
										    listeners: {
										        render: function (c) {
										            Ext.QuickTips.register({
										                target: c.getEl(),
										                text: 'Muestra el número total de preguntas que aparecerán aleatoriamente. </br> Suma número de preguntas de los temas <b>activos</b>',
										                enabled: true,
										                showDelay: 100,
										                trackMouse: true,
										                autoShow: true
										            });
										        }
										    }
										},
										{
										    //xtype: 'textfield',
										    xtype: 'displayfield',
										    id: 'txbNumeroDePreguntasCorrectasParaAprobar',
										    fieldLabel: '<b>Preguntas Correctas</b> para Aprobar',
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 300,
										    allowBlank: false,
										    blankText: 'Campo Obligatorio',
										    maskRe: /[0-9]/,
										    margin: '0 0 0 20',
										    value: 0,
										    //										    maxLength: 4,
										    //										    enforceMaxLength: true,
										    // disabled: true,
										    // disabledCls: 'af-item-disabled',
										    listeners: {
										        render: function (c) {
										            Ext.QuickTips.register({
										                target: c.getEl(),
										                text: 'Muestra el número total de preguntas que deben ser contestadas </br> correctamente. Suma de los temas <b>activos</b>',
										                enabled: true,
										                showDelay: 100,
										                trackMouse: true,
										                autoShow: true
										            });
										        }
										    }
										},
                                        {
                                            xtype: 'textarea',
                                            id: 'txADescripcionCertificacion',
                                            grow: true,
                                            editable: true,
                                            fieldLabel: 'Descripción Certificación',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 620,
                                            height: 100,
                                            colspan: 2,
                                            rowspan: 2,
                                            margin: '20 0 0 0',
                                            disabledCls: 'af-item-disabled',
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maskRe: /[áéíóúÁÉÍÓÚA-Z a-zñÑ0-9\/.().+.*.'.¿.?.¡.!.:.;.,.-._.-]/,
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
										    xtype: 'combo',
										    id: 'cbEntidadesCertificadoras',
										    store: Ext.getStore('catalogos.stEntidadCertificadora').load(),
										    emptyText: 'SELECCIONAR',
										    triggerAction: 'all',
										    queryMode: 'local',
										    grow: true,
										    editable: false,
										    fieldLabel: "Entidades Certificadoras",
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 620,
										    colspan: 2,
										    margin: '10 0 0 0',
										    disabledCls: 'af-item-disabled',
										    valueField: 'idEntidadCertificadora',
										    displayField: 'ecDescripcion',
										   // autoSelect: true,
										    //  disabled: true,
										    allowBlank: false,
										    blankText: 'Campo Obligatorio, Por favor seleccione una opción'

										},
										{
										    xtype: 'combo',
										    id: 'cbEntidadesEvaluadoras',
										    store: Ext.getStore('catalogos.stEntidadEvaluadora').load(),
										    emptyText: 'SELECCIONAR',
										    triggerAction: 'all',
										    queryMode: 'local',
										    grow: true,
										    editable: false,
										    fieldLabel: "Entidades Evaluadoras",
										    labelAlign: 'right',
										    labelWidth: 200,
										    width: 620,
										    colspan: 2,
										    disabledCls: 'af-item-disabled',
										    valueField: 'idEntidadEvaluadora',
										    displayField: 'eeDescripcion',
										    //autoSelect: true,
										    //disabled: true,
										    allowBlank: false,
										    blankText: 'Campo Obligatorio, Por favor seleccione una opción'
										}
                                 ]
        });
        this.callParent(arguments);
    }
});
