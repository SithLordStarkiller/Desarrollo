<%@ Page Title="Reportes Avanzados" Language="C#" MasterPageFile="masterpage.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1">
        <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">    
    <script src="Scripts/jquery-1.11.0.min.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
        
     <script>
         $(function () {
             $("#tabs").tabs();
         });
</script>
    <style type="text/css">
       .ui-state-default .ui-tabs-anchor{font: 14px 'Open Sans', sans-serif;color: #ffffff!important;background: #4A3A41;}
       .ui-state-default .ui-tabs-anchor:hover{color: gray!important}
       .ui-state-default{ border: none !important;}
       .ui-state-active .ui-tabs-anchor{color: #000000!important;background: #EDEDED!important;}
       .ui-state-active .ui-tabs-anchor:hover{color: #000000!important;background: #EDEDED!important;}
       .ui-tabs-active{background:#EDEDED!important }
       .ui-tabs-nav{background: #4A3A41}
       .ui-tabs{ background: #EDEDED;border: none}
       /*reportes*/
       #RankingDS{background:url('imagenes/ReportesAvanzados.png') 0 -905px; width: 1140px;height: 367px;}
       #RankingD{background:url('imagenes/ReportesAvanzados.png') 0 -1302px; width: 1140px;height: 367px; }

       #AsignacionesE{background:url('imagenes/ReportesAvanzados.png') 0 0; width: 1140px;height: 455px;}
       #AsignacionesCxM{background:url('imagenes/ReportesAvanzados.png') 0 -490px; width: 555px;height: 380px;}
       #AsignacionesCM_MC{background:url('imagenes/ReportesAvanzados.png') 0 -1699px; width: 555px;height: 340px;Top: -40px; position: relative;}

       #GestionesM{background:url('imagenes/ReportesAvanzados.png') -585px -490px; width: 555px;height: 380px;}
       #GestionesD{background:url('imagenes/ReportesAvanzados.png') -585px -1699px; width: 555px;height: 340px;Top: -40px; position: relative;}
       #EstadosG{background:url('imagenes/ReportesAvanzados.png') 0 -2877px; width: 555px;height: 407px;}

       #PagosG_MC{background:url('imagenes/ReportesAvanzados.png') 0 -2068px; width: 555px;height: 380px;}
       #PagosNP{background:url('imagenes/ReportesAvanzados.png') -585px -2068px; width: 555px;height: 380px;}

       #SolucionesD_MC{background:url('imagenes/ReportesAvanzados.png') 0 -2479px; width: 1140px;height: 367px;}
       /*fin reportes*/
       #tabs-1,#tabs-2,#tabs-3,#tabs-4,#tabs-5{display: inline-block}
       #tabs-1 div,#tabs-2 div,#tabs-3 div,#tabs-4 div,#tabs-5 div{ margin-bottom: 30px;display: inherit;}
    </style>    
       <div style="margin-left: 10px;margin-top: 30px;width: 1200px;" > 
           <div id="tabs">
                <ul>
                <li><a href="#tabs-1">Ranking</a></li>
                <li><a href="#tabs-2">Asignaciones</a></li>
                <li><a href="#tabs-3">Gestiones</a></li>
                <li><a href="#tabs-4">Pagos</a></li>
                <li><a href="#tabs-5">Soluciones</a></li>
                </ul>

                <div id="tabs-1">
                    <div id="RankingDS" ></div>
                    <div id="RankingD"></div>
                </div>
                <div id="tabs-2">
                    <div id="AsignacionesE" ></div>
                    <div id="AsignacionesCxM"></div>
                    <div id="AsignacionesCM_MC" ></div>
                </div>
                <div id="tabs-3">
                    <div id="GestionesM"></div>
                    <div id="GestionesD"></div>
                    <div id="EstadosG"></div>
                </div>
                <div id="tabs-4">
                    <div id="PagosG_MC"></div>
                    <div id="PagosNP"></div>
                </div>
                <div id="tabs-5">
                    <div id="SolucionesD_MC"></div>
                </div>
            </div>
        </div>
<!--    <div style="text-align: center; margin-left: 30px; top: 30px; margin-bottom: 30px; position: relative">
        <img alt="" src="imagenes/ReportesAvanzados.png" />
    </div>
    -->
</asp:Content>
