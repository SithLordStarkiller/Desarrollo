Ext.define('app.store.catalogos.stInstAplicaExamen', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdInstAplicaExamen',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoInstitucionAplicaExamen', reader: { type: 'json'} })
});

