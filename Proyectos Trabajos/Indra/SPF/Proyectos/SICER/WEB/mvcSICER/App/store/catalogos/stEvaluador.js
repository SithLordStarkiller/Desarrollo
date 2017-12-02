Ext.define('app.store.catalogos.stEvaluador', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdEvaluador',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoEvaluador', reader: { type: 'json'} })
});

