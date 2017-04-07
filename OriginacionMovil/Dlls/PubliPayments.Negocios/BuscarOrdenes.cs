using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class BuscarOrdenes
    {

        public DataTable BuscaOrdenes(Busqueda modelo)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, modelo.IdUsuario, "BuscarOrdenes", "BuscaOrdenes - Credito : " + modelo.Credito);

            modelo.Credito = LimpiarCaracteres.DejarSoloNumeros(modelo.Credito);
            modelo.Delegacion = LimpiarCaracteres.QuitarInvalidosQuerys(modelo.Delegacion);
            modelo.Municipio = LimpiarCaracteres.QuitarInvalidosQuerys(modelo.Municipio);
            modelo.Nombre = LimpiarCaracteres.QuitarInvalidosQuerys(modelo.Nombre);
            modelo.Nss = LimpiarCaracteres.DejarSoloNumeros(modelo.Nss);
            modelo.Rfc = LimpiarCaracteres.QuitarInvalidosQuerys(modelo.Rfc);

            var busquedaOrdebes = new EntBuscarOrdenes().ObtenerOrdenes(modelo.Credito, modelo.Nss, modelo.Rfc, modelo.Nombre, modelo.Delegacion, modelo.Municipio, modelo.IdUsuario);
            return busquedaOrdebes;
        }

        public IEnumerable ObtenerNombres()
        {
            var nombres = new EntBuscarOrdenes().ObtenerNombreCombo();
            return nombres;
        }

        public IEnumerable ObtenerMunicipios(string delegacion)
        {
            var municipios = new EntBuscarOrdenes().ObtenerMunicipioCombo(delegacion);
            return municipios;
        }

        public IEnumerable ObtenerDelegaciones()
        {
            var delegaciones = new EntBuscarOrdenes().ObtenerDelegacionesFiltro();
            return delegaciones;
        }

        public Dictionary<string,string> ObtenerOrden(string credito)
        {
            var cat = new Dictionary<string, string>();

            var orden = new Orden().ObtenerOrdenxCredito(credito);

            if (orden != null)
            {
                if (orden.Estatus == 6)
                {
                    cat.Add("ord", "-1");
                    cat.Add("sit", "6");
                    return cat;
                }
                cat.Add("ord", orden.IdOrden.ToString(CultureInfo.InvariantCulture));
                cat.Add("sit", ValidarSituacionOrden(orden));
            }
            else
            {
                cat.Add("ord", "-1");
                cat.Add("sit", "1");
            }
            return cat;
        }

        public string ValidarSituacionOrden(OrdenModel ordenMod)
        {
            var capturaWeb = "1";

            if (ordenMod == null) return capturaWeb;
            //var orden = new Orden().ObtenerOrdenxCredito(ordenMod.NumCred);

            if (ordenMod.Estatus == 3 || ordenMod.Estatus == 4)
                capturaWeb = "-1";
            
            return capturaWeb;
        }

        public void Refrescar(HttpSessionStateBase session)
        {
            session["Credito"] = null;
            session["Nss"] = null;
            session["Rfc"] = null;
            session["Nombre"] = null;
            session["Delegacion"] = null;
            session["Municipio"] = null;
            session["Del"] = null;
        }

        public bool ValidarBusqueda(HttpSessionStateBase session, ref Busqueda modelo)
        {
            if ((modelo.Credito == null && session["Credito"] == null) &&
                (modelo.Nss == null && session["Nss"] == null) &&
                (modelo.Rfc == null && session["Rfc"] == null) &&
                (modelo.Nombre == null && session["Nombre"] == null) &&
                (modelo.Delegacion == null && session["Delegacion"] == null) &&
                (modelo.Municipio == null && session["Municipio"] == null))
                return false;

            if (modelo.Credito == null && modelo.Nss == null && modelo.Rfc == null && modelo.Nombre == null && modelo.Delegacion == null && modelo.Municipio == null) 
            {
                modelo.Credito = (string)session["Credito"];
                modelo.Nss = (string)session["Nss"];
                modelo.Rfc = (string)session["Rfc"];
                modelo.Nombre = (string)session["Nombre"];
                modelo.Delegacion = (string)session["Delegacion"];
                modelo.Municipio = (string)session["Municipio"];
            }

            session["Credito"] = modelo.Credito;
            session["Nss"] = modelo.Nss;
            session["Rfc"] = modelo.Rfc;
            session["Nombre"] = modelo.Nombre;
            session["Delegacion"] = modelo.Delegacion;
            session["Municipio"] = modelo.Municipio;

            return true;
        }

        public string ValidaMun(HttpSessionStateBase session,ref string delegacion)
        {
            string del;
            if (delegacion != null)
            {
                session["Del"] = delegacion;
                del = delegacion;
            }
            else
                del = (string) session["Del"];

            return del;
        }

        public int CrearOrden(string credito, int idUsuario, int idUsuarioPadre, int idUsuarioAlta, int idDominio,
            int idOrden)
        {
            var cnnSql = ConexionSql.Instance;
            DataSet ds = cnnSql.GeneraOrdenXml(credito, idUsuario, idUsuarioPadre, idUsuarioAlta, idDominio, idOrden);

            try
            {
                idOrden = Convert.ToInt32(ds.Tables[0].Rows[0]["idOrden"]);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "BuscarOrdenes: CrearOrden", ex.Message);
                idOrden = 0;
            }
            

            return idOrden;
        }
    }
}
