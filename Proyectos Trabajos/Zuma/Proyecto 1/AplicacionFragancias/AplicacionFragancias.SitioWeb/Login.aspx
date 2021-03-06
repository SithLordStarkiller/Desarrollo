﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/Sitio.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="loginbox" style="margin-top: 100px;" class="mainbox col-md-4 col-md-offset-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <div class="panel-title">Login</div>
                <div style="float: right; font-size: 80%; position: relative; top: -10px"><a href="#">Olvide Contraseña?</a></div>
            </div>

            <div style="padding-top: 30px" class="panel-body">

                <div style="display: none" id="login-alert" class="alert alert-danger col-sm-4"></div>

                <form runat="server" id="loginform" class="form-horizontal" role="form">

                    <div style="margin-bottom: 25px; top: 0px; left: 0px;" class="input-group col-md-8 col-md-offset-2">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox runat="server" ID="tbxUsuario" type="text" class="form-control col-lg-8" name="username" value="" placeholder="Usuario" />
                    </div>

                    <div style="margin-bottom: 25px" class="input-group col-md-8 col-md-offset-2">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <asp:TextBox runat="server" ID="tbxContrasena" type="password" class="form-control col-lg-8" name="password" placeholder="Contraseña" />
                    </div>

                    <div class="input-group col-lg-8">
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox runat="server" id="cbxRecordarContrasena" type="checkbox" name="Recordar Contraseña" value="1" Text="Recordar contraseña"/>
                            </label>
                        </div>
                    </div>

                    <div style="margin-top: 10px; float:right;" class="form-group">
                        <div class="col-sm-6 controls">
                            <asp:Button runat="server" id="btnLogin" href="#" class="btn btn-success" Text="Log in" OnClick="btnLogin_OnClick"></asp:Button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
