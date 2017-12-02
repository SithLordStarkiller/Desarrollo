using System;
using System.Collections.Generic;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using SICOGUA.Negocio;
using System.Web.UI.WebControls;
using System.Data;
using SICOGUA.Datos;
using System.Text;
using System.Web.UI;
using System.Collections.Specialized;
using System.Collections;
using System.Data.Linq;
using System.Linq;

public partial class Personal_frmInmovilidad : System.Web.UI.Page
{
    static private byte[] oficio = null; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
                clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];

                #region Datos Generales
                DataSet dsUltima = new DataSet();
                DataSet dsEmpleado = new DataSet();
                clsDatEmpleado.buscarAsignacionActual(objEmpleado.IdEmpleado, ref dsUltima, (clsEntSesion)Session["objSession" + Session.SessionID]);
                hfIdEmpleado.Value = objEmpleado.IdEmpleado.ToString();
                DataTable dTable = new DataTable();
                dTable = clsNegEmpleado.consultarInmovilidadPorEmpleado(objEmpleado.IdEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);

                List<clsEntEmpleadoInmovilidad> lst = new List<clsEntEmpleadoInmovilidad>();
                foreach (DataRow drEmpleado in dTable.Rows)
                {
                    clsEntEmpleadoInmovilidad obj = new clsEntEmpleadoInmovilidad();
                    obj.idEmpleado = new Guid(drEmpleado["idEmpleado"].ToString());
                    obj.miDescripcion = drEmpleado["miDescripcion"].ToString();
                    obj.eiDescripcion = drEmpleado["eiDescripcion"].ToString();
                    obj.PersonaAutoriza = drEmpleado["PersonaAutoriza"].ToString();
                    obj.eiFechaInicio = Convert.ToDateTime(drEmpleado["eiFechaInicio"].ToString());
                    obj.eiFechaFin = Convert.ToDateTime(drEmpleado["eiFechaFin"].ToString());
                    obj.eiImagen = drEmpleado["eiImagen"].ToString() == "" ? null : (byte[])drEmpleado["eiImagen"];
                    lst.Add(obj);
                }

                oficio = null;
                Session["lstInmovilidad"] = null;

                Session["lstInmovilidad"] =
                grvBusqueda.DataSource = lst;
                grvBusqueda.DataBind();
                clsCatalogos.llenarMotivoInmovilidad(ddlMotivoInmovilidad, objSesion);
                clsCatalogos.llenarAutoriza(ddlAutoriza, objSesion);
                clsUtilerias.llenarLabel(lblIntegrante, objEmpleado.EmpPaterno + " " + objEmpleado.EmpMaterno + " " + objEmpleado.EmpNombre, "-");
                clsUtilerias.llenarLabel(lblNoEmpleado, objEmpleado.EmpNumero, "-");
                clsUtilerias.llenarLabel(lblCuip, objEmpleado.EmpCUIP, "-");
                clsUtilerias.llenarLabel(lblCurp, objEmpleado.EmpCURP, "-");
                clsUtilerias.llenarLabel(lblRfc, objEmpleado.EmpRFC, "-");

                lblFechaAlta.Text = objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/1900" &&
                                    objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/0001"
                                        ? objEmpleado.EmpFechaIngreso.ToShortDateString()
                                        : "--/--/----";
                lblFechaBaja.Text = objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/1900" &&
                                    objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/0001"
                                        ? objEmpleado.EmpFechaBaja.ToShortDateString()
                                        : "--/--/----";

                clsUtilerias.llenarLabel(lblJerarquia, (dsUltima.Tables[0].Rows[0]["jerDescripcion"]).ToString());
                //clsUtilerias.llenarLabel(lblCargo, (dsUltima.Tables[0].Rows[0]["faDescripcion"]).ToString());
                //clsUtilerias.llenarLabel(lblZona, (dsUltima.Tables[0].Rows[0]["zonDescripcion"]).ToString());
                clsUtilerias.llenarLabel(lblServicio, (dsUltima.Tables[0].Rows[0]["serDescripcion"]).ToString(), "-");
                clsUtilerias.llenarLabel(lblInstalacion, (dsUltima.Tables[0].Rows[0]["insNombre"]).ToString(), "-");

                #endregion
            }
            else
            {
                string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
                string script = "if(confirm('¡Para guardar un registro es necesario seleccionar un Empleado!.')) location.href='./../" + busqueda + "'; else location.href='./../frmInicio.aspx';";
                ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
            }
        }       
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string strMensaje = validaciones();
        if (string.IsNullOrEmpty(strMensaje))
        {
            /*Agregar Registro*/
            clsEntEmpleadoInmovilidad objInmovilidad = new clsEntEmpleadoInmovilidad();
            objInmovilidad.idEmpleado = new Guid(hfIdEmpleado.Value);
            objInmovilidad.idMotivoInmovilidad = Convert.ToByte(ddlMotivoInmovilidad.SelectedValue);
            objInmovilidad.miDescripcion = ddlMotivoInmovilidad.SelectedItem.Text;
            objInmovilidad.idAutoriza = new Guid(ddlAutoriza.SelectedValue);
            objInmovilidad.eiDescripcion = txbDescripcion.Text.ToUpper();
            objInmovilidad.eiFechaInicio = Convert.ToDateTime(txbFechaInicio.Text);
            objInmovilidad.eiFechaFin = Convert.ToDateTime(txbFechaFin.Text);
            objInmovilidad.PersonaAutoriza = ddlAutoriza.SelectedItem.Text.Split('-')[0].Trim() + ", " + ddlAutoriza.SelectedItem.Text.Split('-')[1].Trim();
            objInmovilidad.estatus = true;
            if (oficio != null)
            {
                objInmovilidad.eiImagen = oficio;
                oficio = null;
            }

            if (Session["lstInmovilidad"] != null)
            {
                  ((List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"]).FindAll(
                        /*Encuentra el registro repetido en caso de ser nuevo*/
                       delegate(clsEntEmpleadoInmovilidad bk)
                       {
                           if (bk.eiFechaFin >= Convert.ToDateTime(txbFechaInicio.Text))
                           {
                               strMensaje = "Revise las fechas, no pueden ser menor o igual a los registros almacenados";
                               return true;
                           }
                           return false;
                       });

                  if (!string.IsNullOrEmpty(strMensaje))
                  {
                      wucMensaje.Mensaje(strMensaje);                      
                      return;
                  }
                  else
                  {
                      ((List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"]).Add(objInmovilidad);
                  }
            }
            else
            {
                /*Primer Registro*/
                List<clsEntEmpleadoInmovilidad> lstInmovilidad = new List<clsEntEmpleadoInmovilidad> { objInmovilidad };
                Session["lstInmovilidad"] = lstInmovilidad;
                
            }

            Session["lstInmovilidad"] = ((List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"]).OrderByDescending(p => p.eiFechaInicio).ToList(); 
            grvBusqueda.DataSource = (List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"];
            grvBusqueda.DataBind();
            limpiar();
        }
        else
        {
            wucMensaje.Mensaje(strMensaje);            
            return;       
        }
    }
    protected string validaciones()
    {
        string strMensaje = string.Empty;
        if (string.IsNullOrEmpty(ddlMotivoInmovilidad.SelectedValue)) { strMensaje = "* Debe seleccionar Motivo Inmovilidad </br>";}
        if (string.IsNullOrEmpty(txbFechaInicio.Text)) { strMensaje = strMensaje + "* Debe ingresar la fecha de inicio </br>"; }
        if (string.IsNullOrEmpty(txbFechaFin.Text)) { strMensaje = strMensaje + "* Debe ingresar la fecha fin </br>"; }
        if (string.IsNullOrEmpty(txbDescripcion.Text)) { strMensaje = strMensaje + "* Debe ingresar la descripción de la inmovilidad </br>"; }
        if (string.IsNullOrEmpty(ddlAutoriza.SelectedValue)) { strMensaje = strMensaje + "* Debe seleccionar la persona que autorizó </br>"; }
        if (!string.IsNullOrEmpty(txbFechaInicio.Text) && !string.IsNullOrEmpty(txbFechaFin.Text)) 
        {
            if (Convert.ToDateTime(txbFechaInicio.Text) > Convert.ToDateTime(txbFechaFin.Text)) 
            {
                strMensaje = strMensaje + "* La fecha de inicio no puede ser menor a la fecha fin";
            }
            if(Convert.ToDateTime(lblFechaAlta.Text) > Convert.ToDateTime(txbFechaInicio.Text))
            {
                strMensaje = strMensaje + "* Revise la fecha de inicio, no puede ser menor a: " + lblFechaAlta.Text + " </br>";
            }
            if (lblFechaBaja.Text != "--/--/----")
            {
                if (Convert.ToDateTime(lblFechaBaja.Text) < Convert.ToDateTime(txbFechaFin.Text))
                {
                    strMensaje = strMensaje + "* Revise la fecha fin, no puede ser mayor a: " + lblFechaBaja.Text + " </br>";
                }
            }
        }
        return strMensaje;
    }
    protected void limpiar() {
        ddlAutoriza.SelectedValue = ddlMotivoInmovilidad.SelectedValue = txbDescripcion.Text = txbFechaInicio.Text = txbFechaFin.Text = string.Empty;
        imbVerPdf.ImageUrl = "~/Imagenes/novalidado.png";
        imbVerPdf.Enabled = false;
    }

    protected void imbVerPdf_Click(object sender, ImageClickEventArgs e)
    {

        if (oficio != null)
        {
            imbVerPdf.ImageUrl = "~/Imagenes/Symbol-Check.png";
            imbVerPdf.Enabled = true;
            Session["Oficio" + Session.SessionID] = oficio;
            ClientScript.RegisterClientScriptBlock(GetType(), "oficio", "window.open('../Generales/verArchivo.ashx?idSession=Oficio');", true);
        }
        else
        {
            imbVerPdf.ImageUrl = "~/Imagenes/novalidado.png";
            imbVerPdf.Enabled = false;
        }
    }
    protected void imbAgregarPdf_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnAgregarIndex2_Click(object sender, EventArgs e)
    {
        popAgregarOficioIndex.Hide();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);
    }
    protected void btnCancelarIndex_Click(object sender, EventArgs e)
    {
        popAgregarOficioIndex.Hide();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);
        oficio = null;

        imbVerPdf.ImageUrl = "~/Imagenes/novalidado.png";
        imbVerPdf.Enabled = false;

    }
    public void AsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (AsyncFileUpload.HasFile)
        {
            oficio = AsyncFileUpload.FileBytes;
        }
    }
    protected void grvBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            List<clsEntEmpleadoInmovilidad> lstInmovilidad = (List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"];
            if (lstInmovilidad.Count != 0)
            {
                GridViewRow row = (GridViewRow)e.Row;
                TableCell id = row.Cells[6];
                Control imbSeleccionar = id.FindControl("imbDocumento");
                ImageButton imaSeleccionar = (ImageButton)imbSeleccionar;
                
                if (lstInmovilidad[e.Row.RowIndex].eiImagen != null)
                {
                    imaSeleccionar.ImageUrl = "~/Imagenes/Symbol-Check.png";
                    imaSeleccionar.Enabled = true;
                }
                else
                {
                    imaSeleccionar.ImageUrl = "~/Imagenes/novalidado.png";
                    imaSeleccionar.Enabled = false;
                }

            }
        }
    }
    protected void grvBusqueda_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         List<clsEntEmpleadoInmovilidad> lstInmovilidad = (List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"];
         if (lstInmovilidad.Count != 0)
         {
             if (lstInmovilidad[e.RowIndex].eiImagen != null)
             {
                 Session["Oficio" + Session.SessionID] = lstInmovilidad[e.RowIndex].eiImagen;
                 ClientScript.RegisterClientScriptBlock(GetType(), "oficio", "window.open('../Generales/verArchivo.ashx?idSession=Oficio');", true);
             }
         }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
        List<clsEntEmpleadoInmovilidad> lstInmovilidad = (List<clsEntEmpleadoInmovilidad>)Session["lstInmovilidad"];
        if (lstInmovilidad != null)
        {
            lstInmovilidad = lstInmovilidad.OrderBy(p => p.eiFechaInicio).ToList();
            if (clsNegEmpleado.insertarEmpleadoInmovilidad(lstInmovilidad, (clsEntSesion)Session["objSession" + Session.SessionID]))
            {
                Session["lstInmovilidad"] = Session["Oficio" + Session.SessionID] = null;
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
                Response.Redirect(sbUrl.ToString());
            }
        }
        else
        {
            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
            Response.Redirect(sbUrl.ToString());
        }
        
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        oficio = null;
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lstInmovilidad"] = Session["Oficio" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        oficio = null;
        Session["lstInmovilidad"] = Session["Oficio" + Session.SessionID] = null;
        string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
        Response.Redirect("~/" + busqueda);
    }
}