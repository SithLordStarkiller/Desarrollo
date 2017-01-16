<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="ReasignarUsuarios.aspx.cs" Inherits="PubliPayments.ReasignarUsuarios"  enableEventValidation="false"  %>
<%@ PreviousPageType VirtualPath="~/AdminUsuarios.aspx" %> 

<%@ Register Src="~/UserControls/TablaUsuarios.ascx" TagPrefix="uc1" TagName="TablaUsuarios" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .RegresarBtn{float: right;margin-left: 10px}
    </style>
    <script >
        var PostBack = "<%=IsPostBack%>";
        $(document).ready(function () {
            
            if ($("select option").length == 0)
                $("select").first().prepend('<option value="" Selected="True">No hay usuarios disponibles</option>');
            else {
                $("select").first().prepend('<option value="" Selected="True">Seleccionar Usuario</option>');
            }
            if ($("#<%=lbInformacion.ClientID%>").html() == "") { $("#<%=lbInformacion.ClientID%>").hide(); }
            $("#<%=NuevoPadre.ClientID%>").val($("#<%=ListPadre.ClientID%>").val());
            $("#SeleccionarTodosDiv").replaceWith($("#SeleccionarTodosTU")[0].innerHTML);
            $("#SeleccionarTodosTU").remove();            

            $("#<%=ListPadre.ClientID%>").on('change', function () {
                $("#<%=NuevoPadre.ClientID%>").val($(this).val());
            });

            $("#Asignar").on('click', function() {
                var cont = 0;
                $("#<%=ListaReasignar.ClientID%>").val("");
                $.each($("#ContentPlaceHolder1_TablaUsuarios_lbUsuariosLondon :checkbox"), function (key, value) {
                    if (value.checked) {
                        $("#<%=ListaReasignar.ClientID%>").val($("#<%=ListaReasignar.ClientID%>").val() + "," + $(value).parent().parent().find(".idUsuarioHidden")[0].textContent);
                        cont++;
                    }
                });
                if (cont > 0 && $("#<%=NuevoPadre.ClientID%>").val() != "") 
                {   
                    $("#<%= btAsignar.ClientID %>").click();
                } else {
                    $("#<%=lbInformacion.ClientID%>").show();
                    $("#<%=lbInformacion.ClientID%>").html("Debe seleccionar almenos un usuario para reasignar");
                }
                if ($("#<%=NuevoPadre.ClientID%>").val() == "") {
                    $("#<%=lbInformacion.ClientID%>").show();
                    $("#<%=lbInformacion.ClientID%>").html("Debe seleccionar al nuevo usuario encargado");
                }
                return false;
            });
        });
    </script>
    <div style="margin-left: 30px; margin-right: 30px; position: relative">
               <div class="TituloSeleccion" style="height: 66px; display: inline-block; width: 100%; vertical-align: middle;">
            Reasignar Usuarios de <%=Nombre%><asp:Image ID="Image1" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 30px; margin-right: 30px; position: relative; top: 10px;" />
            <div id="SeleccionarTodosDiv"></div>
            <div style="width: 30px; display: inline-block"></div>
            <asp:DropDownList ID="ListPadre" runat="server" Height="30px" Width="150px" DataTextField="Usuario" DataValueField="idUsuario" CssClass="Combos"></asp:DropDownList>
            &nbsp;&nbsp;&nbsp;<Button ID="Asignar" style="Width:90px;Height:30px"  class="Botones" > Reasignar</Button>
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Regresar" Width="90px" Height="30px" CssClass="Botones" OnClick="Regresar_Click"  runat="server" Text="Regresar"/> 
        </div>
        <asp:TextBox runat="server" ID ="NuevoPadre" CssClass="Oculto"  />
        <asp:TextBox runat="server" ID ="ListaReasignar" CssClass="Oculto"  />
        <asp:Button ID="btAsignar" runat="server" Height="30px" Text="Asignar" Width="90px" OnClick="btAsignar_Click" CssClass="Botones Oculto" />
    <!--  Mensaje de error  -->
        <div style="text-align: left;width: 100%;">
         <asp:Label runat="server" ID="lbInformacion" CssClass="MensajeAlerta ArribaMenos5"></asp:Label>
     </div>
        <!--  tabla Usuarios  -->
        <uc1:TablaUsuarios runat="server" ID="TablaUsuarios" />
        </div>
</asp:Content>
