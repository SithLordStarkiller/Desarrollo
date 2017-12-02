Ext.define('app.store.Examen.stCertificacionesRegistro', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdCertificacionesRegistro',
    autoLoad: true
    , proxy: new Ext.data.HttpProxy({ url: '../Sistema/consultaCertificacionesRegistro', reader: { type: 'json'} })
});

