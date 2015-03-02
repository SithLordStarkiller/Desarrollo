<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="~/Content/Sitio.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1>Hello, world!</h1>
        <p>This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
        <p><a class="btn btn-primary btn-lg" href="#" role="button">Learn more</a></p>

        <div class="list-group">
            <a href="#" class="list-group-item">
                <h4 class="list-group-item-heading">List group item heading</h4>
                <p class="list-group-item-text">While not always necessary, sometimes you need to put your DOM in a box. For those situations, try the panel component.</p>
            </a>
        </div>
        
        <div class="list-group">
            <a href="#" class="list-group-item">
                <h4 class="list-group-item-heading">List group item heading</h4>
                <p class="list-group-item-text">While not always necessary, sometimes you need to put your DOM in a box. For those situations, try the panel component.</p>
            </a>
        </div>
        
        <div class="list-group">
            <a href="#" class="list-group-item">
                <h4 class="list-group-item-heading">List group item heading</h4>
                <p class="list-group-item-text">While not always necessary, sometimes you need to put your DOM in a box. For those situations, try the panel component.</p>
            </a>
        </div>
    </div>
</asp:Content>
