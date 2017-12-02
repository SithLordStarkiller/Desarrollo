Ext.define('app.view.Administracion.RegistroCertificacion.panFuncionesCertificaciones', {
    extend: 'Ext.Panel',
    alias: 'widget.panelFuncionesCertificaciones',
    id: 'panelFuncionesCertificaciones',
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    //bodyPadding: '10 10 10 10',
    margin: '40 0 0 0',
    initComponent: function () {
        Ext.apply(this, {
            title: '<span style="font-size: 160%;margin-top:20px;">Funciones</span>',
            layout: {
                type: 'vbox',
                align: 'center'
                //  columns: 2
            },
            items: [
                 { xtype: 'gridFunciones' }
            ]
        });
        this.callParent(arguments);
    }
});