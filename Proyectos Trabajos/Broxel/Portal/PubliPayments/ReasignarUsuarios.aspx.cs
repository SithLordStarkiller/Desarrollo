using System;
using System.Linq;
using System.Web.UI;
using PubliPayments.Entidades;
using PubliPayments.Negocios;

namespace PubliPayments
{
    public partial class ReasignarUsuarios : Page
    {
        private int _rol, _dominio, _idPadre, _rolActual;
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        protected string Nombre;
        protected void Page_Load(object sender, EventArgs e)
        {
            _rolActual = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            _dominio = TablaUsuarios.ObtenerDomino();
            _idPadre =int.Parse(Request.QueryString["Usuario"] ?? "0");

                var lista = from u in _ctx.VUsuarios
                            where u.idDominio == _dominio
                            && u.idUsuario == _idPadre
                            select u;
                var sel = lista.First();
                _rol = sel.idRol;
                Nombre = sel.Usuario; 

                var listaReasignar = from u in _ctx.VUsuarios
                    where u.idDominio == _dominio
                          && u.Estatus == 1
                          && u.idRol == _rol
                          && u.idUsuario != _idPadre
                    select u;
                ListPadre.DataSource = listaReasignar.ToList();

                ListPadre.DataBind();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (_idPadre != 0)
            { TablaUsuarios.CargaUsuariosReasignar(_dominio, _idPadre); }
            if (_rolActual != 0 && _rolActual != 1 && _rolActual != 2)
            {
                TablaUsuarios.Visible = false;
                ListPadre.Visible = false;
            }
        }

        protected void btAsignar_Click(object sender, EventArgs e)
        {
            string nvoPadre = NuevoPadre.Text;
            string valores = ListaReasignar.Text;
            string[] idReasignar= valores.Split(',');

            
            foreach (var s in idReasignar)
            {
                if (s != "")
                {
                    var resultado = new Usuarios().ReasignarUsuarios(Convert.ToString(_idPadre), nvoPadre, s);
                    lbInformacion.Text = resultado=="OK" ? "Resignación completa" : "Ocurrió un error";
                }
            }
            ListaReasignar.Text = "";
            lbInformacion.Visible = lbInformacion.Text != "";
        }

        protected void Regresar_Click(object sender, EventArgs e)
        {
            if(Session["PaginaAnterior"]!= null)
            {
                string anterior = Session["PaginaAnterior"].ToString();
                Session.Remove("PaginaAnterior");
                Response.Redirect(anterior);
            }
            else
            {
                Response.Redirect("~/Dashboard");    
            }
            
        }
    }
}