Ext.define('app.model.Examen.mdCertificacionesRegistro', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idCertificacionRegistro', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'cerNombre', type: 'string' },
                { name: 'cerDescripcion', type: 'string' },
                { name: 'cerSiglas', type: 'string' },
                { name: 'idRegistro', type: 'int' },
                { name: 'cerTiempoIntento', type: 'int' },
                { name: 'crFechaExamen', type: 'string' },
                { name: 'crHora', type: 'string' },
                { name: 'eeDescripcion', type: 'string' },
                { name: 'idLugarAplica', type: 'int' },
                { name: 'laDescripcion', type: 'string' },
                { name: 'idEvaluador', type: 'int' },
                { name: 'evaDescripcion', type: 'string' }

        ]
});