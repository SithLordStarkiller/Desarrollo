Ext.define('app.view.Administracion.RegistroCertificacion.panTemasCertificaciones', {
    extend: 'Ext.Panel',
    alias: 'widget.panelTemasCertificaciones',
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    //bodyPadding: '10 10 10 10',
    margin: '40 0 0 0',
    initComponent: function () {
        Ext.apply(this, {
            title: '<span style="font-size: 160%;margin-top:20px;">Temas</span>',
            layout: {
                type: 'vbox',
                align: 'center'
                //  columns: 2
            },
            items: [
                    /*{
                    xtype: 'fieldset',
                    width: '100%',
                    padding: '20 20 20 20',
                    items: [
                        {
                            xtype: 'label',
                            id: 'lblTemas1',
                            html: '<h4 style="margin-left: 5px;margin-right: 5px;font-size: 160%;color: #4986D5;" width:=>Temas</h4>'
                            //text: 'Temas',
                            //colspan: 3
                        },

                       
                    ]
                    },*/
                 { xtype: 'gridTemas' }
            ]
        });
        this.callParent(arguments);
    }
});