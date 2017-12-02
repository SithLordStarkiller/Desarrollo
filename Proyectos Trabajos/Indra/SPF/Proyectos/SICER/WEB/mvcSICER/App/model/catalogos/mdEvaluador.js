Ext.define('app.model.catalogos.mdEvaluador', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idEvaluador', type: 'int' },
                { name: 'evaDescripcion', type: 'string' },
                { name: 'evaTipo', type: 'string' }
        ]
});