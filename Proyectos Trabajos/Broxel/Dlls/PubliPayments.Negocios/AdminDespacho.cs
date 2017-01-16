using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class AdminDespacho
    {
        public DataTable ObtenerDespachos()
        {
            var lista = new EntAdminDespacho().ObtenerDespachos("", "", -1);
            return lista;
        }

        public DominioModel ObtenerDatosDominio(int idDominio)
        {
            var datosDominio = new EntAdminDespacho().ObtenerDatosDominio(idDominio);
            return datosDominio;
        }

        public bool ValidaNomCorto(string nomCorto,int idDominio)
        {
            var result = new EntAdminDespacho().ValidaNomCorto(nomCorto,idDominio);

            return result;
        }

        public bool ActualizarDominio(int dominio,string nombreDominio,string nomCorto,int estatus)
        {
            var dato = new EntAdminDespacho().ActualizarDominio(dominio, nombreDominio, nomCorto, estatus);
            return dato;
        }

        public VUsuarios ObtenerUsuarioPorEmail(string correo)
        {
            var usuarioEmail = new EntUsuario().ObtenerUsuarioPorEmail(correo);
            return usuarioEmail;
        }

        public bool ValidaUsuario(string usuario)
        {
            var validacion = new EntAdminDespacho().ValidaUsuario(usuario);
            return validacion;
        }

        public bool InsertaDominio(string nombreDominio,string nomCorto,string usuarioAdmin,string nombre,string password,string email)
        {
            var dato = new EntAdminDespacho().InsertaDominio(nombreDominio, nomCorto, usuarioAdmin, nombre,password, email);
            return dato;
        }
    }
}
