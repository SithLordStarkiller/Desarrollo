Ext.define('app.store.catalogos.stEntidadEvaluadora', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdEntidadEvaluadora',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoEntidadEvaluadora', reader: { type: 'json'} })
});

