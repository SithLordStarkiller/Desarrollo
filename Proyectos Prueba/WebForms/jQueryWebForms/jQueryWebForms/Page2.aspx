<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="jQueryWebForms.Page2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
        "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Page-2</title>

    <%--<script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>--%>

    <script src="Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.11.3.js" type="text/javascript"></script>
    <script src="Scripts/datepicker-es.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Content/themes/base/datepicker.css" type="text/css"/>
    <link rel="stylesheet" href="Content/Sitio.css" type="text/css"/>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['es']);
            $("#txt").datepicker();
        });
    </script>

    <script type="text/javascript">
        //
        function IsValidNumber() {
            if ($(this).val() == "") {
                //$(this).val("0");
                alert("Please enter valid value!");
                $(this).focus();
            }
            else if ($.isNumeric($(this).val()) == false) {
                alert("Please enter valid value!");
                $(this).focus();
            }


        }

        function Add() {
            var Num1 = parseInt($('#txtNum1').val());
            var Num2 = parseInt($('#txtNum2').val());
            var Result = Num1 + Num2;
            $('#txtResult').val(Result);
        }

        function InIEvent() {
            $('#txtNum1').change(IsValidNumber);
            $('#txtNum2').change(IsValidNumber);
            $('#btnClientAdd').click(Add);
        }

        $(document).ready(function () {

            $('#txtNum1').keyup(function () {
                $('#txtNum2').val($('#txtNum1').val());
            });

            $(document).on('click', '#botton1', function () {
                //alert("Click");

                $.ajax({
                    type: "POST",
                    url: "Page2.aspx/MetodoServidor",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#txtResult').val(data.d);
                        alert(data.d);
                    },
                    error: function (e) {
                        alert("Ajax Error " + e.getError);
                    }
                });
            });
        });

        $(document).ready(InIEvent);
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM" runat="server">
        </asp:ScriptManager>

        <%-- <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>--%>

        <asp:UpdatePanel ID="upMain"
            runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <input type="button"
                                    id="btnClientAdd" value=" + " />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNum1" runat="server"
                                    Width="100px"></asp:TextBox>
                                +
                            <asp:TextBox ID="txtNum2" runat="server"
                                Width="100px"></asp:TextBox>
                                =
                            <asp:TextBox ID="txtResult"
                                runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnServerAdd" runat="server"
                                    Text=" +(*) " OnClick="btnServerAdd_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button runat="server" ID="botton1" Text="btn" />
                    <asp:TextBox runat="server" CssClass="form-control" ID="txt"></asp:TextBox>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
