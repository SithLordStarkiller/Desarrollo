Ext.define('app.store.Examen.stTemas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdTema',
    autoLoad: true,
     proxy: new Ext.data.HttpProxy({ url: '../Sistema/consultaTemasdeCertificacion', reader: { type: 'json'} })
});

