Ext.define('app.model.Examen.mdRespuesta', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idPregunta', type: 'int' },
                { name: 'idRespuesta', type: 'int' },
                { name: 'idFuncion', type: 'int' },
                { name: 'idTema', type: 'int' },
                { name: 'resDescripcion', type: 'string' },
                { name: 'resTipoArchivo', type: 'string' },
                { name: 'resNombreArchivo', type: 'string' },
                { name: 'imagen', type: 'string' },
                { name: 'resCorrecta', type: 'boolean' }
        ]
});