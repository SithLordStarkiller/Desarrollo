Ext.define('app.store.catalogos.stIntegrantes', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdIntegrantes',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarIntegrantes', reader: { type: 'json'} })
});