Ext.define('app.store.catalogos.stCertificaciones', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdCertificaciones',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoCertificaciones', reader: { type: 'json'} })
});

