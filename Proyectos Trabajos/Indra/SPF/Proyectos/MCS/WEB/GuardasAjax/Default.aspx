<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="init" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MODULO DE CONTROL DE SERVICIOS</title>
    <style type="text/css">
        .Submenu
        {
            text-align: left;
            border-bottom-color: Menu;
            background-color: ThreeDFace;
        }
        a
        {
	        color: Black;
	        background-color: Transparent;
	        font-variant: small-caps;
        }

        A:Hover
        {
	        color: blue;
        }
        A:link
        {
	        text-decoration: none;
	        background-color: Transparent;
        }
        .back
        {
            background-color: #f7f7f7; width: 1050px; margin: 0 auto 0 auto;
        }
        .encabezado
        {
            background-image: url('Imagenes/header.png'); 
margin: 0 auto 0 auto;
                width: 1000px; height: 84px
        }
        .posicion
        {
            width: 1000px; margin: 5px auto 5px auto; border-collapse: collapse; 
                float: none;  font-size: 10px; color: White; border-style: outset;
                background-color: #727272;  font-weight: bold;
                font-family: Verdana
        }
        .contenido
        {
            background-repeat: repeat; background-image: url(./Imagenes/line.png);
                width: 900px; margin: 0 auto 0 auto; height: 450px
        }
        .fecha
        {
            margin: 5px auto 5px auto; text-align: right;
        }
        .footer
        {
            margin: 5px auto 5px auto; width: 1000px; text-align: center
        }
        #pieObj
        {
            width: 1044px;
        }
    </style>

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
    
        var message="SSP - Servicio de Protección Federal";
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

</head>
<body style="background-color: #f7f7f7">
    <form id="form1" runat="server">
        <div class="back">
            <br />
            <div class="encabezado">
            </div>
            <div class="posicion">
                <div class="fecha">
                    <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="contenido">
                <br />
                <center style="font-family: Verdana">
                    <h3>
                        &nbsp;</h3>
                    <h4>
                        MÓDULO DE CONTROL DE SERVICIOS
                    </h4>
                    <br />
                    <asp:ImageButton ID="imgIniciar" runat="server" BorderStyle="Double" ImageAlign="Middle"
                        ImageUrl="~/Imagenes/Entrar.png" ToolTip="Iniciar Sesión" Width="50px" OnClick="imgIniciar_Click" />
                    <h6>
                        <asp:LinkButton ID="lnkIniciar" runat="server" onmouseover="javascript:this.style.cursor='hand';"
                            OnClick="lnkIniciar_Click">Iniciar Sesión</asp:LinkButton>
                    </h6>
                </center>
            </div>
            <div class="piepagina_principal">
                          <object id="pieObj" class="pieObj" align="middle" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
                codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0">
                <param name="allowScriptAccess" value="sameDomain" />
                <param name="allowFullScreen" value="false" />
                <param name="movie" value="imagenes\pie.swf" />
                <param name="quality" value="high" />
                <param name="bgcolor" value="#ffffff" />
                <param name="wmode" value="transparent" />
                <embed align="middle" allowfullscreen="false" allowscriptaccess="sameDomain" bgcolor="#ffffff"
                    id="pieObj" name="encabezadoIntranet" pluginspage="http://www.adobe.com/go/getflashplayer"
                    quality="high" src="imagenes/pie.swf" type="application/x-shockwave-flash"
                    wmode="transparent"></embed>
            </object>
               
                    </div>
            <br />
        </div>
    </form>
</body>
</html>
