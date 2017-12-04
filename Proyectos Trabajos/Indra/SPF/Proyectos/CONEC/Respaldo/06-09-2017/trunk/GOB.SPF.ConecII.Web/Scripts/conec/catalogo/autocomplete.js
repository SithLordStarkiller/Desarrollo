$(document).ready(function () {

    $(function () {
        $("#Prueba").autocomplete({
            source: "Catalogo/Obtener",
            minLength: 2,
        scroll: true,
        select: function (event, ui) {
            console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
            $("#IdRemitente").val(ui.item.id).attr("selected", "selected").change();
        }
    });
});

});