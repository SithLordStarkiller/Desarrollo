using System;
using System.Text;
using proUtilerias;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;


using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;




using System.Collections.Generic;
using proUtilerias;

public partial class Personal_frmLaboralesConfirmacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntEmpleado objEmpleado = new clsEntEmpleado();
                objEmpleado = (clsEntEmpleado) Session["objEmpleado" + Session.SessionID];

                #region Datos Generales

                clsUtilerias.llenarLabel(lblNombreEmpleado, objEmpleado.EmpNombre, "-");
                clsUtilerias.llenarLabel(lblNumeroEmpleado, objEmpleado.EmpNumero, "-");
                clsUtilerias.llenarLabel(lblCUIP, objEmpleado.EmpCUIP, "-");
                lblFechaAlta.Text = objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/1900" &&
                                    objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/0001"
                                        ? objEmpleado.EmpFechaIngreso.ToShortDateString()
                                        : "-";
                
                #endregion

                #region Datos Laborales

                clsUtilerias.llenarLabel(lblJerarquia, objEmpleado.EmpleadoPuesto.Puesto.Jerarquia.JerDescripcion, "-");
                clsUtilerias.llenarLabel(lblPuesto, objEmpleado.EmpleadoPuesto.Puesto.PueDescripcion, "-");
                
               
          
                
                #endregion

                #region Asignaciones

                grvAsignacion.DataSource = objEmpleado.EmpleadoAsignacion;
                grvAsignacion.DataBind();
                
                #endregion
            }
            else
            {
                Response.Redirect("~/Personal/frmDatosLaborales.aspx");
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();

        if (Session["objEmpleado" + Session.SessionID] != null)
        {
            clsEntEmpleado objEmpleado = (clsEntEmpleado) Session["objEmpleado" + Session.SessionID];

            try
            {
                if (clsDatEmpleadoPuesto.insertarEmpleadoPuesto(objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]) )
                {
                    clsNegEmpleado.consultarEmpleado(ref objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);
                    #region insertaOficioAsignacion

                    if (Session["objOficioAsignacionAnterior" + Session.SessionID] != null)
                    {
                        clsNegOficioAsignacion.insertarOficioAsignacion((clsEntOficioAsignacion)Session["objOficioAsignacionAnterior" + Session.SessionID], (clsEntSesion)Session["objSession" + Session.SessionID]);
                    }


                    if (Session["objOficioAsignacion" + Session.SessionID] != null)
                    {
                        ((clsEntOficioAsignacion)Session["objOficioAsignacion" + Session.SessionID]).idEmpleadoAsignacion = objEmpleado.EmpleadoAsignacion[0].IdEmpleadoAsignacion;
                        clsNegOficioAsignacion.insertarOficioAsignacion((clsEntOficioAsignacion)Session["objOficioAsignacion" + Session.SessionID], (clsEntSesion)Session["objSession" + Session.SessionID]);
                    }


                    #endregion
                    sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
                   
                    DataSet dsBuscar = new DataSet("dsBuscar");
                    clsEntEmpleado objBuscar = new clsEntEmpleado();

                    objBuscar.IdEmpleado = objEmpleado.IdEmpleado;
                    objBuscar.EmpPaterno = objEmpleado.EmpPaterno;
                    objBuscar.EmpMaterno = objEmpleado.EmpMaterno;
                    objBuscar.EmpNombre = objEmpleado.EmpNombre;
                    objBuscar.EmpCURP = objEmpleado.EmpCURP;
                    objBuscar.EmpCUIP = objEmpleado.EmpCUIP;
                    objBuscar.EmpRFC = objEmpleado.EmpRFC;
                    objBuscar.EmpCartilla = objEmpleado.EmpCartilla;

                    objBuscar.EmpleadoAsignacion2 = new clsEntEmpleadoAsignacion();
                    objBuscar.EmpleadoAsignacion2.Servicio = new clsEntServicio();
                    objBuscar.EmpleadoAsignacion2.Instalacion = new clsEntInstalacion();






                    clsDatEmpleado.buscarEmpleado(objBuscar, "empleado.spBuscarEmpleadoPermisoConsulta", ref dsBuscar, (clsEntSesion)Session["objSession" + Session.SessionID]);
                    int conteo=dsBuscar.Tables[0].Rows.Count;
                    if (conteo == 0)
                    {
                    Session["objEmpleado" + Session.SessionID] = null;
                    Session["lstAsignaciones" + Session.SessionID] = null;
                    Session["lisHorarios"] = null;
                    }

                   



                }
            }
            catch (Exception ex)
            {
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
            }
            finally
            {
                Response.Redirect(sbUrl.ToString());
            }
        }
        else
        {
            Response.Redirect("~/Personal/frmDatosLaborales.aspx");
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lisHorarios"] = null;
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Personal/frmDatosLaborales.aspx");
    }

}
