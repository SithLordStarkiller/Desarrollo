<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EditarOrdenCompra.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.OrdenDeCompra.EditarOrdenCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Editar Orden de compra</div>
            </div>
            <div id="dvGrid" style="padding: 10px; width: 550px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" Width="550px"
                            AutoGenerateColumns="false" Font-Names="Arial"
                            Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green" AllowPaging="true" ShowFooter="true"
                            <%-- OnPageIndexChanging="OnPaging" OnRowEditing="EditCustomer"
                    OnRowUpdating="UpdateCustomer" OnRowCancelingEdit="CancelEdit"--%>
                            PageSize="10">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="CustomerID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerID" runat="server"
                                            Text='<%# Eval("CustomerID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCustomerID" Width="40px"
                                            MaxLength="5" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactName" runat="server"
                                            Text='<%# Eval("ContactName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtContactName" runat="server"
                                            Text='<%# Eval("ContactName")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtContactNameF" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompany" runat="server"
                                            Text='<%# Eval("CompanyName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCompany" runat="server"
                                            Text='<%# Eval("CompanyName")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCompanyF" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRemove" runat="server"
                                    CommandArgument='<%# Eval("CustomerID")%>'
                                    OnClientClick="return confirm('Do you want to delete?')"
                                    Text="Delete" OnClick="DeleteCustomer"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                    OnClick="AddNewCustomer" />
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                                <asp:CommandField ShowEditButton="True" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#C2D69B" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
