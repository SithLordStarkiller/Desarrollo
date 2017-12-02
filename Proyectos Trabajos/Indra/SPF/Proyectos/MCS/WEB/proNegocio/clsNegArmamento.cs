using System;
using System.Collections.Generic;
using System.Data;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;


namespace SICOGUA.Negocio
{
    public class clsNegArmamento
    {
        //ya
        public static List<clsEntEquipoCatalogo> consultaCatalogoEquipo(clsEntInstalacion objInstalacion,clsEntSesion objSesion)
        {
            DataSet dsEquipo=new DataSet();
            clsDatInstalacion.consultaCatalogoEquipo(objInstalacion, ref dsEquipo, objSesion);
            List<clsEntEquipoCatalogo> lst = new List<clsEntEquipoCatalogo>();

            foreach (DataRow objRes in dsEquipo.Tables[0].Rows)
            {
                lst.Add(
                new clsEntEquipoCatalogo
                {
                    idEquipo = Convert.ToInt32(objRes["idEquipo"])
                    ,
                    equDescripcion = objRes["equDescripcion"].ToString()
                    ,
                    ieCantidad = Convert.ToDecimal(objRes["ieCantidad"])
                }
                );
            }

            return lst;
        }
        //ya
        public static List<clsEntEquipoCatalogo> consultaInventario(clsEntInstalacion objInstalacion, clsEntSesion objSesion)
        {
            DataSet dsEquipo = new DataSet();
            clsDatInstalacion.consultaInventario(objInstalacion,ref dsEquipo, objSesion);
            List<clsEntEquipoCatalogo> lst = new List<clsEntEquipoCatalogo>();
             foreach (DataRow objRes in dsEquipo.Tables[0].Rows)
            {
                lst.Add(
                new clsEntEquipoCatalogo
                {
                    ieFechaInicio =Convert.ToDateTime(objRes["ieFechaInicio"]).ToShortDateString()=="01/01/1900"?string.Empty:Convert.ToDateTime(objRes["ieFechaInicio"]).ToShortDateString()
                    ,
                    ieFechaFin =Convert.ToDateTime(objRes["ieFechaFin"]).ToShortDateString()=="01/01/1900"?string.Empty:Convert.ToDateTime(objRes["ieFechaFin"]).ToShortDateString()
                    ,
                    idInstalacionEquipo = Convert.ToInt32(objRes["idInstalacionEquipo"])
                    ,
                    idServicio = Convert.ToInt32(objRes["idServicio"])
                    ,
                    idInstalacion = Convert.ToInt32(objRes["idInstalacion"])
                    ,
                    ieVigente = Convert.ToBoolean(objRes["ieVigente"])==false?"Inactivo":"Activo"
                }
                );
            }
            return lst;
        }

        //ya
        public static List<clsEntEquipoCatalogo> consultaInventarioDetallado(clsEntInstalacion objInstalacion, clsEntSesion objSesion)
        {
            DataSet dsEquipo = new DataSet();
            clsDatInstalacion.consultaInventarioDetallado(objInstalacion, ref dsEquipo, objSesion);
            List<clsEntEquipoCatalogo> lst = new List<clsEntEquipoCatalogo>();
            foreach (DataRow objRes in dsEquipo.Tables[0].Rows)
            {
                lst.Add(
                new clsEntEquipoCatalogo
                {
                    equDescripcion =Convert.ToString(objRes["equDescripcion"])
                   , ieCantidad=Convert.ToDecimal(objRes["iecCantidad"])
                   ,
                    idEquipo =Convert.ToInt32(objRes["idEquipo"])
                }
                );
            }
            return lst;
        }

        //ya
         public static bool insertarInventario(List<clsEntEquipoCatalogo> lstInventario,List<clsEntEquipoCatalogo> lstInventarioEquipo, clsEntSesion objSesion)
        {
             int idInstalacionEquipo=0;
             bool error = true;

             foreach (clsEntEquipoCatalogo objInv in lstInventario)
             {
                 if (objInv.semaforo != 0)
                 {
                     error = clsDatInstalacion.insertarInventario(ref idInstalacionEquipo, objInv, objSesion);
                 }
                     if (error == true)
                     {
                         if (objInv.semaforo == 1)
                         {
                             if (error == true)
                             {
                                 foreach (clsEntEquipoCatalogo obj in lstInventarioEquipo)
                                 {
                                     if (error == true)
                                     {
                                         error = clsDatInstalacion.insertarEquipoInventario(idInstalacionEquipo,obj, objSesion);
                                     }

                                 }
                             }
                         }
                     }
             }
             return error;
        }



         public static bool consultaPermisos(string idServicio, string idInstalacion, clsEntSesion objSesion)
         {

             clsEntEquipoCatalogo obj = new clsEntEquipoCatalogo
             {
                 idServicio=Convert.ToInt32(idServicio),
                 idInstalacion=Convert.ToInt32(idInstalacion)
             };

             DataSet dsEquipo = new DataSet();
             clsDatInstalacion.consultarPermisos(obj, objSesion,ref dsEquipo);


             bool resp =
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiConsultar"]) == true &&
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiAsignar"]) == true
             ? true :
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiConsultar"]) == true &&
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiAsignar"]) == false
             ? false :
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiConsultar"]) == false &&
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiAsignar"]) == false
             ? false :
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiConsultar"]) == false &&
             Convert.ToBoolean(dsEquipo.Tables[0].Rows[0]["usiAsignar"]) == true
             ? false : false;
             ;

             return resp;
     
         }


    }
}