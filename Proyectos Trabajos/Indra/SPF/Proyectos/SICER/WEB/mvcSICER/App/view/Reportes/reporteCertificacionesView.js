Ext.define('app.view.Reportes.reporteCertificacionesView', {
    extend: 'Ext.Panel',
    alias: 'widget.reporteCertificaciones',
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    closable: true,
    bodyPadding: '10 10 10 10',
    initComponent: function () {
        Ext.apply(this,
        {
            collapsible: true,
            title: 'Reporte Certificaciones',
            layout: {
                type: 'vbox',
                align: 'center'
            },
            items: [
            { xtype: 'filtrosReporteCertificaciones', height: 150 },
            { xtype: 'reporteViewr', flex: 4.7, id: 'idReporteCertificaciones' }
            ]
        });
        this.callParent(arguments);
    }
});