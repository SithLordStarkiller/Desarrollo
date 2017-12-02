Ext.define('app.model.Examen.mdEvaluacionRespuesta', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idTema', type: 'int' },
                { name: 'idFuncion', type: 'int' },
                { name: 'idPregunta', type: 'int' },
                { name: 'idRespuesta', type: 'int' },
               // { name: 'idRegistro', type: 'int' },
               // { name: 'idCertificacion', type: 'int' },
               // { name: 'idCertificacionRegistro', type: 'int' }

        ]
});