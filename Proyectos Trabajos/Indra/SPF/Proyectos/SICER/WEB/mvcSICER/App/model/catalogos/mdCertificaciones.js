Ext.define('app.model.catalogos.mdCertificaciones', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idCertificacion', type: 'int' },
                { name: 'cerNombre', type: 'string' },
                { name: 'cerDescripcion', type: 'string' },
                { name: 'cerSiglas', type: 'string' },
                { name: 'certificacion', type: 'string' },
                { name: 'cerFechaAlta', type: 'date' },
                { name: 'cerFechaBaja', type: 'date' },
                { name: 'cerPrimeraValidez', type: 'int' },
                { name: 'cerRenovacionValidez', type: 'int' },
                { name: 'cerPreguntas', type: 'int' },
                { name: 'cerPreguntasCorrectas', type: 'int' },
                { name: 'cerTiempoIntento', type: 'int' },
                { name: 'idEntidadEvaluadora', type: 'int' },
                { name: 'idEntidadCertificadora', type: 'int' },
                { name: 'idUsuario', type: 'int' },
                { name: 'cerFechaModificacion', type: 'date' },
                { name: 'cerVigente', type: 'int' }
        ]
});