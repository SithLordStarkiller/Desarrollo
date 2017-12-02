Ext.define('app.model.Examen.mdPregunta', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idPregunta', type: 'int' },
                { name: 'idFuncion', type: 'int' },
                { name: 'idTema', type: 'int' },
                { name: 'preDescripcion', type: 'string' },
                { name: 'preObligatoria', type: 'boolean' },
                { name: 'preTipoArchivo', type: 'string' },
                { name: 'preNombreArchivo', type: 'string' },
                 { name: 'imagen', type: 'string' }
        ]
});