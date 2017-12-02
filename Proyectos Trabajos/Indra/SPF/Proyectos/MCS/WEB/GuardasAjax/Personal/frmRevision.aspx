<%@ Page Title="Módulo de Control de Servicios ::: Proceso de Revisión" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmRevision.aspx.cs" Inherits="Personal_frmRevision" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
<script language="javascript" type="text/javascript">

function UploadFileCheck(source, arguments)  

 {     

 var sFile = arguments.Value;     

arguments.IsValid =         

(sFile.endsWith('.pdf'))   ;      


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


function ok() 
{
    var hfColumna = document.getElementById('ctl00_ContentPlaceHolder1_hfColumna');
    var hfIndex = document.getElementById('ctl00_ContentPlaceHolder1_hfIndex');
    var imbVerPdf = document.getElementById('ctl00_ContentPlaceHolder1_imbVerPdf');

    if (hfColumna.value == '-1' && hfIndex.value == '-1') 
    {
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


                document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
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
        var btnAgregarIndex = document.getElementById('ctl00_ContentPlaceHolder1_btnAgregarIndex2');
        btnAgregarIndex.disabled = true;

        document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').innerText = 'Archivo no cargado';
        document.getElementById('ctl00_ContentPlaceHolder1_lblEstadoIndex').style.color = 'red';
        document.getElementById('ctl00_ContentPlaceHolder1_lblNombreIndex').innerText = 'Desconocido';
        document.getElementById('ctl00_ContentPlaceHolder1_lblTamañoIndex').innerText = 'Desconocido';
        document.getElementById('ctl00_ContentPlaceHolder1_lblContenidoIndex').innerText = 'Desconocido';
    }
}



 

    function obtiene_fecha() {

        var fecha_actual = new Date()

        var dia = fecha_actual.getDate()
        var mes = fecha_actual.getMonth() + 1
        var anio = fecha_actual.getFullYear()

        if (mes < 10)
            mes = '0' + mes

        if (dia < 10)
            dia = '0' + dia

        return (dia + "/" + mes + "/" + anio)
    }



    function validaFechaRenuncia() {
        var ckhFecha = document.getElementById('ctl00_ContentPlaceHolder1_ckbRenuncia');
        var fecha = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaRenuncia');
        var calFecha = document.getElementById('ctl00_ContentPlaceHolder1_imbFechaRenuncia');
        var imbAgregarPdf = document.getElementById('ctl00_ContentPlaceHolder1_imbAgregarPdf');
        var imbOficio = document.getElementById('ctl00_ContentPlaceHolder1_imbOficio');

        var txbNoOficio= document.getElementById('ctl00_ContentPlaceHolder1_txbNoOficio');
        var txbFechaOficio = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaOficio');

        var txbObs = document.getElementById('ctl00_ContentPlaceHolder1_txbObs');
        var imbFechaRenuncia= document.getElementById('ctl00_ContentPlaceHolder1_imbFechaRenuncia');
        var txbFechaRenuncia = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaRenuncia');

        if (ckhFecha.checked) {
            fecha.disabled = false
            fecha.value = '';
            calFecha.disabled = false
            imbAgregarPdf.src = '../Imagenes/agregar.png';
            imbAgregarPdf.disabled = false;
            txbNoOficio.disabled = false;
            txbFechaOficio.disabled = false;
            txbObs.disabled = false;
            imbFechaRenuncia.disabled = false;
            txbFechaRenuncia.disabled = false;
            imbOficio.disabled = false;

            txbNoOficio.Value = "";
            txbFechaOficio.Value = "";
            txbObs.Value = "";
            txbFechaRenuncia.Value = "";
        }
        else {
            fecha.disabled = true
            fecha.value = "";
            calFecha.disabled = true
            imbAgregarPdf.src = '../Imagenes/agregarDeshabilitado.png';
            imbAgregarPdf.disabled = true;

            imbAgregarPdf.disabled = true;
            txbNoOficio.disabled = true;
            txbFechaOficio.disabled = true;
            txbObs.disabled = true;
            imbFechaRenuncia.disabled = true;
            txbFechaRenuncia.disabled = true;
            imbOficio.disabled = true;
            txbNoOficio.Value = "";
            txbFechaOficio.Value = "";
            txbObs.Value = "";
            txbFechaRenuncia.Value = "";
        }
    }

    function validaFechaPrimerActa() {
        var ckhFecha = document.getElementById('ctl00_ContentPlaceHolder1_ckbPrimerActa');
        var fecha = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaPrimerActa');
        var calFecha = document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActa');
         var txbNoOficioPrimerActa=document.getElementById('ctl00_ContentPlaceHolder1_txbNoOficioPrimerActa');
         var txbFechaOficioPrimerActa=document.getElementById('ctl00_ContentPlaceHolder1_txbFechaOficioPrimerActa');
         var imbPrimerActaOficio = document.getElementById('ctl00_ContentPlaceHolder1_imbPrimerActaOficio');
         var txbObservacionesActa= document.getElementById('ctl00_ContentPlaceHolder1_txbObservacionesActa');

        if (ckhFecha.checked) {
            fecha.disabled = false
            fecha.value = '';
            calFecha.disabled = false;
            txbNoOficioPrimerActa.disabled = false;
            txbFechaOficioPrimerActa.disabled = false;
            imbPrimerActaOficio.disabled = false;
            txbNoOficioPrimerActa.value = '';
            txbFechaOficioPrimerActa.value = '';
            txbObservacionesActa.value = '';
            txbObservacionesActa.disabled = false;
        }
        else {
            fecha.disabled = true
            fecha.value = "";
            calFecha.disabled = true;
            txbNoOficioPrimerActa.disabled = true;
            txbFechaOficioPrimerActa.disabled = true;
            imbPrimerActaOficio.disabled = true;
            txbNoOficioPrimerActa.value = '';
            txbFechaOficioPrimerActa.value = '';
            txbObservacionesActa.value = ''
            txbObservacionesActa.disabled = true;
        }
    }

   

    function validaFechaCancelacion() {


        

        var ckhCancelado = document.getElementById('ctl00_ContentPlaceHolder1_ckbCancelado');
        var txbFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_txbFechaCancelacion');
        var calFechaCancelacion = document.getElementById('ctl00_ContentPlaceHolder1_imbCancelacion');
        var txbObservaciones = document.getElementById('ctl00_ContentPlaceHolder1_txbObservaciones');


        if (ckhCancelado.checked) {
 
            txbFechaCancelacion.value = '';

   
            txbObservaciones.value = '';
            calFechaCancelacion.disabled = true;
            txbObservaciones.disabled = true;
            txbObservaciones.value = ''
            txbFechaCancelacion.disabled = false;
            calFechaCancelacion.disabled = false;
            txbObservaciones.disabled = false;

 


        }

    }


</script>

     
    <table width="100%">
                        <tr>
                            <td class="titulo" colspan="8">
                                Procedimiento</td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <div id="divErrorRevision" runat="server" class="divError" style="width: 100%" 
                                    visible="false">
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="left" width="100%">
                                                <asp:Label ID="lblerrorRevision" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="subtitulo" colspan="8">
                                Datos Generales</td>
                        </tr>
                        <tr>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="der">
                                Apellido Paterno:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblPaterno" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                Apellido Materno:</td>
                            <td class="izq">
                                <asp:Label ID="lblMaterno" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                Nombre:</td>
                            <td class="izq">
                                <asp:Label ID="lblNombre" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                Jerarquía:</td>
                            <td class="izq">
                                <asp:Label ID="lblJeraraquia" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                N° Empleado:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblNumeroEmpleado" runat="server" Text="-"> </asp:Label>
                            </td>
                            <td class="der">
                                CUIP:
                            </td>
                            <td class="izq" colspan="5">
                                <asp:Label ID="lblCUIP" runat="server" Text="-"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Fecha de Alta:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblFechaAlta" runat="server" Text="-"> </asp:Label>
                            </td>
                            <td class="der">
                                Fecha de Baja:</td>
                            <td class="izq">
                                <asp:Label ID="lblFechaBaja" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="subtitulo" colspan="8">
                                Asignación Actual</td>
                        </tr>
                        <tr>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq" colspan="7">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="der">
                                Servicio:</td>
                            <td class="izq" colspan="7">
                                <asp:Label ID="lblServicio" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Instalación:</td>
                            <td class="izq" colspan="7">
                                <asp:Label ID="lblInstalacion" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Función:</td>
                            <td class="izq" colspan="7">
                                <asp:Label ID="lblFuncion" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Fecha Inicio Comisión:</td>
                            <td class="izq">
                                <asp:Label ID="lblInicioComision" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                Fecha Fin Comisión:</td>
                            <td class="izq">
                                <asp:Label ID="lblFinComision" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="der">
                                <asp:HiddenField ID="hfIdEmpleado" runat="server" />
                                <asp:HiddenField ID="hfPermiso" runat="server" Value="-1" />
                            </td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                <asp:HiddenField ID="hfIdEmpleadoAsignacion" runat="server" />
                                <asp:HiddenField ID="hfIdServicio" runat="server" />
                            </td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                <asp:HiddenField ID="hfIdInstalacion" runat="server" />
                                <asp:HiddenField ID="hfIdRenuncia" runat="server" />
                            </td>
                            <td class="izq">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="subtitulo" colspan="8">
                                Procedimiento</td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                                                    <table class="centro">
                                                        <tr>
                                                            <td class="der">
                                                                &nbsp;</td>
                                                            <td class="cen">
                                                                &nbsp;</td>
                                                            <td class="izq">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="der">
                                                                <asp:ImageButton ID="imgAtras" runat="server" 
                                                                    ImageUrl="~/Imagenes/rewind-icon.png" OnClick="imgAtras_Click" ToolTip="Atrás" 
                                                                    Visible="False" />
                                                            </td>
                                                            <td class="cen">

                                                             <asp:GridView ID="grvBusqueda" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="1" CssClass="texto"   OnRowCommand="grvCartilla_RowCommand" OnRowUpdating="grvBusqueda_RowUpdating" OnPageIndexChanging="grvBusqueda_PageIndexChanging" PageSize="6"  
           
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
                                                                         
                                                                        <asp:BoundField DataField="fechaActaPr" HeaderText="Fecha Procedimiento" />
                                                                       
                                                                       
                                                                        <asp:TemplateField HeaderText="Servicio">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("desServicio") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Instalación">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInstalacion" runat="server" 
                                                                                    Text='<%# Eval("desInstalacion") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         
                                                                        <asp:BoundField DataField="fechaCancelacion" HeaderText="Cancelado" />
                                                                        
                                                                         <asp:TemplateField HeaderText="Actividades">
                                                                            <ItemTemplate>
                                                                            <table>
                                                                            <tr>
                                                                       <td>
                                                                                <asp:ImageButton ID="imbOficio1er" runat="server" CommandArgument="2" 
                                                                                    CommandName="Update" Height="22px" ImageUrl="~/Imagenes/Agregar.png" 
                                                                                    ToolTip="Agregar Oficio" Width="22px" /></td>
                                                                                    <td>
                                                                                <asp:ImageButton ID="imbOficio1raVerifica" runat="server" CommandArgument="4" 
                                                                                    CommandName="Update" Enabled="false" Height="22px" 
                                                                                    ImageUrl="~/Imagenes/novalidado.png" ToolTip="Consultar Oficio" Width="22px" />
</td><td>
                                                                                              <asp:ImageButton ID="imbCancelar1er" runat="server" CommandArgument="6" 
                                                                                    CommandName="Update" Enabled="false" Height="22px" 
                                                                                    ImageUrl="~/Imagenes/Symbol-Delete.png" ToolTip="Eliminar Oficio" Width="22px" />
</td>

                                                                                         </tr></table>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                              
                                                                        <asp:TemplateField HeaderText="Modificar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbSeleccionar" runat="server" CommandArgument="1" 
                                                                                    CommandName="Update" ImageUrl="~/Imagenes/Download.png" ToolTip="Consultar" />
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
                                                                <center>
                                                                    <strong>
                                                                    <asp:Label ID="lblPagina" runat="server" Text="0" Visible="False"></asp:Label>
                                                                    &nbsp;<asp:Label ID="lblde" runat="server" Text="de" Visible="False"></asp:Label>
                                                                    &nbsp;<asp:Label ID="lblPaginas" runat="server" Text="0" Visible="False"></asp:Label>
                                                                    </strong>
                                                                </center>
                                                            </td>
                                                            <td class="izq">
                                                                <asp:ImageButton ID="imgAdelante" runat="server" 
                                                                    ImageUrl="~/Imagenes/forward-icon.png" OnClick="imgAdelante_Click" 
                                                                    ToolTip="Siguiente" Visible="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="der">
                                                                &nbsp;</td>
                                                            <td class="der" colspan="2">
                                                                <asp:Button ID="btnAgregar" runat="server" CssClass="boton" 
                                                                    onclick="btnAgregar_Click" Text="Agregar Procedimiento" />
                                                                <cc1:ModalPopupExtender ID="popAsignacion" runat="server" 
                                                                    BackgroundCssClass="modalBackground" PopupControlID="pnlAsignacion" 
                                                                    TargetControlID="btnAgregar">
                                                                </cc1:ModalPopupExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="der">
                                                                &nbsp;</td>
                                                            <td class="der" colspan="2">
                                                                <asp:HiddenField ID="hfColumna" runat="server" Value="-1" />
                                                                <asp:HiddenField ID="hfNuevo" runat="server" Value="Nuevo" />
                                                            </td>
                                                        </tr>
                                                    </table></ContentTemplate></asp:UpdatePanel>

                                                </td>
                        </tr>
                        <tr>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq" colspan="2">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                            <td class="der">
                                &nbsp;</td>
                            <td class="izq">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="subtitulo" colspan="8">
                                Renuncia</td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 169px" class="izq">
                                            &nbsp;</td>
                                        <td style="width: 147px">
                                            &nbsp;</td>
                                        <td class="izq" style="width: 148px">
                                            &nbsp;</td>
                                        <td style="width: 157px">
                                            &nbsp;</td>
                                        <td style="width: 562px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 169px" class="izq">
                                            &nbsp;</td>
                                        <td style="width: 147px" class="izq">
                                <asp:CheckBox ID="ckbRenuncia" runat="server" Text="Renuncia" onClick="validaFechaRenuncia();" />
                                        </td>
                                        <td class="der" style="width: 148px">
                                No. de Oficio:</td>
                                        <td style="width: 157px" class="izq">
                                            <table style="width: 30%;">
                                                <tr>
                                                    <td>
                                            <asp:TextBox ID="txbNoOficio" runat="server" CssClass="texto" Width="133px" 
                                                            MaxLength="40" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 562px" class="izq" rowspan="2">
                        
                                            <table style="width: 19%;">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imbVerPdf" runat="server" ImageUrl="~/Imagenes/novalidado.png" 
                                                            Width="30px" onclick="imbVerPdf_Click" 
                                                            ToolTip="Ver oficio" Enabled="False" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imbAgregarPdf" runat="server" 
                                                            ImageUrl="~/Imagenes/agregarDeshabilitado.png" Width="30px" 
                                                            ToolTip="Agregar oficio" onclick="imbAgregarPdf_Click" 
                                                          />
                                                    </td>
                                                </tr>
                                            </table>
                                  
                                      
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 169px" class="der">
                                            Fecha de Renuncia:</td>
                                        <td style="width: 147px">
                                <table style="width: 43%; border-collapse: collapse;">
                                    <tr>
                                        <td class="izq">
                                            <asp:TextBox ID="txbFechaRenuncia" runat="server" CssClass="textbox" 
                                                Enabled="False" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>

                                                                           <cc1:CalendarExtender ID="calFechaRenunciaP" runat="server" PopupButtonID="imbFechaRenuncia"
                                                                TargetControlID="txbFechaRenuncia">
                                                            </cc1:CalendarExtender>

    


                                        </td>
                                        <td>
                                                            <asp:ImageButton ID="imbFechaRenuncia" runat="server" 
                                                Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="16px" EnableTheming="False" />

                                                            </td>
                                    </tr>
                                </table>
                                        </td>
                                        <td class="der" style="width: 148px">
                                            Fecha de Oficio:</td>
                                        <td style="width: 157px" class="izq">
                                <table style="width: 28%; border-collapse: collapse;">
                                    <tr>
                                        <td class="izq">
                                            <asp:TextBox ID="txbFechaOficio" runat="server" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>



                                                                           <cc1:CalendarExtender ID="txbFechaOficio_CalendarExtender" runat="server" PopupButtonID="imbOficio"
                                                                TargetControlID="txbFechaOficio">
                                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                                            <asp:ImageButton ID="imbOficio" runat="server" 
                                                Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="16px" />

                                                            </td>
                                    </tr>
                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 169px" class="der">
                                            Observaciones:</td>
                                        <td class="izq" colspan="3">
                                            <asp:TextBox ID="txbObs" runat="server" Height="72px" TextMode="MultiLine" onKeyUp="Count(this,300)"
                                                CssClass="texto" onblur="javascript:onLosFocus(this)"
                                                Width="413px"   MaxLength="300" onfocus="javascript:onFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>
                                        </td>
                                        <td class="izq">
                                            &nbsp;</td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr class="cen">
                            <td class="centro" colspan="8">
                                &nbsp;</td>
                        </tr>
                        <tr class="cen">
                            <td class="centro" colspan="8">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 297px">
                                            &nbsp;</td>
                                        <td style="width: 68px">
                                            <asp:Button ID="btnGuardar" runat="server" CssClass="boton" Text="Guardar" 
                                                Width="100px" onclick="btnGuardar_Click" />
                                        </td>
                                        <td style="width: 67px">
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="boton" Text="Cancelar" 
                                                Width="100px" onclick="btnCancelar_Click" />
                                        </td>
                                        <td style="width: 52px">
                                            <asp:Button ID="btnBuscar" runat="server" CssClass="boton" Text="Buscar" 
                                                Width="100px" onclick="btnBuscar_Click" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="der" colspan="8">
                                &nbsp;</td>
                        </tr>
                        </table>




                              <asp:Panel ID="pnlAsignacion" runat="server" >
            <div style="background-color: White;  margin: 0 auto 0 auto;" class="nder">
                <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                    margin: 30px auto 30px auto; border: outset 2px Black;">
                    <asp:UpdatePanel ID="upnAsignacion" runat="server">
                        <ContentTemplate>

                        <table width="750px" style="width: 680px; padding-left:1em; padding-right:1em; padding-top:iem; padding-bottom:1em;"><tr><td colspan="4">&nbsp;</td></tr><tr>
                            <td class="titulo" 
                                colspan="4">Procedimiento</td></tr><tr>
                            <td colspan="4">
                                <div ID="divErrorActas"  style="width: 100%" runat="server" class="divError" visible="false">
                                    <table style="width: 100%">
                                        <tr>
                                            <td width="100%" align="left">
                                                <asp:Label ID="lblerrorActas" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </td></tr><tr>
                            <td class="subtitulo" colspan="4">procedimiento</td></tr><tr>
                            <td style="width: 303px">&nbsp;</td><td style="width: 136px">&nbsp;</td>
                            <td style="width: 138px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr><tr>
                            <td style="width: 303px" class="izq">
                                <asp:CheckBox ID="ckbPrimerActa" runat="server" 
                                    Text="Procedimiento" 
                                    onclick="validaFechaPrimerActa();" />
                            </td><td style="width: 136px">&nbsp;</td><td class="der" style="width: 138px">No de 
                            Oficio:</td><td style="width: 155px" class="izq">
                                <asp:TextBox ID="txbNoOficioPrimerActa" runat="server" CssClass="texto" onblur="javascript:onLosFocus(this)"
                                    MaxLength="40" onfocus="javascript:onFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>
                            </td></tr><tr><td class="der" style="width: 303px">Fecha de Procedimiento:</td>
                                <td style="width: 136px">
                                    <table style="width: 43%; border-collapse: collapse;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txbFechaPrimerActa" runat="server" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>


 

                                                <cc1:CalendarExtender ID="calFechaPrimerActa" runat="server" 
                                                    PopupButtonID="imbPrimerActa" TargetControlID="txbFechaPrimerActa">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imbPrimerActa" runat="server" Height="16px" 
                                                    ImageUrl="~/Imagenes/Calendar.png" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td><td class="der" style="width: 138px">Fecha de Oficio:</td>
                                <td style="width: 155px" class="izq">
                                    <table style="width: 43%; border-collapse: collapse;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txbFechaOficioPrimerActa" runat="server" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>

            

                                                <cc1:CalendarExtender ID="calOficioPrimeraActa" runat="server" 
                                                    PopupButtonID="imbPrimerActaOficio" TargetControlID="txbFechaOficioPrimerActa">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imbPrimerActaOficio" runat="server" Height="16px" 
                                                    ImageUrl="~/Imagenes/Calendar.png" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td></tr><tr><td style="width: 303px" class="der">Observaciones:</td>
                                <td class="izq" colspan="3">
                                    <asp:TextBox ID="txbObservacionesActa" runat="server" CssClass="texto" 
                                        Enabled="False" Height="45px" MaxLength="300" 
                                        onblur="javascript:onLosFocus(this)" 
                                        onchange="this.value=quitaacentos(this.value)" 
                                        onfocus="javascript:onFocus(this)" 
                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onKeyUp="Count(this,300)" 
                                        TextMode="MultiLine" Width="396px"></asp:TextBox>
                                </td>
                                </tr><tr><td style="width: 303px">&nbsp;</td><td style="width: 136px">&nbsp;</td>
                                <td style="width: 138px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr><tr>
                            <td class="subtitulo" colspan="4">cancelación</td></tr><tr>
                            <td style="width: 303px">&nbsp;</td><td style="width: 136px">&nbsp;</td>
                            <td style="width: 138px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr><tr>
                            <td style="width: 303px" class="izq">
                                <asp:radiobutton GroupName="cancelacion" ID="ckbCancelado" runat="server" 
                                    Text="CANCELADO" onclick="validaFechaCancelacion();" Enabled="False"  />
                            </td><td style="width: 136px">&nbsp;</td><td style="width: 138px">&nbsp;</td>
                            <td style="width: 155px">&nbsp;</td></tr><tr>
                            <td class="der" 
                                style="width: 303px">Fecha de Cancelación:</td><td style="width: 136px">
                                <table style="width: 43%; border-collapse: collapse;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txbFechaCancelacion" runat="server" CssClass="textbox" 
                                                Enabled="False" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}"></asp:TextBox>




                                            <cc1:CalendarExtender ID="calCancelacion" runat="server" 
                                                PopupButtonID="imbCancelacion" TargetControlID="txbFechaCancelacion">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imbCancelacion" runat="server" Height="16px" 
                                                ImageUrl="~/Imagenes/Calendar.png" Width="16px" Enabled="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td><td style="width: 138px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr><tr>
                            <td class="der" style="width: 303px">Observaciones:</td>
                            <td colspan="3" 
                                rowspan="3" class="izq">
                            <asp:TextBox ID="txbObservaciones" runat="server" CssClass="texto"  onKeyUp="Count(this,300)"
                                Height="45px" TextMode="MultiLine" Width="396px" Enabled="False" onblur="javascript:onLosFocus(this)"
                                    
                                    
                            
                               onfocus="javascript:onFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" MaxLength="300"
                                    
                                    ></asp:TextBox>
                            </td></tr><tr><td style="width: 303px">&nbsp;</td></tr><tr>
                            <td style="width: 303px">&nbsp;</td></tr><tr>
                            <td style="width: 303px">
                                <asp:HiddenField ID="hfIndex" runat="server" Value="-1" />
                            </td><td style="width: 136px">&nbsp;</td>
                            <td style="width: 138px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr>
                        
                            </table>

                     
                                <table style="width:42%;">
                                    <tr>
                                        <td style="width: 273px">
                                            &nbsp;</td>
                                        <td style="width: 101px">
                                            <asp:Button ID="btnAgregarIndex" runat="server" CssClass="boton" Text="Agregar" 
                                                Width="100px" onclick="btnAgregarIndex_Click" />
                                        </td>
                                        <td style="width: 100px">
                                            <asp:Button ID="btn" runat="server" CssClass="boton" Text="Cancelar" 
                                                Width="100px" onclick="btn_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 273px">
                                            &nbsp;</td>
                                        <td style="width: 101px">
                                            &nbsp;</td>
                                        <td style="width: 100px">
                                            &nbsp;</td>
                                    </tr>
                                </table>

                                                           </ContentTemplate>
                    </asp:UpdatePanel>
                    
                                </div>
            </div>
        </asp:Panel>
         

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
                                    Agregar Oficio</td>
                            </tr>
                            <tr>
                                <td class="izq" colspan="3">
                                    <div ID="divErrorIndex" runat="server" class="divError" 
                                        style="visibility: hidden;">
                                        <asp:Label ID="lblErrorIndex" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td class="izq">
                               
                                    <asp:Label ID="Label1" runat="server" 
                                        style="font-size: 9px; font-style: italic" Text="(Archivos no mayores a 4MB)"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="imgCheck" runat="server" Height="20px" 
                                        Style="visibility: hidden;" Width="20px" />
                                </td>
                                <td class="izq">
                                    <cc1:AsyncFileUpload ID="AsyncFileUpload" runat="server" 
                                        CompleteBackColor="Gainsboro" ErrorBackColor="Gainsboro" Height="25px" 
                                        OnClientUploadComplete="finCarga" OnClientUploadError="error" 
                                        OnClientUploadStarted="iniciarCarga" 
                                        OnUploadedComplete="AsyncFileUpload_UploadedComplete" ThrobberID="Throbber" 
                                        UploadingBackColor="Gainsboro" Width="447px" />
                                </td>
                                <td>
                                    <asp:Image ID="imgCargar" runat="server" Height="20px" 
                                        ImageUrl="~/Imagenes/loading.gif" Style="visibility: hidden;" Visible="true" 
                                        Width="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="izq">
                                    <asp:Button ID="btnAgregarIndex2" runat="server" CssClass="boton" 
                                        Enabled="False" OnClick="btnAgregarIndex2_Click" Text="Guardar" />
                                    <asp:Button ID="btnCancelarIndex" runat="server" CssClass="boton" 
                                        OnClick="btnCancelarIndex_Click" Text="Cancelar" />
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
                                    <table id="tabDetallesIndex" style="width:100%; visibility:hidden">
                                        <tr>
                                            <td class="style1">
                                                Estado:</td>
                                            <td class="izq">
                                                <asp:Label ID="lblEstadoIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Nombre del archivo:</td>
                                            <td class="izq">
                                                <asp:Label ID="lblNombreIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Tamaño:</td>
                                            <td class="izq">
                                                <asp:Label ID="lblTamañoIndex" runat="server">-</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Tipo de contenido:</td>
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
                                    &nbsp;</td>
                                <td>
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
                
                
                

</asp:Content>

