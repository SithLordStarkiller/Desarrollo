Ext.define('app.store.Administracion.RegistroPersonas.stEliminar', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroPersonas.mdEliminar',
    autoLoad: true//,
    //    pruneModifiedRecords: true,
    //    proxy: new Ext.data.HttpProxy({ url: '../Home/ConsultaG', reader: { type: 'json', root: 'd'} })

});