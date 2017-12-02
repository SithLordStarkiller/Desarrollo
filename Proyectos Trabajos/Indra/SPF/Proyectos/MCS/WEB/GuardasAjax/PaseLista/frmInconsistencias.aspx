<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmInconsistencias.aspx.cs" Inherits="PaseLista_frmInconsistencias" %>


<%@ Import Namespace="SICOGUA.Entidades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <script type="text/jscript">
        /*Función que selecciona todos los checkbox de una pagina de un GridView*/
        /*se reciben al grid y el número de celda en donde se encuentra el check*/
        function SeleccionarTodoCheck() {
            var grid = document.getElementById("ctl00_ContentPlaceHolder1_grvAsistencia");
            var horarioAbierto = document.getElementById("hffHorarioAbierto");
            var cell;
            var cell2;
            var cell3;
            var falta
            var retardo
            /*SOlo si el Grid no es nulo podrán recorrerse sus celdas*/
            if (grid != null) {
                y = grid.rows.length;
                /*Si hay mas de o renglones, se recorren todos*/
                if (grid.rows.length > 0) {
                    for (i = 1; i < y; i++) {
                        /*para los horarios abiertos siempre sera presente*/
                        if (horarioAbierto.defaultValue == "True") {
                            if (grid.rows[i].cells[5].firstChild.value == "")
                                grid.rows[i].cells[6].innerText = "";
                            else
                                grid.rows[i].cells[6].innerText = "PRESENTE";
                        } else {

                            /*Obteniendo el valor de la columna*/
                            cell = grid.rows[i].cells[1].all[1].value
                            cell2 = grid.rows[i].cells[1].all[2].value
                            cell3 = grid.rows[i].cells[5].all[0].value
                            var d1 = new Date(cell);
                            var d2 = new Date(cell2);
                            var d3 = new Date(cell3);
                            retardo = dateCompare(cell3 + ":00", cell);

                            if (retardo == 1) {
                                falta = dateCompare(cell3 + ":00", cell2);
                                if (falta == 1) {
                                    grid.rows[i].cells[6].innerText = "FALTA";
                                }
                                else {
                                    grid.rows[i].cells[6].innerText = "RETARDO";
                                }
                            }

                            else
                                if (retardo == 3) {
                                    grid.rows[i].cells[6].innerText = "";
                                }
                                else {
                                    /*debo tomar la hora */

                                    /*lo que ya traiga en su descripcion mas retardo o falta o presente*/
                                    grid.rows[i].cells[6].innerText = "PRESENTE";
                                }
                        }

                    }
                }
            }

            function dateCompare(time1, time2) {
                var t1 = new Date();
                var parts = time1.split(":");
                t1.setHours(parts[0], parts[1], parts[2], 0);
                var t2 = new Date();
                parts = time2.split(":");
                t2.setHours(parts[0], parts[1], parts[2], 0);

                /*significa que no lleva valor alguno con quien comparar*/
                if (t1.getTime() > 1 == false) return 3;

                // returns 1 if greater, -1 if less and 0 if the same
                if (t1.getTime() > t2.getTime()) return 1;
                if (t1.getTime() < t2.getTime()) return 2;
                return 0;
            }


        }
    </script>
    <script runat="server">

  void CustomersGridView_SelectedIndexChanged(Object sender, EventArgs e)
  {

    // Determine the index of the selected row.
      int index = grvAsistencia.SelectedIndex;
      string valor = grvAsistencia.DataKeys[index].Value.ToString() ;

  }
        

</script>

    <div id="Contenido" class="scrollbar">
        <div class="titulo">
            INCONSISTENCIAS EN PASE DE LISTA</div>
        <div class="subtitulo">
            Datos de la Asistencia
        </div>
        <br />
        <asp:UpdatePanel ID="updLista" runat="server">
            <ContentTemplate>
                <table class="tamanio">
                    <tr>
                        <td class="der">
                            Fecha:
                        </td>
                        <td>
                            <asp:Label ID="lblFecha" runat="server" CssClass="textbox"></asp:Label>                          
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Instalación:
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="textbox" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="Debe Seleccionar un Servicio."
                                ControlToValidate="ddlServicio" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Servicio:
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="textbox" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlInstalacion_SelectedIndexChanged" 
                                Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvInstalacion" runat="server" ErrorMessage="Debe Seleccionar una Instalación."
                                ControlToValidate="ddlInstalacion" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="nder">
                    <asp:DropDownList ID="ddlFuncion" runat="server" AutoPostBack="true" 
                        CssClass="textbox" 
                        OnSelectedIndexChanged="ddlInstalacion_SelectedIndexChanged" 
                        EnableTheming="True" Enabled="False" Visible="False">
                    </asp:DropDownList>
                    <asp:Button ID="btnGenerar" runat="server" Text="Mostrar Incosistencias" class="boton"
                        onMouseOver="javascript:this.style.cursor='hand';" OnClick="btnGenerar_Click"
                        ToolTip="Mostrar Inconsistencias" ValidationGroup="SICOGUA" />
                </div>
                <br />
                <div class="subtitulo">
                    <asp:Label ID="lblEntradaSalida" runat="server" Text="INCONSISTENCIAS" />
                </div>
                <br />
                <center>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updLista">
                        <ProgressTemplate>
                            <center>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/loading.gif" Width="50px" /><br />
                                <br />
                                <asp:Label ID="lblGenerando" runat="server" Text="Generando...." CssClass="negritas"></asp:Label>
                                <br />
                            </center>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="centro">
                        <asp:GridView ID="grvAsistencia" runat="server" CssClass="texto" AutoGenerateColumns="False" 
                            EmptyDataText="" CellPadding="4" ForeColor="#333333" PageSize="5" Font-Bold="False"
                            OnSelectedIndexChanged="grvAsistencia_SelectedIndexChanged" 
                            OnRowCommand="grvAsistencia_RowCommand" 
                            onrowdatabound="grvAsistencia_RowDataBound">
                            <Columns>
                               <asp:TemplateField HeaderText="Capturó">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCapturo" runat="server" Text='<%# Eval("capturaNombre")%>'></asp:Label>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                          <asp:TemplateField HeaderText="Servicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("serDescripcion")%>'></asp:Label>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Instalación">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("insNombre")%>'></asp:Label>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Integrante">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("empleadoNombre")%>'></asp:Label>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Función">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlFuncionTabla" runat="server" 
                                         OnSelectedIndexChanged="seleccionaFuncion"
                                                                           CssClass=texto AutoPostBack="True" ></asp:DropDownList>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="¿Cambiar?">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCambiar" AutoPostBack=true runat="server" OnCheckedChanged="seleccionaCambiar" CausesValidation=true ></asp:CheckBox>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                            <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                        </asp:GridView>
                    </div>
                </center>
                <table style="margin: 0 auto 0 auto;" visible="false" runat="server" id="tOperaciones">
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>                   
                    <tr>
                        <td>
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" class="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                OnClick="btnNuevo_Click" ToolTip="Nuevo" />
                        </td>
                        <td>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                OnClick="btnGuardar_Click" ToolTip="Guardar" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="PANDetalles" runat="server" HorizontalAlign="Center" Style="display: none">
                                <div>
                                    <div class="nder" style="margin: 0px auto; background-color: white">
                                        <table>
                                            <tr>
                                                <td class="titulo" colspan="4">
                                                    Detalles Horario
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Nombre Completo:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblNomCompleto" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Zona:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblZona" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Servicio:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblServicio" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Instalación:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblInstalacion" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Función:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblFuncion" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Horario:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblHorario" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Descripción del Horario:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblDescHorario" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Fecha de Inicio del Horario:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblFechaInicio" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Hora de Entrada:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblHoraEntrada" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="der">
                                                    Hora de Salida:
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Label ID="lblHoraSalida" runat="server" CssClass="textbox"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Button ID="btnCancelarAsocia" runat="server" Text="Cerrar" CssClass="boton"
                                                        onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Salir de la ventana" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
    </div>
</asp:Content>

