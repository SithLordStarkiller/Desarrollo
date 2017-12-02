Ext.define('app.store.Examen.stRespuestas', {
    extend: 'Ext.data.Store',
    model: 'app.model.Examen.mdRespuesta',
    autoLoad: true
    , proxy: new Ext.data.HttpProxy({ url: 'Sistema/consultaRespuestasTema', reader: { type: 'json'} })
   /* ,   proxy : {
            type : 'ajax',
            url: 'Sistema/consultaRespuestasTema',
            reader : {
                type : 'json'//,
          //      rootProperty : 'Data'
            }
        }*/

});

