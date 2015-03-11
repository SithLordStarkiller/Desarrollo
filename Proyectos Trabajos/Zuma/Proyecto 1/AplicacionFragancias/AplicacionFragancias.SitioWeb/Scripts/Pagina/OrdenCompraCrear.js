$(document).ready(function () {

    $("#<%=txtFehaEntrega.ClientID%>").datepicker();
    $("#<%=txtFechaPedido.ClientID%>").datepicker();
});

$(function () {
    var $gv = $("table[id$=grvStudentDetails]");
    var $rows = $("> tbody > tr:not(:has(th, table))", $gv);
    var $inputs = $(".datepickers", $rows);

    $rows.css("background-color", "white");

    $inputs.datepicker();
});

$(document).ready(function () {
    $("#<%=grvStudentDetails.ClientID%> [id*='txtCantidad']").keyup(function () {
        var cantidad = $(this).val();
        var precio = $(this).parent().parent().find("[id*='txtPrecioUnitario']").val();
        $(this).parent().parent().find("[id*='txtSubTotal']").val(cantidad * precio);
    });

    $("#<%=grvStudentDetails.ClientID%> [id*='txtPrecioUnitario']").keyup(function () {
        var precio = $(this).val();
        var cantidad = $(this).parent().parent().find("[id*='txtCantidad']").val();
        $(this).parent().parent().find("[id*='txtSubTotal']").val(cantidad * precio);
    });
});
