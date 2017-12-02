Ext.define('app.store.Administracion.RegistroPersonas.stRegistroPersona', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroPersonas.mdRegistroPersona',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarDatosIntegrantes', reader: { type: 'json'} })
});

