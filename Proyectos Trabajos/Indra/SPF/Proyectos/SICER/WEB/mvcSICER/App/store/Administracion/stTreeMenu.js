Ext.define('app.store.Administracion.stTreeMenu', {
    extend: 'Ext.data.TreeStore',
    autoLoad: true,
    root: {
        //id: "menuSistema",
        draggable: false,
        expanded: true
    },

    proxy: {
        type: 'ajax', url: '/Sistema/catalogoMenu'
    }

    //proxy: new Ext.data.HttpProxy({ url: '../Sistema/catalogoMenu', reader: { type: 'json'} })
    /*
     , listeners: {
         beforeload: function (store, operation, eOpts) {
             if (store.isLoading()) return false;
         }
     }*/
});


