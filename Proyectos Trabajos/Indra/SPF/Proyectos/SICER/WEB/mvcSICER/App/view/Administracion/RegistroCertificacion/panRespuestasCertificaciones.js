Ext.define('app.view.Administracion.RegistroCertificacion.panRespuestasCertificaciones', {
    extend: 'Ext.Panel',
    alias: 'widget.panelRespuestasCertificaciones',
    id:'panelRespuestasCertificaciones',
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    //bodyPadding: '10 10 10 10',
    margin: '40 0 40 0',
    initComponent: function () {
        Ext.apply(this, {
            title: '<span style="font-size: 160%;margin-top:20px;">Respuestas</span>',
            layout: {
                type: 'vbox',
                align: 'center'
                //  columns: 2
            },
            items: [
                { xtype: 'gridRespuestas' }
            ]
        });
        this.callParent(arguments);
    }
});