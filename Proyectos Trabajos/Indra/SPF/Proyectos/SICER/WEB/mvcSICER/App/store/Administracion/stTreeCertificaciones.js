Ext.define('app.store.Administracion.stTreeCertificaciones', {
    extend: 'Ext.data.TreeStore',
    autoLoad: true,
    root: {
        //id: "menuSistema",
        draggable: false,
        expanded: true
    },
 
    /*proxy: {
    type: 'ajax', url: '/Sistema/catalogoMenu'
    }*/

    proxy: new Ext.data.HttpProxy({ url: '../Sistema/menuCertificaciones', reader: { type: 'json'} })

    ,listeners: {        
     beforeload: function (store, operation, eOpts) {            
          if(store.isLoading()) return false;        
     }    
}
});


