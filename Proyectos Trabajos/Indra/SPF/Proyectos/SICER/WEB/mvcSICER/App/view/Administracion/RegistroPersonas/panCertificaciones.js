Ext.define('app.view.Administracion.RegistroPersonas.panCertificaciones', {
    extend: 'Ext.Panel',

    alias: 'widget.panelCertificaciones',
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    bodyPadding: '10 10 10 10',
    initComponent: function () {

        Ext.create('Ext.data.Store', {
            model: 'app.model.Administracion.RegistroPersonas.mdCertificacionRegistro',
            storeId: 'stCertificacionRegistro'
        });



        Ext.apply(this, {
            title: 'Certificaciones',
            layout: {
                type: 'vbox',
                align: 'center'
                //  columns: 2
            },

            items: [
             { xtype: 'hiddenfield', id: 'hfIdCertificacionRegistro', value: '' },
            { xtype: 'hiddenfield', id: 'hfIdCertificacion', value: '0' },

//                    { xtype: 'fieldset'
//                      , width: '100%'
//                     , padding: '20 20 20 20'
//                        /* , items: [
//                        { xtype: 'button'
//                        , text: 'Agregar certificación'
//                        , handler: function () {
//                        session();
//                        var win = new Ext.Window({
//                        id: 'win',
//                        title: 'Agregar Certificación'
//                        , width: 900
//                        , resizable: false
//                        , modal: true
//                        , plain: true
//                        , layout: {
//                        type: 'vbox',
//                        align: 'center'
//                        }
//                        , bodyPadding: '10 10 10 10'
//                        , items: [
//                        { xtype: 'panel',
//                        title: 'Datos de certificación'
//                        , bodyPadding: '10 10 50 10'
//                        , width: '100%'
//                        , layout: {
//                        type: 'table'
//                        , columns: 2
//                        //tdAttrs: { style: 'padding: 2px;' }
//                        },
//                        items: [
//                        { xtype: 'hiddenfield', id: 'inserto', value: 'true' },
//                        {
//                        xtype: 'combo',
//                        id: 'ddlCertificacion',
//                        store: Ext.getStore('catalogos.stCertificaciones').load(),
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        grow: true,
//                        editable: false,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        fieldLabel: 'Certificación',
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        valueField: 'idCertificacion',
//                        displayField: 'certificacion',
//                        msgTarget: 'side'
//                        },

//                        {
//                        xtype: 'datefield',
//                        id: 'dfFechaAplicaExamen',
//                        format: 'd/m/Y',
//                        submitFormat: 'd/m/Y',
//                        altFormats: 'd/m/Y',
//                        emptyText: 'DD/MM/AAAA',
//                        editable: false,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        Value: new Date(),
//                        fieldLabel: "Fecha de Examen",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 300,
//                        msgTarget: 'side'
//                        },
//                        {
//                        xtype: 'combo',
//                        id: 'ddlInstAplicaExamen',
//                        store: Ext.getStore('catalogos.stInstAplicaExamen').load(),
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        grow: true,
//                        editable: false,
//                        fieldLabel: "Institución que aplica el Examen",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        valueField: 'idInstitucionAplicaExamen',
//                        displayField: 'iaeDescripcion',
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'
//                        },
//                        {
//                        xtype: 'timefield',
//                        id: 'tfHoraExamen',
//                        fieldLabel: 'Hora de Examen',
//                        maskRe: /[0-9:]/,
//                        format: 'H:i',
//                        altFormats: 'H:i'
//                        , increment: 15
//                        , fieldLabel: "Hora de Examen",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 300,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'

//                        },
//                        {
//                        xtype: 'combo',
//                        id: 'ddlLugarAplicaExamen',
//                        store: Ext.getStore('catalogos.stLugarAplicacion').load(),
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        colspan: 2,
//                        grow: true,
//                        editable: false,
//                        fieldLabel: "Lugar de aplicación del Examen",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        valueField: 'idLugarAplica',
//                        displayField: 'laDescripcion',
//                        // disabled: true,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'
//                        , listeners:
//                        {
//                        select: function (field, newValue, oldValue) {

//                        if (Ext.getCmp('ddlLugarAplicaExamen').getValue() != ("" || null)) {
//                        if (!isNaN(Ext.getCmp('ddlLugarAplicaExamen').getValue())) {
//                        Ext.getCmp('dfDomicilioLugarExamen').setValue(Ext.data.StoreManager.lookup('catalogos.stLugarAplicacion').findRecord('idLugarAplica', Ext.getCmp('ddlLugarAplicaExamen').getValue()).data.laDomicilio);
//                        }
//                        }

//                        }
//                        }
//                        },
//                        {
//                        xtype: 'combo',
//                        id: 'ddlEvaluador',
//                        store: Ext.getStore('catalogos.stEvaluador').load(),
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        grow: true,
//                        editable: false,
//                        fieldLabel: "Evaluador",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        colspan: 2,
//                        valueField: 'idEvaluador',
//                        displayField: 'evaDescripcion',
//                        //disabled: true,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'
//                        },



//                        {
//                        xtype: 'combo',
//                        id: 'ddlNivelSeguridad',
//                        store: Ext.getStore('catalogos.stNivelSeguridad').load(),
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        grow: true,
//                        editable: false,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio',
//                        fieldLabel: "NIVEL DE SEGURIDAD",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        colspan: 2,
//                        valueField: 'idNivelSeguridad',
//                        displayField: 'nsDescripcion',
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'
//                        , listeners:
//                        {

//                        select: function (field, newValue, oldValue) {

//                        if (Ext.getCmp('ddlNivelSeguridad').getValue() != ("" || null)) {
//                        if (!isNaN(Ext.getCmp('ddlNivelSeguridad').getValue())) {
//                        Ext.getCmp('ddlDependenciaExterna').clearValue();
//                        Ext.getCmp('ddlDependenciaExterna').store.load({ params: { idNivelSeguridad: this.getValue()} });
//                        Ext.getCmp('ddlDependenciaExterna').setDisabled(false);
//                        Ext.getCmp('ddlDependenciaExterna').clearValue();
//                        Ext.getCmp('ddlInstitucionExterna').clearValue();
//                        }
//                        }

//                        }
//                        }
//                        },
//                        {
//                        xtype: 'combo',
//                        id: 'ddlDependenciaExterna',
//                        store: 'catalogos.stDependenciaExterna',
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        grow: true,
//                        editable: false,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio',
//                        fieldLabel: 'DEPENDENCIA O INSTITUCIÓN',
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        colspan: 2,
//                        valueField: 'idDependenciaExterna',
//                        displayField: 'deDescripcion',
//                        disabled: true,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'
//                        , listeners:
//                        {
//                        select: function (field, newValue, oldValue) {

//                        if (Ext.getCmp('ddlDependenciaExterna').getValue() != ("" || null)) {
//                        if (!isNaN(Ext.getCmp('ddlDependenciaExterna').getValue())) {
//                        Ext.getCmp('ddlInstitucionExterna').clearValue();
//                        Ext.getCmp('ddlInstitucionExterna').store.load({ params: { idDependenciaExterna: this.getValue()} });
//                        Ext.getCmp('ddlInstitucionExterna').setDisabled(false);
//                        Ext.getCmp('ddlInstitucionExterna').clearValue();
//                        }
//                        }

//                        }
//                        }

//                        }, {
//                        xtype: 'combo',
//                        id: 'ddlInstitucionExterna',
//                        store: 'catalogos.stInstitucionExterna',
//                        emptyText: 'SELECCIONAR',
//                        triggerAction: 'all',
//                        queryMode: 'local',
//                        grow: true,
//                        editable: false,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio',
//                        fieldLabel: 'NOMBRE DE LA INSTITUCIÓN',
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500,
//                        colspan: 2,
//                        valueField: 'idInstitucionExterna',
//                        displayField: 'ieDescripcion',
//                        disabled: true,
//                        allowBlank: false,
//                        blankText: 'Campo Obligatorio, Por favor seleccione una opción',
//                        msgTarget: 'side'
//                        },


//                        {
//                        xtype: 'displayfield',
//                        id: 'dfDomicilioLugarExamen',
//                        value: '-',
//                        // store: '',
//                        // emptyText: 'SELECCIONAR',
//                        // triggerAction: 'all',
//                        // queryMode: 'local',
//                        grow: true,
//                        // editable: false,
//                        fieldLabel: "Domicilio",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500
//                        // valueField: 'idInstalacion',
//                        //  displayField: 'insNombre',
//                        //  disabled: true,

//                        }


//                        ]
//                        },
//                        { xtype: 'panel',
//                        title: 'Resultados de certificación'
//                        , bodyPadding: '10 10 10 10'
//                        , width: '100%'
//                        , layout: {
//                        type: 'table'
//                        , columns: 2
//                        //tdAttrs: { style: 'padding: 2px;' }
//                        },
//                        items: [

//                        {
//                        xtype: 'displayfield',
//                        id: 'dfResultadoCedula',
//                        value: '-',
//                        colspan: 1,
//                        // store: '',
//                        // emptyText: 'SELECCIONAR',
//                        // triggerAction: 'all',
//                        // queryMode: 'local',
//                        grow: true,
//                        // editable: false,
//                        fieldLabel: "Resultado de la Cédula",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500
//                        // valueField: 'idInstalacion',
//                        //  displayField: 'insNombre',
//                        //  disabled: true,


//                        },
//                        {
//                        xtype: 'button'
//                        //,icon:'Imagenes/BotonCertificacionL.png'
//                        //,cls:'descargaCertificado'
//                        , iconCls: 'descargaCertificado'
//                        , rowspan: 3
//                        , margin: '10 10 10 150'
//                        , width: 100
//                        , height: 100
//                        // ,html:'<span class="footDescCert">Descarga certificado</span>'
//                        , handler: function () {
//                        alert('Has dado clic en el botón');
//                        }

//                        },
//                        {
//                        xtype: 'displayfield',
//                        id: 'dfFolio',
//                        value: '-',
//                        colspan: 1,
//                        // store: '',
//                        // emptyText: 'SELECCIONAR',
//                        // triggerAction: 'all',
//                        // queryMode: 'local',
//                        grow: true,
//                        // editable: false,
//                        fieldLabel: "Folio",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500
//                        // valueField: 'idInstalacion',
//                        //  displayField: 'insNombre',
//                        //  disabled: true,
//                        },

//                        {
//                        xtype: 'displayfield',
//                        id: 'dfCalificacion',
//                        value: '-',
//                        colspan: 1,
//                        // store: '',
//                        // emptyText: 'SELECCIONAR',
//                        // triggerAction: 'all',
//                        // queryMode: 'local',
//                        grow: true,
//                        // editable: false,
//                        fieldLabel: "Calificación",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500
//                        // valueField: 'idInstalacion',
//                        //  displayField: 'insNombre',
//                        //  disabled: true,


//                        },
//                        {
//                        xtype: 'displayfield',
//                        id: 'dfVigencia',
//                        value: '-',
//                        colspan: 2,
//                        // store: '',
//                        // emptyText: 'SELECCIONAR',
//                        // triggerAction: 'all',
//                        // queryMode: 'local',
//                        grow: true,
//                        // editable: false,
//                        fieldLabel: "Vigencia",
//                        labelAlign: 'right',
//                        labelWidth: 200,
//                        width: 500
//                        // valueField: 'idInstalacion',
//                        //  displayField: 'insNombre',
//                        //  disabled: true,

//                        }


//                        //                                                ,{
//                        //                                                xtype: 'image',
//                        //                                                //src: '/path/to/img.png',
//                        //                                                height: 50, // Specifying height/width ensures correct layout
//                        //                                                width: 50,
//                        //                                                listeners: {
//                        //                                                render: function(c) {
//                        //                                                c.getEl().on('click', function(e) {
//                        //                                                alert('User clicked image');
//                        //                                                }, c);
//                        //                                                }
//                        //                                                }
//                        //                                                }



//                        ]
//                        }

//                        ],
//                        buttons: [
//                        {
//                        text: 'Aceptar', handler: function () {
//                        session();

//                        if (Ext.getCmp('ddlCertificacion').getValue() == 0

//                        || Ext.getCmp('ddlNivelSeguridad').getValue() == 0
//                        || Ext.getCmp('ddlDependenciaExterna').getValue() == 0
//                        || Ext.getCmp('ddlInstitucionExterna').getValue() == 0

//                        || Ext.getCmp('dfFechaAplicaExamen').getValue() == ''
//                        || Ext.getCmp('tfHoraExamen').getRawValue() == ''
//                        || Ext.getCmp('ddlLugarAplicaExamen').getValue() == 0
//                        || Ext.getCmp('ddlEvaluador').getValue() == 0
//                        || Ext.getCmp('ddlInstAplicaExamen').getValue() == 0) {
//                        Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
//                        return false;
//                        }


//                        var alerta = false;


//                        Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stCertificacionRegistro').each(function (subrec) {
//                        if (subrec.data.certificacion.trim() == Ext.getCmp('ddlCertificacion').getRawValue()) {
//                        alerta = true;
//                        }
//                        });

//                        if (alerta == true) {
//                        Ext.MessageBox.alert('SICER', '<b>Certificación existente</b></br> Favor de verificarlo');
//                        }
//                        else {


//                        //if (Ext.getCmp('hfIdCertificacionRegistro').getValue() == 0) {


//                        Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stCertificacionRegistro').add({


//                        idCertificacion: Ext.getCmp('ddlCertificacion').getValue()
//                        , certificacion: Ext.getCmp('ddlCertificacion').getRawValue()

//                        , idNivelSeguridad: Ext.getCmp('ddlNivelSeguridad').getValue()
//                        , nsDescripcion: Ext.getCmp('ddlNivelSeguridad').getRawValue()


//                        , idDependenciaExterna: Ext.getCmp('ddlDependenciaExterna').getValue()
//                        , deDescripcion: Ext.getCmp('ddlDependenciaExterna').getRawValue()

//                        , idInstitucionExterna: Ext.getCmp('ddlInstitucionExterna').getValue()
//                        , ieDescripcion: Ext.getCmp('ddlInstitucionExterna').getRawValue()

//                        , crFechaExamen: Ext.getCmp('dfFechaAplicaExamen').rawValue
//                        , crHora: Ext.getCmp('tfHoraExamen').getRawValue()
//                        , idLugarAplica: Ext.getCmp('ddlLugarAplicaExamen').getValue()
//                        , idEvaluador: Ext.getCmp('ddlEvaluador').getValue()
//                        , idInstitucionAplicaExamen: Ext.getCmp('ddlInstAplicaExamen').getValue()
//                        , participante: Ext.getCmp('participante').getValue('E')

//                        , laDescripcion: Ext.getCmp('ddlLugarAplicaExamen').getRawValue()////agregado
//                        , iaeDescripcion: Ext.getCmp('ddlInstAplicaExamen').getValue()
//                        , evaDescripcion: Ext.getCmp('ddlEvaluador').getValue()
//                        , inserto: Ext.getCmp('inserto').getValue('true')


//                        , resAprobado: 'PENDIENTE'
//                        , actualiza: 1


//                        });
//                        //  }//fin idcertificacion


//                        }



//                        win.destroy();

//                        }
//                        },
//                        {
//                        text: 'Cancelar', handler: function () {
//                        session();
//                        win.destroy();
//                        }
//                        }
//                        ]
//                        });
//                        win.show();
//                        }
//                        }
//                        ]*/

//                    }
//                ,

                    { xtype: 'gridCertificaciones' }
            ]
        });
        this.callParent(arguments);
    }
});
