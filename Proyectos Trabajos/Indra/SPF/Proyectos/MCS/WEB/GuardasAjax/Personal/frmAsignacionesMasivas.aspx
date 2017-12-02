<%@ Page  Title="Módulo de Control de Servicios ::: Asignaciones Masivas" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmAsignacionesMasivas.aspx.cs" Inherits="Personal_frmAsignacionesMasivas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
<script type="text/jscript">

    function scrollList2() {
        var list66 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox6");
        var list5 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox5");
        var list4 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox4");

        var list2 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox1");
        list4.scrollTop = list2.scrollTop;
        list5.scrollTop = list2.scrollTop;
        list66.scrollTop = list2.scrollTop;

    }

    function scrollList1() {

        var list4 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox7");

        var list2 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox2");
        list4.scrollTop = list2.scrollTop;
    }




    function primaria(ele) {

        var list4 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox7");
        var list1 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox2");
        var acumulador = 0;
        var conteo = list1.length;


        for (acumulador; acumulador < conteo; acumulador++) {


            if (list1[acumulador].selected) {

                list4.options[acumulador].selected = true;
      
            }
            else {

                list4.options[acumulador].selected = false;
                

            }
        }

        var list4 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox2");
        var list5 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox7");
   


        list5.scrollTop = list4.scrollTop;

    }







    function secundaria(ele) {
        var list66 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox6");
        var list5 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox5");
        var list4 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox4");

        var list1 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox1");
        var acumulador = 0;
        var conteo = list1.length;


        for (acumulador; acumulador < conteo; acumulador++) {


            if (list1[acumulador].selected) {
       
                list4.options[acumulador].selected = true;
                list5.options[acumulador].selected = true;
                list66.options[acumulador].selected = true;
            }
            else {

                list4.options[acumulador].selected = false;
                list5.options[acumulador].selected = false;
                list66.options[acumulador].selected = false;

            }
        }

        var list4 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox1");
        var list5 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox4");
        var list6 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox5");
        var list66 = document.getElementById("ctl00_ContentPlaceHolder1_ListBox6");


        list5.scrollTop = list4.scrollTop;
        list6.scrollTop = list4.scrollTop;
        list66.scrollTop = list4.scrollTop;
    }

</script>
    <asp:Panel ID="panBusqueda" runat="server">
        <div id="Contenido">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio" border="0">
                                <tr>
                                    <td class="titulo">
                                        asignaciones masivas
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table class="tamanio">
                                                    <tr>
                                                        <td colspan="6" style="height: 16px">
                                                            <div id="MensajesError">
                                                                <asp:ValidationSummary ID="vsuMensajes" runat="server" Font-Size="Smaller" HeaderText="Mensajes:"
                                                                    ShowMessageBox="True" Width="100%" ValidationGroup="busqueda" BackColor="#FFFF80"
                                                                    BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" CssClass="divError" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="subtitulo" colspan="6" style="height: 16px">
                                                            Búsqueda
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="height: 16px">
                                                            <asp:RadioButton ID="rbtActivos" runat="server" CssClass="text" 
                                                                GroupName="Empleados" Text="Activos" Checked="True" />
                                                            <asp:RadioButton ID="rbtInactivos" runat="server" CssClass="text" 
                                                                GroupName="Empleados" Text="Inactivos" />
                                                            <asp:RadioButton ID="rbtTodos" runat="server" CssClass="text" GroupName="Empleados"
                                                                Text="Todos" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Apellido Paterno:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbPaterno" runat="server" CssClass="texto" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" onfocus="javascript:onFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="return validar(event)"
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td class="der" style="width: 130px">
                                                            Apellido Materno:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbMaterno" runat="server" CssClass="textbox" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" onfocus="javascript:onFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="return validar(event)"
                                                                Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nombre:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbNombre" runat="server" CssClass="textbox" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" onfocus="javascript:onFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="return validar(event)"
                                                                Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Fecha Nacimiento:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbNacimiento" runat="server" CssClass="textbox" MaxLength="10"
                                                                onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return validarNoEscritura(event);" Width="125px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:ImageButton ID="imbNacimiento" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="16px" />
                                                            <cc1:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbNacimiento"
                                                                TargetControlID="txbNacimiento">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td class="der" style="width: 130px">
                                                            Fecha Alta:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbIngreso" runat="server" CssClass="textbox" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return validarNoEscritura(event);" Width="125px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:ImageButton ID="imbIngreso" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="16px" />
                                                            <cc1:CalendarExtender ID="calIngreso" runat="server" PopupButtonID="imbIngreso" TargetControlID="txbIngreso">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td class="der">
                                                            Fecha Baja:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbBaja" runat="server" CssClass="textbox" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return validarNoEscritura(event);" Width="125px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:ImageButton ID="imbCaptura" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="16px" />
                                                            <cc1:CalendarExtender ID="calCaptura" runat="server" PopupButtonID="imbCaptura" TargetControlID="txbBaja">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            No Cartilla:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbCartilla" runat="server" CssClass="textbox" MaxLength="20" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" onfocus="javascript:onFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="return avalidar(event)"
                                                                Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der" style="width: 130px">
                                                            LOC:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:RadioButtonList ID="rblLOC" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="2">Todos</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td class="der">
                                                            Curso Básico:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblCurso" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="2">Todos</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            N° Empleado:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbNumero" runat="server" CssClass="textbox" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return nvalidar(event)" Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der" style="width: 130px">
                                                            CUIP:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbCuip" runat="server" CssClass="textbox" MaxLength="22" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" onfocus="javascript:onFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="return avalidar(event)"
                                                                Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            RFC:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbRfc" runat="server" CssClass="textbox" MaxLength="13" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                                Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der" style="width: 130px">
                                                            CURP:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txbCurp" runat="server" CssClass="textbox" MaxLength="18" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                                Width="150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                <td class="der">
                                                    Tipo de Servicio:
                                                </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlTipoServicio_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Jerarquia:
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:DropDownList ID="ddlJerarquia" runat="server" AutoPostBack="True" CssClass="texto"
                                                                OnSelectedIndexChanged="ddlJerarquia_SelectedIndexChanged" TabIndex="1">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Servicio:
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" CssClass="texto"
                                                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Instalación
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" 
                                                                Enabled="False" onselectedindexchanged="ddlInstalacion_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            &nbsp;
                                                        </td>
                                                        <td colspan="5">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <table width="100%">
                                            <tr>
                                                <td class="cen" colspan="6">
                                                    <asp:Button ID="btnBuscar" runat="server" CssClass="boton" OnClick="btnBuscar_Click"
                                                        onMouseOver="javascript:this.style.cursor='hand';" Text="Buscar" ToolTip="Buscar Empleados"
                                                        ValidationGroup="busqueda" Width="125px" />
                                                    <asp:Button ID="btnNuevaBusqueda" runat="server" CssClass="boton" OnClick="btnNuevaBusqueda_Click"
                                                        onMouseOver="javascript:this.style.cursor='hand';" Text="Nueva Búsqueda" ToolTip="Nueva Búsqueda"
                                                        Width="125px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der" style="width: 149px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="der" style="width: 130px">
                                                    &nbsp;
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td class="subtitulo" colspan="4">
                                                            Nueva Asignación
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="IZQ" colspan="4">
                                                            <div id="divErrorAsignación" runat="server" class="divError" visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td width="835">
                                                                            <asp:Label ID="lblerrorAsignacion" runat="server" Width="845px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                           
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Fecha Inicio Comisión
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbFechaInicioComision" runat="server" CssClass="textbox" MaxLength="10"
                                                                onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                            <asp:ImageButton ID="imbFechaInicioComision" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                                TabIndex="12" Width="18px" OnClick="imbFechaInicioComision_Click" />
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imbFechaInicioComision"
                                                                TargetControlID="txbFechaInicioComision">
                                                            </cc1:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvPuesto" runat="server" ControlToValidate="txbFechaInicioComision"
                                                                ErrorMessage="Debe Seleccionar una fecha de inicio de comisión para poder efectuar la búsqueda."
                                                                Font-Bold="True" SetFocusOnError="True" Text="*" 
                                                                ValidationGroup="busqueda"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvPuesto6" runat="server" ControlToValidate="txbFechaInicioComision"
                                                                ErrorMessage="Debe Seleccionar una Fecha de Inicio de Comisión." Font-Bold="True"
                                                                SetFocusOnError="True" Text="*" ValidationGroup="SICOGUA"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="der">
                                                            Fecha Término Comisión:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbFechaBaja" runat="server" CssClass="textbox" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                                onFocus="javascript:onFocus(this)" onkeypress="return validarNoEscritura(event);"
                                                                TabIndex="13"></asp:TextBox>
                                                            <asp:ImageButton ID="imbFechaBaja" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="18px" />
                                                            <cc1:CalendarExtender ID="cleFechaBaja" runat="server" PopupButtonID="imbFechaBaja"
                                                                TargetControlID="txbFechaBaja">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Función
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlFuncion" runat="server" CssClass="texto">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvFuncion" runat="server" ControlToValidate="ddlFuncion"
                                                                ErrorMessage="Debe Seleccionar una Funcion" Font-Bold="True" SetFocusOnError="True"
                                                                Text="*" ValidationGroup="SICOGUA"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Servicio:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlServicioAsignacion" runat="server" AutoPostBack="True" CssClass="texto"
                                                                OnSelectedIndexChanged="ddlServicioAsignacion_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ControlToValidate="ddlServicioAsignacion"
                                                                ErrorMessage="Debe Seleccionar un Servicio." Font-Bold="True" SetFocusOnError="True"
                                                                Text="*" ValidationGroup="SICOGUA"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            Instalación:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlInstalacionAsignacion" runat="server" CssClass="texto" 
                                                                Enabled="False" 
                                                                onselectedindexchanged="ddlInstalacionAsignacion_SelectedIndexChanged" 
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvInstalacion" runat="server" ControlToValidate="ddlInstalacionAsignacion"
                                                                ErrorMessage="Debe Seleccionar una Instalación" Font-Bold="True" SetFocusOnError="True"
                                                                Text="*" ValidationGroup="SICOGUA"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                             <tr>
                                                        <td class="der" style="width: 149px">
                                                            Horario:
                                                        </td>
                                                        <td>
                                                            <table style="width: 100%; border-collapse: collapse;">
                                                                <tr>
                                                                    <td style="width: 66px">
                                                                        <asp:DropDownList ID="ddlTipoHorario" runat="server" CssClass="texto" 
                                                                            TabIndex="3" Enabled="False">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="der">
                                                            <asp:Label ID="lblFinHorario0" runat="server" Text="Fecha Inicio Horario:" 
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbFechaInicioHorario" runat="server" CssClass="textbox" MaxLength="10"
                                                                onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                                                onkeypress="return validarNoEscritura(event);" Enabled="False" Visible="False"></asp:TextBox>
                                                            <asp:ImageButton ID="imbFechaInicioHorario" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                                TabIndex="12" Width="18px" Visible="False" />
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imbFechaInicioHorario"
                                                                TargetControlID="txbFechaInicioHorario">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            No de oficio:</td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txbOficio" runat="server" CssClass="textbox" Width="300px" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" onfocus="javascript:onFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                                onkeypress="return oficioN(event)" MaxLength="60"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" style="width: 149px">
                                                            <asp:Label ID="lblFinHorario1" runat="server" Text="Fecha Fin Horario:" 
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txbFinHorario" runat="server" CssClass="textbox" MaxLength="10"
                                                                onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                                                onkeypress="return validarNoEscritura(event);" Visible="False"></asp:TextBox>
                                                            <asp:ImageButton ID="imbFinHorario" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                                TabIndex="12" Width="18px" Visible="False" />
                                                            <cc1:CalendarExtender ID="ceFinHorario" runat="server" PopupButtonID="imbFinHorario"
                                                                TargetControlID="txbFinHorario">
                                                            </cc1:CalendarExtender>
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="subtitulo" colspan="4">
                                                            Resultado de los Integrantes</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der" colspan="4">
                                                            <div ID="divError" runat="server" class="divError" visible="false" >
                                                                <table>
                                                                    <tr>
                                                                        <td class="izq" style="width: 100%">
                                                                            <asp:Label ID="lblerror" runat="server" Width="845px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr style="width: 50%">
                                    <td>
                                        <asp:UpdatePanel ID="listas" runat="server">
                                            <ContentTemplate>
                                                <table width="100%" style="border-collapse: collapse" >
                                                    <tr>
                                                        <td class="cen" colspan="3">
                                                            <table style="border-collapse: collapse; width: 100%; height:270px"  >
                                                                <tr>
                                                                    <td class="list texto">
                                                                        &nbsp;empleado</td>
                                                                    <td class="list texto" width="300px">
                                                                        nombre</td>
                                                                    <td class="texto list" width="380">
                                                                        servicio/instalación</td>
                                                                    <td class="texto list">
                                                                        observaciones</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="list">

                                                                    <style type="text/css"> 


.capaSelect {   position:relative;   width:50px;   height:250px;   overflow:hidden; } .capaSelect select {   background:white;   position:absolute;   width:50px;   top:-2px;   left:-2px; } 
.capaSelect2 {   position:relative;   width:100%;   height:250px;   overflow:hidden; } .capaSelect2 select {   background:white;   position:absolute;   width:100%;   top:-2px;   left:-2px; } 
.capaSelect3 {   position:relative;   width:400px;   height:250px;   overflow:hidden; } .capaSelect3 select {   background:white;   position:absolute;   width:400px;   top:-2px;   left:-2px; } 
.capaSelect4 {   position:relative;   width:118px;   height:250px;   overflow:hidden; } .capaSelect4 select {   background:white;   position:absolute;   width:118px;   top:-2px;   left:-2px; } 

</style>
        <div class="capaSelect"> 
                                                                        <asp:ListBox ID="ListBox4" runat="server" CssClass="texto scroll" 
                                                                            Height="250px" onchange="secundaria(this)" SelectionMode="Multiple" 
                                                                            Width="72px"></asp:ListBox></div>
                                                                    </td>
                                                                    <td class="list">
                                                                       <div class="capaSelect2" > 
                                                                           <asp:ListBox ID="ListBox1" runat="server" CssClass="texto" Height="250px" 
                                                                               onchange="secundaria(this)" SelectionMode="Multiple" Width="293px">
                                                                           </asp:ListBox>
                                                                        </div>
                                                                    </td>
                                                                    <td class="list">
                                                                        <div class="capaSelect3 izq"> 
                                                                            <asp:ListBox ID="ListBox5" runat="server" CssClass="texto scroll izq" 
                                                                                Height="250px" onchange="secundaria(this)" SelectionMode="Multiple" 
                                                                                Width="450px"></asp:ListBox>
                                                                            </div>
                                                                    </td>
                                                                    <td class="list">
                                                                   <div class="capaSelect4"> 
                                                                        <asp:ListBox ID="ListBox6" runat="server" CssClass="texto scroll" 
                                                                            Height="250px" onchange="secundaria(this)" SelectionMode="Multiple" 
                                                                            Width="190px"></asp:ListBox>
                                                                            </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cen" colspan="3">
                                                            <table border="0" style="width: 99%;">
                                                                <tr>
                                                                    <td class="izq" style="width: 165px; font-weight: 700;" rowspan="2">
                                                                        Registros Disponibles: Inconsistencias:
                                                                    </td>
                                                                    <td class="izq" style="width: 259px" rowspan="2">
                                                                        <asp:Label ID="lblParcial" runat="server">0</asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblInconsistencias" runat="server" Text="0"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 122px" rowspan="2">
                                                                        <table style="width: 24%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="Button7" runat="server" ImageUrl="~/Imagenes/abajo.png" 
                                                                                        OnClick="Button1_Click" Width="35px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                                                                        AssociatedUpdatePanelID="listas">
                                                                                        <ProgressTemplate>
                                                                                            <asp:Image ID="Image3" runat="server" Height="37px" 
                                                                                                ImageUrl="~/Imagenes/loading.gif" Width="37px" />
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="Button8" runat="server" ImageUrl="~/Imagenes/arriba.png" 
                                                                                        OnClick="Button2_Click" Width="35px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td class="der" style="width: 332px">
                                                                        <asp:Button ID="Button3" runat="server" CssClass="boton" 
                                                                            OnClick="Button3_Click" Text="Seleccionar Todos" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton3" runat="server" 
                                                                            ImageUrl="~/Imagenes/Symbol-Delete-Mini.png" OnClick="ImageButton2_Click" 
                                                                            ToolTip="Eliminar los elementos seleccionados de la lista" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 332px">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="subtitulo" colspan="3">
                                                            Integrantes</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cen" colspan="3" style="height: 10px">
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cen" colspan="3">

                                                                                                                            <style type="text/css"> 


.capaSelect6 {   position:relative;   width:100%;   height:250px;   overflow:hidden; } .capaSelect6 select {   background:white;   position:absolute;   width:100%;   top:-2px;   left:-2px; } 

</style>
   
                                              
                                                            <table style="border-collapse: collapse; width:100%; height:270px" class="list">
                                                                <tr>
                                                                    <td class="texto">
                                                                        empleado</td>
                                                                    <td class="texto list" width="100%" height="12px">
                                                                        nombre</td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="250" class="style1">
                                                                         <div class="capaSelect"> 
                                                                        <asp:ListBox ID="ListBox7" runat="server" CssClass="texto scroll" Height="100%" 
                                                                            onchange="primaria(this)" SelectionMode="Multiple" Width="72px">
                                                                        </asp:ListBox>
                                                                        </div>
                                                                    </td>
                                                                    <td class="izq">
                                                                            <div class="capaSelect6"> 
                                                                        <asp:ListBox ID="ListBox2" runat="server" CssClass="texto" Height="250px" 
                                                                            onchange="primaria(this)" SelectionMode="Multiple" Width="100%">
                                                                        </asp:ListBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cen" colspan="3">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="izq" style="width: 153px; font-weight: 700;">
                                                                        Registros Agregados:
                                                                    </td>
                                                                    <td class="izq" style="width: 191px">
                                                                        <asp:Label ID="lblAgregados" runat="server">0</asp:Label>
                                                                    </td>
                                                                    <td style="width: 415px">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="der">
                                                                        <asp:Button ID="Button4" runat="server" CssClass="boton" 
                                                                            OnClick="Button4_Click" Text="Seleccionar Todos" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                                                            ImageUrl="~/Imagenes/Symbol-Delete-Mini.png" OnClick="ImageButton1_Click" 
                                                                            ToolTip="Eliminar los elementos seleccionados de la lista" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="cen" style="width: 100%">
                                                            &nbsp;
                                                            
                                                        </td>
                                                        <td class="cen" style="width: 27px">
                                                            &nbsp;
                                                        </td>
                                                        <td class="der">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                     <tr>
                                    <td class="cen">
                                        <asp:Button ID="btnGuardar" runat="server" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                            Text="Guardar" ToolTip="Guardar" Width="125px" ValidationGroup="SICOGUA" OnClick="btnGuardar_Click" />
                                            <asp:Button ID="btnGenerarOficio" runat="server"  CssClass="boton" 
                                            onclick="btnGenOficioI_Click" Text="Generar Oficios" Width="125px"  onMouseOver="javascript:this.style.cursor='hand';"  />
                                        <asp:Button ID="btnNuevoReg" runat="server" CssClass="boton" 
                                            onclick="btnNuevoReg_Click" Text="Nuevo Registro" Width="125px" />
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="boton" OnClick="btnCancelar_Click"
                                            onMouseOver="javascript:this.style.cursor='hand';" Text="Cancelar" ToolTip="Cancelar"
                                            Width="125px" />
                                    </td>
                                </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                               



                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Button ID="btnConfirmacion" runat="server" Text="Button" style="visibility:hidden;" />
         <asp:Button ID="btnOficio" runat="server" Text="Button" style="visibility:hidden;" />
    </asp:Panel>

     <cc1:ModalPopupExtender ID="pop" runat="server" PopupControlID="pnlConfirmacion"
        TargetControlID="btnConfirmacion" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>


                                     <asp:Panel ID="pnlConfirmacion" runat="server" DefaultButton="btnConfirmacion" Width="550px">
        <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
            <div style="background-repeat: repeat; background-image: url(../Imagenes/line.png);
                margin: 30px auto 30px auto; border: outset 1px Black;">



                                    <asp:UpdatePanel ID="UpdatePanelFileLoad" runat="server">
                    <ContentTemplate>


                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                                <td style="width: 35px">
                                                    &nbsp;</td>
                                                <td style="width: 440px">
                                                    &nbsp;</td>
                                                <td style="width: 17px">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td colspan="4" class="cen">
                                                                ¿Confirma la asignación de
                                                                <asp:Label ID="lblConfirmacion" runat="server" style="font-weight: 700"></asp:Label>
                                                                &nbsp;integrantes?</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 181px">
                                                                &nbsp;</td>
                                                            <td width="80">
                                                                <asp:Button ID="Button9" runat="server" CssClass="boton" 
                                                                    onclick="Button9_Click" Text="Confirmar" Width="80px" />
                                                            </td>
                                                            <td style="width: 11px">
                                                                <asp:Button ID="Button10" runat="server" CssClass="boton" 
                                                                    onclick="Button10_Click" Text="Cancelar" Width="80px" />
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td class="izq" rowspan="4" style="width: 66px">
                                                                <asp:Image ID="Image4" runat="server" 
                                                                    ImageUrl="~/Imagenes/Symbol-Exclamation.png" />
                                                            </td>
                                                            <td class="izq" style="width: 82px">
                                                                <strong>Asignación</strong></td>
                                                            <td class="izq">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="der" style="width: 82px">
                                                                Servicio:</td>
                                                            <td class="izq">
                                                                <asp:Label ID="lblServicio" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="der" style="width: 82px">
                                                                Instalación:</td>
                                                            <td class="izq">
                                                                <asp:Label ID="lblInstalacion" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="izq" style="width: 82px">
                                                                &nbsp;</td>
                                                            <td class="izq">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                                <td style="width: 35px">
                                                    <asp:ImageButton ID="imgAtras" runat="server" 
                                                        ImageUrl="~/Imagenes/rewind-icon.png" OnClick="imgAtras_Click" ToolTip="Atrás" 
                                                        Visible="False" />
                                                </td>
                                                <td style="width: 440px">
                                                    <asp:GridView ID="grvBusqueda" runat="server" AllowPaging="True" 
                                                        AutoGenerateColumns="False" CellPadding="1" CssClass="texto" 
                                                        OnPageIndexChanging="grvBusqueda_PageIndexChanging" PageSize="6" 
                                                        ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="idPersonar" runat="server" 
                                                                        Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                    </Label>
                                                                </ItemTemplate>
                                                                <ControlStyle Width="35px" />
                                                                <ItemStyle Width="18px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Servicio-origen">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("servicio") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Instalación-origen">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("instalacion") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cantidad">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcantidad" runat="server" Text='<%# Eval("cantidad") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" 
                                                            HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                    </asp:GridView>
                                                </td>
                                                <td style="width: 17px">
                                                    <asp:ImageButton ID="imgAdelante" runat="server" 
                                                        ImageUrl="~/Imagenes/forward-icon.png" OnClick="imgAdelante_Click" 
                                                        ToolTip="Siguiente" Visible="False" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                                <td style="width: 35px">
                                                    &nbsp;</td>
                                                <td style="width: 440px" class="cen">
                                                    <strong style="text-align: center">
                                                    <asp:Label ID="lblPagina" runat="server" Text="0" Visible="False"></asp:Label>
                                                    &nbsp;<asp:Label ID="lblde" runat="server" Text="de" Visible="False"></asp:Label>
                                                    &nbsp;<asp:Label ID="lblPaginas" runat="server" Text="0" Visible="False"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 17px">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                                <td style="width: 35px">
                                                    &nbsp;</td>
                                                <td style="width: 440px">
                                                    &nbsp;</td>
                                                <td style="width: 17px">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                                    </div>
        </div>
    </asp:Panel>

    


     <cc1:ModalPopupExtender ID="mpuGenOficio" runat="server" PopupControlID="pnlGenOficio"
        TargetControlID="btnOficio" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>


    
       <asp:Panel ID="pnlGenOficio" runat="server" DefaultButton="btnOficio" 
        Width="500px">
        <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
            <div style="background-repeat: repeat; background-image: url(../Imagenes/line.png);
                margin: 30px auto 30px auto; border: outset 1px Black;">



                 <asp:UpdatePanel ID="UpdatepnlGenOficio" runat="server">
                    <ContentTemplate>


                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                               
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <table  style="width: 100%">
                                                        <tr>
                                                            <th class="titulo" colspan="5" class="cen">
                                                               <strong> Generando Oficios de Asignación</strong>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="izq">
                                                               <div ID="divErrorOficio"  style="width: 100%" runat="server" class="divError" visible="false">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td width="100%" align="left">
                                                                            <asp:Label ID="lblErrorFirmante" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1" class="cen">
                                                        </td>
                                                            <td colspan="4" class="izq">
                                                                Seleccionar persona que firmará los oficios de asignación:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="1" class="izq">
                                                        </td>
                                                            <td colspan="3" class="izq">
                                                                <asp:DropDownList ID="ddlPersonaFirma" runat="server" Font-Size="Small">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td colspan="1" class="izq">
                                                        </td>
                                                        </tr>
                                                       
                                                    </table>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td class="style3" colspan="2">
                                             </td>
                                            <td class="style3" colspan="1">
                                                <asp:Button ID="btnAceptarGO" runat="server" CssClass="boton" 
                                                    onclick="btnGenOficio_Click" Text="Aceptar" Width="80px" />
                                            
                                                <asp:Button ID="btnCancelarGO" runat="server" CssClass="boton" 
                                                    onclick="btnCancelarGO_Click" Text="Cancelar" Width="80px" />
                                            </td>
                                             <td class="style3" colspan="2">
                                             </td>
                                           
                                        </tr>
                                         <tr>
                                                <td colspan="5" style="width: 10px">
                                                 <br>
                                                </br>
                                                </td>
                                               
                                            </tr>
                                       
                                        </table>
                                        </td>
                                        </tr>
                                        
                                        </table>
                            </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>




</asp:Content>
