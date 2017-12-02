<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmInicio.aspx.cs" Inherits="SICOGUA.Presentacion.frmInicio" Title="Módulo de Control de Servicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="Generales/JScript.js"></script>

    <script language="javascript" type="text/javascript">
        window.moveTo(0,0);
        if(document.all)
        {
            top.window.resizeTo(screen.availWidth,screen.availHeight);
        }
        else if(document.layers || document.getElementById)
        {
            if(top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth)
            {
                top.window.outerHeight = screen.availHeight;
                top.window.outerWidth = screen.availWidth;
            }
        }    
    
        var message="Servicio de Protección Federal";
        function clickIE4(){
	        if (event.button==2){
		        alert(message);
		        return false;
	        }
        }
         
        function clickNS4(e){
        if (document.layers||document.getElementById&&!document.all){
		        if (e.which==2||e.which==3){
			        alert(message);
			        return false;
		        }
	        }
        }
         
        if (document.layers){
	        document.captureEvents(Event.MOUSEDOWN);
	        document.onmousedown=clickNS4;
	    }
        else if (document.all&&!document.getElementById){
	        document.onmousedown=clickIE4;
	    }
	        
        document.oncontextmenu=new Function("alert(message);return false")
    </script>

    <center style="font-family: Verdana">

        <br />
        <br />
        <br />
        <br />
        <br />

        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/logo.png" />
        <br />
        <h3>
            MÓDULO DE CONTROL DE SERVICIOS
        </h3>
        <h4>
            &nbsp;</h4>
    </center>
</asp:Content>
