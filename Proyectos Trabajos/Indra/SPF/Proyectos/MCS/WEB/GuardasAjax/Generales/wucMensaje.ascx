<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucMensaje.ascx.cs" Inherits="Generales_wucMensaje" %>
<asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="divError" id="divMensaje" runat="server">
            <p>
                &nbsp;<asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </p>
        </div>
        <asp:Timer ID="timMensaje" runat="server" Interval="3000" Enabled="False" OnTick="timMensaje_Tick">
        </asp:Timer>
    </ContentTemplate>
</asp:UpdatePanel>
