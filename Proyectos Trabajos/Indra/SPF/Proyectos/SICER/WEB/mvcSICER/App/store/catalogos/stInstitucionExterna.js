

Ext.define('app.store.catalogos.stInstitucionExterna', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdInstitucionExterna',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoInstitucionExterna', reader: { type: 'json'} })
});

