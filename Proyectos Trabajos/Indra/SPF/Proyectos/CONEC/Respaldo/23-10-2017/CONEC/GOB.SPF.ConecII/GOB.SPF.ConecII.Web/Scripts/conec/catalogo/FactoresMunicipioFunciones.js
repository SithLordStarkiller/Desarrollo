//$(document).ready(function(){

    $("#Clasificaciones").select2().change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Factores").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/ClasificacionObtieneFactor",
                datatype: "json",
                data: {
                    IdClasificacionFactor: id
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "<option value='0'>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Nombre + '</option>';

                    });

                    $("#Factores").append(optionhtml);
                }
            });
        }
        else {
            $("#Factores").empty();
            var optionhtml = "<option value='0'>Seleccione</option>";
            $("#Factores").append(optionhtml);
        }

    });

    $("#Estados").select2().change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Municipios").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/IdEstadosObtenerMunicipios",
                datatype: "json",
                data: {
                    id: id
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "<option value='0'>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Value + '">' + data[i].Text + '</option>';

                    });

                    $("#Municipios").append(optionhtml);
                }
            });
        }
        else
        {
            $("#Municipios").empty();
            var optionhtml = "<option value='0'>Seleccione</option>";
            $("#Municipios").append(optionhtml);
        }

    });

    //$(".pAdd").click(function () {
    //    return !$('#Municipios option:selected').remove().appendTo('#MunicipiosDestino');
    //});

    //$(".pAddAll").click(function () {
    //    $("#Municipios option").prop('selected', true);
    //    return !$('#Municipios option:selected').remove().appendTo('#MunicipiosDestino');
    //});

    //$(".pRemove").click(function () {
    //    return !$('#MunicipiosDestino option:selected').remove().appendTo('#Municipios');
    //});

    //$(".pRemoveAll").click(function () {
    //    $("#MunicipiosDestino option").prop('selected', true);
    //    return !$('#MunicipiosDestino option:selected').remove().appendTo('#Municipios');
    //});

    $("#IdEstado").select2().change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {
            debugger;
            $("#Municipios").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/FactoresMunicipioObtenerMunicipios",
                datatype: "json",
                data: {
                    IdClasificacionFactor: $("#IdClasificacionFactor").val(),
                    IdEstado: $("#IdEstado").val(),
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "";

                    $.each(data, function (i) {
                        debugger;
                        optionhtml += '<option value="' +
                    data[i].Value + '">' + data[i].Text + '</option>';

                    });

                    $("#Municipios").append(optionhtml);
                }
            });
        }
    });

    $("#IdClasificacionFactor").select2().change(function () {
        var id = $(this).val();
        alert("entra");
        if (id != "" || id != 0) {

            $("#IdFactor").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/ClasificacionObtieneFactor",
                datatype: "json",
                data: {
                    IdClasificacionFactor: id
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "<option value='0'>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Nombre + '</option>';

                    });

                    $("#IdFactor").append(optionhtml);
                }
            });
        }
    });

//});