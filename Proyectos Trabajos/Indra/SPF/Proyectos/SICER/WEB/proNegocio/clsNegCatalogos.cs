using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proDatos;
using proEntidad;

namespace proNegocio
{
    public class clsNegCatalogos
    {
        public static List<spuConsultarEstado_Result> catalogoEstados()
        {
            return clsDatCatalogos.catalogoEstados();
        }

        public static List<spuConsultarMunicipio_Result> catalogoMunicipios(int idEstado)
        {
            return clsDatCatalogos.catalogoMunicipios(idEstado);
        }

        public static List<spuConsultarEntidadCertificadora_Result> catalogoEntidadCertificadora()
        {
            return clsDatCatalogos.catalogoEntidadCertificadora();
        }

        public static List<spuConsultarEvaluador_Result> catalogoEvaluador()
        {
            return clsDatCatalogos.catalogoEvaluador();
        }

        public static List<spuConsultarNivelSeguridad_Result> catalogoNivelSeguridad()
        {
            return clsDatCatalogos.catalogoNivelSeguridad();
        }
        
        public static List<spuConsultarDependenciaExterna_Result> catalogoDependenciaExterna(int nivelSeguridad)
        {
            return clsDatCatalogos.catalogoDependenciaExterna(nivelSeguridad);
        }

        public static List<spuConsultarInstitucionExterna_Result> catalogoInstitucionExterna(int dependenciaExterna)
        {
            return clsDatCatalogos.catalogoInstitucionExterna(dependenciaExterna);
        }

        public static List<spuConsultarComboCertificacion_Result> catalogoCertificaciones()
        {
            return clsDatCatalogos.catalogoCertificaciones();
        }/*
        public static List<spuConsultarCertificaciones_Result> catalogoCertificacionesTree()
        {
            return clsDatCatalogos.catalogoCertificacionesTree();
        }*/

        public static List<spuConsultarInstitucionAplicaExamen_Result> catalogoInstitucionAplicaExamen()
        {
            return clsDatCatalogos.catalogoInstitucionAplicaExamen();
        }

        public static List<spuConsultarLugarAplicacion_Result> catalogoLugarAplicacion()
        {
            return clsDatCatalogos.catalogoLugarAplicacion();
        }

        public static List<spuConsultarEntidadEvaluadora_Result> catalogoEntidadEvaluadora()
        {
            return clsDatCatalogos.catalogoEntidadEvaluadora();
        }

        public static List<spuConsultarTemasCertificacion_Result> consultarTemas(int idCertificacion) {
            return clsDatCatalogos.consultarTemas(idCertificacion);
        }

        public static List<spuConsultarFuncionesCertificacion_Result> consultarFunciones(int idCertificacion)
        {
            return clsDatCatalogos.consultarFunciones(idCertificacion);
        }


        public static List<spuConsultarPreguntasCertificacion_Result> consultarPreguntas(int idCertificacion)
        {
            return clsDatCatalogos.consultarPreguntas(idCertificacion);
        }


        public static List<spuConsultarRespuestasCertificacion_Result> consultarRespuestas(int idCertificacion)
        {
            return clsDatCatalogos.consultarRespuestas(idCertificacion);
        }


        public static List<spuConsultarIntegrantes_Result> consultarIntegrantes(string empPaterno, string empMaterno, string empNombre, string empCURP, string empRFC, string empActivo, int empNumero)
        {
            return clsDatCatalogos.consultarIntegrantes(empPaterno, empMaterno, empNombre, empCURP, empRFC, empActivo, empNumero);
        }


        public static List<clsEntDatosIntegrante> consultarDatosIntegrantes(string idEmpleado) 
        {
            
            if (idEmpleado != null && idEmpleado!="")
            {
                Guid idEmp = new Guid(idEmpleado);

                List <clsEntDatosIntegrante> lista = new List<clsEntDatosIntegrante>();
                List<spuConsultarDatosIntegrante_Result> lst = clsDatCatalogos.consultarDatosIntegrantes(idEmp);
                foreach (spuConsultarDatosIntegrante_Result elem in lst)
                {
                    lista.Add(new clsEntDatosIntegrante 
                    {
                        idEmpleado = idEmpleado,
                        //idRegistro = elem.idRegistro,
                        paterno = elem.perPaterno,
                        materno = elem.perMaterno,
                        nombre = elem.perNombre,
                        numempleado = Convert.ToString(elem.empNumero),
                        curp = elem.perCurp,
                        rfc = elem.perRFC,
                        cuip = elem.EmpCuip,
                        loc = Convert.ToString(elem.EmpLoc),
                        idGrado = elem.idJerarquia,
                        gradoDesc = elem.jerDescripcion,
                        idCargo = elem.idPuesto,
                        cargoDesc = elem.pueDescripcion,
                        edad = elem.edad,
                        escolaridad = elem.neDescripcion,
                        telcasa = elem.telNumero,
                        emailpersonal = elem.perEmail,
                        telcelular = elem.telefonoCelular,
                        emaillaboral = elem.perEmail,
                        tellaboral = elem.extension,

                        calle = elem.domCalle,
                        numext = elem.domNumeroExterior,
                        numint = elem.domNumeroInterior,
                        colonia = elem.aseDescripcion,
                        codpostal = elem.aseCodigoPostal,
                        idestado = Convert.ToInt32(elem.idEstado),
                        idDelMunicipio = Convert.ToInt32(elem.idMunicipio),

                        //fechaingreso = elem.fechaingreso,
                        //idnivelSeg = elem.idnivelSeg,
                        //iddependendiaexterna = elem.iddependendiaexterna,
                        //idinstitucionexterna = elem.idinstitucionexterna
                    });
                }


                return lista;
            }
            else {
                return new List<clsEntDatosIntegrante>();
            }
        }


        public static List<spuConsultarZonas_Result> consultarZona()
        {
            return clsDatCatalogos.consultarZona();
        }


        public static List<spuConsultarServicios_Result> catalogoServicio(int idZona)
        {
            return clsDatCatalogos.catalogoServicio(idZona);
        }


        public static List<spuConsultarFirmasCertificado_Result> consultarFirmasCertificado()
        {
            return clsDatCatalogos.consultarFirmasCertificado();
        }


        public static string consultaContenidoCertificado()
        {
            return clsDatCatalogos.consultarContenidoCertificado();
        }


        public static List<spuConsultarDatosCorreo_Result> consultarDatosCorreo()
        {
            return clsDatCatalogos.consultarDatosCorreo();
        }


        public static Boolean consultarCalificacion(int idCertificacion)
        {
            return clsDatCatalogos.consultarCalificacion(idCertificacion);
        }
    }
}
