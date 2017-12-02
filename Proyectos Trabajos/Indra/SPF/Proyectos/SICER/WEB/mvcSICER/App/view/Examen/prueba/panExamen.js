Ext.define('app.view.Examen.prueba.panExamen', {
    extend: 'Ext.Panel',
    alias: 'widget.panelExamen',

    //title: 'SELECCIONAR ACCIÓN REALIZAR',
    layout: 'fit',
    //bodyPadding: 5,
    width: '90%',
    //height: 700,
    initComponent: function () {
        this.items = [
        {
            xtype: 'tabpanel',
            id: 'tabpanFuncionesantes',
            //title: 'Tab Panel'+this.getItemId(),
            //tabBarHeaderPosition: 1,
            tabRotation: 0,
            tabPosition: 'left',
            hidden:true,
            // height:'600',

            tabBar: {
                flex: 1

            }//,
            /*header: {
            title: {
            text: 'Tab Panel',
            flex: 0
            }
            },
            items: []*/
        }
        ],
        this.callParent();

    } //Llave de funcion init
});
