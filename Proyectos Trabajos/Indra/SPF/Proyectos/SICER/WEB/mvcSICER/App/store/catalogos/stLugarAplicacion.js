

Ext.define('app.store.catalogos.stLugarAplicacion', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdLugarAplicacion',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoLugarAplicacion', reader: { type: 'json'} })
});

