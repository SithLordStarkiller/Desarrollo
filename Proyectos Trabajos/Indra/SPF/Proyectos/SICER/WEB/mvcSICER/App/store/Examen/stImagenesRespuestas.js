Ext.define('app.store.Examen.stImagenesRespuestas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdRespuesta',
    autoLoad: true,
    proxy: new Ext.data.HttpProxy({ url: '../Sistema/consultaImagenesRespuestas', reader: { type: 'json'} })
});

