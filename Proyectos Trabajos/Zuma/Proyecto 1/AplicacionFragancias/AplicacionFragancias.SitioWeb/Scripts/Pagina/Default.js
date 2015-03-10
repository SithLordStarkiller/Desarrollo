$(document).ready(function () { 
    alert('jquery working!');
    $("#Boton").click(function () {
        alert('Se invoco evento click');
    });

    $("[id$=Boton]").click(function () {
        alert('Se invoco evento click');
    });
});