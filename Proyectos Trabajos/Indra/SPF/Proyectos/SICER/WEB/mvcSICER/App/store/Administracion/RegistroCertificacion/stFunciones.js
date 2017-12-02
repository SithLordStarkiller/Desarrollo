Ext.define('app.store.Administracion.RegistroCertificacion.stFunciones', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroCertificacion.mdFuncion',
    autoLoad: true
     , proxy: new Ext.data.HttpProxy({ url: '../Catalogos/consultarFunciones', reader: { type: 'json', root: 'lstResponse', totalProperty: 'total'} })
});