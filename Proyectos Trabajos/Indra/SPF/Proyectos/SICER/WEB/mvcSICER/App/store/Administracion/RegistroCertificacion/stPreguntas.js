Ext.define('app.store.Administracion.RegistroCertificacion.stPreguntas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroCertificacion.mdPregunta',
    autoLoad: true
     , proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarPreguntas', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});