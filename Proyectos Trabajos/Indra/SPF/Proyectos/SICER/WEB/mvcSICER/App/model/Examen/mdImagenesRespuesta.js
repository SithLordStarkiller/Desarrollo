Ext.define('app.model.Examen.mdImagenesRespuesta', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idRespuesta', type: 'int' },
                { name: 'idPregunta', type: 'int' },
                { name: 'idFuncion', type: 'string' },
                { name: 'resDescripcion', type: 'string' },
                { name: 'idTema', type: 'string' },
                { name: 'resTipoArchivo', type: 'string' },
                { name: 'resNombreArchivo', type: 'string' },
                { name: 'imagen', type: 'string' },
                { name: 'resCorrecta', type: 'string' }

        ]
});
