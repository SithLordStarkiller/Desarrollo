<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuArbol.aspx.cs" Inherits="Universidad.WebAdministrativaPrueba.MenuArbol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphArbol" runat="server">
    <div class="row">
        <asp:TreeView runat="server" ID="tvMenuArbol" BackColor="blue" BorderColor="red" BorderWidth="1" EnableTheming="True"
            ExpandImageToolTip="favicon.ico" ImageSet="BulletedList">
            <Nodes>
                <asp:TreeNode Text="Node">
                    <asp:TreeNode Text="ChildNode" />
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
        <asp:TextBox runat="server"></asp:TextBox>
    </div>
</asp:Content>
