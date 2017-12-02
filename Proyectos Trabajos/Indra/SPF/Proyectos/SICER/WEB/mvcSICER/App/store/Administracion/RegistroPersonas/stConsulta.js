Ext.define('app.store.Administracion.RegistroPersonas.stConsulta', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroPersonas.mdConsulta',
    //autoLoad: true,
    autoLoad: false,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Home/Consulta', reader: { type: 'json', totalProperty: 'total'} })
});