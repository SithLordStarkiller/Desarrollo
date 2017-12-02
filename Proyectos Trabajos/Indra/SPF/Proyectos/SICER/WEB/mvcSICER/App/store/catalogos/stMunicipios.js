Ext.define('app.store.catalogos.stMunicipios', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdMunicipios',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoMunicipios', reader: { type: 'json'} })
});

