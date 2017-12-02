using System;
using proUtilerias;
using SICOGUA.Entidades;



public partial class Personal_frmDatosGenerales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                Session["impresion" + Session.SessionID] = null;
                clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                string empLoc=string.Empty;  if(objEmpleado.EmpLOC==1){empLoc="Si";}
                string empCurso = string.Empty;  if (objEmpleado.EmpCurso == 1) { empCurso = "Si"; }
  
                clsUtilerias.llenarLabel(lblApellidoPaterno, objEmpleado.EmpPaterno, "-");
                clsUtilerias.llenarLabel(lblApellidoMaterno, objEmpleado.EmpMaterno, "-");
                clsUtilerias.llenarLabel(lblNombre, objEmpleado.EmpNombre, "-");
                clsUtilerias.llenarLabel(lblCURP, objEmpleado.EmpCURP, "-");
                clsUtilerias.llenarLabel(lblNumero, objEmpleado.EmpNumero, "-");
                clsUtilerias.llenarLabel(lblCUIP, objEmpleado.EmpCUIP, "-");
                clsUtilerias.llenarLabel(lblRFC, objEmpleado.EmpRFC, "-");
                clsUtilerias.llenarLabel(lblNoCartilla, objEmpleado.EmpCartilla, "-");
                clsUtilerias.llenarLabel(lblCursoBasico, empCurso, "-");
                clsUtilerias.llenarLabel(lblLOC,empLoc, "-");

                lblFechaNacimiento.Text = objEmpleado.EmpFechaNacimiento.ToShortDateString() != "01/01/1900" &&
                                          objEmpleado.EmpFechaNacimiento.ToShortDateString() != "01/01/0001"
                                              ? objEmpleado.EmpFechaNacimiento.ToShortDateString()
                                              : "-";
                lblFechaAlta.Text = objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/1900" &&
                                    objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/0001"
                                        ? objEmpleado.EmpFechaIngreso.ToShortDateString()
                                        : "-";
                lblFechaBaja.Text = objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/1900" &&
                                    objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/0001"
                                        ? objEmpleado.EmpFechaBaja.ToShortDateString()
                                        : "-";

                clsUtilerias.llenarLabel(lblSexo, objEmpleado.EmpSexoValor, "-");
                //clsUtilerias.llenarLabel(lblPais, objEmpleado.Pais, "-");
                //clsUtilerias.llenarLabel(lblEstado, objEmpleado.Estado, "-");
                //clsUtilerias.llenarLabel(lblMunicipio, objEmpleado.Municipio, "-");
                clsUtilerias.llenarLabel(lblTipoSangre, objEmpleado.Sangre, "-");                            
            }
            else
            {
                string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
                string script = "if(confirm('¡Para visualizar un registro es necesario seleccionar un Empleado!.')) location.href='./../" + busqueda + "'; else location.href='./../frmInicio.aspx';";
                ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
            }
        }








    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
        Response.Redirect("~/" + busqueda);
    }
}
