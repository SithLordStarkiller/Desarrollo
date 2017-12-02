Ext.define('app.store.Administracion.RegistroCertificacion.stRespuestas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroCertificacion.mdRespuesta',
    autoLoad: true
     , proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarRespuestas', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});