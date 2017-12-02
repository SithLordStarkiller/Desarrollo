Ext.define('app.model.Examen.mdCalificacion', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idTema', type: 'int' },
                { name: 'preguntasCorrectas', type: 'int' },
                { name: 'numero', type: 'int' },
                { name: 'alerta', type: 'string' }
        ]
});