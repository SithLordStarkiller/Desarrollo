Ext.define('app.store.catalogos.stInstalacion', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdInstalacion',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoInstalacion', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});