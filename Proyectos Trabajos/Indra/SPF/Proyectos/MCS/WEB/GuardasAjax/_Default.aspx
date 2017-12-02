<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_Default.aspx.cs" Inherits="SICOGUA.Presentacion._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Acceso</title>
    <link href="Generales/StyleSheet.css" rel="stylesheet" type="text/css" />

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

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="margin: 0 auto 0 auto;">
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
                        <div class="login">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:UpdatePanel ID="updLogon" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="divError" runat="server" id="divMensajes" visible="false">
                                                    Mensaje(s):
                                                    <ul>
                                                        <li>
                                                            <p>
                                                                <asp:Label ID="lblError" runat="server" Width="100%"></asp:Label>
                                                            </p>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <asp:Timer ID="timLogon" runat="server" Enabled="false" Interval="3000" OnTick="timLogon_Tick">
                                                </asp:Timer>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr style="color: #252525">
                                    <td rowspan="4" valign="top">
                                        <img src="Imagenes/LoginKey.png" width="64px" alt="" />
                                    </td>
                                    <td class="der">
                                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUsuario" runat="server" Width="150px" MaxLength="30" ValidationGroup="Logon"
                                            CssClass="textbox" onFocus="javascript:onFocus(this)" onblur="javascript:onLostFocus(this)"
                                            onKeyDown="if(event.keyCode==13){event.keyCode=9;}">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px" MaxLength="30"
                                            CssClass="textbox" ValidationGroup="Logon" onFocus="javascript:onFocus(this)"
                                            onblur="javascript:onLostFocus(this)" >
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="der">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnEntrar" runat="server" OnClick="btnEntrar_Click" Text="Entrar"
                                                        Width="60px" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                                        ValidationGroup="Logon" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="60px" CssClass="boton"
                                                        onMouseOver="javascript:this.style.cursor='hand';" OnClick="btnCancelar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
