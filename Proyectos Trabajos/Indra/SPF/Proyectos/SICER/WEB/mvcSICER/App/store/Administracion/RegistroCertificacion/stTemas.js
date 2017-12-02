Ext.define('app.store.Administracion.RegistroCertificacion.stTemas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroCertificacion.mdTemas',
    autoLoad: true
    , proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarTemas', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});