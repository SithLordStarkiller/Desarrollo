Ext.define('app.store.catalogos.stDatosCorreo', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdDatosCorreo',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarDatosCorreo', reader: { type: 'json'} })
});