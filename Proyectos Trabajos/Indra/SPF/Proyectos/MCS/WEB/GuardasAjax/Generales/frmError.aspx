<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmError.aspx.cs" Inherits="Generales_frmError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error en el Sistema</title>

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
<body bgcolor="#ffff99">
    <form id="form1" runat="server">
        <div>
            <table width="1200px" align="center">
                <tr>
                    <td align="center">
                        <img src="../Imagenes/header.png" width="1200" height="153" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <img src="../Imagenes/Symbol-Error-Max.png" alt="" height="128" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblMensaje" runat="server" Text="HA OCURRIDO UN ERROR EN EL SISTEMA, INTENTE NUEVAMENTE SU OPERACIÓN O CONSULTE A UN ADMINISTRADOR."
                            Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="Firebrick"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
