Ext.define('app.view.Administracion.RegistroPersonas.gridpanBusqueda', {//app.view.Administracion.RegistroPersonas.RegistroPersonasView
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridpanBusquedaParticipantes',
    id: 'pnlBusqueda',


    initComponent: function () {

        var myStoreI = Ext.create('app.store.Administracion.RegistroPersonas.stBusqueda');

        // var stRP = Ext.create('Administracion.RegistroPersonas.stRegistroPersona');

        var pagerI = new Ext.PagingToolbar({
            store: myStoreI,
            id: 'paginadorI',
            displayInfo: true,
            displayMsg: '{0} - {1} de {2} Registros',
            emptyMsg: myStoreI.count() == 0 ? 'Sin registros' : 'Buscando tipo de valor  por favor espere...',
            pageSize: 25
        });


        Ext.apply(this, {


            bbar: pagerI,
            store: myStoreI,
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
            features: [{
                ftype: 'grouping',
                hideGroupedHeader: false
            }],
            verticalScroller: {
                variableRowHeight: true
            },

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

           {
               text: "Paterno",
               dataIndex: 'paterno',
               width: '20%',
               hidden: false,
               align: 'left',
               sortable: true
           },
           {
               text: "Materno",
               dataIndex: 'materno',
               width: '20%',
               hidden: false,
               align: 'left',
               sortable: true
           },

            {
                text: "Nombre",
                dataIndex: 'nombre',
                width: '29%',
                hidden: false,
                align: 'left',
                sortable: true
            },
            {
                text: "CURP",
                dataIndex: 'CURP', //'curp',
                width: '25%',
                align: 'left',
                hidden: false,
                sortable: true
            }

            ]
        })
        this.callParent(arguments);
    }
});
