<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmInmovilidad.aspx.cs" Inherits="Personal_frmInmovilidad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Generales/wucMensaje.ascx" TagName="wucMensaje" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <script language="javascript" type="text/javascript">

        function UploadFileCheck(source, arguments) {
            var sFile = arguments.Value;
            arguments.IsValid = (sFile.endsWith('.pdf'));
        }

        function error(sender, args) {
            var lblError = args.get_errorMessage().toString();
            if (lblError == 'Acceso denegado.') {
                document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').innerText = 'Error Desconocido';
                var imgError = document.getElementById('ctl00_ContentPlaceHolder1_imgCheck');
                imgError.src = '../Imagenes/Symbol-Error.png';
                document.getElementById('ctl00_ContentPlaceHolder1_imgCheck').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_divErrorIndex').style.visibility = 'visible';
                document.getElementById('tabDetallesIndex').style.visibility = 'visible';

                var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
                btnAgregarIndex.disabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = "red";
                document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = 'Desconocido';
                document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = 'Desconocido';
                document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = 'Desconocido';
            }
            else {
                var imgError = document.getElementById('ctl00_ContentPlaceHolder1_imgCheck');
                imgError.src = '../Imagenes/Symbol-Error.png';
                document.getElementById('ctl00_ContentPlaceHolder1_imgCheck').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_divErrorIndex').style.visibility = 'visible';
                document.getElementById('tabDetallesIndex').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').innerText = args.get_errorMessage();
                var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
                btnAgregarIndex.disabled = true;


                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = 'red';
                document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = 'Desconocido';
                document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = 'Desconocido';
                document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = 'Desconocido';
            }
            finCarga();
        }

        function iniciarCarga() {
            document.getElementById('ctl00_ContentPlaceHolder1_imgCargar').style.visibility = 'visible';
            document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').innerText = '';
            document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').style.visibility = 'hidden';
            document.getElementById('ctl00_ContentPlaceHolder1_divErrorIndex').style.visibility = 'hidden';
            document.getElementById('tabDetallesIndex').style.visibility = 'hidden';
        }


        function ok() {
            var hfColumna = document.getElementById('ctl00_ContentPlaceHolder1_hfColumna');
            var hfIndex = document.getElementById('ctl00_ContentPlaceHolder1_hfIndex');
            var imbVerPdf = document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf');

            if (hfColumna.value == '-1' && hfIndex.value == '-1') {
                hfColumna.Value = "-1";
                imbVerPdf.src = "../Imagenes/Symbol-Check.png";
                imbVerPdf.disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_hfColumna').innerText = '-1'
                document.getElementById('ctl00_ContentPlaceHolder1_hfIndex').innerText = '-1'
            }
        }

        function ok1() {
            var hfColumna = document.getElementById('ctl00_ContentPlaceHolder1_hfColumna');
            var hfIndex = document.getElementById('ctl00_ContentPlaceHolder1_hfIndex');



            document.getElementById('ctl00_ContentPlaceHolder1_hfColumna').innerText = '-1'
            document.getElementById('ctl00_ContentPlaceHolder1_hfIndex').innerText = '-1'

        }


        function finCarga(sender, args) {
            document.getElementById('ctl00_ContentPlaceHolder1_imgCargar').style.visibility = 'hidden';

            try {
                var fileExtension = args.get_fileName();
                if (fileExtension.indexOf('.pdf') == -1) {
                    document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').innerText = 'La extensión del archivo seleecionado es invalido';
                    var imgError = document.getElementById('ctl00_ContentPlaceHolder1_imgCheck');
                    imgError.src = '../Imagenes/Symbol-Error.png';
                    document.getElementById('ctl00_ContentPlaceHolder1_imgCheck').style.visibility = 'visible';
                    document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').style.visibility = 'visible';
                    document.getElementById('ctl00_ContentPlaceHolder1_divErrorIndex').style.visibility = 'visible';
                    document.getElementById('tabDetallesIndex').style.visibility = 'visible';
                    var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
                    btnAgregarIndex.disabled = true;

                    document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').disabled = true;
                    document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').src = "../Imagenes/novalidado.png";
                    document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
                    document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = 'red';
                    document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = args.get_fileName();
                    document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = args.get_length() + ' Bytes';
                    document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = args.get_contentType();
                }
                else {
                    if (parseInt(args.get_length()) >= 4194304) {
                        document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').innerText = 'El archivo excede el tamaño maximo establecido de 4 MB';
                        var imgError = document.getElementById('ctl00_ContentPlaceHolder1_imgCheck');
                        imgError.src = '../Imagenes/Symbol-Error.png';
                        document.getElementById('ctl00_ContentPlaceHolder1_imgCheck').style.visibility = 'visible';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').style.visibility = 'visible';
                        document.getElementById('ctl00_ContentPlaceHolder1_divErrorIndex').style.visibility = 'visible';
                        document.getElementById('tabDetallesIndex').style.visibility = 'visible';
                        var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
                        btnAgregarIndex.disabled = true;

                        document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').disabled = true;
                        document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').src = "../Imagenes/novalidado.png";
                        document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
                        document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').src = "../Imagenes/agregarDeshabilitado.png";
                        document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = 'red';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = args.get_fileName();
                        document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = args.get_length() + ' Bytes';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = args.get_contentType();

                    }
                    else {
                        var imgError = document.getElementById('ctl00_ContentPlaceHolder1_imgCheck');
                        imgError.src = '../Imagenes/Symbol-Check.png';
                        document.getElementById('ctl00_ContentPlaceHolder1_imgCheck').style.visibility = 'visible';
                        document.getElementById('tabDetallesIndex').style.visibility = 'visible';
                        var hfGuardar = document.getElementById('ctl00_ContentPlaceHolder1_hfGuardar');
                        hfGuardar.value = '1';
                        var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
                        btnAgregarIndex.disabled = false;

                        document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo listo para cargarse...';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = '#006600';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = args.get_fileName();
                        document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = args.get_length() + ' Bytes';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = args.get_contentType();
                        document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').disabled = false;
                        document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').src = "../Imagenes/Symbol-Check.png";
                    }
                }
            }

            catch (e) {
                document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').innerText = 'Error, archivo en formato desconocido o dañado';
                var imgError = document.getElementById('ctl00_ContentPlaceHolder1_imgCheck');
                imgError.src = '../Imagenes/Symbol-Error.png';
                document.getElementById('ctl00_ContentPlaceHolder1_imgCheck').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_lblErrorIndex').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_divErrorIndex').style.visibility = 'visible';
                document.getElementById('tabDetallesIndex').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').src = "../Imagenes/novalidado.png";
                var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
                btnAgregarIndex.disabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = 'red';
                document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = 'Desconocido';
                document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = 'Desconocido';
                document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = 'Desconocido';
            }
        }

        function cancela() {
            document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').disabled = true;
            document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf').src = "../Imagenes/novalidado.png";
        }

    </script>
    <table class="tamanio">
        <tr>
            <td class="titulo" colspan="2">
                Inmovilidad
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:wucMensaje ID="wucMensaje" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="2">
                Datos Generales
            </td>
        </tr>
        <tr>
            <td style="width: 166px" align="center">
                <img id="imgFoto" alt="" src="ghFotografia.ashx" style="height: 112px; width: 98px" />
            </td>
            <td>
                <table class="tamanio">
                    <tr>
                        <td class="der" width="100">
                            Integrante:
                        </td>
                        <td>
                            <asp:Label ID="lblIntegrante" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der" width="100">
                            cuip:
                        </td>
                        <td>
                            <asp:Label ID="lblCuip" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            no Empleado:
                        </td>
                        <td>
                            <asp:Label ID="lblNoEmpleado" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            rfc:
                        </td>
                        <td>
                            <asp:Label ID="lblRfc" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            jerarquía:
                        </td>
                        <td>
                            <asp:Label ID="lblJerarquia" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            curp:
                        </td>
                        <td>
                            <asp:Label ID="lblCurp" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            servicio:
                        </td>
                        <td>
                            <asp:Label ID="lblServicio" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            fecha de alta:
                        </td>
                        <td>
                            <asp:Label ID="lblFechaAlta" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            instalación:
                        </td>
                        <td>
                            <asp:Label ID="lblInstalacion" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            fecha de baja:
                        </td>
                        <td>
                            <asp:Label ID="lblFechaBaja" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="subtitulo" colspan="2" style="height: 17px">
                Inmovilidad
            </td>
        </tr>
    </table>
    <table class="tamanio">
        <tr>
            <td class="der">
                Motivo Inmovilidad:
            </td>
            <td style="width: 117px">
                <asp:DropDownList ID="ddlMotivoInmovilidad" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
            <td class="der">
                Fecha Inicio:
            </td>
            <td>
                <asp:TextBox ID="txbFechaInicio" runat="server" CssClass="textbox" MaxLength="10"
                    onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                    onkeypress="event.returnValue= validarNoEscritura(event);" Width="125px"></asp:TextBox>
                <asp:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbFechaInicio"
                    TargetControlID="txbFechaInicio">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imbFechaInicio" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                    Width="16px" />
            </td>
            <td class="der">
                Fecha Fin:
            </td>
            <td>
                <asp:TextBox ID="txbFechaFin" runat="server" CssClass="textbox" MaxLength="10" onblur="javascript:onLosFocus(this)"
                    onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                    onkeypress="event.returnValue= validarNoEscritura(event);" Width="125px"></asp:TextBox>
                <asp:CalendarExtender ID="txbFechaFin_CalendarExtender" runat="server" PopupButtonID="imbFechaFin"
                    TargetControlID="txbFechaFin">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imbFechaFin" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                    Width="16px" />
            </td>
        </tr>
        <tr>
            <td class="der">
                Descripción:
            </td>
            <td colspan="5">
                <asp:TextBox ID="txbDescripcion" runat="server" Height="50px" TextMode="MultiLine"
                    onblur="javascript:onLosFocus(this)" onKeyUp="Count(this,500)" onChange="Count(this,500); this.value=quitaacentos(this.value)"
                    onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                    Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="der">
                Autorizo:
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlAutoriza" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
            <td class="der">
                Documento:
            </td>
            <td>
                <asp:ImageButton ID="imbVerPdf" runat="server" Enabled="False" ImageUrl="~/Imagenes/novalidado.png"
                    ToolTip="Ver oficio" Width="30px" OnClick="imbVerPdf_Click" />
                <asp:ImageButton ID="imbAgregarPdf" runat="server" ImageUrl="~/Imagenes/agregar.png"
                    ToolTip="Agregar oficio" Width="30px" OnClick="imbAgregarPdf_Click" />
            </td>
        </tr>
        <tr>
            <td class="der">
                &nbsp;
            </td>
            <td style="width: 117px">
                &nbsp;
            </td>
            <td class="der">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td class="der">
                &nbsp;
            </td>
            <td align="center">
                &nbsp;
                <asp:Button ID="btnAgregar" runat="server" CssClass="boton" Text="Agregar" OnClick="btnAgregar_Click" />
            </td>
        </tr>
        <tr>
            <td class="der">
                &nbsp;
            </td>
            <td style="width: 117px">
                &nbsp;
            </td>
            <td class="der">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td class="der">
                &nbsp;
            </td>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6">
                <table>
                    <tr>
                        <td class="der">
                            &nbsp;
                        </td>
                        <td class="cen">
                            &nbsp;
                        </td>
                        <td class="izq">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            &nbsp;
                        </td>
                        <td align="center">
                            <%-- <center>--%>
                            <div id="Layer1" style="height: 300px; overflow: scroll;">
                                <asp:GridView ID="grvBusqueda" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                    AllowSorting="true" CellPadding="1" CssClass="texto" ShowHeaderWhenEmpty="True"
                                    Width="850px" OnRowDataBound="grvBusqueda_RowDataBound" OnRowUpdating="grvBusqueda_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="idPersona" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                </Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="35px" />
                                            <ItemStyle Width="18px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Motivo Inmovilidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMotivo" runat="server" Text='<%# Eval("miDescripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("eiDescripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="eiFechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}"
                                            ItemStyle-Width="80px" />
                                        <asp:BoundField DataField="eiFechaFin" HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}"
                                            ItemStyle-Width="80px" />
                                        <asp:BoundField DataField="personaAutoriza" HeaderText="Autorizo" ItemStyle-Width="200" />
                                        <asp:TemplateField HeaderText="Documento">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="imbDocumento" runat="server" CommandArgument="4" CommandName="Update"
                                                                Enabled="false" Height="22px" ImageUrl="~/Imagenes/novalidado.png" ToolTip="Consultar Oficio"
                                                                Width="22px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
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
                            <%--</center>--%>
                        </td>
                        <td class="izq">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:HiddenField ID="hfColumna" runat="server" Value="-1" />
                            <asp:HiddenField ID="hfNuevo" runat="server" Value="Nuevo" />
                            <asp:HiddenField ID="hfIndex" runat="server" Value="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnGuardar" runat="server" CssClass="boton" Text="Guardar" ValidationGroup="SICOGUA"
                                            OnClick="btnGuardar_Click" onMouseOver="javascript:this.style.cursor='hand';" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="boton" Text="Cancelar" OnClick="btnCancelar_Click"
                                            onMouseOver="javascript:this.style.cursor='hand';" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="boton" OnClick="btnBuscar_Click"
                                            onMouseOver="javascript:this.style.cursor='hand';" TabIndex="12" ToolTip="Buscar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfIdEmpleado" runat="server" />
    <cc1:ModalPopupExtender ID="popAgregarOficioIndex" runat="server" PopupControlID="pnlAgregarOficioIndex"
        TargetControlID="imbAgregarPdf" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarOficioIndex" runat="server" DefaultButton="imbAgregarPdf">
        <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
            <div style="background-repeat: repeat; background-image: url(../Imagenes/line.png);
                margin: 30px auto 30px auto; border: outset 1px Black;">
                <asp:UpdatePanel ID="UpdatePanelFileLoad" runat="server">
                    <ContentTemplate>
                        <table style="width: 481px">
                            <tr>
                                <td class="izq subtitulo" colspan="3">
                                    Agregar Oficio
                                </td>
                            </tr>
                            <tr>
                                <td class="izq" colspan="3">
                                    <div id="divErrorIndex" runat="server" class="divError" style="visibility: hidden;">
                                        <asp:Label ID="lblErrorIndex" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="izq">
                                    <asp:Label ID="Label1" runat="server" Style="font-size: 9px; font-style: italic"
                                        Text="(Archivos no mayores a 4MB)"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="imgCheck" runat="server" Height="20px" Style="visibility: hidden;"
                                        Width="20px" />
                                </td>
                                <td class="izq">
                                    <cc1:AsyncFileUpload ID="AsyncFileUpload" runat="server" CompleteBackColor="Gainsboro"
                                        ErrorBackColor="Gainsboro" Height="25px" OnClientUploadComplete="finCarga" OnClientUploadError="error"
                                        OnClientUploadStarted="iniciarCarga" OnUploadedComplete="AsyncFileUpload_UploadedComplete"
                                        ThrobberID="Throbber" UploadingBackColor="Gainsboro" Width="447px" />
                                </td>
                                <td>
                                    <asp:Image ID="imgCargar" runat="server" Height="20px" ImageUrl="~/Imagenes/loading.gif"
                                        Style="visibility: hidden;" Visible="true" Width="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="izq">
                                    <asp:Button ID="btnAgregarIndex2" runat="server" CssClass="boton" Enabled="False"
                                        OnClick="btnAgregarIndex2_Click" Text="Guardar" />
                                    <asp:Button ID="btnCancelarIndex" runat="server" CssClass="boton" OnClick="btnCancelarIndex_Click"
                                        OnClientClick="cancela()" Text="Cancelar" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hfGuardar" runat="server" Value="-1" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <table id="tabDetallesIndex" style="width: 100%; visibility: hidden">
                                        <tr>
                                            <td class="style1">
                                                Estado:
                                            </td>
                                            <td class="izq">
                                                <asp:Label ID="lblEstadoIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Nombre del archivo:
                                            </td>
                                            <td class="izq">
                                                <asp:Label ID="lblNombreIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Tamaño:
                                            </td>
                                            <td class="izq">
                                                <asp:Label ID="lblTamañoIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Tipo de contenido:
                                            </td>
                                            <td class="izq">
                                                <asp:Label ID="lblContenidoIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
