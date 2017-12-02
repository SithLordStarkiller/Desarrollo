Ext.define('app.store.Administracion.RegistroPersonas.stBusqueda', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroPersonas.mdBusqueda',
    autoLoad: false,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Home/Busqueda', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});