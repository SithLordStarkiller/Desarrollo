Ext.define('app.store.Administracion.RegistroPersonas.stConsultaUbicacion', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroPersonas.mdConsultaUbicacion',
    autoLoad: true,
    pruneModifiedRecords: true,
    proxy: new Ext.data.HttpProxy({ url: '../Home/ConsultaUbicacionInterno', reader: { type: 'json', root: 'd'} })

});