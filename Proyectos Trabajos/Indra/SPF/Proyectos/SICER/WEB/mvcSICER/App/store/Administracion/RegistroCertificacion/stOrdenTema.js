Ext.define('app.store.Administracion.RegistroCertificacion.stOrdenTema', {
    extend: 'Ext.data.Store',
    model: 'app.model.Administracion.RegistroCertificacion.mdOrdenTema',

    data: [
        { "idTema": "CONOCIMIENTO", "ordenTema": "1" },
        { "idTema": "REACTIVOS DE CASOS PRACTICOS", "ordenTema": "2" },
        { "idTema": "REACTIVOS DE FORMATOS", "ordenTema": "3" },
        { "idTema": "REACTIVOS", "ordenTema": "4" }

    ]
});