Ext.define('app.store.Examen.stCertificacionPDF', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdCertificacionPDF',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Home/buscarCertificacion', reader: { type: 'json'} })
});