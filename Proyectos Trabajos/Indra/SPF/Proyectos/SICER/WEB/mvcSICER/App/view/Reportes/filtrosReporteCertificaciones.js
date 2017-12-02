Ext.define('app.view.Reportes.filtrosReporteCertificaciones',
{
    extend: 'Ext.form.Panel',
    alias: 'widget.filtrosReporteCertificaciones',
    title: 'Filtros de búsqueda',
    layout: {
        type: 'table',
        width: '100%',
        columns: 2
    },
    initComponent: function () {

        var myStoreEntidadCertificadora = Ext.create('app.store.catalogos.stEntidadCertificadora');
        var myStoreEntidadEvaluadora = Ext.create('app.store.catalogos.stEntidadEvaluadora');

        var scope = this;
        Ext.apply(this,
        {
            margins: '0 0 10 0',
            bodyPadding: '10 10 10 10',
            collapsible: true,
            lbar: [{
                text: 'Generar',
                disabled: false,
                iconCls: 'generar',
                handler: function () {
                    session();
                    //                    //          

                    Ext.getCmp('tabReporteCertificaciones').add({ xtype: 'reporteViewr', flex: 4.7, id: 'idReporteCertificaciones' });

                    var fechaInicioRC = "";
                    var fechaFinRC = "";
                    //Ext.getCmp('fechaFinGrl').getRawValue()
                    var fechaInicioRC = Ext.getCmp('dfFechaInicioRepCer').getRawValue();
                    var fechaFinRC = Ext.getCmp('dfFechaFinRepCer').getRawValue();


                    var fecha1 = Ext.getCmp('dfFechaInicioRepCer').getRawValue();
                    var dia1 = fecha1.substr(0, 2);
                    var mes1 = fecha1.substr(3, 2);
                    var anyo1 = fecha1.substr(6);

                    var fecha2 = Ext.getCmp('dfFechaFinRepCer').getRawValue();
                    var dia2 = fecha2.substr(0, 2);
                    var mes2 = fecha2.substr(3, 2);
                    var anyo2 = fecha2.substr(6);


                    var nuevafecha1 = new Date(anyo1 + "/" + mes1 + "/" + dia1);
                    var nuevafecha2 = new Date(anyo2 + "/" + mes2 + "/" + dia2);

                    if (nuevafecha1 > nuevafecha2) {
                        Ext.MessageBox.alert('SICER', '<b>Fecha Inicio</b></br> La fecha de inicio no puede ser mayor a la de fin');
                        return false;
                    }

                    /////////////// FECHAS OBLIGATORIAS ///////////////////////

//                    if (fechaInicioRepCer != ("")) {
//                        var fechaInicioRC = fechaInicioRepCer;
//                    }
//                    else {
//                        Ext.MessageBox.alert('SICER', '<b>Fecha Inicio</b></br> Campo obligatorio');
//                        return false;
//                    }

//                    if (fechaFinRepCer != ("")) {

//                        var fechaFinRC = fechaFinRepCer;
//                    }
//                    else {
//                        Ext.MessageBox.alert('SICER', '<b>Fecha Fin</b></br> Campo obligatorio');
//                        return false;
                    //                    }

                    /////////// FIN FECHAS OBLIGATORIAS /////////////////////

                    var EntidadEvaluadora = '';
                    var EntidadCertificadora = '';
                    //                 
                    var EntidadEvaluadora = Ext.getCmp('ddlEntEva').getValue();
                    var EntidadCertificadora = Ext.getCmp('ddlEntCer').getValue();

                    if (Ext.getCmp('rblTipoActivaciones').getValue().rblTipoActivaciones == "Activa") {
                        var Activa = 1
                    }

                    if (Ext.getCmp('rblTipoActivaciones').getValue().rblTipoActivaciones == "No Activa") {
                        var Activa = 0
                    }

                    if (Ext.getCmp('rblTipoActivaciones').getValue().rblTipoActivaciones == undefined) {
                        var Activa = 3
                    }


                    //                    cambiar filtros
                    var filtros = (
                            '../Generales/frmVisorReporte.aspx?' +
                            'fechaInicio=' + fechaInicioRC +
                            '&fechaFin=' + fechaFinRC +
                            '&cerActivaDesactivada=' + Activa +
                            '&idEntidadEvaluadora=' + EntidadEvaluadora +
                            '&idEntidadCertificadora=' + EntidadCertificadora +
                            '&reporte=1'
                          )


                    var frm = '<object type="text/html" data=' + filtros + ' scrolling="yes" style="float:left;width:100%;height:100%;" background-image:url(../Imagenes/cargandoObject.gif); background-repeat:no-repeat; background-position:center;></object>'
                    Ext.getCmp('idReporteCertificaciones').update(frm);

                }
            }],
            items: [
            //                { xtype: 'label', text: 'FECHA INICIO: ', cls: "texto padR" },
                        {xtype: 'datefield', id: 'dfFechaInicioRepCer', labelPad: 10,
                        fieldLabel: 'Fecha Inicio',
                        format: 'd/m/Y',
                        submitFormat: 'd/m/Y',
                        altFormats: 'd/m/Y',
                        emptyText: 'DD/MM/AAAA',
                        editable: false,
                        maxValue: new Date(),
                        maskRe: /[\/\-0-9]/,
                        allowBlank: true,
                        editable: false
                    },
            //                            { xtype: 'label', text: 'FECHA FIN: ', cls: "texto padR" },
                        {xtype: 'datefield', id: 'dfFechaFinRepCer', labelPad: 10,
                        format: 'd/m/Y',
                        fieldLabel: 'Fecha Fin',
                        submitFormat: 'd/m/Y',
                        altFormats: 'd/m/Y',
                        emptyText: 'DD/MM/AAAA',
                        editable: false,
                        maxValue: new Date(),
                        maskRe: /[\/\-0-9]/,
                        allowBlank: true,
                        editable: false
                    },


             { xtype: 'combo',
                 id: 'ddlEntCer',
                 fieldLabel: 'Entidad Certificadora:',
                 store: myStoreEntidadCertificadora,
                 emptyText: 'SELECCIONAR',
                 //hidden:true,
                 grow: true,
                 triggerAction: 'all',
                 fieldStyle: 'text-transform:uppercase',
                 //            editable: false,
                 valueField: 'idEntidadCertificadora',
                 displayField: 'ecDescripcion',
                 labelPad: 10,
                 width: 350,
                 x: 10,
                 y: 195

             },
            //Combos servicio
            {xtype: 'combo',
            id: 'ddlEntEva',
            fieldLabel: 'Entidad Evaluadora',
            store: myStoreEntidadEvaluadora,
            emptyText: 'SELECCIONAR',
            grow: true,
            //hidden:true,
            triggerAction: 'all',
            fieldStyle: 'text-transform:uppercase',
            //            editable: false,
            valueField: 'idEntidadEvaluadora',
            displayField: 'eeDescripcion',
            labelPad: 10,
            width: 500,
            x: 10,
            y: 195

            },


            {xtype: 'radiogroup', // x: 150, y: 130,
            //                          columns: 2,
            width: 600,
            id: 'rblTipoActivaciones',
            cls: "texto",
            colspan: 4,
            itemId: 'chkTipoActivaciones',
            items: [
                { xtype: 'radiofield', boxLabel: 'Activa ', id: 'rbTipCer1', checked: false, name: 'rblTipoActivaciones', inputValue: 'Activa', cls: "texto" },
                { xtype: 'radiofield', boxLabel: 'No Activa ', id: 'rbTipCer2', checked: false, name: 'rblTipoActivaciones', inputValue: 'No Activa', cls: "texto" }
            ],

            listeners:
            {
                change: function (field, newValue, oldValue) {
                    switch (field.lastValue.rblTipoActivaciones) {
                        case 'Activa':
                        var Activa = 1;

                        case 'No Activa':
                        var Activa = 0;
                        break;
                        }
                    }
            }
            },
            ]
    })
    this.callParent(arguments);
}
});
