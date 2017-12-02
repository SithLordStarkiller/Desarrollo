Ext.define('app.view.Administracion.RegistroPersonas.panDatosComplementarios', {
    extend: 'Ext.Panel',
    alias: 'widget.panelDatosComplementarios',
    autoScroll: true,
    initComponent: function () {
    
        Ext.apply(this, {
           // title: 'Datos Complementarios',
            xtype: 'panel',
            layout: {
                type: 'vbox',
                width: '100%',
                align: 'left'
                
            },
           // bodyPadding: '10 0 0 10',
            items: [
            {
                xtype: 'panel',
                bodyPadding: '10 10 10 10',
                //width: '100%',
                layout: {
                    type: 'table',
                    columns: 5
                    //tdAttrs: { style: 'padding: 2px;' }
                },
                items: [

        {
            xtype: 'label',
            id: 'lblDomicilioParticular',
            //text: 'DOMICILIO PARTICULAR',
            html: '<h4 style="margin-left: 30px;margin-right: 30px;font-size: 160%;" width:=>Domicilio Particular</h4>',
            rowspan: 2
        },

        {
            xtype: 'textfield',
            id: 'txbDomCalle',
            maskRe: /[0-9A-Za-zñÑ\s]/,
            msgTarget: 'side',
            maxLengthText: 'Máximo 300 caracteres el resto sera omitido',
            maxLength: 300,
            disabledCls: 'af-item-disabled',
            allowBlank: false,
            blankText: 'Campo Obligatorio',
            fieldLabel: 'Calle',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 300));
                }
            }
        },
        {
            xtype: 'textfield',
            id: 'txbNumExterior',
            //maskRe: /[0-9A-Za-zñÑ\s]/,
            msgTarget: 'side',
            maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
            maxLength: 30,
            allowBlank: true,
            disabledCls: 'af-item-disabled',
            fieldLabel: 'Núm. Exterior',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 30));
                }
            }
        },
        {
            xtype: 'textfield',
            id: 'txbNumInterior',
            //maskRe: /[0-9A-Za-zñÑ\s]/,
            msgTarget: 'side',
            maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
            maxLength: 30,
            allowBlank: true,
            disabledCls: 'af-item-disabled',
            fieldLabel: 'Núm. Interior',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 30));
                }
            }
        },
         {
             xtype: 'textfield',
             id: 'txbColonia',
             maskRe: /[0-9A-Za-zñÑ\s]/,
             msgTarget: 'side',
             maxLengthText: 'Máximo 70 caracteres el resto sera omitido',
             maxLength: 70,
             allowBlank: true,
             disabledCls: 'af-item-disabled',
             blankText: 'Campo Obligatorio',
             fieldLabel: 'Colonia',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 blur: function (field, newValue, oldValue) {
                     this.setValue(field.rawValue.toUpperCase().substring(0, 70));
                 }
             }
         },
         {
             xtype: 'textfield',
             id: 'txbCodPostal',
             maskRe: /[0-9]/,
             msgTarget: 'side',
             maxLengthText: 'Máximo 5 caracteres el resto sera omitido',
             maxLength: 5,
             allowBlank: true,
             blankText: 'Campo Obligatorio',
             disabledCls: 'af-item-disabled',
             fieldLabel: 'Código Postal',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 /*change: function (field, newValue, oldValue) {
                 field.setValue(newValue.substring(0, 5));
                 },*/
                 change: function (field, newValue, oldValue) {
                     if (Ext.util.Format.trim(this.getRawValue()) != '') {
                         if (this.getRawValue().length >= 6) {
                             Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'El Código Postal ingresado <b>' + (this.getRawValue()) + '</b> es invalida, acepta 5 caracteres');
                             this.setValue('');
                             return false;
                         }
                     }
                 }
             }
         },
         {
             xtype: 'combo',
             id: 'ddlEstado',
             store: Ext.getStore('catalogos.stEstados').load(),
             emptyText: 'SELECCIONAR',
             triggerAction: 'all',
             queryMode: 'local',
             grow: true,
             editable: false,
             allowBlank: false,
             blankText: 'Campo Obligatorio',
             disabledCls: 'af-item-disabled',
             fieldLabel: "Estado",
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             valueField: 'idEstado',
             displayField: 'estDescripcion',
             //  disabled: true,
             msgTarget: 'side',
             listeners:
            {
                change: function (field, newValue, oldValue) {

                    if (Ext.getCmp('ddlEstado').getValue() != ("" || null)) {
                        if (!isNaN(Ext.getCmp('ddlEstado').getValue())) {
                            Ext.getCmp('ddlDelegacionMun').clearValue();
                            Ext.getCmp('ddlDelegacionMun').store.load({ params: { idEstado: this.getValue()} });
                            Ext.getCmp('ddlDelegacionMun').setDisabled(false);
                            //Ext.getCmp('ddlDelegacionMun').clearValue(); 
                        }
                    }

                }
            }
         },
        {
            xtype: 'combo',
            id: 'ddlDelegacionMun',
            store: Ext.getStore('catalogos.stMunicipios').load(), // store: 'catalogos.stMunicipios',
            emptyText: 'SELECCIONAR',
            triggerAction: 'all',
            queryMode: 'local',
            grow: true,
            editable: false,
            allowBlank: false,
            blankText: 'Campo Obligatorio',
            disabledCls: 'af-item-disabled',
            fieldLabel: "Delegación/ Municipio",
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            valueField: 'idMunicipio',
            displayField: 'munDescripcion',
            //disabled: true,
            msgTarget: 'side'
        }


        ]
            }

            ]
        });
        this.callParent(arguments);
    }
});