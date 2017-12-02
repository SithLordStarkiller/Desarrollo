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

public partial class Personal_frmRevision : System.Web.UI.Page
{
    static private List<clsEntActaAdministrativa> lstProceso = null;
    static private List<clsEntRenuncia> lstRenuncia = null;
    static private byte[] oficio = null; 

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            deshabilitaPrimerActa();
            ckbPrimerActa.Enabled = true;
            Session["impresion" + Session.SessionID] = null;
            lstProceso = null;
            lstRenuncia = null;

            if (lstProceso == null)
            {
                lstProceso = new List<clsEntActaAdministrativa>();
            }

            if (lstRenuncia == null)
            {

                lstRenuncia = new List<clsEntRenuncia>();
            }
            oficio = null;
      
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];

            if (Session["objEmpleado" + Session.SessionID] != null)
            {

         


                clsEntEmpleado objEmpleadoID = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];



                DataSet dsUltima = new DataSet();
                clsDatEmpleado.buscarAsignacionActual(objEmpleadoID.IdEmpleado,ref dsUltima, (clsEntSesion)Session["objSession" + Session.SessionID]);



                
                txbFechaPrimerActa.Text = "";
                //ckbPrimerActa.Checked = true;

                #region Datos Generales




                hfIdEmpleadoAsignacion.Value = Convert.ToString((dsUltima.Tables[0].Rows[0]["idEmpleadoAsignacion"]).ToString());

                clsUtilerias.llenarLabel(lblPaterno, (dsUltima.Tables[0].Rows[0]["empPaterno"]).ToString());
                clsUtilerias.llenarLabel(lblMaterno, (dsUltima.Tables[0].Rows[0]["empMaterno"]).ToString());
                clsUtilerias.llenarLabel(lblNombre, (dsUltima.Tables[0].Rows[0]["empNombre"]).ToString());

                clsUtilerias.llenarLabel(lblJeraraquia, (dsUltima.Tables[0].Rows[0]["jerDescripcion"]).ToString());
                clsUtilerias.llenarLabel(lblFechaBaja, Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["empFechaBaja"]).ToShortDateString() != "01/01/1900" && Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["empFechaBaja"]).ToShortDateString() != "01/01/0001" ? Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["empFechaBaja"]).ToShortDateString() : "--/--/----");

                hfIdServicio.Value = (dsUltima.Tables[0].Rows[0]["idServicio"]).ToString();
                hfIdInstalacion.Value = (dsUltima.Tables[0].Rows[0]["idInstalacion"]).ToString();
                hfIdEmpleado.Value = (dsUltima.Tables[0].Rows[0]["idEmpleado"]).ToString();


                cargaLista(objEmpleadoID.IdEmpleado);
                if (lstProceso.Count != 0)
                {
                    lblPagina.Visible = true;
                    lblPaginas.Visible = true;
                    lblde.Visible = true;

                    lblPagina.Text = "1";
                    lblPaginas.Text = (grvBusqueda.PageIndex + 1).ToString();


                    if (lstProceso.Count > 6)
                    {
                        imgAdelante.Visible = true;
                        imgAtras.Visible = true;
                    }
                    else
                    {
                        imgAdelante.Visible = false;
                        imgAtras.Visible = false;
                    }

                    clsEntActaAdministrativa objProceso = lstProceso[0];
                    if (objProceso.revVigente == true) { btnAgregar.Enabled = false; btnAgregarIndex.Enabled = false; } else { btnAgregar.Enabled = true; btnAgregarIndex.Enabled = true; }
                }
                else
                {
                    lblPagina.Visible = false;
                    lblPaginas.Visible = false;
                    lblde.Visible = false;
                }

               


                cargaRenuncia(objEmpleadoID.IdEmpleado);

              
                
                
                
                
                
                
                
                
                
                //escribe();


                clsUtilerias.llenarLabel(lblNumeroEmpleado, (dsUltima.Tables[0].Rows[0]["empNumero"]).ToString(), "-");
                clsUtilerias.llenarLabel(lblCUIP, (dsUltima.Tables[0].Rows[0]["empCuip"]).ToString(), "-");
                lblFechaAlta.Text = Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["empFechaIngreso"]).ToShortDateString() != "01/01/1900" &&
                                   Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["empFechaIngreso"]).ToShortDateString() != "01/01/0001"
                                        ? Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["empFechaIngreso"]).ToShortDateString()
                                        : "-";

                clsUtilerias.llenarLabel(lblServicio, (dsUltima.Tables[0].Rows[0]["serDescripcion"]).ToString(), "-");
                clsUtilerias.llenarLabel(lblInstalacion, (dsUltima.Tables[0].Rows[0]["insNombre"]).ToString(), "-");
                clsUtilerias.llenarLabel(lblFuncion, (dsUltima.Tables[0].Rows[0]["faDescripcion"]).ToString(), "-");
                
                lblInicioComision.Text = Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["inicioComisionFecha"]).ToShortDateString() != "01/01/1900" &&
                                   Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["inicioComisionFecha"]).ToShortDateString() != "01/01/0001"
                                        ? Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["inicioComisionFecha"]).ToShortDateString()
                                        : "-";

                lblFinComision.Text = Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["finComisionFecha"]).ToShortDateString() != "01/01/1900" &&
                                   Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["finComisionFecha"]).ToShortDateString() != "01/01/0001"
                                        ? Convert.ToDateTime(dsUltima.Tables[0].Rows[0]["finComisionFecha"]).ToShortDateString()
                                        : "-";
                hfPermiso.Value=(dsUltima.Tables[0].Rows[0]["permiso"]).ToString();
                #endregion



                if (objEmpleadoID.EmpFechaBaja.Date <= DateTime.Now
                && objEmpleadoID.EmpFechaBaja.ToShortDateString() != "01/01/1900"
                && objEmpleadoID.EmpFechaBaja.ToShortDateString() != "01/01/0001")
                {
                    ckbRenuncia.Enabled=false;
                    txbFechaRenuncia.Enabled=false;
                    imbFechaRenuncia.Enabled=false;
                    txbObs.Enabled=false;
                    txbNoOficio.Enabled=false;
                    txbFechaOficio.Enabled=false;
                    imbOficio.Enabled = false;
                    imbVerPdf.Enabled=false;
                    imbAgregarPdf.Enabled=false;
                    btnGuardar.Enabled = false;
                    btnAgregar.Enabled = false;
                    imbAgregarPdf.ImageUrl="~/Imagenes/agregarDesHabilitado.png";
                    divErrorRevision.Visible = true;
                    lblerrorRevision.Text = "El empleado ha sido dado de baja por lo que no se podrá realizar ningún movimiento";
                    lblerrorRevision.Visible = true;


                    if (lstProceso.Count != 0)
                    {
                        escribe();
                    }

                }

                if (hfPermiso.Value == "0")
                {
                    ckbRenuncia.Enabled = false;
                    txbFechaRenuncia.Enabled = false;
                    imbFechaRenuncia.Enabled = false;
                    txbObs.Enabled = false;
                    txbNoOficio.Enabled = false;
                    txbFechaOficio.Enabled = false;
                    imbOficio.Enabled = false;
                    imbVerPdf.Enabled = false;
                    imbAgregarPdf.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnAgregar.Enabled = false;
                }





        //Operacion 32 Solo Consulta-ASIGNACIONES
                clsEntPermiso objPermiso = new clsEntPermiso();
                objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;




            if (clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, 32, objSesion) == false)
            {
                ckbRenuncia.Enabled = false;
                txbFechaRenuncia.Enabled = false;
                imbFechaRenuncia.Enabled = false;
                txbObs.Enabled = false;
                txbNoOficio.Enabled = false;
                txbFechaOficio.Enabled = false;
                imbOficio.Enabled = false;
                imbVerPdf.Enabled = false;
                imbAgregarPdf.Enabled = false;
                btnGuardar.Enabled = false;
                btnAgregar.Enabled = false;    
            }


         



            }
            else
            {
                string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
                string script = "if(confirm('¡Para guardar un registro es necesario seleccionar un Empleado!.')) location.href='./../" + busqueda + "'; else location.href='./../frmInicio.aspx';";
                ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
            }



        }




    

    }

    protected void grvCartilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfColumna.Value = e.CommandArgument.ToString();
    }

    protected void imgAdelante_Click(object sender, ImageClickEventArgs e)
    {

        if (grvBusqueda.PageIndex < grvBusqueda.PageCount)
        {
            grvBusqueda_PageIndexChanging(grvBusqueda, new GridViewPageEventArgs(grvBusqueda.PageIndex + 1));
        }
        escribe();
    }
    protected void imgAtras_Click(object sender, ImageClickEventArgs e)
    {

        if (grvBusqueda.PageIndex > 0)
        {
            grvBusqueda_PageIndexChanging(grvBusqueda, new GridViewPageEventArgs(grvBusqueda.PageIndex - 1));
        }
        escribe();
     
    }

    protected void grvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        List<clsEntActaAdministrativa> evaluaciones = (List<clsEntActaAdministrativa>)lstProceso;

        if (evaluaciones != null)
        {

            grvBusqueda.PageIndex = e.NewPageIndex;
            grvBusqueda.DataSource = evaluaciones;
            grvBusqueda.DataBind();
            lblPagina.Text = (grvBusqueda.PageIndex + 1).ToString();
            lblPaginas.Text = (grvBusqueda.PageIndex + 1).ToString();

        }
        escribe();

 

    }

    protected void btn_Click(object sender, EventArgs e)
    {
      
        limpiarforma();
        lblerrorActas.Visible = false;
        divErrorActas.Visible = false;
        lblerrorActas.Text = string.Empty;
        popAsignacion.Hide();
        hfIndex.Value = "-1";
        btnAgregarIndex.Enabled = true;
        hfNuevo.Value = "Nuevo";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
        //divErrorOficio.Visible = false;
        //lblerrorOficio.Text = "";
        //lblerrorOficio.Visible = false;
        //hfColumna.Value = "-1";
    }
   
   
    protected void btnAgregarOficio_Click(object sender, EventArgs e)
    {        

    }


    protected void btnAgregarIndex_Click(object sender, EventArgs e)
    {


        divErrorActas.Visible = false;
        lblerrorActas.Text = "";
        string mensaje = string.Empty;
        if (ckbPrimerActa.Enabled == true && txbFechaPrimerActa.Text.Trim() == "")
        {
            mensaje = "La fecha de la acta se encuentra en blanco";
            lblerrorActas.Visible = true;
            divErrorActas.Visible = true;
            lblerrorActas.Text = mensaje;
            habilitaPrimerActa();
            ckbPrimerActa.Checked = true;
            popAsignacion.Show();
            return;
        }

        int SEM = 0;

        int registro = 0;

   
        if (ckbPrimerActa.Checked == true && ckbPrimerActa.Enabled == true)
        {
           
            bool fechaPrimerActa = ValidaFecha.ValidaFe(txbFechaPrimerActa.Text);
            bool fechaOficioPrimer= ValidaFecha.ValidaFe(txbFechaOficioPrimerActa.Text);
            if (fechaPrimerActa == false) { if (txbFechaPrimerActa.Text == "") { mensaje = "La fecha del procedimiento se encuentra en blanco"; } else { mensaje = "Error en la Fecha del procedimiento"; } } else { mensaje = string.Empty; registro = 1; }
            if (mensaje == string.Empty) { if (txbFechaOficioPrimerActa.Text.Trim() != "") { if (fechaOficioPrimer == false) { mensaje = "Erroe en la fecha del oficio"; } } }


        }


   

        if (ckbCancelado.Checked == true)
        {
            bool fechaCancelacionActa = ValidaFecha.ValidaFe(txbFechaCancelacion.Text);
            if (fechaCancelacionActa == false) { if (txbFechaCancelacion.Text == "") { lblerrorActas.Visible = true; divErrorActas.Visible = true; mensaje = "La fecha de la cancelación se encuentra en blanco"; habilitaCancelacion();  lblerrorActas.Text = mensaje; popAsignacion.Show(); return; } else { lblerrorActas.Visible = true; divErrorActas.Visible = true; lblerrorActas.Text = mensaje; popAsignacion.Show(); mensaje = "Error en la Fecha de la cancelación"; habilitaCancelacion(); return; } } else { mensaje = string.Empty; registro = 3; }
        }



        if(ckbPrimerActa.Checked == true || ckbCancelado.Checked==true )
        {
           // mensaje = string.Empty;
        }
        else{

            if (ckbCancelado.Checked == true && txbFechaCancelacion.Text.Trim()=="")
            {
                lblerrorActas.Visible = true; divErrorActas.Visible = true; lblerrorActas.Text = mensaje; popAsignacion.Show();
               mensaje = "Existe un error en la fecha de cancelación o bien el campo se encuentra en blanco";
               habilitaCancelacion();
               return;
            }

           


         
        }


   



        if (mensaje != string.Empty)
        {
            divErrorActas.Visible = true;
            lblerrorActas.Text = mensaje;
            lblerrorActas.Visible = true;
            popAsignacion.Show();
        }
        else
        {
            clsEntActaAdministrativa objProceso = new clsEntActaAdministrativa();
            clsEntActaAdministrativa objProcesoRev = new clsEntActaAdministrativa();



            if (lstProceso.Count != 0)
            {
                if (lstProceso[0].idCancelacion != 0)
                {
                    if (Convert.ToDateTime(lstProceso[0].fechaCancelacion) >= Convert.ToDateTime(txbFechaPrimerActa.Text))
                    {
                        mensaje = "La fecha ingresada es menor a la última cancelación";
                        ckbPrimerActa.Enabled = true;txbFechaPrimerActa.Enabled = true; imbPrimerActa.Enabled = true;txbNoOficioPrimerActa.Enabled = true;txbFechaOficioPrimerActa.Enabled = true;imbPrimerActaOficio.Enabled = true; 
                        ckbPrimerActa.Checked = true;
                        lblerrorActas.Visible = true; divErrorActas.Visible = true; lblerrorActas.Text = mensaje; popAsignacion.Show();
                        return;
                    }
                    else
                    {
                        mensaje = string.Empty;
                    }

                }

        

                if (lstProceso[0].fechaActaPr != "--/--/----" && txbFechaCancelacion.Text!="" && mensaje == string.Empty)
                {

                    if (Convert.ToDateTime(txbFechaCancelacion.Text) < Convert.ToDateTime(lstProceso[0].fechaActaPr))
                    {
                        mensaje = "La fecha de cancelación debe ser mayor a la del procedimiento";
                        habilitaCancelacion();
                    }
                    else
                    {
                       
                                mensaje = string.Empty;
                          
                    

                  
                    }

                }



              }













            if (mensaje == string.Empty)
            {

                if (lstProceso.Count != 0)
                {





                    objProcesoRev = lstProceso[0];
                    //if (objProcesoRev.idRevision != 0)
                    //{
                        if (hfNuevo.Value == "Update")
                        {

                            objProceso = lstProceso[0];
                            SEM = 1;
                        }




                    //}
                }





                objProceso.idEmpleado = new Guid(hfIdEmpleado.Value);
                objProceso.idServicio = Convert.ToInt32(hfIdServicio.Value);
                objProceso.idInstalacion = Convert.ToInt32(hfIdInstalacion.Value);
                objProceso.desServicio = lblServicio.Text;
                objProceso.desInstalacion = lblInstalacion.Text;

                switch (registro)
                {
                    case 1:



                        objProceso.fechaActaPr = txbFechaPrimerActa.Text.Trim();
                        objProceso.NoOficioPr = txbNoOficioPrimerActa.Text.Trim();
                        objProceso.fechaOficioPr = txbFechaOficioPrimerActa.Text.Trim();
                        objProceso.rdObservaciones = txbObservacionesActa.Text.Trim();
                      
                        objProceso.fechaCancelacion = "--/--/----";


                        break;

                    case 2:


                        if (objProceso.idCancelacion == 0)
                        {
                            objProceso.fechaCancelacion = "--/--/----";
                            objProceso.observaciones = null;
                        }




                        objProceso.fechaCancelacion = "--/--/----";



                        break;

                    case 3:


                

                        objProceso.fechaCancelacion = txbFechaCancelacion.Text;
                        objProceso.observaciones = txbObservaciones.Text;





                        break;
                }
                btnAgregar.Enabled = false;








                if (hfNuevo.Value == "Update")
                {
                    lstProceso[Convert.ToInt32(hfIndex.Value)] = objProceso;
                }
                else
                {
                    if (SEM == 0)
                    {
                        objProceso.idRevision = 0;
                    }
                    lstProceso.Insert(0, objProceso);

                }




                grvBusqueda.DataSource = lstProceso;
                grvBusqueda.DataBind();
                limpiarforma();
                popAsignacion.Hide();
                escribe();
                hfNuevo.Value = "Nuevo";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);


                switch (registro)
                {
                    case 1:
                        txbFechaPrimerActa.Text = string.Empty;
                        txbNoOficioPrimerActa.Text = string.Empty;
                        txbFechaOficioPrimerActa.Text = string.Empty;
                        break;

              

                    case 3:
                        txbFechaCancelacion.Text = string.Empty;
                        txbObservaciones.Text = string.Empty;
                        break;
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);
                lblerrorActas.Visible = false;
                divErrorActas.Visible = false;
                lblerrorActas.Text = string.Empty;
    

                if (lstProceso.Count != 0)
                {
                    escribe();
                }

            }
            else
            {
                lblerrorActas.Visible = true;
                divErrorActas.Visible = true;
                lblerrorActas.Text = mensaje;
                popAsignacion.Show();
            }





        }


       lblPagina.Text = "1";
       lblPaginas.Text = (grvBusqueda.PageIndex + 1).ToString();
       lblPaginas.Visible = true;
       lblPagina.Visible = true;
       lblde.Visible = true;
    }

    public class ValidaFecha
    {
        static public bool ValidaFe(string fecha)
        {
            bool dictamen;
            try
            {
                DateTime fechaVal = Convert.ToDateTime(fecha);
                int dia = Convert.ToInt16(fechaVal.Day);
                int mes = Convert.ToInt16(fechaVal.Month);
                int año = Convert.ToInt16(fechaVal.Year);
                int error = 0;

                string bi16 = Convert.ToString(ValidaFecha.CalcularBi(año));
                if (mes == 2 && dia == 29 && bi16 == "False") { error++; }
                if (año >= 1900 && mes <= 12 && dia <= 31) { } else { error++; }
                if (mes == 1) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 2) { if (dia > 29 || dia == 0) { error++; } }
                if (mes == 3) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 4) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 5) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 6) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 7) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 8) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 9) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 10) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 11) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 12) { if (dia > 31 || dia == 0) { error++; } }
                if (error == 0) { dictamen = true; } else { dictamen = false; }



            }
            catch
            {
            
                    dictamen = false;
            
            }


            return dictamen;


        }

        static public string CalcularBi(int anno)
        {
            string alfa = Convert.ToString((anno % 4 == 0) && !(anno % 100 == 0 && anno % 400 != 0)); return alfa;
        }

    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
      


    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {

      

        divErrorRevision.Visible = false;
        lblerrorRevision.Text = "";
        int revision=0;
        int error = 0;
        int erroPrimerActa = 0;

        int erroCancelacion = 0;
        int documentoPorCancelar = 0;
        string mensaje = string.Empty;
        clsEntActaAdministrativa objProceso =new clsEntActaAdministrativa();
        clsEntRevision objRevision = new clsEntRevision();
        bool prim=ValidaFecha.ValidaFe(txbFechaPrimerActa.Text);
        bool ren = ValidaFecha.ValidaFe(txbFechaRenuncia.Text);
        int movimiento = 0;
        if (hfIdRenuncia.Value != "") { desHabilitaRenuncia(); } else { habilitaRenuncia(); }
        if (lstProceso.Count != 0 || ckbRenuncia.Enabled == true && txbFechaRenuncia.Text != "")
        {
            if (lstProceso.Count != 0)
            {
                if (txbFechaRenuncia.Text == "" && ckbRenuncia.Checked == true && prim == false) { lblerrorRevision.Text = "Error en la Fecha del procedimiento"; mensaje = "Error en la Fecha del procedimiento"; } else { mensaje = string.Empty; }
                if (txbFechaRenuncia.Text != "" && ckbRenuncia.Checked == true && ren == false) { lblerrorRevision.Text = "Error en la Fecha de renuncia"; mensaje = "Error en la Fecha del renuncia"; } else { mensaje = string.Empty; }
                if (lstProceso[0].fechaActaPr != "--/--/----" && lstProceso[0].idRevDocumento1ra == 0) { movimiento = 1; }
                if (lstProceso[0].fechaCancelacion != "--/--/----" && lstProceso[0].idCancelacion == 0) { movimiento = 4; }
            }

            if (txbFechaRenuncia.Text != "" && ckbRenuncia.Enabled == true) { movimiento = 2; }
            if (movimiento == 0) { lblerrorRevision.Text = "Debe realizar un movimiento para poder continuar"; mensaje = "No existe ninguna movimiento pendiente"; }
        }



        if (ckbRenuncia.Enabled == true && ckbRenuncia.Checked == true)
        {
            if (txbFechaRenuncia.Text.Trim() == "")
            {
                divErrorRevision.Visible = true;
                lblerrorRevision.Visible = true;
                lblerrorRevision.Text = "La fecha de renuncia es obligatoria";
                habilitaRenuncia();
                return;
            }
        }
        else
        {
            if (lstProceso.Count == 0)
            {
                divErrorRevision.Visible = true;
                lblerrorRevision.Visible = true;
                lblerrorRevision.Text = "Debe realizar un movimiento para poder continuar";
                return;
            }
        }


        if (mensaje == string.Empty )
        {
            StringBuilder sbUrl = new StringBuilder();
            try
            {

                if (lstProceso.Count != 0)
                {
                    if (lstProceso[0].fechaActaPr =="--/--/----") { erroPrimerActa = 1; }         
                    if (lstProceso[0].fechaCancelacion == "--/--/----") { erroCancelacion = 1; }

                    if (erroPrimerActa == 1)
                    {
                        lblerrorRevision.Text = "Debe agregar una fecha a la  acta administrativa para poder continuar";
                        error = 1;
                    }


                    if (erroCancelacion == 1 && lstProceso[0].idRevDocumento1ra != 0 && error == 0)
                    {

                        lblerrorRevision.Text = "Debe agregar un fecha a cancelación para poder continuar";
                        error = 1;
                    }

                    if (lstProceso[0].idCancelacion != 0 && error == 0 && txbFechaRenuncia.Enabled == false || lstProceso[0].idCancelacion != 0 && error == 0 && txbFechaRenuncia.Enabled == true) { error = 2; lblerrorRevision.Text = "No existe ningún movimiento pendiente"; }

                }

                if (txbFechaRenuncia.Text != "" && ckbRenuncia.Enabled == true)
                {
                    bool fecharen = ValidaFecha.ValidaFe(txbFechaRenuncia.Text);
                    if (fecharen != true) { lblerrorRevision.Text = "Error en la fecha de renuncia"; error = 1; habilitaRenuncia(); }
                    else
                    {
                        error = 0;

                        //RENUNCIA-------------------------------------
                        if (txbFechaRenuncia.Text.Trim() != "" && ckbRenuncia.Enabled == true) { revision = 1; }
                        if (revision == 1)
                        {

                            clsEntRenuncia objRenuncia = new clsEntRenuncia();
                            if (lstRenuncia.Count == 0)
                            {
                                objRenuncia = new clsEntRenuncia();
                                if (txbFechaRenuncia.Text.Trim() != "") { objRenuncia.fechaRenuncia = Convert.ToDateTime(txbFechaRenuncia.Text.Trim()); }
                                if (txbFechaOficio.Text.Trim() != "") { objRenuncia.fechaOficio = Convert.ToDateTime(txbFechaOficio.Text.Trim()); }
                                objRenuncia.noOficio = txbNoOficio.Text.Trim();
                                objRenuncia.observaciones = txbObs.Text.Trim();
                                objRenuncia.idEmpleado = new Guid(hfIdEmpleado.Value);
                                objRenuncia.idEmpleadoAsignacion =Convert.ToInt32(hfIdEmpleadoAsignacion.Value);
                                //objRenuncia.idServicio = Convert.ToInt32(hfIdServicio.Value);
                                //objRenuncia.idInstalacion = Convert.ToInt32(hfIdInstalacion.Value);

                                lstRenuncia.Add(objRenuncia);
                                clsDatRevision.insertarRenuncia(objRenuncia, (clsEntSesion)Session["objSession" + Session.SessionID]);
                                error = 5;
                            }
                            else
                            {
                                objRenuncia = lstRenuncia[0];
                                if (txbFechaRenuncia.Text.Trim() != "") { objRenuncia.fechaRenuncia = Convert.ToDateTime(txbFechaRenuncia.Text.Trim()); }
                                if (txbFechaOficio.Text.Trim() != "") { objRenuncia.fechaOficio = Convert.ToDateTime(txbFechaOficio.Text.Trim()); }
                                objRenuncia.noOficio = txbNoOficio.Text.Trim();
                                objRenuncia.observaciones = txbObs.Text.Trim();
                                objRenuncia.idEmpleado = new Guid(hfIdEmpleado.Value);
                                //objRenuncia.idServicio = Convert.ToInt32(hfIdServicio.Value);
                                //objRenuncia.idInstalacion = Convert.ToInt32(hfIdInstalacion.Value);
                                objRenuncia.idEmpleadoAsignacion = Convert.ToInt32(hfIdEmpleadoAsignacion.Value);
                                lstRenuncia.Add(objRenuncia);
                                clsDatRevision.insertarRenuncia(objRenuncia, (clsEntSesion)Session["objSession" + Session.SessionID]);
                                error = 5;
                            }

                            clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                            objEmpleado.idRenuncia = 1;
                            Session["objEmpleado" + Session.SessionID] = objEmpleado;
                        }
                        //FIN RENUNCIA---------------------------------
                    }
                }

                if (lstProceso.Count != 0 && error == 0 || lstProceso.Count != 0 && error == 5)
                {
        

                    //PRIMER PROCESO------------------------------
                    if (erroPrimerActa == 0 && lstProceso[0].idRevDocumento1ra == 0 && lstProceso[0].fechaActaPr != "--/--/----")
                    {
                        if (lstProceso[0].fechaActaPr != null)
                        {
                            objRevision.idEmpleado = new Guid(hfIdEmpleado.Value);
                            objRevision.idEmpleadoAsignacion = Convert.ToInt32(hfIdEmpleadoAsignacion.Value);
                            //objRevision.idServicio = Convert.ToInt32(hfIdServicio.Value);
                            //objRevision.idInstalacion = Convert.ToInt32(hfIdInstalacion.Value);
                            objRevision.idRevision = objRevision.idRevision;
                         
                            objProceso = lstProceso[0];

                            if (objProceso.idRevision == 0)
                            {
                                clsDatRevision.insertarRevision(ref objRevision, (clsEntSesion)Session["objSession" + Session.SessionID]);
                            }

                            clsEntRevisionDocumentos objRevisionDocumentos = new clsEntRevisionDocumentos();
                            objRevisionDocumentos.idEmpleado = new Guid(hfIdEmpleado.Value);
                            objRevisionDocumentos.idRevision = objRevision.idRevision;
                            objRevisionDocumentos.rdFechaActa = Convert.ToDateTime(objProceso.fechaActaPr);
                            objRevisionDocumentos.rdNoOficio = objProceso.NoOficioPr;
                            if (objProceso.fechaOficioPr != "") { objRevisionDocumentos.rdFechaOficio = Convert.ToDateTime(objProceso.fechaOficioPr); }
                            objRevisionDocumentos.rdOficio = objProceso.docAdjuntoPr;
                            objRevisionDocumentos.idRevisionDocumento = objProceso.idRevDocumento1ra;
                            objRevisionDocumentos.idRevisionTipoDocumento = 1;
                            objRevisionDocumentos.rdObservaciones = objProceso.rdObservaciones;
                            objRevisionDocumentos.idEmpleadoAsignacion = Convert.ToInt16(hfIdEmpleadoAsignacion.Value);

                            clsDatRevision.insertarRevisionDocumentos(objRevisionDocumentos, (clsEntSesion)Session["objSession" + Session.SessionID]);

                            clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                            objEmpleado.idRevision = objRevision.idRevision;
                            Session["objEmpleado" + Session.SessionID] = objEmpleado;
                        }
                    }
                    //FIN PRIMER PROCESO--------------------------

                    //CANCELACION----------------------------------

                    if (erroCancelacion == 0 && lstProceso[0].idCancelacion == 0 && lstProceso[0].fechaCancelacion != "--/--/----")
                    {
              

                            if (lstProceso[0].idRevDocumento1ra != 0) { documentoPorCancelar = lstProceso[0].idRevDocumento1ra; }



                            clsEntRevisionCancelacion objCancelacion = new clsEntRevisionCancelacion();
                            objCancelacion.idRevisionDocumento = documentoPorCancelar;
                            objCancelacion.idEmpleado = new Guid(hfIdEmpleado.Value);
                            if (lstProceso[0].fechaCancelacion != "--/--/----") { objCancelacion.rcFechaCancelacion = Convert.ToDateTime(lstProceso[0].fechaCancelacion); }
                            objCancelacion.rcObservaciones = lstProceso[0].observaciones;
                            objCancelacion.idRevision = lstProceso[0].idRevision;
                            objCancelacion.idEmpleadoAsignacion =Convert.ToInt16(hfIdEmpleadoAsignacion.Value);
                            clsDatRevision.insertarCancelacion(objCancelacion, (clsEntSesion)Session["objSession" + Session.SessionID]);

                            clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                            objEmpleado.idRevision = 0;
                            Session["objEmpleado" + Session.SessionID] = objEmpleado;
                        
                    }
                    //FIN CANCELACION------------------------------






                    lstProceso = null;
                    lstRenuncia = null;
                    oficio = null;
                    sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));

                }
                else
                {
                    if (error == 5)
                    {
                        lstProceso = null;
                        lstRenuncia = null;
                        oficio = null;
                        sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
                    }
                    else
                    {
                        divErrorRevision.Visible = true;
                        lblerrorRevision.Visible = true;
                    }

                }


            }
            catch (Exception ex)
            {
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
            }

            finally
            {
                if (error == 0 || error == 5)
                {
                    Response.Redirect(sbUrl.ToString());
                }
            }

        }
        else
        {
            divErrorRevision.Visible = true;
            lblerrorRevision.Visible = true;
        }
        
        
    }

    protected void grvBusqueda_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = grvBusqueda.Rows[e.RowIndex];
        TableCell id = row.Cells[0];
        Control objP = id.FindControl("idPersonar");
        Label lbl = (Label)objP;


        hfIndex.Value = (Convert.ToInt32(lbl.Text) - 1).ToString();

        switch(Convert.ToInt32(hfColumna.Value))
        {
            case 1:



                  popAsignacion.Show();
                  hfNuevo.Value = "Update";
                  clsEntActaAdministrativa objProceso = lstProceso[Convert.ToInt32(hfIndex.Value)];



                 if (objProceso.idCancelacion != 0 && objProceso.idRevDocumento1ra!=0)
                  {
                      deshabilitaPrimerActa();

                      deshabilitaCancelacion();

                
              
                      ckbPrimerActa.Checked = false;
                      ckbCancelado.Checked = true;

                  }
                  else
                  {
                      if (objProceso.idRevDocumento1ra != 0) { deshabilitaPrimerActa(); deshabilitaCancelacion(); ckbCancelado.Enabled = true;  ckbCancelado.Checked = false;  } else { habilitaPrimerActa(); deshabilitaCancelacion();  }
                      if (objProceso.idRevDocumento1ra != 0) {  deshabilitaCancelacion();}
                  
                  }

                  if (lstProceso.Count != 0)
                  {
           
                      if (objProceso.revVigente == true) { btnAgregarIndex.Enabled = true; } else {  btnAgregarIndex.Enabled = false; }
                  }


                  if (objProceso.idCancelacion == 0 && objProceso.fechaCancelacion != "--/--/----" && objProceso.idRevDocumento1ra!=0) { habilitaCancelacion(); ckbCancelado.Checked = true;  }
                  if (ckbPrimerActa.Enabled == true) { btnAgregarIndex.Enabled = true; }







                  txbFechaPrimerActa.Text = objProceso.fechaActaPr;
                  txbFechaOficioPrimerActa.Text = objProceso.fechaOficioPr;
                  txbNoOficioPrimerActa.Text = objProceso.NoOficioPr;
                  txbObservacionesActa.Text = objProceso.rdObservaciones;

                  

                  if (objProceso.fechaCancelacion == "--/--/----") { objProceso.fechaCancelacion = ""; }
                  else
                  { txbFechaCancelacion.Text = objProceso.fechaCancelacion; }
                  txbObservaciones.Text = objProceso.observaciones;

                  popAsignacion.Show();


       

                  if (txbFechaCancelacion.Text.Trim() != "" && txbFechaCancelacion.Enabled == false)
                  {
                      deshabilitaCancelacion();
                      btnAgregarIndex.Enabled = false;
                  }

                  if (txbFechaPrimerActa.Text.Trim()!="") { ckbPrimerActa.Checked = true; }
                  if (txbFechaCancelacion.Text.Trim() != "") { ckbCancelado.Checked = true; }
                
            break;

            case 2:
            popAgregarOficioIndex.Show();
            break;

            case 3:
            popAgregarOficioIndex.Show();
            break;

            case 4:
                  Session["Oficio" + Session.SessionID] = null;
                  clsEntActaAdministrativa objActaPrimer= lstProceso[e.RowIndex];
                  Session["Oficio" + Session.SessionID] = objActaPrimer.docAdjuntoPr;
                  if (objActaPrimer.docAdjuntoPr != null)
                  {
                      ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "oficio", "window.open('../Generales/verArchivo.ashx?idSession=Oficio');", true);
                  }
            break;

            case 6:
            clsEntActaAdministrativa objProcesoDelete = lstProceso[e.RowIndex];
                    objProcesoDelete.docAdjuntoPr = null;
                    lstProceso[e.RowIndex] = objProcesoDelete;
                    escribe();
            break;
            case 7:
                    clsEntActaAdministrativa objProcesosDelete = lstProceso[e.RowIndex];
           
                    lstProceso[e.RowIndex] = objProcesosDelete;
                    escribe();

            break;
                
        }



        if (lblFechaBaja.Text != "--/--/----")
        {
            if (Convert.ToDateTime(lblFechaBaja.Text) <= DateTime.Now)
            {
                deshabilitaPrimerActa();
                deshabilitaCancelacion();
            }
        }

        if (hfPermiso.Value == "0")
        {
            deshabilitaPrimerActa();
            deshabilitaCancelacion();
        }



     
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            if (objSesion.usuario.Perfil.IdPerfil != 1)
            {
                deshabilitaCancelacion();
            }
            else
            {
                if (txbFechaCancelacion.Text.Trim() == string.Empty)
                {
                    habilitaCancelacion();
                }
            }

            if (ckbPrimerActa.Enabled == true)
            {
                deshabilitaCancelacion();
            }


    }

    public void limpiarforma()
    {
        txbFechaPrimerActa.Text = string.Empty;
        txbNoOficioPrimerActa.Text = string.Empty;
        txbFechaOficioPrimerActa.Text = string.Empty;
       
        txbFechaCancelacion.Text = string.Empty;
        txbObservaciones.Text = string.Empty;

        deshabilitaCancelacion();
        habilitaPrimerActa();
       
    }

    public void habilitaPrimerActa()
    {
        ckbPrimerActa.Enabled=true;
        txbFechaPrimerActa.Enabled=true;
        imbPrimerActa.Enabled=true;
        txbNoOficioPrimerActa.Enabled=true;
        txbFechaOficioPrimerActa.Enabled=true;
        imbPrimerActaOficio.Enabled = true;
        txbObservacionesActa.Enabled = true;
        txbFechaPrimerActa.Text = string.Empty;
        txbObservacionesActa.Text = string.Empty;
    }

    public void deshabilitaPrimerActa()
    {
        ckbPrimerActa.Enabled = false;
        txbFechaPrimerActa.Enabled = false;
        imbPrimerActa.Enabled = false;
        txbNoOficioPrimerActa.Enabled = false;
        txbFechaOficioPrimerActa.Enabled = false;
        imbPrimerActaOficio.Enabled = false;
        txbObservacionesActa.Enabled = false;
    }

 



    public void habilitaCancelacion()
    { 
        txbFechaCancelacion.Enabled=true;
        imbCancelacion.Enabled=true;
        txbObservaciones.Enabled = true;
        ckbCancelado.Enabled = true;
  

    }

    public void deshabilitaCancelacion()
    {
        txbFechaCancelacion.Enabled = false;
        imbCancelacion.Enabled = false;
        txbObservaciones.Enabled = false;
        ckbCancelado.Enabled = false;
        ckbCancelado.Checked = false;
    }

    public void escribe()
        {
            GridViewRow row =  grvBusqueda.Rows[0];
            TableCell id = row.Cells[0];
            Control objP = id.FindControl("idPersonar");
            Label lbl = (Label)objP;

            int inicio = 0;
            int iter=Convert.ToInt32(lbl.Text) - 1;
            int fin = inicio + grvBusqueda.Rows.Count;

            do
            {


                if (lblFechaBaja.Text == "--/--/----" || hfPermiso.Value=="1")
                {
                    if (lstProceso[iter].docAdjuntoPr != null)
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbOficio1raVerifica");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/Symbol-Check.png";
                        primerActa.Enabled = true;
                    }
                    else
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbOficio1raVerifica");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/novalidado.png";
                        primerActa.Enabled = false;
                    }




                    if (lstProceso[iter].idRevDocumento1ra == 0 && lstProceso[iter].fechaActaPr!="--/--/----")
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbOficio1er");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/agregar.png";
                        primerActa.Enabled = true;
                    }
                    else
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbOficio1er");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/agregarDesHabilitado.png";
                        primerActa.Enabled = false;
                    }



                    if (lstProceso[iter].idCancelacion != 0)
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbOficio1er");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/agregarDesHabilitado.png";
                        primerActa.Enabled = false;
                    }




              



                  




        



                    if (lstProceso[iter].idRevDocumento1ra == 0 && lstProceso[iter].docAdjuntoPr != null)
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbCancelar1er");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/Symbol-Delete.png";
                        primerActa.Enabled = true;
                    }
                    else
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbCancelar1er");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/errorDeshabilitado.png";
                        primerActa.Enabled = false;
                    }

                    if (lstProceso[iter].idRevDocumento1ra != 0)
                    {
                        GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                        TableCell idP = rowLeer.Cells[2];
                        Control obj = idP.FindControl("imbCancelar1er");
                        ImageButton primerActa = (ImageButton)obj;
                        primerActa.ImageUrl = "~/Imagenes/errorDeshabilitado.png";
                        primerActa.Enabled = false;
                    }




       

                   
                }
                else
                {
                    GridViewRow rowLeer = grvBusqueda.Rows[inicio];
                    TableCell idP = rowLeer.Cells[2];
                    Control obj = idP.FindControl("imbOficio1er");
                    ImageButton primerActa = (ImageButton)obj;
                    primerActa.ImageUrl = "~/Imagenes/agregarDesHabilitado.png";
                    primerActa.Enabled = false;

                    if (lstProceso[iter].docAdjuntoPr != null)
                    {
                        GridViewRow rowLeerPR = grvBusqueda.Rows[inicio];
                        TableCell idPPR = rowLeerPR.Cells[2];
                        Control objPR = idPPR.FindControl("imbOficio1raVerifica");
                        ImageButton primerActaPR = (ImageButton)objPR;
                        primerActaPR.ImageUrl = "~/Imagenes/Symbol-Check.png";
                        primerActaPR.Enabled = true;
                    }
                    else
                    {
                        GridViewRow rowLeerPR = grvBusqueda.Rows[inicio];
                        TableCell idPPR = rowLeerPR.Cells[2];
                        Control objPR = idPPR.FindControl("imbOficio1raVerifica");
                        ImageButton primerActaPR = (ImageButton)objPR;
                        primerActaPR.ImageUrl = "~/Imagenes/novalidado.png";
                        primerActaPR.Enabled = false;
                    }

                    GridViewRow rowLeerCa = grvBusqueda.Rows[inicio];
                    TableCell idPCa = rowLeerCa.Cells[2];
                    Control objCa = idPCa.FindControl("imbCancelar1er");
                    ImageButton primerActaCa = (ImageButton)objCa;
                    primerActaCa.ImageUrl = "~/Imagenes/errorDeshabilitado.png";
                    primerActaCa.Enabled = false;

                    //GridViewRow rowLeerOfi = grvBusqueda.Rows[inicio];
                    //TableCell idPOfi = rowLeerOfi.Cells[4];
                    //Control objOfi = idPOfi.FindControl("imbOficio2da");
                    //ImageButton primerActaOfi = (ImageButton)objOfi;
                    //primerActaOfi.ImageUrl = "~/Imagenes/agregarDesHabilitado.png";
                    //primerActaOfi.Enabled = false;

                   

                    //GridViewRow rowLeerCac = grvBusqueda.Rows[inicio];
                    //TableCell idPCac = rowLeerCac.Cells[4];
                    //Control objCac = idPCac.FindControl("imbCancelar2da");
                    //ImageButton primerActaCac = (ImageButton)objCac;
                    //primerActaCac.ImageUrl = "~/Imagenes/errorDeshabilitado.png";
                    //primerActaCac.Enabled = false;



               
                }






                inicio++;
               iter++;
            } while (inicio < fin);


        }

    protected void imbVerPdf_Click(object sender, ImageClickEventArgs e)
    {
        imbVerPdf.ImageUrl = "~/Imagenes/Symbol-Check.png";
        imbVerPdf.Enabled = true;
        if (lstRenuncia.Count != 0)
        {
            clsEntRenuncia objRenuncia = lstRenuncia[0];

            if (objRenuncia.oficioAdjunto != null)
            {
                Session["Oficio" + Session.SessionID] = objRenuncia.oficioAdjunto;
                ClientScript.RegisterClientScriptBlock(GetType(), "oficio", "window.open('../Generales/verArchivo.ashx?idSession=Oficio');", true);

            }
        }
   

    }


    public void cargaRenuncia(Guid idEmpleado)
    {
        DataSet dsRevision = new DataSet("dsBuscar");
        clsDatRevision.buscarRenuncia(idEmpleado, ref dsRevision, (clsEntSesion)Session["objSession" + Session.SessionID]);
        int registros = dsRevision.Tables[0].Rows.Count;
        if (registros != 0)
        {


            
            ckbRenuncia.Checked = true;
            
            txbNoOficio.Text = (String)dsRevision.Tables[0].Rows[0]["renNoOficio"];
            txbObs.Text = (String)dsRevision.Tables[0].Rows[0]["renObservaciones"];
            hfIdRenuncia.Value = (dsRevision.Tables[0].Rows[0]["idRenuncia"]).ToString();


            clsEntRenuncia objRenuncia = new clsEntRenuncia();


            object objFeOfi = (object)dsRevision.Tables[0].Rows[0]["renFechaOficio"]; if (objFeOfi != null && !(objFeOfi is System.DBNull)) { objRenuncia.fechaOficio = Convert.ToDateTime(dsRevision.Tables[0].Rows[0]["renFechaOficio"]); txbFechaOficio.Text =(Convert.ToDateTime(dsRevision.Tables[0].Rows[0]["renFechaOficio"])).ToShortDateString(); }
            object objFeRen = (object)dsRevision.Tables[0].Rows[0]["renFechaRenuncia"];

            if (objFeRen != null && !(objFeRen is System.DBNull)) { objRenuncia.fechaRenuncia = Convert.ToDateTime(dsRevision.Tables[0].Rows[0]["renFechaRenuncia"]); txbFechaRenuncia.Text = (Convert.ToDateTime(dsRevision.Tables[0].Rows[0]["renFechaRenuncia"])).ToShortDateString(); }
            
            objRenuncia.noOficio = (String)dsRevision.Tables[0].Rows[0]["renNoOficio"];
            objRenuncia.observaciones = (String)dsRevision.Tables[0].Rows[0]["renObservaciones"];
            object obj = (object)dsRevision.Tables[0].Rows[0]["renOficio"];
           
            lstRenuncia.Add(objRenuncia);
            
            if (obj != null && !(obj is System.DBNull))
            { objRenuncia.oficioAdjunto = (byte[])dsRevision.Tables[0].Rows[0]["renOficio"];

            imbVerPdf.ImageUrl = "~/Imagenes/Symbol-Check.png";
            imbVerPdf.Enabled = true;
            imbAgregarPdf.ImageUrl = "~/Imagenes/agregarDesHabilitado.png";
            imbAgregarPdf.Enabled = false;
 
            }
            else
            {
               imbAgregarPdf.ImageUrl="~/Imagenes/agregarDesHabilitado.png";
               imbVerPdf.ImageUrl = "~/Imagenes/novalidado.png";
               imbVerPdf.Enabled = false;
              
           
            }
 
            desHabilitaRenuncia();
        }
        else
        {
            desHabilitaRenuncia();
            ckbRenuncia.Enabled = true;
  
        }
    }

    public void cargaLista(Guid idEmpleado)
    {
        DataSet dsRevision = new DataSet("dsBuscar");
        clsDatRevision.buscarRevision(idEmpleado, ref dsRevision, (clsEntSesion)Session["objSession" + Session.SessionID]);
        int registros = dsRevision.Tables[0].Rows.Count;
        if (registros != 0)
        {

            grvBusqueda.DataSource = dsRevision;
            grvBusqueda.DataBind();
            int inicial = 0;
            do
            {
                clsEntActaAdministrativa objActa = new clsEntActaAdministrativa();
                objActa.idEmpleado = (Guid)dsRevision.Tables[0].Rows[inicial]["idEmpleado"];
                objActa.idRevision = (Int32)dsRevision.Tables[0].Rows[inicial]["idRevision"];
                objActa.idServicio = (Int32)dsRevision.Tables[0].Rows[inicial]["idServicio"];
                objActa.idInstalacion = (Int32)dsRevision.Tables[0].Rows[inicial]["idInstalacion"];
                objActa.desServicio = (String)dsRevision.Tables[0].Rows[inicial]["desServicio"];
                objActa.desInstalacion = (String)dsRevision.Tables[0].Rows[inicial]["desInstalacion"];
                objActa.idRevDocumento1ra = (Int32)dsRevision.Tables[0].Rows[inicial]["idRevDocumento1ra"];
                objActa.fechaActaPr = (String)dsRevision.Tables[0].Rows[inicial]["fechaActaPr"];
                objActa.NoOficioPr = (String)dsRevision.Tables[0].Rows[inicial]["NoOficioPr"];
                objActa.fechaOficioPr = (string)dsRevision.Tables[0].Rows[inicial]["fechaOficioPr"];
                object objPr = (object)dsRevision.Tables[0].Rows[inicial]["docAdjuntoPr"];
                if (objPr != null && !(objPr is System.DBNull)) { objActa.docAdjuntoPr = (byte[])dsRevision.Tables[0].Rows[inicial]["docAdjuntoPr"]; }
                objActa.idCancelacion = (Int32)dsRevision.Tables[0].Rows[inicial]["idCancelacion"];
                objActa.observaciones = (String)dsRevision.Tables[0].Rows[inicial]["observaciones"];
                objActa.fechaCancelacion = (String)dsRevision.Tables[0].Rows[inicial]["fechaCancelacion"];
                objActa.revVigente = (Boolean)dsRevision.Tables[0].Rows[inicial]["revVigente"];
                objActa.rdObservaciones = (String)dsRevision.Tables[0].Rows[inicial]["rdObservaciones"];

                
                lstProceso.Add(objActa);

                inicial++;
            } while (inicial < registros);


            if (registros != 0)
            {
                escribe();
            }

        }
    }

    public void desHabilitaRenuncia()
    {
        ckbRenuncia.Enabled=false;
        txbFechaRenuncia.Enabled=false;
        txbObs.Enabled=false;
        imbFechaRenuncia.Enabled=false;
        txbNoOficio.Enabled=false;
        txbFechaOficio.Enabled=false;
        txbObs.Enabled=false;
        imbAgregarPdf.Enabled = false;
        imbFechaRenuncia.Enabled = false;
        imbOficio.Enabled = false;
    }
    public void habilitaRenuncia()
    {
        ckbRenuncia.Enabled = true;
        txbFechaRenuncia.Enabled = true;
        txbObs.Enabled = true;
        imbFechaRenuncia.Enabled = true;
        txbNoOficio.Enabled = true;

        txbObs.Enabled = true;
        imbAgregarPdf.Enabled = true;
     imbOficio.Enabled = true;
    }   



    protected void btnAgregarIndex2_Click(object sender, EventArgs e)
    {
        int columna = Convert.ToInt32(hfColumna.Value);
        int renglon = Convert.ToInt32(hfIndex.Value);


        if (hfGuardar.Value == "1")
        {
            hfGuardar.Value = "-1";
            popAgregarOficioIndex.Hide();


                    if (renglon == -1 && renglon == -1)
                    {

                        clsEntRenuncia objRenuncia = new clsEntRenuncia();
                        objRenuncia.oficioAdjunto = oficio;

                        if (lstRenuncia.Count == 0)
                        {
                            lstRenuncia.Add(objRenuncia);
                        }
                        else
                        {
                            lstRenuncia[0] = objRenuncia;
                        }

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok()", true);
                   



                    }
                    else
                    {
                        clsEntActaAdministrativa objProceso = lstProceso[renglon];
                        switch (columna)
                        {
                            case 2:
                                objProceso.docAdjuntoPr = oficio;
                            break;

                            


                            break;

                        }

                            lstProceso[renglon] = objProceso;
                            grvBusqueda.DataSource = lstProceso;
                            grvBusqueda.DataBind();
                            escribe();

   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);
                    }
            //GUARDO

                 
        }
                
    }
    protected void btnCancelarIndex_Click(object sender, EventArgs e)
    {
        popAgregarOficioIndex.Hide();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ok", "ok1()", true);
       oficio=null;
    }

    public void AsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (AsyncFileUpload.HasFile)
        {
            oficio = AsyncFileUpload.FileBytes;
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lstProceso = null;
        lstRenuncia = null;
        oficio = null;
        string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
        Response.Redirect("~/" + busqueda);
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lstProceso = null;
        lstRenuncia = null;
        oficio = null;
        Session["objEmpleado" + Session.SessionID] = null;

        Response.Redirect("~/frmInicio.aspx");
    }
    protected void imbAgregarPdf_Click(object sender, ImageClickEventArgs e)
    {

    }
}