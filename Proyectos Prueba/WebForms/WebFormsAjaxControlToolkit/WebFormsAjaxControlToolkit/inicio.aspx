<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="WebFormsAjaxControlToolkit.inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .HellowWorldPopup {
            min-width: 200px;
            min-height: 150px;
            background: white;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            ShowPopup();
            setTimeout(HidePopup, 2000);
        }

        function ShowPopup() {
            $find('modalpopup').show();
            //$get('Button1').click();
        }

        function HidePopup() {
            $find('modalpopup').hide();
            //$get('btnCancel').click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <asp:Button ID="Button1" runat="server" Text="Button" />

    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        CancelControlID="btnCancelr" OkControlID="btnOkayr"
        TargetControlID="Button1" PopupControlID="Panel1"
        PopupDragHandleControlID="PopupHeaderr" Drag="true"
        BackgroundCssClass="ModalPopupBG">
    </ajaxtoolkit:ModalPopupExtender>

    <asp:Panel ID="Panel1" Style="display: none" runat="server">
        <div class="HellowWorldPopup">
            <div class="PopupHeader" id="PopupHeader">Header</div>
            <div class="PopupBody">
                <p>This is a simple modal dialog</p>
            </div>
            <div class="Controls">
                <input id="btnOkay" type="button" value="Done" />
                <input id="btnCancel" type="button" value="Cancel" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
