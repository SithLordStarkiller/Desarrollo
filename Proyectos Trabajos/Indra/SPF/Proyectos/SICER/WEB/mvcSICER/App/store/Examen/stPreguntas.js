Ext.define('app.store.Examen.stPreguntas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdPregunta',
    autoLoad: true
    , proxy: new Ext.data.HttpProxy({ url: '../Sistema/consultaPreguntasTema', reader: { type: 'json'} })
});

