Ext.define('app.model.Examen.mdCertificacionPDF', {
    extend: 'Ext.data.Model',
    fields: [

                { name: 'idCertificacion', type: 'int' },
                { name: 'cerNombre', type: 'string' },
                { name: 'cerDescripcion', type: 'string' },
                { name: 'cerSiglas', type: 'string' },
                { name: 'cerPrimeraValidez', type: 'int' },
                { name: 'cerRenovacionValidez', type: 'int' },
                { name: 'idEntidadEvaluadora', type: 'int' },
                { name: 'eeDescripcion', type: 'string' },
                { name: 'idEntidadCertificadora', type: 'int' },
                { name: 'ecDescripcion', type: 'string' }


        ]
});