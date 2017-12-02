Ext.define('app.store.catalogos.stServicio', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdServicio',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoServicio', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});