Ext.define('app.store.catalogos.stEntidadCertificadora', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdEntidadCertificadora',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoEntidadCertificadora', reader: { type: 'json'} })
});

