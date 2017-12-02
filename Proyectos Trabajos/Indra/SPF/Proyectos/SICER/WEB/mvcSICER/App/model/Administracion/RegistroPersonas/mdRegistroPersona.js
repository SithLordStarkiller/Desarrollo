Ext.define('app.model.Administracion.RegistroPersonas.mdRegistroPersona', {
    extend: 'Ext.data.Model',
    fields: [
    //Registro de Personas
                {name: 'idEmpleado', type: 'string' },
                { name: 'idRegistro', type: 'int' },
                { name: 'paterno', type: 'string' },
                { name: 'materno', type: 'string' },
                { name: 'nombre', type: 'string' },
                { name: 'numempleado', type: 'string' },
                { name: 'curp', type: 'string' },
                { name: 'rfc', type: 'string' },
                { name: 'cuip', type: 'string' },
                { name: 'loc', type: 'string' },
                { name: 'idGrado', type: 'int' },
                { name: 'gradoDesc', type: 'string' },
                { name: 'idCargo', type: 'int' },
                { name: 'cargoDesc', type: 'string' },
                { name: 'edad', type: 'int' },
                { name: 'escolaridad', type: 'string' },
                { name: 'telcasa', type: 'string' },
                { name: 'emailpersonal', type: 'string' },
                { name: 'telcelular', type: 'string' },
                { name: 'emaillaboral', type: 'string' },
                { name: 'tellaboral', type: 'string' },

                { name: 'calle', type: 'string' },
                { name: 'numext', type: 'string' },
                { name: 'numint', type: 'string' },
                { name: 'colonia', type: 'string' },
                { name: 'codpostal', type: 'string' },
                { name: 'idestado', type: 'int' },
    //{ name: 'idDelMunicipio', type: 'int' },
                {name: 'dfFechaIngreso', type: 'string' },
                { name: 'dfFechaNacimiento', type: 'string' },
                { name: 'participante', type: 'string' }
                , { name: 'foto', type: 'string' }
               , { name: 'idMunicipio', type: 'int' }
        ]
});