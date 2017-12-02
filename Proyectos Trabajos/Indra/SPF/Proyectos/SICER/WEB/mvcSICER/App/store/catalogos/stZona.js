Ext.define('app.store.catalogos.stZona', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdZona',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoZona', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});