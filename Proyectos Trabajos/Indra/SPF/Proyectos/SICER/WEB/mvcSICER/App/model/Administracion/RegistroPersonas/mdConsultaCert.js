Ext.define('app.model.Administracion.RegistroPersonas.mdConsultaCert', {
    extend: 'Ext.data.Model',
    fields: [
    //Registro de Certificacion
                {name: 'idCertificacionRegistro', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'idRegistro', type: 'int' },
                { name: 'crFechaExamen', type: 'string' },
                { name: 'crHora', type: 'string' },
                { name: 'idLugarAplica', type: 'int' },
                { name: 'idEvaluador', type: 'int' },
                { name: 'crFechaModificacion', type: 'date' },
                { name: 'crVigente', type: 'int' },
                { name: 'idUsuario', type: 'int' },
                { name: 'idInstitucionAplicaExamen', type: 'int' },


    //Resultado Certificacion
    /*  {name: 'idResultado', type: 'int' },
    { name: 'resCalificacion', type: 'float' },
    { name: 'resAprobado', type: 'string' },
    { name: 'resFechaResultado', type: 'date' },
    { name: 'resFechaModificacion', type: 'date' },
    { name: 'resVigente', type: 'bool' },
    { name: 'resIdUsuario', type: 'int' },

    { name: 'idNivelSeguridad', type: 'int' },
    { name: 'iddependendiaexterna', type: 'int' },
    { name: 'idinstitucionexterna', type: 'int' },




    { name: 'participante', type: 'string' },
    { name: 'actualiza', type: 'int' }*/

                  {name: 'idResultado', type: 'int' },
                { name: 'resCalificacion', type: 'float' },
                { name: 'resAprobado', type: 'string' },
                { name: 'resFechaResultado', type: 'date' },
                { name: 'resFechaModificacion', type: 'date' },
                { name: 'resVigente', type: 'bool' },
                { name: 'resIdUsuario', type: 'int' },
                { name: 'idNivelSeguridad', type: 'int' },
                { name: 'iddependendiaexterna', type: 'int' },
                { name: 'idinstitucionexterna', type: 'int' },
                { name: 'participante', type: 'string' },
                { name: 'actualiza', type: 'int' }


        ]
});