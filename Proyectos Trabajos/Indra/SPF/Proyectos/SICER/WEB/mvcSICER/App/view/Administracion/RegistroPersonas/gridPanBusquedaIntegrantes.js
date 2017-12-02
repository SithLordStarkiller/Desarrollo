Ext.define('app.view.Administracion.RegistroPersonas.gridPanBusquedaIntegrantes', {//app.view.Administracion.RegistroPersonas.RegistroPersonasView
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridPanBusquedaIntegrantes',
    id: 'pnlBusquedaI',
    initComponent: function () {

        Ext.apply(this, {

            store: 'catalogos.stIntegrantes',
            collapsible: false,
            loadMask: true,
            selModel:
            {
                pruneRemoved: false
            },
            multiSelect: true,
            viewConfig:
            {
                trackOver: false
            },
            
            columns: [
           {
               text: 'No.',
               xtype: 'rownumberer',
               width: '5%'
           },
//           {
//               text: 'Selección',
//               hideable: false,
//               renderer: function renderRadio() {
//                   var a = '<input type= "radio" name="radiogroup" style="margin-left:10px;"/>';
//                   return a;
//               },
//               editor: {
//                   xtype: 'radio',
//                   id: 'rbSelec'
//               },
//               align: 'center'
//           },
           {
               text: "Apellido Paterno",
               dataIndex: 'empPaterno',
               hidden: false,
               align: 'center',
               sortable: true
           },
            {
                text: "Apellido Materno",
                dataIndex: 'empMaterno',
                hidden: false,
                align: 'center',
                sortable: true
            },
            {
                text: "Nombre(s)",
                dataIndex: 'empNombre',
                width: '20%',
                hidden: false,
                align: 'center',
                sortable: true
            },
            {
                text: "CURP",
                dataIndex: 'empCURP',
                width: '15%',
                align: 'left',
                hidden: false,
                sortable: true
            },
            {
                text: "RFC",
                dataIndex: 'empRFC',
                hideable: false,
                align: 'left',
                hidden: false,
                sortable: true
            },
            {
                text: "Activo",
                dataIndex: 'empActivo',
                width: '5%',
                align: 'center',
                hidden: false,
                sortable: true
            },
            {
                text: "No. Emp.",
                dataIndex: 'empNumero',
                hidden: false,
                align: 'center',
                sortable: true
            }
            ]
        })
        this.callParent(arguments);
    }
});