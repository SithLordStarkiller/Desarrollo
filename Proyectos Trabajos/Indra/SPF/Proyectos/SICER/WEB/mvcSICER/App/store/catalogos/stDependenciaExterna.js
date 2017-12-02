Ext.define('app.store.catalogos.stDependenciaExterna', {
    extend: 'Ext.data.Store',
    model: 'app.model.catalogos.mdDependenciaExterna',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/catalogoDependenciaExterna', reader: { type: 'json'} })
});

