﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.AccesoDatos.Personas;
using Universidad.Entidades;

namespace Universidad.LogicaNegocios.Personas
{
    public class Persona
    {
        public PER_PERSONAS InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona, DIR_DIRECCIONES personaDirecciones)
        {
            var taskTelefonos = Task<PER_CAT_TELEFONOS>.Factory.StartNew(() => new Telefonos().InsertaTelefonosLinq(personaTelefonos));
            var taskMediosElectronicos = Task<PER_MEDIOS_ELECTRONICOS>.Factory.StartNew(() => new AccesoDatos.Personas.MediosElectronicos().InsertaMediosElectronicosLinq(personaMediosElectronicos));
            var taskFotografia = Task<PER_FOTOGRAFIA>.Factory.StartNew(() => new Fotografias().InsertaFotografiaLinq(personaFotografia));
            var taskDirecciones = Task<DIR_DIRECCIONES>.Factory.StartNew(() => new Direcciones().InsertaDireccionesLinq(personaDirecciones));

            var resultados = new Task[] { taskTelefonos, taskMediosElectronicos, taskFotografia, taskDirecciones };

            Task.WaitAll(resultados);

            personaTelefonos = ((Task<PER_CAT_TELEFONOS>)resultados[0]).Result;
            personaMediosElectronicos = ((Task<PER_MEDIOS_ELECTRONICOS>)resultados[1]).Result;
            personaFotografia = ((Task<PER_FOTOGRAFIA>)resultados[2]).Result;
            personaDirecciones = ((Task<DIR_DIRECCIONES>)resultados[3]).Result;

            persona.IDDIRECCION = personaDirecciones.IDDIRECCION;
            persona.IDFOTO = personaFotografia.IDFOTO;
            persona.ID_MEDIOS_ELECTRONICOS = personaMediosElectronicos.ID_MEDIOS_ELECTRONICOS;
            persona.ID_TELEFONOS = personaTelefonos.ID_TELEFONOS;
            var datos = (persona.A_PATERNO.ToCharArray())[0] + (persona.A_MATERNO.ToCharArray())[0] + (persona.NOMBRE.ToCharArray())[0] + (persona.SEXO.ToCharArray())[0];
            var fRegistro = DateTime.Now.Day.ToString("D") + DateTime.Now.Month.ToString("D") +
                            DateTime.Now.Year.ToString("D");
            var cve = datos + fRegistro + persona.ID_TIPO_PERSONA;
            persona.NOMBRE_COMPLETO = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
            persona.ID_PER_LINKID = cve;
            persona.FECHAINGRESO = DateTime.Now;

            persona = new AccesoDatos.Personas.Personas().InsertaPersonaLinq(persona);

            return persona;
        }
    }
}