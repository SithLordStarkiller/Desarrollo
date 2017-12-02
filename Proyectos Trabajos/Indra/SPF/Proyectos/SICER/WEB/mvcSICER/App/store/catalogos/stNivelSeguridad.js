Ext.define('app.store.catalogos.stNivelSeguridad', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdNivelSeguridad',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoNivelSeguridad', reader: { type: 'json'} })
});

