$(document).ready(function () { 
    alert('jquery working!');
    $("#Buton").click(function () {
        alert('Se invoco evento click');
    });

    $("[id$=Boton]").click(function () {
        alert('Se invoco evento click');
    });
});