<%@ Page  Title="Módulo de Control de Servicios ::: Reporte Proceso Revisión" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmProcesoRevision.aspx.cs" Inherits="Reportes_frmProcesoRevision" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
<script language="javascript" type="text/javascript">



    function activaActaNoClean() {
        var rbActa = document.getElementById('ctl00_ContentPlaceHolder1_rbActa');
        var cbPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_cbPrimerActa');
        var txbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var imbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaPrimerActa');
        var txbPrimerActaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaNoOficio');
        var txbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaFechaOficio');
        var imbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActaFechaOficio');
        var txbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaRenuncia');
        var imbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaRenuncia');
        var txbRenunciaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaNoOficio');
        var txbRenunciaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaFechaOficio');
        var imbFechaOficioRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaOficioRenuncia');

        var txbCancelacionFecha = document.getElementById('ctl00_ContentPlaceHolder1_txbCancelacionFecha');
        var imbFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaCancelacion');
        var cbCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_cbCancelacion');

        if (rbActa.checked) {
            cbPrimerActa.checked = false;
            cbCancelacion.checked = false;
            txbCancelacionFecha.disabled = false;
            imbFechaCancelacion.disabled = false;
            cbCancelacion.disabled = false;

            cbPrimerActa.disabled = false;
            txbFechaPrimerActa.disabled = false;
            imbFechaPrimerActa.disabled = false;
            txbPrimerActaNoOficio.disabled = false;
            txbPrimerActaFechaOficio.disabled = false;
            imbPrimerActaFechaOficio.disabled = false;
            txbFechaRenuncia.disabled = true;
            imbFechaRenuncia.disabled = true;
            txbRenunciaNoOficio.disabled = true;
            txbRenunciaFechaOficio.disabled = true;
            imbFechaOficioRenuncia.disabled = true;
        }
    }

    function activaRenunciaNoClean() {



        var rbActa = document.getElementById('ctl00_ContentPlaceHolder1_rbActa');
        var cbPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_cbPrimerActa');
        var txbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var imbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaPrimerActa');
        var txbPrimerActaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaNoOficio');
        var txbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaFechaOficio');
        var imbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActaFechaOficio');


        var rbRenuncias = document.getElementById('ctl00_ContentPlaceHolder1_rbRenuncias');
        var txbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaRenuncia');
        var imbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaRenuncia');
        var txbRenunciaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaNoOficio');
        var txbRenunciaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaFechaOficio');
        var imbFechaOficioRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaOficioRenuncia');


        var txbCancelacionFecha = document.getElementById('ctl00_ContentPlaceHolder1_txbCancelacionFecha');
        var imbFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaCancelacion');
        var cbCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_cbCancelacion');

        if (rbRenuncias.checked) {
            cbPrimerActa.checked = false;
            cbCancelacion.checked = false;
            cbPrimerActa.disabled = true;
            txbFechaPrimerActa.disabled = true;
            imbFechaPrimerActa.disabled = true;
            txbPrimerActaNoOficio.disabled = true;
            txbPrimerActaFechaOficio.disabled = true;
            imbPrimerActaFechaOficio.disabled = true;
            rbRenuncias.disabled = false;
            txbFechaRenuncia.disabled = false;
            imbFechaRenuncia.disabled = false;
            txbRenunciaNoOficio.disabled = false;
            txbRenunciaFechaOficio.disabled = false;
            imbFechaOficioRenuncia.disabled = false;
            imbFechaCancelacion.disabled = true;
            cbCancelacion.disabled = true;
            txbCancelacionFecha.disabled = true;
        }
    }

  

    function activaActa() {

     

        var rbActa= document.getElementById('ctl00_ContentPlaceHolder1_rbActa');
        var cbPrimerActa= document.getElementById('ctl00_ContentPlaceHolder1_cbPrimerActa');
        var txbFechaPrimerActa= document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var imbFechaPrimerActa= document.getElementById('ctl00_ContentPlaceHolder1_imbFechaPrimerActa');
        var txbPrimerActaNoOficio= document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaNoOficio');
        var txbPrimerActaFechaOficio= document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaFechaOficio');
        var imbPrimerActaFechaOficio= document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActaFechaOficio');
        var txbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaRenuncia');
        var imbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaRenuncia');
        var txbRenunciaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaNoOficio');
        var txbRenunciaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaFechaOficio');
        var imbFechaOficioRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaOficioRenuncia');

        var txbCancelacionFecha = document.getElementById('ctl00_ContentPlaceHolder1_txbCancelacionFecha');
        var imbFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaCancelacion');
        var cbCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_cbCancelacion');

        if (rbActa.checked) {
            activaActaNoClean()
            txbFechaPrimerActa.value = '';
            txbPrimerActaNoOficio.value = '';
            txbPrimerActaFechaOficio.value = '';
            txbFechaRenuncia.value = '';
            txbRenunciaNoOficio.value = '';
            txbRenunciaFechaOficio.value = '';
            txbCancelacionFecha.value=''
      

        }
    }

    function activaActa1() {
        var imbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaPrimerActa');
        var cbPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_cbPrimerActa');
        var txbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var txbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var txbPrimerActaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaNoOficio');
        var txbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaFechaOficio');
        var imbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActaFechaOficio');

        if (cbPrimerActa.checked) {
            txbFechaPrimerActa.disabled = false;
            txbFechaPrimerActa.disabled = false;
            txbPrimerActaNoOficio.disabled = false;
            txbPrimerActaFechaOficio.disabled = false;
            imbPrimerActaFechaOficio.disabled = false;
            imbFechaPrimerActa.disabled = false;


            txbFechaPrimerActa.value = '';
            txbFechaPrimerActa.value = '';
            txbPrimerActaNoOficio.value = '';
            txbPrimerActaFechaOficio.value = '';
            imbPrimerActaFechaOficio.value = '';
        }
        else {
            txbFechaPrimerActa.disabled = true;
            txbFechaPrimerActa.disabled = true;
            txbPrimerActaNoOficio.disabled = true;
            txbPrimerActaFechaOficio.disabled = true;
            imbPrimerActaFechaOficio.disabled = true;
            imbFechaPrimerActa.disabled = true;


            txbFechaPrimerActa.value = '';
            txbFechaPrimerActa.value = '';
            txbPrimerActaNoOficio.value = '';
            txbPrimerActaFechaOficio.value = '';
            imbPrimerActaFechaOficio.value = '';
        }

    }


    function activaRenuncia() {



        var rbActa = document.getElementById('ctl00_ContentPlaceHolder1_rbActa');
        var cbPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_cbPrimerActa');
        var txbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var imbFechaPrimerActa = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaPrimerActa');
        var txbPrimerActaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaNoOficio');
        var txbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbPrimerActaFechaOficio');
        var imbPrimerActaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActaFechaOficio');


        var rbRenuncias = document.getElementById('ctl00_ContentPlaceHolder1_rbRenuncias');
        var txbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaRenuncia');
        var imbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaRenuncia');
        var txbRenunciaNoOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaNoOficio');
        var txbRenunciaFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbRenunciaFechaOficio');
        var imbFechaOficioRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaOficioRenuncia');


        var txbCancelacionFecha = document.getElementById('ctl00_ContentPlaceHolder1_txbCancelacionFecha');
        var imbFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaCancelacion');
        var cbCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_cbCancelacion');

        if (rbRenuncias.checked) {
            activaRenunciaNoClean();
            txbFechaPrimerActa.value = '';
            txbPrimerActaNoOficio.value = '';
            txbPrimerActaFechaOficio.value = '';
            txbFechaRenuncia.value = '';
            txbRenunciaNoOficio.value = '';
            txbRenunciaFechaOficio.value = '';
            txbCancelacionFecha.value = '';

        }
    }

    function activaActa2() {


        var txbCancelacionFecha = document.getElementById('ctl00_ContentPlaceHolder1_txbCancelacionFecha');
        var imbFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaCancelacion');
        var cbCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_cbCancelacion');


        if (cbCancelacion.checked) {


            txbCancelacionFecha.disabled = false;
            imbFechaCancelacion.disabled = false;

            txbCancelacionFecha.value = '';

        }
        else {
            txbCancelacionFecha.disabled = true;
            imbFechaCancelacion.disabled = true;

            txbCancelacionFecha.value = '';

        }
    }
        </script>
              <asp:UpdatePanel ID="upReporte" runat="server">
                        <ContentTemplate>
        </updatepanel>
    <table style="width: 100%;">
        <tr>
            <td class="titulo" colspan="4">
                Reporte procedimiento</td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="4">
                Criterios de Búsqueda</td>
        </tr>
        <tr>
            <td class="der" style="width: 77px">
                &nbsp;</td>
            <td style="width: 68%">
                &nbsp;</td>
            <td style="width: 10%">
                &nbsp;</td>
            <td style="width: 10%">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="der" style="width: 77px">
                &nbsp;</td>
            <td style="width: 68%">
                                                    <asp:RadioButtonList ID="rblTipo" 
                    runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True">Todos</asp:ListItem>
                                                        <asp:ListItem Value="2">Activos</asp:ListItem>
                                                        <asp:ListItem Value="3">Inactivos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
            <td style="width: 10%">
                &nbsp;</td>
            <td style="width: 10%">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="der" style="width: 77px">
                Servicio:</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" 
                    onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 77px">
                Instalacion:</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" 
                    onselectedindexchanged="ddlInstalacion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 77px">
                &nbsp;</td>
            <td style="width: 68%">
                &nbsp;</td>
            <td style="width: 10%">
                &nbsp;</td>
            <td style="width: 10%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 546px">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3">
                                        <asp:RadioButton ID="rbActa" runat="server" Text="Procedimiento" 
                                            GroupName="seleccion"   onClick="activaActa();" 
                                            Checked="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 326px">
                                        <asp:CheckBox ID="cbPrimerActa" runat="server" Text="Procedimiento" 
                                            onClick="activaActa1();"  />
                                    </td>
                                    <td class="der" style="width: 22%">
                                        No. de Oficio:</td>
                                    <td style="width: 31%">
                                        <asp:TextBox ID="txbPrimerActaNoOficio" runat="server" CssClass="texto"
                                        
                                     onblur="javascript:onLosFocus(this)"
                                    MaxLength="40" onfocus="javascript:onFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                            Width="125px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 326px">
                                        <table style="width: 100%; margin-right: 26px;">
                                            <tr>
                                                <td class="der" style="width: 159px">
                                                    Fecha de Procedimiento:</td>
                                                <td style="width: 108px">
                                                    <asp:TextBox ID="txbFechaPrimerActa" runat="server" CssClass="textbox"
                                                    
                                                    
                                             onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Enabled="False"
                                                    
                                                    ></asp:TextBox>
                                                </td>
                                                <td>
                                                            <asp:ImageButton ID="imbFechaPrimerActa" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" Enabled="False" />


                                                                              <cc1:CalendarExtender ID="calFechaPrimerActa" runat="server" PopupButtonID="imbFechaPrimerActa"
                                                                TargetControlID="txbFechaPrimerActa">
                                                            </cc1:CalendarExtender>


                                                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="der" style="width: 22%">
                                        Fecha Oficio:</td>
                                    <td style="width: 31%">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="width: 108px">
                                                    <asp:TextBox ID="txbPrimerActaFechaOficio" runat="server" CssClass="textbox"
                                                    
                                                    onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Enabled="False"
                                                    
                                                    ></asp:TextBox>
                                                </td>
                                                <td>
                                                            <asp:ImageButton ID="imbPrimerActaFechaOficio" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" Enabled="False" />

                                                                      <cc1:CalendarExtender ID="cePrimerActaFechaOficio" runat="server" PopupButtonID="imbPrimerActaFechaOficio"
                                                                TargetControlID="txbPrimerActaFechaOficio">
                                                            </cc1:CalendarExtender>

                                                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 326px">
                                        &nbsp;</td>
                                    <td style="width: 22%">
                                        &nbsp;</td>
                                    <td style="width: 31%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 326px">
                                        <asp:CheckBox ID="cbCancelacion" runat="server" onClick="activaActa2();"
                                            Text="Cancelacion" />
                                    </td>
                                    <td class="der" style="width: 22%">
                                        &nbsp;</td>
                                    <td style="width: 31%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 326px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="der" style="width: 137px">
                                                    Fecha Acta:</td>
                                                <td style="width: 108px">
                                                    <asp:TextBox ID="txbCancelacionFecha" runat="server" CssClass="textbox"
                                                    
                                                  onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Enabled="False"
                                                    
                                                    ></asp:TextBox>
                                                </td>
                                                <td>
                                                            <asp:ImageButton ID="imbFechaCancelacion" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" Enabled="False" />


                                                                <cc1:CalendarExtender ID="ceFechaSegundaActa" runat="server" PopupButtonID="imbFechaCancelacion"
                                                                TargetControlID="txbCancelacionFecha">
                                                            </cc1:CalendarExtender>

                                                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="der" style="width: 22%">
                                        &nbsp;</td>
                                    <td style="width: 31%">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="width: 108px">
                                                    &nbsp;</td>
                                                <td>
                                                            &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 7px">
                            &nbsp;</td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3">
                                        <asp:RadioButton ID="rbRenuncias"   runat="server" Text="Renuncias" GroupName="seleccion" onClick ="activaRenuncia();"   />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 82%">
                                        Fecha de Renuncia:</td>
                                    <td style="width: 130px">
                                        <asp:TextBox ID="txbFechaRenuncia" runat="server" CssClass="textbox" 
                                            Enabled="False"
                                            
                                          onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                            
                                            ></asp:TextBox>
                                    </td>
                                    <td style="width: 25%">
                                                            <asp:ImageButton ID="imbFechaRenuncia" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" Enabled="False" />


                                                                      <cc1:CalendarExtender ID="ceFechaRenuncia" runat="server" PopupButtonID="imbFechaRenuncia"
                                                                TargetControlID="txbFechaRenuncia">
                                                            </cc1:CalendarExtender>

                                                            </td>
                                </tr>
                                <tr>
                                    <td style="width: 82%">
                                        &nbsp;</td>
                                    <td style="width: 130px">
                                        &nbsp;</td>
                                    <td style="width: 25%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 82%">
                                        No de Oficio:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txbRenunciaNoOficio" runat="server" CssClass="texto" 
                                            Enabled="False"
                                            
                                           onblur="javascript:onLosFocus(this)"
                                    MaxLength="40" onfocus="javascript:onFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="125px"
                                            
                                            ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 82%">
                                        Fecha de Oficio:</td>
                                    <td style="width: 130px">
                                        <asp:TextBox ID="txbRenunciaFechaOficio" runat="server" CssClass="textbox" 
                                            Enabled="False"
                                            
                                            
                                            onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                            ></asp:TextBox>
                                    </td>
                                    <td style="width: 25%">
                                                            <asp:ImageButton ID="imbFechaOficioRenuncia" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" Enabled="False" />

<cc1:CalendarExtender ID="ceFechaOficioRenuncia" runat="server" PopupButtonID="imbFechaOficioRenuncia"
                                                                TargetControlID="txbRenunciaFechaOficio">
                                                            </cc1:CalendarExtender>


                                                            </td>
                                </tr>
                                <tr>
                                    <td style="width: 82%">
                                        &nbsp;</td>
                                    <td style="width: 130px">
                                        &nbsp;</td>
                                    <td style="width: 25%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 28px; width: 82%">
                                    </td>
                                    <td style="height: 28px; width: 130px">
                                    </td>
                                    <td style="height: 28px; width: 25%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 546px">
                            &nbsp;</td>
                        <td style="width: 7px">
                            &nbsp;</td>
                        <td class="der">
                            <asp:Button ID="btnGenerar" runat="server" CssClass="boton" 
                                Text="Generar Reporte" onclick="btnGenerar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

