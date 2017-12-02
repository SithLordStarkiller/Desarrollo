Ext.define('app.store.catalogos.stEstados', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdEstados',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoEstados', reader: { type: 'json'} })
});

