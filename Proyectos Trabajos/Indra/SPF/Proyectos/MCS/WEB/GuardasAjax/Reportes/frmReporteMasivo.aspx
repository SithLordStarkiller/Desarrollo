<%@ Page  Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmReporteMasivo.aspx.cs" Inherits="Reportes_frmReporteMasivo" Title="Módulo de Control de Servicios ::: Reporte Movimiento del Personal"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <table style="width: 100%;">
    <tr>
        <td class="titulo" colspan="5">
            Reporte del movimiento de personal en las instalaciones</td>
    </tr>
    <tr>
        <td class="subtitulo" colspan="5">
            Criterios de búsqueda</td>
    </tr>
    <tr>
        <td style="width: 24%">
            &nbsp;</td>
        <td style="width: 2%">
            &nbsp;</td>
        <td style="width: 13%">
            &nbsp;</td>
        <td style="width: 1%">
            &nbsp;</td>
        <td style="width: 69%">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 24%" class="der">
            Fecha del Reporte:</td>
        <td style="width: 2%">
            del</td>
        <td style="width: 13%">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txbFechaInicio" runat="server" CssClass="texto"
                          onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                        
                        ></asp:TextBox>
                    </td>
                    <td>

                                                                              <cc1:CalendarExtender ID="ceFechaInicio" runat="server" PopupButtonID="imbFechaInicio"
                                                                TargetControlID="txbFechaInicio">
                                                            </cc1:CalendarExtender>

                                                            <asp:ImageButton ID="imbFechaInicio" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" />

                                                                              </td>
                    <td>
                                                            <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" ErrorMessage="Se Requiere la Fecha de Inicio"
                                                                Font-Bold="True" ControlToValidate="txbFechaInicio" SetFocusOnError="True" 
                                                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>

                                                                              </td>
                </tr>
            </table>
        </td>
        <td style="width: 1%">
            Al</td>
        <td style="width: 69%">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txbFechaFin" runat="server" CssClass="texto"
                        
                          onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                        
                        ></asp:TextBox>
                    </td>
                    <td>
                            
                                                                              <cc1:CalendarExtender ID="ceFechaFin" runat="server" PopupButtonID="imbFechaFin"
                                                                TargetControlID="txbFechaFin">
                                                            </cc1:CalendarExtender>


                                                            <asp:ImageButton ID="imbFechaFin" 
                            runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" 
                           />
                            

                                                                              </td>
                    <td>
                                                            <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ErrorMessage="Se Requiere la Fecha de Fin"
                                                                Font-Bold="True" ControlToValidate="txbFechaFin" SetFocusOnError="True" 
                                                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>


                                                                              </td>
                    <td>
                                                            <asp:CompareValidator ID="covFecha" runat="server" ErrorMessage="La Fecha de Inicio no puede ser Mayor a la Fecha Fin"
                                                                ControlToValidate="txbFechaInicio" Font-Bold="True" SetFocusOnError="True" Type="Date"
                                                                ValidationGroup="SICOGUA" ValueToCompare="&lt;=" 
                                                                Operator="LessThanEqual" ControlToCompare="txbFechaFin">*</asp:CompareValidator>


                                                                              </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 24%">
            &nbsp;</td>
        <td style="width: 2%">
            &nbsp;</td>
        <td style="width: 13%">
            &nbsp;</td>
        <td style="width: 1%">
            &nbsp;</td>
        <td class="der" style="width: 69%">
            <asp:Button ID="btnGenerar" runat="server" CssClass="boton" 
                Text="Generar Formato" onclick="Button1_Click" ValidationGroup="SICOGUA" />
        </td>
    </tr>
</table>
</asp:Content>

