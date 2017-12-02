using System;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using System.Text;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using proUtilerias;

public partial class Personal_frmAsignacionesMasivas : System.Web.UI.Page
{
    static private List<clsEntFirmanteOficioAsignacion> listaFirmantes = new List<clsEntFirmanteOficioAsignacion>();
    static private List<clsEntGuardarMasiva> lstMasivo = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        divError.Visible = false;
        divErrorAsignación.Visible = false;
        inconsistencias();

        if (!IsPostBack)
        {
            Session["impresion" + Session.SessionID] = null;
            lstMasivo = null;

            if (lstMasivo == null)
            {
                lstMasivo = new List<clsEntGuardarMasiva>();
            }




        ListBox1.Attributes.Add("onscroll", "javascript:scrollList2(this);");
        ListBox4.Attributes.Add("onscroll", "javascript:scrollList2(this);");
        ListBox5.Attributes.Add("onscroll", "javascript:scrollList2(this);");
        ListBox6.Attributes.Add("onscroll", "javascript:scrollList2(this);");
        ListBox2.Attributes.Add("onscroll", "javascript:scrollList1(this);");
        ListBox7.Attributes.Add("onscroll", "javascript:scrollList1(this);");
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAsignacionLimitada", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicioAsignacion, "catalogo.spLeerServiciosPorUsuarioAsignacion", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);         
           // clsCatalogos.llenarCatalogoTipoHorario(ddlTipoHorario, "catalogo.spLeerTipoHorario", "thDescripcion", "horario", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlFuncion, "catalogo.spLeerFuncionAsignacion", "faDescripcion", "idFuncionAsignacion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlJerarquia, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);

         
        }
    }

    protected void limpiaCampos()
    {
        imbFechaBaja.Enabled = true;
        txbFechaBaja.Enabled = true;
        txbFechaInicioComision.Enabled = true;
        imbFechaInicioComision.Enabled = true;
        txbFechaInicioComision.Text = "";
        txbFechaBaja.Text = "";
        ListBox1.Items.Clear();
        ListBox2.Items.Clear();
        lblParcial.Text = "0";
        lblInconsistencias.Text = "0";
        lblAgregados.Text = "0";
        txbNumero.Text = "";
        txbBaja.Text = "";
        txbCuip.Text = "";
        txbCurp.Text = "";
        txbIngreso.Text = "";
        txbMaterno.Text = "";
        txbPaterno.Text = "";
        txbRfc.Text = "";
        txbNacimiento.Text = "";
        txbNombre.Text = "";
        ddlTipoHorario.SelectedIndex = -1;
        txbFechaInicioHorario.Text = "";
        txbFechaInicioComision.Text = "";
        txbFechaBaja.Text = "";
        ddlFuncion.SelectedIndex = -1;
        ddlServicioAsignacion.SelectedIndex = -1;
        ddlInstalacionAsignacion.SelectedIndex = -1;
        ddlServicio.SelectedIndex = -1;
        ddlServicio_SelectedIndexChanged(null, null);
        ddlInstalacion.SelectedIndex = -1;
        rbtTodos.Checked = false;
        txbCartilla.Text = "";
        rblCurso.SelectedIndex = 2;
        rblLOC.SelectedIndex = 2;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

        lblerror.Text = string.Empty;
        divError.Visible = divErrorAsignación.Visible = false;
        
        DateTime FechaInicial;
        DateTime FechaFinal;
        bool fechaIni = ValidaFecha.ValidaFe(txbFechaInicioComision.Text);
        bool fechaFin = ValidaFecha.ValidaFe(txbFechaBaja.Text);
        bool fechaHor = ValidaFecha.ValidaFe(txbFechaInicioHorario.Text);
        string mensaje = string.Empty;

        if (fechaIni != true) { mensaje = "La fecha de inicio de comisión es invalida"; }
        if (fechaFin != true && mensaje == string.Empty) { if (txbFechaBaja.Text.Trim() != "") { mensaje = "La fecha de fin de comisión es invalida"; } else { mensaje = string.Empty; } }
        if (fechaHor != true && mensaje == string.Empty) { mensaje = "La fecha de inicio de horario es invalida"; }



        if (mensaje == string.Empty && txbFechaBaja.Text.Trim() != "")
        {
            FechaInicial = Convert.ToDateTime(txbFechaInicioComision.Text);
            FechaFinal = Convert.ToDateTime(txbFechaBaja.Text);
            if (FechaInicial > FechaFinal) { mensaje = "La fecha de inicio de comisión debe ser menor a la fecha de fin de comisión"; }
        }

    

        if (txbFechaBaja.Text.Trim() == "")
        {
            mensaje = string.Empty;
        }


        if (mensaje == string.Empty)
        {
            if (txbFechaBaja.Text.Trim() != "")
            {
                txbFechaBaja.Enabled = false;
                imbFechaBaja.Enabled = false;
            }
            txbFechaInicioComision.Enabled = false;
            imbFechaInicioComision.Enabled = false;

            int inconsistencias = 0;
            ListBox1.Items.Clear();
            ListBox4.Items.Clear();
            ListBox5.Items.Clear();
            ListBox6.Items.Clear();
            clsEntEmpleado objBuscar = new clsEntEmpleado();
            objBuscar.EmpleadoAsignacion2 = new clsEntEmpleadoAsignacion();
            objBuscar.EmpleadoAsignacion2.Servicio = new clsEntServicio();
            objBuscar.EmpleadoAsignacion2.Instalacion = new clsEntInstalacion();

            DataSet dsBuscar = new DataSet("dsBuscar");

            DateTime dtNacimiento = new DateTime();
            DateTime dtIngreso = new DateTime();
            DateTime dtBaja = new DateTime();
            DateTime dtInicioCom = new DateTime();
            DateTime dtFechaFinAsignacion = new DateTime();

            if (txbNacimiento.Text.Trim() != "") DateTime.TryParse(txbNacimiento.Text.Trim(), out dtNacimiento);
            if (txbIngreso.Text.Trim() != "") DateTime.TryParse(txbIngreso.Text.Trim(), out dtIngreso);
            if (txbBaja.Text.Trim() != "") DateTime.TryParse(txbBaja.Text.Trim(), out dtBaja);
            if (txbFechaInicioComision.Text.Trim() != "") DateTime.TryParse(txbFechaInicioComision.Text.Trim(), out dtInicioCom);
            if (txbFechaBaja.Text.Trim() != "") { DateTime.TryParse(txbFechaBaja.Text.Trim(), out dtFechaFinAsignacion); }

            objBuscar.fechaFinAsignacion = dtFechaFinAsignacion;
            objBuscar.EmpPaterno = txbPaterno.Text.Trim();
            objBuscar.EmpMaterno = txbMaterno.Text.Trim();
            objBuscar.EmpNombre = txbNombre.Text.Trim();
            if (!string.IsNullOrEmpty(txbNumero.Text)) objBuscar.EmpNumero = Convert.ToInt32(txbNumero.Text);
            objBuscar.EmpFechaNacimiento = dtNacimiento;
            objBuscar.EmpFechaIngreso = dtIngreso;
            objBuscar.EmpFechaBaja = dtBaja;
            objBuscar.EmpRFC = txbRfc.Text.Trim();
            objBuscar.EmpCURP = txbCurp.Text.Trim();
            objBuscar.EmpCUIP = txbCuip.Text.Trim();
            objBuscar.EmpCartilla = txbCartilla.Text.Trim();
            objBuscar.EmpLOC = Convert.ToInt16(rblLOC.SelectedValue);
            objBuscar.EmpCurso = Convert.ToInt16(rblCurso.SelectedValue);
            objBuscar.fechaIniCom = dtInicioCom;

            if (rbtTodos.Checked == true) { objBuscar.tipo = 3; }
            if (rbtActivos.Checked == true) { objBuscar.tipo = 1; }
            if (rbtInactivos.Checked == true) { objBuscar.tipo = 2; }

            if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
            {
                objBuscar.EmpleadoAsignacion2.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddlInstalacion.SelectedValue))
            {
                objBuscar.EmpleadoAsignacion2.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddlJerarquia.SelectedValue))
            {
                objBuscar.IdJerarquia = Convert.ToInt32(ddlJerarquia.SelectedValue);
            }

            string spBuscarEmpleado = null;
            spBuscarEmpleado = "empleado.spBuscarEmpleadoMasivo";

            clsDatEmpleado.buscarEmpleado(objBuscar, spBuscarEmpleado, ref dsBuscar, (clsEntSesion)Session["objSession" + Session.SessionID]);

            int registros = dsBuscar.Tables[0].Rows.Count;

            int inicial = 0;
            int erro = 0;
            int lstIntegrantes = (ListBox2.Items.Count) - 1;

            if (registros != 0)
            {
                int cont;
                do
                {
                    if (ListBox2.Items.Count != 0)
                    {
                        cont = 0;
                        do
                        {
                            string strCadena = ListBox2.Items[cont].Value;
                            string[] valores = strCadena.Split('&');

                            if (Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleado"] + "&" + Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["procedente"]) + "&" + Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleadoPuesto"]) + "&" + Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idPuesto"])) == valores[0] + "&" +valores[1] + "&" +valores[2] + "&" +valores[3])
                            {
                                erro = 1;
                                cont = lstIntegrantes + 1;
                            }
                            else
                            {
                                erro = 0;
                            }
                            cont++;
                        } while (cont <= lstIntegrantes);
                    }

                    if (erro != 1)
                    {
                       
                        

                        ListBox1.Items.Add(new ListItem(Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empPaterno"]) + " " + dsBuscar.Tables[0].Rows[inicial]["empMaterno"] + " " + dsBuscar.Tables[0].Rows[inicial]["empNombre"]
                            , Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleado"] + "&" + 
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["procedente"]) + "&" + 
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleadoPuesto"]) + "&" + 
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idPuesto"] +"&"+
                              (Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["serDescripcion"]) + "-" + 
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["insNombre"])+"&"+
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["OBSERVACIONES"])+"&"+
                              (Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empNumero"])) +"&"+
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empPaterno"]) + "&" + 
                              dsBuscar.Tables[0].Rows[inicial]["empMaterno"] + "&" + 
                              dsBuscar.Tables[0].Rows[inicial]["empNombre"] 
                              +"&"+
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empPaterno"]) + " " + dsBuscar.Tables[0].Rows[inicial]["empMaterno"] + " " + dsBuscar.Tables[0].Rows[inicial]["empNombre"]
                                    + "&" + (Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["serDescripcion"]))
                                 + "&" + Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["insNombre"])
                              
                              )))));
                        
                        
                        
                        
                        
                        if((Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empNumero"]))!="")
                        {
                  
                            ListBox4.Items.Add(new ListItem(Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empNumero"]), Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleado"])

                                + "&" +
                              Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["empPaterno"]) + " " + dsBuscar.Tables[0].Rows[inicial]["empMaterno"] + " " + dsBuscar.Tables[0].Rows[inicial]["empNombre"]
                          
                                ));
                        }
                        else
                        {
                            ListBox4.Items.Add(new ListItem("--------",Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleado"])));
                        }
                        
                        
                        
                        
                        
                        
                        
                        
                        ListBox5.Items.Add(new ListItem(Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["serDescripcion"]) + "-" + Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["insNombre"]),Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleado"])));
                        ListBox6.Items.Add(new ListItem(Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["OBSERVACIONES"]), Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["idEmpleado"])));


                

                        if (Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["procedente"]) == "NO")
                        {
                            int itemConteo = (ListBox1.Items.Count) - 1;
                            ListBox1.Items[itemConteo].Attributes.Add("Style", "color:Red;");
                          
                            ListBox4.Items[itemConteo].Attributes.Add("Style", "color:Red;");
                            ListBox5.Items[itemConteo].Attributes.Add("Style", "color:Red;");
                            ListBox6.Items[itemConteo].Attributes.Add("Style", "color:Red;");
                            inconsistencias++;
                        }

                        if (Convert.ToString(dsBuscar.Tables[0].Rows[inicial]["procedente"]) == "SIREN")
                        {
                            int itemConteo = (ListBox1.Items.Count) - 1;
                            ListBox1.Items[itemConteo].Attributes.Add("Style", "color:Blue;");
                           
                            ListBox4.Items[itemConteo].Attributes.Add("Style", "color:Blue;");
                            ListBox5.Items[itemConteo].Attributes.Add("Style", "color:Blue;");
                            ListBox6.Items[itemConteo].Attributes.Add("Style", "color:Blue;");
                        
                        }

                    }

                    inicial++;
                } while (inicial < registros);
            }

            ViewState["dsBuscar"] = dsBuscar;
            ViewState["pagina"] = 0;
            lblParcial.Text = ((ListBox1.Items.Count) - inconsistencias).ToString();
            lblAgregados.Text = Convert.ToString(ListBox2.Items.Count);
            lblInconsistencias.Text = inconsistencias.ToString();

            dsBuscar.Dispose();
            ListBox1.Dispose();
            ListBox4.Dispose();
            ListBox5.Dispose();
            ListBox6.Dispose();
        }
        else 
        {

            divErrorAsignación.Visible = true;
            lblerrorAsignacion.Text = mensaje;
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        trParametros.Visible = true;

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
    {
        txbPaterno.Text=string.Empty;
        txbNacimiento.Text=string.Empty;
        txbCartilla.Text=string.Empty;
        txbNumero.Text=string.Empty;
        txbRfc.Text=string.Empty;
        txbMaterno.Text=string.Empty;
        txbIngreso.Text=string.Empty;
        txbCuip.Text=string.Empty;
        txbCurp.Text=string.Empty;
        txbNombre.Text=string.Empty;
        txbBaja.Text=string.Empty;
        rblCurso.SelectedValue="2";
        rblLOC.SelectedValue="2";
        ddlJerarquia.SelectedIndex=0;
        ddlServicio.SelectedIndex = 0;

        if (ddlInstalacion.Items.Count != 0)
        {
            ddlInstalacion.SelectedIndex = 0;
            ddlInstalacion.Items.Clear();
        }

        rbtActivos.Checked = true;
        rbtInactivos.Checked = false;
        rbtTodos.Checked = false;
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlServicioAsignacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicioAsignacion.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacionAsignacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacion", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicioAsignacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
         
            ddlInstalacionAsignacion.Enabled = true;


 



            return;
        }
        ddlInstalacionAsignacion.Items.Clear();
        ddlInstalacionAsignacion.Enabled = false;

       

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ListBox1.Items.Count != 0)
        {
            int conteo = ListBox1.Items.Count;
            int inicial=0;
            while (inicial < conteo)
            {
                ListBox1.Items[inicial].Selected = true;
                ListBox4.Items[inicial].Selected = true;
                ListBox5.Items[inicial].Selected = true;
                ListBox6.Items[inicial].Selected = true;
                inicial++;
            }
       
          
        }
      

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (ListBox2.Items.Count != 0)
        {
            int conteo = ListBox2.Items.Count;
            int inicial = 0;
            while (inicial < conteo)
            {
                ListBox2.Items[inicial].Selected = true;
                ListBox7.Items[inicial].Selected = true;

                inicial++;
            }
        }

    }

    protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

        ListItem[] delete;
        int contador = 0;
        delete = new ListItem[ListBox2.Items.Count + 1];

        if (ListBox2.Items.Count != 0)
        {
            foreach (ListItem ite in ListBox2.Items)
            {
                if (ite.Selected == true)
                {
                    delete[contador] = ite;
                    contador++;
                }
            }

            foreach (ListItem arreglo in delete)
            {
                ListBox2.Items.Remove(arreglo);
            }

            if (ListBox2.Items.Count != 0)
            {
                lblParcial.Text = ListBox1.Items.Count.ToString();
                lblAgregados.Text = ListBox2.Items.Count.ToString();
            }
        }
    }


    private void eliminaLista1()
    {
        if (ListBox1.Items.Count != 0)
        {
            int contador = (ListBox1.Items.Count) - 1;
            do
            {
                if (ListBox1.Items[contador].Selected == true)
                {
                    ListBox1.Items.Remove(ListBox1.Items[contador]);
                    ListBox4.Items.Remove(ListBox4.Items[contador]);
                    ListBox5.Items.Remove(ListBox5.Items[contador]);
                    ListBox6.Items.Remove(ListBox6.Items[contador]);
                }
                contador = contador - 1;
            } while (contador >= 0);



            lblParcial.Text = ListBox1.Items.Count.ToString();
            lblAgregados.Text = ListBox2.Items.Count.ToString();
        }
    }

    protected void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        eliminaLista1();
    }


    public void inconsistencias()
    {
        int conta = 0;
        foreach (ListItem z in ListBox1.Items)
        {

            string strCadena = z.Value;
            string[] valores = strCadena.Split('&');
            if (valores[1] == "NO")
            {

                ListBox1.Items[conta].Attributes.Add("Style", "color:Red;");
                ListBox4.Items[conta].Attributes.Add("Style", "color:Red;");
                ListBox5.Items[conta].Attributes.Add("Style", "color:Red;");
                ListBox6.Items[conta].Attributes.Add("Style", "color:Red;");
            }
            else
            {
                if (valores[1] == "SIREN")
                {
                    ListBox1.Items[conta].Attributes.Add("Style", "color:Blue;");
                    ListBox4.Items[conta].Attributes.Add("Style", "color:Blue;");
                    ListBox5.Items[conta].Attributes.Add("Style", "color:Blue;");
                    ListBox6.Items[conta].Attributes.Add("Style", "color:Blue;");

                }
            }

                conta++;
            
        }
    }




    protected void imbFechaInicioComision_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

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
                if (fecha == "") { dictamen = true; }
                else
                {
                    dictamen = false;
                }
            }


            return dictamen;


        }

        static public string CalcularBi(int anno)
        {
            string alfa = Convert.ToString((anno % 4 == 0) && !(anno % 100 == 0 && anno % 400 != 0)); return alfa;
        }

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        #region Validaciones
        DateTime FechaInicial;
        DateTime FechaFinal;
        DateTime fechaHorario;
        txbFechaInicioHorario.Text = txbFechaInicioComision.Text;

        bool fechaIni = ValidaFecha.ValidaFe(txbFechaInicioComision.Text);
        bool fechaFin = ValidaFecha.ValidaFe(txbFechaBaja.Text);
        bool fechaHor = ddlTipoHorario.Enabled == true ? ValidaFecha.ValidaFe(txbFechaInicioHorario.Text) : true;
        bool fechaFinHor = ValidaFecha.ValidaFe(txbFinHorario.Text.Trim());
        string mensaje = string.Empty;

        if (ddlTipoHorario.Enabled == true)
        {
            if (ddlTipoHorario.SelectedIndex == 0)
            {
                mensaje = "Debe seleccionar un horario para poder continuar";
            }
        }
 

        if (fechaIni != true) { mensaje = "La fecha de inicio de comisión es invalida"; }
     
        if (fechaFin != true && mensaje == string.Empty) { if (txbFechaBaja.Text.Trim() != "") { mensaje = "La fecha de fin de comisión es invalida"; } else { mensaje = string.Empty; } }
        if (fechaHor != true && mensaje == string.Empty) {
            if (ddlTipoHorario.Enabled == true)
            {
                mensaje = "La fecha de inicio de horario es invalida";
            }
            else
            {
                mensaje = string.Empty;
            }
        }

        if (mensaje == string.Empty && txbFechaBaja.Text.Trim() != "")
        {
            FechaInicial = Convert.ToDateTime(txbFechaInicioComision.Text);
            FechaFinal = Convert.ToDateTime(txbFechaBaja.Text);
            fechaHorario = ddlTipoHorario.Enabled==true? Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()):Convert.ToDateTime("01/01/1901");
            if (FechaInicial > FechaFinal) { mensaje = "La fecha de inicio de comisión debe ser menor a la fecha de fin de comisión"; } else { mensaje = string.Empty; }

        }

        if (mensaje == string.Empty)
        {
            FechaInicial = Convert.ToDateTime(txbFechaInicioComision.Text);

            fechaHorario =ddlTipoHorario.Enabled==true?  Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()) : Convert.ToDateTime("01/01/1901");
            if (fechaHorario >= FechaInicial && mensaje == string.Empty)
            {
                mensaje = string.Empty;
            }
            else
            {
                if (ddlTipoHorario.Enabled == true)
                {
                    mensaje = "La fecha de inicio de horario debe ser mayor o igual a la fecha de inicio de comisión";
                }
                else
                {
                    mensaje = string.Empty;
                }

            }
        }

        if (mensaje == string.Empty)
        {
            if (Convert.ToDateTime(txbFechaInicioComision.Text.Trim()).ToShortDateString() == DateTime.Now.ToShortTimeString())
            {
                mensaje = "La fecha de asignación no puede ser igual a la fecha actual";

            }
            else
            {
                mensaje = string.Empty;
            }
        }


        if (ddlServicioAsignacion.SelectedValue == "179" || ddlServicioAsignacion.SelectedValue == "171")
        {
            if (string.IsNullOrEmpty(txbOficio.Text))
            {
                mensaje = "El no de oficio de obligatorio para poder continuar";
                txbOficio.Focus();
            }
        }
        #endregion


        if (mensaje == string.Empty)
        {
            int item = ListBox2.Items.Count;
            if (item == 0) { mensaje = "0"; }
        }


        #region Validaciones Horario
        if (ddlTipoHorario.Enabled == true)
        {
            if (txbFinHorario.Text.Trim() != "")
            {
                if (Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()) > Convert.ToDateTime(txbFinHorario.Text.Trim()))
                {
                    mensaje = "La fecha de cierre de horario debe ser mayor a la fecha de asignación del horario";
                    divErrorAsignación.Visible = true;
                    lblerrorAsignacion.Text = mensaje;
                    return;
                }
            }
        }

        if (fechaFinHor != true) { mensaje = "La fecha de fin de horario es invalida"; return; }




        #endregion




        if (mensaje == string.Empty)
        {

            List<clsEntGuardarMasiva> lstMasaFinal = new List<clsEntGuardarMasiva>();
            foreach (ListItem z in ListBox2.Items)
            {
                clsEntGuardarMasiva objMasiva = new clsEntGuardarMasiva();
                string strCadena = z.Value;
                string[] valores = strCadena.Split('&');
                objMasiva.servicio = valores[11];
                objMasiva.instalacion = valores[12];
                lstMasaFinal.Add(objMasiva);
            }




            var lst = from bases in lstMasaFinal
                      group new { bases.servicio, bases.instalacion } by new { bases.servicio, bases.instalacion } into g
                      let count = g.Count()
                      orderby count descending
                      select new { SERVICIO = g.Key.servicio, INSTALACION = g.Key.instalacion, Count = count };


            foreach (var lstm in lst)
            {
                clsEntGuardarMasiva objMasivaFinal = new clsEntGuardarMasiva();
                objMasivaFinal.servicio = lstm.SERVICIO;
                objMasivaFinal.instalacion = lstm.INSTALACION;
                objMasivaFinal.cantidad = Convert.ToString(lstm.Count);
                lstMasivo.Add(objMasivaFinal);
            }



            lblPagina.Visible = true;
            lblPaginas.Visible = true;
            lblde.Visible = true;

            lblPagina.Text = "1";
            lblPaginas.Text = (grvBusqueda.PageIndex + 1).ToString();

            if (lstMasivo.Count > 6)
            {
                imgAdelante.Visible = true;
                imgAtras.Visible = true;
            }
            else
            {
                imgAdelante.Visible = false;
                imgAtras.Visible = false; ;
            }

            grvBusqueda.DataSource = lstMasivo;
            grvBusqueda.DataBind();
            lblConfirmacion.Text = ListBox2.Items.Count.ToString();
            lblServicio.Text = ddlServicioAsignacion.SelectedItem.Text;
            lblInstalacion.Text = ddlInstalacionAsignacion.SelectedItem.Text;
            lblPaginas.Text = grvBusqueda.PageCount.ToString();
            lblPagina.Text = "1";
            pop.Show();
        }
        else
        {
            if (mensaje != "0")
            {
                divErrorAsignación.Visible = true;
                lblerrorAsignacion.Text = mensaje;
            }
            else
            {
                divError.Visible = true;
                lblerror.Text = "No existe ningun elemento agregado a la lista integrantes";

            }
        }
            

     }

    
  


    private void guardar()
    {
        DateTime FechaInicial;
        DateTime FechaFinal;
        DateTime fechaHorario;
        bool fechaIni = ValidaFecha.ValidaFe(txbFechaInicioComision.Text);
        bool fechaFin = ValidaFecha.ValidaFe(txbFechaBaja.Text);
        bool fechaHor = ddlTipoHorario.Enabled==true? ValidaFecha.ValidaFe(txbFechaInicioHorario.Text):true;
        string mensaje = string.Empty;


        if (fechaIni != true) { mensaje = "La fecha de inicio de comisión es invalida"; }

        #region Validación horario
        if (txbFechaBaja.Text.Trim() != "")
        {
            if (fechaFin != true && mensaje == string.Empty) { if (txbFechaBaja.Text.Trim() != "") { mensaje = "La fecha de fin de comisión es invalida"; return; } else { mensaje = string.Empty; } }
        }
        #endregion

        if (ddlTipoHorario.Enabled == true)
        {
            if (fechaHor != true && mensaje == string.Empty)
            {
                
                    mensaje = "La fecha de inicio de horario es invalida";
              
            }
        }

        if (mensaje == string.Empty && txbFechaBaja.Text.Trim() != "")
        {
            FechaInicial = Convert.ToDateTime(txbFechaInicioComision.Text);
            FechaFinal = Convert.ToDateTime(txbFechaBaja.Text);
            fechaHorario = ddlTipoHorario.Enabled==true?Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()):Convert.ToDateTime("01/01/1901");
            if (FechaInicial > FechaFinal) { mensaje = "La fecha de inicio de comisión debe ser menor a la fecha de fin de comisión"; } else { mensaje = string.Empty; }

        }

        if (ddlTipoHorario.Enabled == true)
        {
            if (mensaje == string.Empty)
            {
                FechaInicial = Convert.ToDateTime(txbFechaInicioComision.Text);

                fechaHorario =ddlTipoHorario.Enabled==true? Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()): Convert.ToDateTime("01/01/1901");
                if (fechaHorario >= FechaInicial && mensaje == string.Empty)
                {
                    mensaje = string.Empty;
                }
                else
                {
                    mensaje = "La fecha de inicio de horario debe ser mayor o igual a la fecha de inicio de comisión";

                }
            }
        }


        if (mensaje == string.Empty)
        {
            int item = ListBox2.Items.Count;
            if (item == 0) { mensaje = "0"; }
        }


        DateTime fechaModificacion = DateTime.Now.ToLocalTime();







        if (mensaje == string.Empty)
        {


            StringBuilder sbUrl = new StringBuilder();
            try
            {
                List<clsEntAsignacionMasiva> lisIntegrantes = new List<clsEntAsignacionMasiva>();
 
                foreach (ListItem z in ListBox2.Items)
                {
                
                    string strCadena = z.Value;
                    string[] valores = strCadena.Split('&');
                    TextBox fecha_= new TextBox();
                     fecha_.Text = ddlTipoHorario.Enabled == true ? txbFechaInicioHorario.Text.Trim() : "01/01/1901";

                    DateTime dtFechaInicioHorario = new DateTime();
                    if (fecha_.Text.Trim() != "") DateTime.TryParse(fecha_.Text.Trim(), out dtFechaInicioHorario);
                    clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];

                    clsEntLaboralMasivo objLabMasivo = new clsEntLaboralMasivo();
                    objLabMasivo.idEmpleado = new Guid(valores[0]);
                    objLabMasivo.idEmpleadoPuesto = Convert.ToInt16(valores[2]);
                    objLabMasivo.idPuesto = Convert.ToInt16(valores[3]);
                    objLabMasivo.fechaIngreso = dtFechaInicioHorario;
               

                    clsEntAsignacionMasiva objAsigMasiva = new clsEntAsignacionMasiva();
                    objAsigMasiva.idEmpleado = new Guid(valores[0]);
                    objAsigMasiva.idEmpleadoAsignacion = 0;
                    objAsigMasiva.idServicio = Convert.ToInt32(ddlServicioAsignacion.SelectedValue);
                    objAsigMasiva.idInstalacion = Convert.ToInt32(ddlInstalacionAsignacion.SelectedValue);
                    objAsigMasiva.fechaIngreso = Convert.ToDateTime(txbFechaInicioComision.Text);
                    objAsigMasiva.fechaModificacion = fechaModificacion;
                    objAsigMasiva.eaOficio = txbOficio.Text.Trim();
                   
                    if (txbFechaBaja.Text == "")
                    {
                        objAsigMasiva.fechaBaja = Convert.ToDateTime("01/01/0001");
                    }
                    else
                    {
                        objAsigMasiva.fechaBaja = Convert.ToDateTime(txbFechaBaja.Text);
                    }



                    objAsigMasiva.idUsuario = objSesion.usuario.IdEmpleado;
                    objAsigMasiva.idFuncionAsignacion = Convert.ToInt32(ddlFuncion.SelectedValue);
                    objAsigMasiva.operacion = 1;


                    clsDatEmpleadoPuesto.insertarEmpleadoPuestoMasivo(objAsigMasiva, objLabMasivo, (clsEntSesion)Session["objSession" + Session.SessionID]);
                    lisIntegrantes.Add(objAsigMasiva);
                    #region Insertar Horario REA

                    #region Encapsulamiento
                    clsEntHorario objHorario= new clsEntHorario
                    {
                        idEmpleado=new Guid(valores[0]),
                        idHorario=ddlTipoHorario.Enabled==true?  Convert.ToInt32(ddlTipoHorario.SelectedValue):0,
                        ahFechaInicio =ddlTipoHorario.Enabled==true?Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()):Convert.ToDateTime("01/01/1901"),
                        ahFechaFin=txbFinHorario.Text.Trim()!=""?Convert.ToDateTime(txbFinHorario.Text.Trim()):Convert.ToDateTime("01/01/1900"),         
                        idServicio=Convert.ToInt32(ddlServicioAsignacion.SelectedValue),
                        idInstalacion=Convert.ToInt32(ddlInstalacionAsignacion.SelectedValue),
                        fechaCierreAsignacion=txbFechaBaja.Text.Trim() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txbFechaBaja.Text.Trim())


                    };
                    #endregion

                    #region Base de datos

                    clsDatEmpleadoAsignacion.insertarEmpleadoHorarioREA(objHorario, (clsEntSesion)Session["objSession" + Session.SessionID]);

                    #endregion

                    #endregion

                }

                if (ListBox2.Items.Count != 0)
                {
                    clsEntReporteMasivo objReporte = new clsEntReporteMasivo();
                    objReporte.fechaInicio = fechaModificacion;
                    objReporte.fechaFin = fechaModificacion;
                    Session["impresion" + Session.SessionID] = objReporte;
                
                }

                /*
                Session["ftbMensaje"] = clsNegOficioAsignacion.generaPDFPersonalizado(lisIntegrantes, (clsEntSesion)Session["objSession" + Session.SessionID]);
                ClientScript.RegisterClientScriptBlock(GetType(), "verReporte",
                                             "window.open('./frmOficioAsignacion.aspx'" +
                                             ",'verReporte'," +
                                             " 'width=1024,height=768,resizable=no,scrollbars=yes,toolbar=no,status=no,menubar=no,copyhistory=no');",
                                             true);
                */
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito.")+"&hyper=frmAdignacionesMasivas");
                Session["objEmpleado" + Session.SessionID] = null;
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


            if (mensaje != "0")
            {
                divErrorAsignación.Visible = true;
                lblerrorAsignacion.Text = mensaje;
            }
            else
            {
                divError.Visible = true;
                lblerror.Text = "No existe ningun elemento agregado a la lista integrantes";

            }
        }
    }
    protected void Button2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        lblerror.Text = string.Empty;
        divError.Visible = false;

      
        if (ListBox2.Items.Count != 0)
        {

            int cont = 0;
            foreach (ListItem itm in ListBox2.Items)
            {
                if (itm.Selected == true)
                {
                    cont++;
                }
            }


            



            if(cont!=0)
            {
            int habilitadas = ListBox2.GetSelectedIndices().Length;


            while (habilitadas > 0)
            {

                string strCadena = ListBox2.SelectedItem.Value;
                string[] valores = strCadena.Split('&');




                if (Convert.ToString(valores[6]) != "")
                {
                    ListBox4.Items.Add(new ListItem(Convert.ToString(valores[6]), Convert.ToString(valores[0]) + valores[10]));

                }
                else
                {
                    ListBox4.Items.Add(new ListItem("--------", Convert.ToString(valores[0])));
                }

                ListBox5.Items.Add(new ListItem(Convert.ToString(valores[4]), Convert.ToString(valores[0])));
                ListBox6.Items.Add(new ListItem(Convert.ToString(valores[5]), Convert.ToString(valores[0])));
                ListBox1.Items.Add(new ListItem(valores[7] + " " + valores[8] + " " + valores[9], Convert.ToString(valores[0]) + "&" + Convert.ToString(valores[1]) + "&" + Convert.ToString(valores[2]) + "&" + Convert.ToString(valores[3]) + "&" + Convert.ToString(valores[4]) + "&" + Convert.ToString(valores[5]) + "&" + Convert.ToString(valores[6]) + "&" + valores[7] + "&" + valores[8] + "&" + valores[9] + "&" + valores[10] + "&" + valores[11] + "&" + valores[12]));










                ListBox2.Items.Remove(ListBox2.SelectedItem);
                ListBox7.Items.Remove(ListBox7.SelectedItem);

                habilitadas = ListBox2.GetSelectedIndices().Length;

            }



            System.Collections.SortedList asd = new System.Collections.SortedList();

            foreach (ListItem ll in ListBox1.Items)
            {
                string strCadena = ll.Value;
                string[] valores = strCadena.Split('&');



                asd.Add(valores[10] + " " + valores[6], ll.Value);
           
            }

            ListBox1.Items.Clear();
            ListBox4.Items.Clear();
            ListBox5.Items.Clear();
            ListBox6.Items.Clear();

            foreach (String key in asd.Keys)
            {
                string strCadena = asd[key].ToString();
                string[] valores = strCadena.Split('&');

                if (Convert.ToString(valores[6]) != "")
                {
                    ListBox4.Items.Add(new ListItem(Convert.ToString(valores[6]), Convert.ToString(valores[0]) + valores[10]));

                }
                else
                {
                    ListBox4.Items.Add(new ListItem("--------", Convert.ToString(valores[0])));
                }

                ListBox5.Items.Add(new ListItem(Convert.ToString(valores[4]), Convert.ToString(valores[0])));
                ListBox6.Items.Add(new ListItem(Convert.ToString(valores[5]), Convert.ToString(valores[0])));
                ListBox1.Items.Add(new ListItem(valores[7] + " " + valores[8] + " " + valores[9], Convert.ToString(valores[0]) + "&" + Convert.ToString(valores[1]) + "&" + Convert.ToString(valores[2]) + "&" + Convert.ToString(valores[3]) + "&" + Convert.ToString(valores[4]) + "&" + Convert.ToString(valores[5]) + "&" + Convert.ToString(valores[6]) + "&" + valores[7] + "&" + valores[8] + "&" + valores[9] + "&" + valores[10] + "&" + valores[11] + "&" + valores[12]));
               
            }







            if (ListBox1.Items.Count != 0)
            {
 
                lblAgregados.Text = ListBox2.Items.Count.ToString();
            }




            inconsistencias();

            int conta = 0;
            foreach (ListItem Items in ListBox1.Items)
            {
                string strCadena = Items.Value;
                string[] valores = strCadena.Split('&');

                if (valores[1] == "SIREN")
                {
                    ListBox1.Items[conta].Attributes.Add("Style", "color:Blue;");
                    ListBox4.Items[conta].Attributes.Add("Style", "color:Blue;");
                    ListBox5.Items[conta].Attributes.Add("Style", "color:Blue;");
                    ListBox6.Items[conta].Attributes.Add("Style", "color:Blue;");

                }
                conta++;
            }
            lblParcial.Text = ListBox1.Items.Count != 0 ? (ListBox1.Items.Count - Convert.ToInt32(lblInconsistencias.Text)).ToString() : "0";
        }
        else{
            lblerror.Text = "No existe ningun registro seleccionado";
            divError.Visible = true;
        
        }

        }
        else
        {
            lblerror.Text = "No existe ningun registro en la lista";
            divError.Visible = true;
        }
    

    }

    protected void Button1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        lblerror.Text = string.Empty;
        divError.Visible = false;
        if (ListBox1.Items.Count != 0)
        {
            /* ACTUALIZACION Marzo 2017 ASIGNACION MASIVA */

            string objEmpleado = ListBox1.SelectedItem.Value;
            string[] datosObjEmpleado = objEmpleado.Split('&');

            clsEntEmpleado empObject = new clsEntEmpleado();

            Guid idObjEmpleado = new Guid(datosObjEmpleado[0]);

            empObject.IdEmpleado = idObjEmpleado;

            clsNegEmpleado.consultarPaseListaREA(ref empObject, (clsEntSesion)Session["objSession" + Session.SessionID]);

            // SE VALIDA SI EL INTEGRANTE TIENE ASISTENCIA, SI TIENE ASISTENCIA SOLO SE PUEDE CON FECHA MAYOR A LA DE HOY.

            if (empObject.PaseLista != null)
            {
                // si tiene Asistencia
                lblerror.Text = "No es posible continuar la fecha de nuevo horario tiene que ser mayor a la fecha actual, porque el integrante seleccionado ya registro ASISTENCIA";
                divError.Visible = true;
                return;
            }


            /* FIN ACTUALIZACION Marzo 2017 ASIGNACION MASIVA*/

            int habilitadas = ListBox1.GetSelectedIndices().Length;
            bool bNoCumplen = false;
            int cont = 0;

            while (habilitadas > 0)
            {
                string strCadena = ListBox1.SelectedItem.Value;
                string[] valores = strCadena.Split('&');

                if (valores[1] == "SI" || valores[1] == "SIREN")
                {
                    ListBox2.Items.Add(ListBox1.SelectedItem); // pasa el elemento seleccionado a la otra lista

                    ListBox7.Items.Add(ListBox4.SelectedItem);
                    ListBox1.Items.Remove(ListBox1.SelectedItem);
                    ListBox4.Items.Remove(ListBox4.SelectedItem);
                    ListBox5.Items.Remove(ListBox5.SelectedItem);
                    ListBox6.Items.Remove(ListBox6.SelectedItem);
                }
                else
                {
                    if (!bNoCumplen)
                    {
                        bNoCumplen = true;
                    }

                    ListBox1.SelectedItem.Selected = false; // no se elimina de la lista orginial, pero si se desmarca de la selección
                    ListBox4.SelectedItem.Selected = false;
                    ListBox5.SelectedItem.Selected = false;
                    ListBox6.SelectedItem.Selected = false;
                }

                habilitadas = ListBox1.GetSelectedIndices().Length;

            }

            if (bNoCumplen)
            {
                // el elemento no cumple las condiciones de asignar el servicio
                lblerror.Text = "Existen registros seleccionados con inconsistencias los cuales no fueron copiados en la lista integrantes";
                divError.Visible = true;
            }
            else
            {
                System.Collections.SortedList asd = new System.Collections.SortedList();

                foreach (ListItem ll in ListBox2.Items)
                {
                    string strCadena = ll.Value;
                    string[] valores = strCadena.Split('&');
                    asd.Add(valores[10] + " " + valores[6], ll.Value);
                }

                ListBox2.Items.Clear();
                ListBox7.Items.Clear();

                foreach (String key in asd.Keys)
                {
                    string strCadena = asd[key].ToString();
                    string[] valores = strCadena.Split('&');

                    if (Convert.ToString(valores[6]) != "")
                    {
                        ListBox7.Items.Add(new ListItem(Convert.ToString(valores[6]), Convert.ToString(valores[0]) + valores[10]));
                    }
                    else
                    {
                        ListBox7.Items.Add(new ListItem("--------", Convert.ToString(valores[0])));
                    }
                    ListBox2.Items.Add(new ListItem(valores[7] + " " + valores[8] + " " + valores[9], Convert.ToString(valores[0]) + "&" + Convert.ToString(valores[1]) + "&" + Convert.ToString(valores[2]) + "&" + Convert.ToString(valores[3]) + "&" + Convert.ToString(valores[4]) + "&" + Convert.ToString(valores[5]) + "&" + Convert.ToString(valores[6]) + "&" + valores[7] + "&" + valores[8] + "&" + valores[9] + "&" + valores[10] + "&" + valores[11] + "&" + valores[12]));

                    cont++;
                }



                int conta = 0;
                foreach (ListItem Items in ListBox1.Items)
                {
                    string strCadena = Items.Value;
                    string[] valores = strCadena.Split('&');

                    if (valores[1] == "SIREN")
                    {
                        ListBox1.Items[conta].Attributes.Add("Style", "color:Blue;");
                        ListBox4.Items[conta].Attributes.Add("Style", "color:Blue;");
                        ListBox5.Items[conta].Attributes.Add("Style", "color:Blue;");
                        ListBox6.Items[conta].Attributes.Add("Style", "color:Blue;");
                    }
                    conta++;
                }

                int conta2 = 0;
                foreach (ListItem Items in ListBox2.Items)
                {
                    string strCadena = Items.Value;
                    string[] valores = strCadena.Split('&');

                    if (valores[1] == "SIREN")
                    {
                        ListBox2.Items[conta2].Attributes.Add("Style", "color:Blue;");
                        ListBox7.Items[conta2].Attributes.Add("Style", "color:Blue;");
                    }
                    conta2++;
                         }


 ListBox7.ClearSelection();
            ListBox2.ClearSelection();
            lblAgregados.Text = ListBox7.Items.Count.ToString();
            if (ListBox2.Items.Count != 0) { lblParcial.Text = ListBox1.Items.Count.ToString(); }
               if (cont != 0) { lblParcial.Text = lblParcial.Text != "0" ? Convert.ToString((Convert.ToInt32(lblParcial.Text)) - (Convert.ToInt32(lblInconsistencias.Text))) : "0"; }

            }


                    
        }
        else
        {
            lblerror.Text = "Debe realizar una busqueda con resultados para poder continuar";
            divError.Visible = true;
        }
    }

    protected void grvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        List<clsEntGuardarMasiva> evaluaciones = (List<clsEntGuardarMasiva>)lstMasivo;

        if (evaluaciones != null)
        {

            grvBusqueda.PageIndex = e.NewPageIndex;
            grvBusqueda.DataSource = evaluaciones;
            grvBusqueda.DataBind();
            lblPagina.Text = (grvBusqueda.PageIndex + 1).ToString();
            lblPaginas.Text = grvBusqueda.PageCount.ToString();

        }
    



    }

    protected void ddlJerarquia_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnNuevoReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/personal/frmAsignacionesMasivas.aspx");
    }
    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlInstalacionAsignacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTipoHorario.Enabled = true;
        if (ddlServicioAsignacion.SelectedIndex != 0 && ddlInstalacionAsignacion.SelectedIndex != 0)
        {
            ddlTipoHorario.Items.Clear();
            clsCatalogos.llenarCatalogoHorarioREA(ddlTipoHorario, Convert.ToInt32(ddlServicioAsignacion.SelectedValue), Convert.ToInt32(ddlInstalacionAsignacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

            if (ddlServicioAsignacion.SelectedValue == "1" || ddlServicioAsignacion.SelectedItem.Text=="RECURSOS HUMANOS" )
            {
                ddlTipoHorario.SelectedIndex = 0;
                ddlTipoHorario.Enabled = false;
                txbFechaInicioHorario.Text = string.Empty;
                txbFechaInicioHorario.Enabled = false;

            }
            else
            {
                ddlTipoHorario.SelectedIndex = 0;
                ddlTipoHorario.Enabled = true;
                txbFechaInicioHorario.Text = string.Empty;
                txbFechaInicioHorario.Enabled = true;

            }

        }

    }
    protected void imgAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (grvBusqueda.PageIndex < grvBusqueda.PageCount)
        {
            grvBusqueda_PageIndexChanging(grvBusqueda, new GridViewPageEventArgs(grvBusqueda.PageIndex + 1));
        }
    }
    protected void imgAtras_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (grvBusqueda.PageIndex > 0)
        {
            grvBusqueda_PageIndexChanging(grvBusqueda, new GridViewPageEventArgs(grvBusqueda.PageIndex - 1));
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        lstMasivo = null;

        if (lstMasivo == null)
        {
            lstMasivo = new List<clsEntGuardarMasiva>();
        }
        grvBusqueda.DataSource = null;
        grvBusqueda.DataBind();
        pop.Hide();
        guardar();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        lstMasivo = null;

        if (lstMasivo == null)
        {
            lstMasivo = new List<clsEntGuardarMasiva>();
        }
        grvBusqueda.DataSource = null;
        grvBusqueda.DataBind();
        pop.Hide();


    }

    protected void btnGenOficioI_Click(object sender, EventArgs e)
    {
        #region Validaciones

        string mensaje = string.Empty;

        DateTime FechaInicial;
        DateTime FechaFinal;
        DateTime fechaHorario;
        bool fechaIni = ValidaFecha.ValidaFe(txbFechaInicioComision.Text);
        bool fechaFin = ValidaFecha.ValidaFe(txbFechaBaja.Text);
        bool fechaHor = ddlTipoHorario.Enabled == true ? ValidaFecha.ValidaFe(txbFechaInicioHorario.Text) : true;

        if (fechaIni != true) { mensaje = "La fecha de inicio de comisión es inválida"; }
        divErrorOficio.Visible = false;
        /*
        #region Validación horario
        if (txbFechaBaja.Text.Trim() != "")
        {
            if (fechaFin != true && mensaje == string.Empty) { if (txbFechaBaja.Text.Trim() != "") { mensaje = "La fecha de fin de comisión es inválida"; return; } else { mensaje = string.Empty; } }
        }
        #endregion

        
        if (ddlTipoHorario.Enabled == true)
        {
            if (fechaHor != true && mensaje == string.Empty)
            {

                mensaje = "La fecha de inicio de horario es invalida";

            }
        }

        if (mensaje == string.Empty && txbFechaBaja.Text.Trim() != "")
        {
            FechaInicial = Convert.ToDateTime(txbFechaInicioComision.Text);
            FechaFinal = Convert.ToDateTime(txbFechaBaja.Text);
            fechaHorario = ddlTipoHorario.Enabled == true ? Convert.ToDateTime(txbFechaInicioHorario.Text.Trim()) : Convert.ToDateTime("01/01/1901");
            if (FechaInicial > FechaFinal) { mensaje = "La fecha de inicio de comisión debe ser menor a la fecha de fin de comisión."; } else { mensaje = string.Empty; }

        }
        */
        if (mensaje == string.Empty && ListBox2.Items.Count == 0)
        {
            mensaje = "No existe ningún elemento agregado a la lista integrantes.";
            divErrorAsignación.Visible = true;
            lblerrorAsignacion.Text = mensaje;
            return;
        }


        if (ddlServicioAsignacion.SelectedIndex == 0)
        {
            mensaje = "Debe seleccionar un servicio para poder continuar.";
            divErrorAsignación.Visible = true;
            lblerrorAsignacion.Text = mensaje;
            return;
        }

        if (ddlInstalacionAsignacion.SelectedIndex == 0)
        {
            mensaje = "Debe seleccionar una instalación para poder continuar.";
            divErrorAsignación.Visible = true;
            lblerrorAsignacion.Text = mensaje;
            return;
        }

       

        listaFirmantes = clsCatalogos.llenarCatalogoFirmantesPorZona(ddlPersonaFirma, Convert.ToInt32(ddlServicioAsignacion.SelectedValue), Convert.ToInt32(ddlInstalacionAsignacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

        if (listaFirmantes.Count <= 0)
        {
            mensaje = "No se tiene registrada ninguna persona que pueda firmar el oficio, contacte a un administrador.";
        }


        if (!clsNegOficioAsignacion.verificaZona(Convert.ToInt32(ddlServicioAsignacion.SelectedValue), Convert.ToInt32(ddlInstalacionAsignacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]))
        {
            mensaje = "No es posible generar los Oficios de Asignación para Zonas no operativas.";
        }

        if (ddlTipoHorario.Enabled == true)
        {
            if (ddlTipoHorario.SelectedIndex == 0)
            {
                mensaje = "Debe seleccionar un horario para poder continuar.";
            }
        }


        #endregion

        if (mensaje == string.Empty)
        {
            List<clsEntGuardarMasiva> lstMasaFinal = new List<clsEntGuardarMasiva>();


            foreach (ListItem z in ListBox2.Items)
            {
                clsEntGuardarMasiva objMasiva = new clsEntGuardarMasiva();
                string strCadena = z.Value;
                string[] valores = strCadena.Split('&');
                objMasiva.servicio = valores[11];
                objMasiva.instalacion = valores[12];
                lstMasaFinal.Add(objMasiva);
            }

            mpuGenOficio.Show();

        }
        else
        {
            divErrorAsignación.Visible = true;
            lblerrorAsignacion.Text = mensaje;
        }
    }
    protected void btnGenOficio_Click(object sender, EventArgs e)
    {
        if (ddlPersonaFirma.SelectedIndex <= 0)
        {
            divErrorOficio.Visible = true;
            lblErrorFirmante.Text = "Es necesario seleccionar una persona para firmar los Oficios de Asignación.";
            return;
        }
        
        
        List<clsEntAsignacionMasiva> lisIntegrantes = new List<clsEntAsignacionMasiva>();
        DateTime fechaModificacion = DateTime.Now.ToLocalTime();

        foreach (ListItem z in ListBox2.Items)
        {
            string strCadena = z.Value;
            string[] valores = strCadena.Split('&');
            TextBox fecha_ = new TextBox();
            fecha_.Text = ddlTipoHorario.Enabled == true ? txbFechaInicioHorario.Text.Trim() : "01/01/1901";

            DateTime dtFechaInicioHorario = new DateTime();
            if (fecha_.Text.Trim() != "") DateTime.TryParse(fecha_.Text.Trim(), out dtFechaInicioHorario);
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];


            clsEntAsignacionMasiva objAsigMasiva = new clsEntAsignacionMasiva();
            objAsigMasiva.idEmpleado = new Guid(valores[0]);
            objAsigMasiva.idEmpleadoAsignacion = 0;
            objAsigMasiva.idServicio = Convert.ToInt32(ddlServicioAsignacion.SelectedValue);
            objAsigMasiva.idInstalacion = Convert.ToInt32(ddlInstalacionAsignacion.SelectedValue);
            objAsigMasiva.fechaIngreso = Convert.ToDateTime(txbFechaInicioComision.Text);
            objAsigMasiva.fechaModificacion = fechaModificacion;
            objAsigMasiva.eaOficio = txbOficio.Text.Trim();

            if (ddlTipoHorario.Enabled == true)
            {
                objAsigMasiva.idHorario = Convert.ToInt32(ddlTipoHorario.SelectedValue);
            }
            else 
            {
                objAsigMasiva.idHorario =-1;
            }



            objAsigMasiva.idUsuario = objSesion.usuario.IdEmpleado;
            //objAsigMasiva.idFuncionAsignacion = Convert.ToInt32(ddlFuncion.SelectedValue);
            objAsigMasiva.operacion = 1;


            // clsDatEmpleadoPuesto.insertarEmpleadoPuestoMasivo(objAsigMasiva, objLabMasivo, (clsEntSesion)Session["objSession" + Session.SessionID]);
            lisIntegrantes.Add(objAsigMasiva);


        }


        List<string> arrImagenes = new List<string>();
        arrImagenes.Add(Server.MapPath("~/Imagenes/HeaderOficioAsignacion.png"));
        arrImagenes.Add(Server.MapPath("~/Imagenes/BackgroundOficioAsignacion.png"));


        Session["ftbMensaje"] = clsNegOficioAsignacion.generaPDFPersonalizado(
            lisIntegrantes,

            listaFirmantes.Find(h => h.IdEmpleado == new Guid(ddlPersonaFirma.SelectedValue)),

            (clsEntSesion)Session["objSession" + Session.SessionID], arrImagenes);



        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn",
                            "window.open('./frmOficioAsignacion.aspx'" +
                            ",'verReporte'," +
                            " 'width=1024,height=768,resizable=no,scrollbars=yes,toolbar=no,status=no,menubar=no,copyhistory=no');", true);


    }
    protected void btnCancelarGO_Click(object sender, EventArgs e)
    {
        mpuGenOficio.Hide();
    }
}