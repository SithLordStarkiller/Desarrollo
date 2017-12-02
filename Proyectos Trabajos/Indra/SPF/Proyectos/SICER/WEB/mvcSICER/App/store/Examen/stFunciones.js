Ext.define('app.store.Examen.stFunciones', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdFuncion',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Sistema/consultaFuncionesTema', reader: { type: 'json'} })
});

