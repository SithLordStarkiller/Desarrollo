/// <reference path="../Service/ServiceDashboard.svc" />
function PintaIndicadores() {
    $("#valDASH_CREDASIGPOOL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#porcDASH_CREDASIGPOOL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#valDASH_CREDASIGMOVIL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#porcDASH_CREDASIGMOVIL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#TablaIndicadores1").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#TablaIndicadores2").html("<img src='/imagenes/ajax-loader-trans.gif' />");

    var random = Math.random();
    window.UltimaSolicitud = random;

    setTimeout(function() {
        EsperaAjax(random);
    }, 1000);
}

function EsperaAjax(solicitud) {
    if (solicitud == window.UltimaSolicitud) {
        window.PintarTablaIndicadores1(1,solicitud);
        window.PintarTablaIndicadores2(2, solicitud);
        window.PintarCreditosAsignados(solicitud);
    }
}


function PintarFechaCorte() {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (day < 10 ? '0' : '') + day + '-' +
        (month < 10 ? '0' : '') + month + '-' +
        d.getFullYear();

    $("#LblFechaCorte").html(output);
}

function obtenerCombos(idSelect, titleSelect, metodoChange, accion, idCampo, pintarIndicadores, listaCombo) {

    var selectFinal = '<label style="display: none;"><img src="/imagenes/ajax-loader-trans.gif" /></label><select id="' + idSelect + '" style="height:30px;width:95%;" class="Combos" title="' + titleSelect + '" onchange="' + metodoChange + '" > <option value="%" selected="selected">Todas</option>';
    $.each(listaCombo, function(index, OpcionesFiltroDashboard) {
        selectFinal += '<option value="' + OpcionesFiltroDashboard.Value + '">' + OpcionesFiltroDashboard.Description + '</option>';
    });

    selectFinal += '</select>';

    $("#" + idCampo).html(selectFinal);
    if (pintarIndicadores) {
        PintaIndicadores();
    }

}

function decideCombosVisibles(tipoDashboard,usuario) {

    switch (tipoDashboard) {
        case "Administrador":
            $("#DDLDelegacion").show(); $("#LblDelegacion").show();
            //DDLEstado.Visible = true; LblEstado.Visible = true; 

            $("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLDespacho").show(); $("#LblDespacho").show();
            $("#DDLSupervisor").show(); $("#LblSupervisor").show();
            $("#DDLGestor").show(); $("#LblGestor").show();
            servicioCombos("DDLDelegacion", "Delegacion", "PintaIndicadores();", "FILL_DELEGACION", "selDDLDelegacion", false, "ObtenerListaFiltros", usuario);
            servicioCombos("DDLDespacho", "Despacho", "CambiaDespacho('" + tipoDashboard + "','" + usuario + "',this.id + this.selectedIndex)", "FILL_DESPACHO", "selDDLDespacho", false, "ObtenerListaFiltros", usuario);
            break;
        case "Despacho":
            $("#DDLDelegacion").hide(); $("#LblDelegacion").hide();
            //$("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLDespacho").hide(); $("#LblDespacho").hide();
            $("#DDLSupervisor").show(); $("#LblSupervisor").show();
            $("#DDLGestor").show(); $("#LblGestor").show();
            servicioCombos("DDLSupervisor", "Supervisor", "CambiaSupervisor('" + usuario + "')", "FILL_SUPERVISOR", "selDDLSupervisor", true, "ObtenerListaFiltros", usuario);
            break;
        case "Supervisor":
            $("#DDLDelegacion").hide(); $("#LblDelegacion").hide();
            //$("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLDespacho").hide(); $("#LblDespacho").hide();
            $("#DDLSupervisor").hide(); $("#LblSupervisor").hide();
            $("#DDLGestor").show(); $("#LblGestor").show();
            servicioCombos("DDLGestor", "Gestor", "CambiaGestor(this.id + this.selectedIndex)", "FILL_GESTOR", "selDDLGestor", true, "ObtenerListaFiltros", usuario);
            break;
        case "Gestor":
            $("#DDLDelegacion").hide(); $("#LblDelegacion").hide();
            //$("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLDespacho").hide(); $("#LblDespacho").hide();
            $("#DDLSupervisor").hide(); $("#LblSupervisor").hide();
            $("#DDLGestor").hide(); $("#LblGestor").hide();
            break;
        case "Delegacion":
            $("#DDLDelegacion").hide(); $("#LblDelegacion").hide();
            //$("#DDLEstado").show(); $("#LblEstado").show();

            $("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLDespacho").show(); $("#LblDespacho").show();
            $("#DDLSupervisor").show(); $("#LblSupervisor").show();
            $("#DDLGestor").show(); $("#LblGestor").show();

            servicioCombos("DDLDespacho", "Despacho", "CambiaDespacho('" + tipoDashboard + "','" + usuario + "',this.id + this.selectedIndex)", "FILL_DESPACHO", "selDDLDespacho", false, "ObtenerListaFiltros", usuario);
            break;
        case "Direccion":
            $("#DDLDelegacion").show(); $("#LblDelegacion").show();
            //$("#DDLEstado").show(); $("#LblEstado").show();

            $("#DDLEstado").hide(); $("#LblEstado").hide();

            $("#DDLDespacho").show(); $("#LblDespacho").show();
            $("#DDLSupervisor").hide(); $("#LblSupervisor").hide();
            $("#DDLGestor").hide(); $("#LblGestor").hide();
            servicioCombos("DDLDelegacion", "Delegacion", "PintaIndicadores();", "FILL_DELEGACION", "selDDLDelegacion", false, "ObtenerListaFiltros", usuario);
            servicioCombos("DDLDespacho", "Despacho", "CambiaDespacho('" + tipoDashboard + "','" + usuario + "',this.id + this.selectedIndex)", "FILL_DESPACHO", "selDDLDespacho", false, "ObtenerListaFiltros", usuario);
            break;

        default:
            break;
    }
}

function CambiaDespacho(tipoDashboard, usuario, comboSelecionado) {
    var random = Math.random();
    window.UltimaSolicitud = random;
    document.querySelector("#selDDLSupervisor label").style.display = "block";
    document.querySelector("#selDDLSupervisor select").style.display = "none";
    document.querySelector("#selDDLGestor label").style.display = "block";
    document.querySelector("#selDDLGestor select").style.display = "none";
    $("#valDASH_CREDASIGPOOL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#porcDASH_CREDASIGPOOL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#valDASH_CREDASIGMOVIL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#porcDASH_CREDASIGMOVIL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#TablaIndicadores1").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#TablaIndicadores2").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    setTimeout(function() {
        if (random==window.UltimaSolicitud) {
            if (tipoDashboard == "Administrador" || tipoDashboard == "Despacho" || tipoDashboard == "Delegacion") {
                $("#selDDLGestor").html('<label style="display: none;"><img src="/imagenes/ajax-loader-trans.gif" /></label><select id="DDLGestor" style="height:30px;width:95%;" class="Combos" title="Gestor" onchange="CambiaGestor(this.id + this.selectedIndex)" > <option value="%" selected="selected">Todas</option></select>');
                servicioCombos("DDLSupervisor", "Supervisor", "CambiaSupervisor('" + usuario + "')", "FILL_SUPERVISOR", "selDDLSupervisor", true, "ObtenerListaFiltros", usuario);

            } else {
                PintaIndicadores();
            }
        }
    }, 1000);
}

function CambiaSupervisor(usuario) {
    var random = Math.random();
    window.UltimaSolicitud = random;
    document.querySelector("#selDDLGestor label").style.display = "block";
    document.querySelector("#selDDLGestor select").style.display = "none";
    $("#valDASH_CREDASIGPOOL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#porcDASH_CREDASIGPOOL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#valDASH_CREDASIGMOVIL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#porcDASH_CREDASIGMOVIL").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#TablaIndicadores1").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    $("#TablaIndicadores2").html("<img src='/imagenes/ajax-loader-trans.gif' />");
    setTimeout(function() {
        if (random == window.UltimaSolicitud) {
            servicioCombos("DDLGestor", "Gestor", "CambiaGestor(this.id + this.selectedIndex)", "FILL_GESTOR", "selDDLGestor", true, "ObtenerListaFiltros", usuario);
        }
    }, 1000);

}

function CambiaGestor(comboSeleccionado) {
    PintaIndicadores();
}

Array.prototype.equals = function (array) {
    // if the other array is a falsy value, return
    if (!array)
        return false;

    // compare lengths - can save a lot of time 
    if (this.length != array.length)
        return false;

    for (var i = 0, l = this.length; i < l; i++) {
        // Check if we have nested arrays
        if (this[i] instanceof Array && array[i] instanceof Array) {
            // recurse into the nested arrays
            if (!this[i].equals(array[i]))
                return false;
        }
        else if (this[i] != array[i]) {
            // Warning - two different object instances will never be equal: {x:20} != {x:20}
            return false;
        }
    }
    return true;
}

///////////////////////////////////////////// Para bloque /////////////////////////////////////////////
function irAServicio(datosFiltros, tipoDashBoard, sUser, nUser, nRol, nDominio, metodo) {
    var filtros = ObtenerDatosFiltros();
    if (!datosFiltros.equals(filtros)) {
        return false;
    } else {
        servicioIndicadores(datosFiltros, tipoDashBoard, sUser, nUser, nRol, nDominio, metodo, "0");
        servicioIndicadores(datosFiltros, tipoDashBoard, sUser, nUser, nRol, nDominio, metodo, "1");
        servicioIndicadores(datosFiltros, tipoDashBoard, sUser, nUser, nRol, nDominio, metodo, "2");
    }
    return true;
}

function servicioIndicadores(datosFiltros, tipoDashBoard, sUser, nUser, nRol, nDominio, metodo, bloque) {
    $.support.cors = true;
    $.ajax({
        url: "/Dashboard/" + metodo,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        cache: false,
        traditional: true,
        data: JSON.stringify({"valores": datosFiltros, "tipoDashboard": tipoDashBoard, "sUser": sUser, 'nUser': nUser, 'nRol': nRol, 'nDominio': nDominio, 'parteTabla': bloque }),
        success: function (response) {
            
            procesaTabla(response);

        },
        error: function (msg) {
            if (msg.responseText.toString() == "" || msg.responseText.toString() == null) {

                alertaError("No se pudo obtener el reporte.\nIntentelo mas tarde.");
                return false;
            }

            if (msg.getAllResponseHeaders()) {
                //if (msg.responseText.toString().indexOf("</title>") > 0) {
                //  window.location = "/Login.aspx";
                //}
                alertaError("No se pudo obtener el reporte.\nIntentelo mas tarde.");
                return false;
            }
            return false;
        }
    });
}

function procesaTabla(datos1) {
    
        $.each(datos1, function(indice, indicador) {
            var valor = document.getElementById("val" + indicador.FcClave);
                valor.innerHTML = indicador.FiValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");;
                var valor1 = document.getElementById("porc" + indicador.FcClave);
                valor1.innerHTML = indicador.FiPorcentaje + "%";
                if (indicador.FiParte != 0) {
                    var valor2 = document.getElementById("desc" + indicador.FcClave);
                    valor2.innerHTML = indicador.FcDescripcion;
                }
            }
        );
    
}

function servicioCombos(idSelect, titleSelect, metodoChange, accion, idCampo, pintarIndicadores, metodo, usuario1) {
    //document.querySelector("#" + idCampo + " label").style.display = "block";
    //document.querySelector("#" + idCampo + " select").style.display = "none";

    $.support.cors = true;
    $.ajax({
        url: "/Dashboard/" + metodo,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        cache: false,
        traditional: true,
        data: JSON.stringify({"valores": ObtenerDatosFiltros(), "accion": accion,"usuario":usuario1}),
        success: function (response) {
            
            
            
            obtenerCombos(idSelect, titleSelect, metodoChange, accion, idCampo, pintarIndicadores, response);

        },
        error: function (msg) {
            if (msg.responseText.toString() == "" || msg.responseText.toString() == null) {
                
                alertaError("No se pudo obtener el reporte.\nIntentelo mas tarde.");
                return false;
            }
            
            if (msg.getAllResponseHeaders()) {             
                //if (msg.responseText.toString().indexOf("</title>") > 0) {
                //     window.location = "/Login.aspx";
                //}
                     alertaError("No se pudo obtener el reporte.\nIntentelo mas tarde.");
                return false;
            }
            return false;
        }
    });
}

//function obtenerFormularios(listaFormularios) {

//    var selectFinal = '<select id="DDLTipoFormulario" style="height:30px;width:95%;" class="Combos" title="naaaaaa" onchange="" >';
//    $.each(listaFormularios, function (index, formulario) {
//        selectFinal += '<option value="' + formulario.Ruta + '">' + formulario.Descripcion + '</option>';
//    });

//    selectFinal += '</select>';

//    $("#DDLTipoFormulario").html(selectFinal);

//}

var mensajeMostrado = false;

function alertaError(texto) {
    if (mensajeMostrado == false) {
        mensajeMostrado = true;
        alert(texto);
        mensajeMostrado = false;
    }
    
}