<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSalir.aspx.cs" Inherits="Generales_frmSalir" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salir</title>
    <link href="../Generales/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="JScript.js"></script>

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
<body>
    <form id="form1" runat="server">
        <table align="center">
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div class="Header">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="Logon">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="fondoLogin">
                        <div id="ContenidoInicio" class="cen">
                            <strong><span style="font-size: 16pt">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                SESIÓN FINALIZADA<br />
                                <br />
                                <span style="font-size: 9pt">(DAR CLICK EN LA IMAGEN)<br />
                                    <br />
                                </span>
                                <br />
                                <asp:ImageButton ID="imgSalir" runat="server" BorderColor="#8080FF" BorderStyle="Inset"
                                    ImageUrl="~/Imagenes/Logout.png" ToolTip="SALIR" Width="50px" ImageAlign="Middle"
                                    OnClick="imgSalir_Click" />
                                &nbsp; &nbsp;&nbsp;<asp:ImageButton ID="imgIniciar" runat="server" BorderStyle="Double"
                                    ImageAlign="Middle" ImageUrl="~/Imagenes/Entrar.png" ToolTip="INICIAR SESIÓN"
                                    Width="50px" OnClick="imgIniciar_Click" />
                            </span></strong>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
