Ext.define('app.store.Administracion.RegistroPersonas.stCertificacionRegistro', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroPersonas.mdCertificacionRegistro',
    autoLoad: false,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Home/ConsultaRegistrosCertificacion', reader: { type: 'json', root: 'd'} })
});

