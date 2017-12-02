Ext.define('app.model.Examen.mdFuncion', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idTemaFuncion', type: 'int' },
                { name: 'idFuncion', type: 'int' },
                { name: 'idTema', type: 'int' },
                { name: 'funNombre', type: 'string' },
                { name: 'funAleatorias', type: 'int' },
                { name: 'funCorrectas', type: 'int' },
                { name: 'funTiempo', type: 'int' },
                { name: 'funOrden', type: 'int' },
                { name: 'funCodigo', type: 'int' }
        ]
});