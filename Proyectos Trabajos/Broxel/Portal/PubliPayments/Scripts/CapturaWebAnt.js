var obteniendoGPS = false;
var hiddenGPS = null;
var imagenGPS = null;
var arrValores = {};
var arrValoresGuardar = {};
var urlAjaxSolicitud = "/CapturaWeb/InvokeWS01800Pagos";

function ObtenerGps(idHidden, imagen) {
    var options = {
        enableHighAccuracy: true,
        timeout: 15000,
        maximumAge: 0
    };

    if (obteniendoGPS == false) {
        obteniendoGPS = true;
        hiddenGPS = document.getElementById(idHidden);
        imagenGPS = imagen;
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition, showError, options);
        } else {
            obteniendoGPS = false;
            alert("Su navegador no soporta localización por GPS");
        }
    } else {
        alert("Se esta intentando obtener una geoposición por favor espere antes de intentar obtener otra...");
    }
}

function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            hiddenGPS.value = "Error: El usuario no permitió la toma de la geolocalización.";
            break;
        case error.POSITION_UNAVAILABLE:
            hiddenGPS.value = "Error: No se pudo obtener la posición geográfica.";
            break;
        case error.TIMEOUT:
            hiddenGPS.value = "Error: Se venció el tiempo de espera para obtener la posición geográfica.";
            break;
        case error.UNKNOWN_ERROR:
            hiddenGPS.value = "Error: Ocurrió un error desconocido al intentar obtener la posición geográfica.";
            break;
    }
    imagenGPS.src = "/imagenes/gps_unsel.png";
    alert(hiddenGPS.value);
    obteniendoGPS = false;
}

function showPosition(position) {
    var fhGps = new Date(position.timestamp);
    hiddenGPS.value = "Lat:" + position.coords.latitude +
        "Lon:" + position.coords.longitude +
        "Sat:0" +
        "Fecha:" + fhGps.getFullYear() + padLeft(fhGps.getMonth() + 1, 2, '0') + padLeft(fhGps.getDate(), 2, '0') +
        " " + padLeft(fhGps.getHours(), 2, '0') + ":" + padLeft(fhGps.getMinutes(), 2, '0') + ":" + padLeft(fhGps.getSeconds(), 2, '0') +
        ",TZ:" + ((fhGps.getTimezoneOffset() / 60) * -1);
    imagenGPS.src = "/imagenes/gps_sel.png";
    obteniendoGPS = false;
}

function padLeft(numero, cant, str) {
    return Array(cant - String(numero).length + 1).join(str || '0') + numero;
}

function SigPestania(element) {
    var sigSubForm = $(element).attr("id").replace("SubForm", "");
    var totSubforms = $("[id^=pcFormularios_T].dxtc-link").length;
    do {
        sigSubForm++;
        if (totSubforms == sigSubForm) {
            sigSubForm = 0;
            break;
        }
    } while ($("#pcFormularios_T" + sigSubForm).attr("class").indexOf("dxtcLiteDisabled") > -1)

    $("#pcFormularios_T" + sigSubForm).click();
}

function GuardarParcialmente() {
    $("#btGuardar").html('Guardando...');
    $("#btGuardar").attr('disabled', 'disabled');

    var contDictamen = 0;
    var anterior = null;
    var guardarOk = true;
    var arrGestionData = [];
    arrValoresGuardar = {};

    $(".CWDefault.CWValidacion").not($("input[name^='ValSMS_']").parent()).each(function () {
        $(this).removeClass("CWValidacion");
    });


    $("li[id^='pcFormularios_T'],li[id*=' pcFormularios_T']").each(function () {
        if ($(this).css("backgroundColor") == "rgb(238, 136, 136)") {
            $(this).css("backgroundColor", "");
        }
    });

    $("li[id^='pcFormularios_AT'],li[id*=' pcFormularios_AT']").each(function () {
        if ($(this).css("backgroundColor") == "rgb(238, 170, 170)") {
            $(this).css("backgroundColor", "");
        }
    });


    arrValoresGuardar["ExternalType"] = ExternalType;
    arrValoresGuardar["idOrden"] = window.ordenActual.toString();
    arrValoresGuardar["InitialDate"] = window.InitialDate.toString().replace("-", "").replace("-", "");
    arrValoresGuardar["FinalDate"] = FormatearFecha(new Date()).replace("-", "").replace("-", "");
    arrValoresGuardar["ResponseDate"] = FormatearFecha(new Date()).replace("-", "").replace("-", "");
    arrValoresGuardar["AssignedTo"] = AssignedTo;
    // ReSharper disable once Html.IdNotResolved
    var pos = document.getElementById("PositionGps");
    if (pos != null && pos.value.substring(0, 4) == "Lat:") {
        if (!(arrValoresGuardar["CodSMS"] != "" || arrValoresGuardar["CelularSMS_Actualizado"] != "")) {
            arrValoresGuardar["gps_automatico"] = pos.value;
        }
        arrValoresGuardar["gpsFin"] = pos.value;
        arrValoresGuardar["InitialLatitude"] = pos.value.substring(4, pos.value.indexOf("Lon"));
        arrValoresGuardar["FinalLatitude"] = pos.value.substring(4, pos.value.indexOf("Lon"));
        arrValoresGuardar["InitialLongitude"] = pos.value.substring(pos.value.indexOf("Lon:") + 4, pos.value.indexOf("Sat:", pos.value.indexOf("Lon:")));
        arrValoresGuardar["FinalLongitude"] = pos.value.substring(pos.value.indexOf("Lon:") + 4, pos.value.indexOf("Sat:", pos.value.indexOf("Lon:")));
        arrValoresGuardar["FormiikResponseSource"] = "CapturaWeb";

        $('.CWDefault input').each(function (i, obj) {
            if (obj.className.indexOf("CWDisabled") < 0 || obj.name.toUpperCase().substring(0, 8) == "DICTAMEN") {
                if (obj.parentElement.className.indexOf("CWHidden") < 0 && obj.parentElement.parentElement.className.indexOf("CWHidden") < 0) {
                    if ($(obj).attr("tabName") != undefined && window.pcFormularios.GetTabByName($(obj).attr("tabName")).clientEnabled) {
                        var requerido = (obj.parentElement.className.indexOf("CWRec") >= 0) || (obj.parentElement.parentElement.className.indexOf("CWRec") >= 0);
                        switch (obj.type) {
                            case "radio":
                                if (anterior != obj.name) {
                                    var valRadioSel = $("input[name='" + obj.name + "']:checked").val();
                                    if (valRadioSel != undefined) {
                                        arrValoresGuardar[obj.name] = valRadioSel;
                                    } else {
                                        if (requerido) {
                                            guardarOk = false;
                                            EsRequerido(obj);
                                        }
                                    }
                                }
                                anterior = obj.name;
                                break;
                            case "checkbox":
                                if (anterior != obj.name) {
                                    var valCheckSel = $("input[class='" + obj.className + "']:checked")[0];
                                    if (valCheckSel != undefined) {
                                        arrValoresGuardar[obj.name] = obj.checked.toString();
                                    } else {
                                        if (requerido) {
                                            guardarOk = false;
                                            EsRequerido(obj);
                                        }
                                    }
                                }
                                anterior = obj.name;
                                break;
                            case "file":
                                if (obj.defaultValue != obj.value && obj.value != "") {
                                    if ($('#' + obj.id + 'Url').val() != undefined && $('#' + obj.id + 'Url').val() != ""
                                        && $('#' + obj.id + 'Url').val().substring(0, 1) != "{") {
                                        arrValoresGuardar[obj.id] = $('#' + obj.id + 'Url').val();
                                    } else {
                                        EsRequerido(obj);
                                        guardarOk = false;
                                    }
                                } else {
                                    if (requerido) {
                                        guardarOk = false;
                                        EsRequerido(obj);
                                    }
                                }
                                break;
                            default:
                                if (obj.className.indexOf("hasDatepicker") >= 0) {
                                    //Si es un campo de fecha se evalua el valor del padre
                                    if (obj.parentElement.parentElement.className.indexOf("CWDisabled") < 0)
                                        if (obj.parentElement.parentElement.className.indexOf("CWHidden") < 0) {
                                            //Guardar fecha
                                            arrValoresGuardar[obj.id] = obj.value.substring(6)+"/"+obj.value.substring(3,5)+"/"+obj.value.substring(0,2);
                                            //console.log(i + ": " + obj.id + " - class: " + obj.className + " - parent: "
                                            //  + obj.parentElement.parentElement.className + " - Valor: " + obj.value);
                                        }
                                } else {
                                    if (obj.value != "" && obj.value != undefined) {
                                        if (obj.defaultValue != obj.value || obj.name.toUpperCase().substring(0, 8) == "DICTAMEN") {
                                            if (window.ValidaFormato(obj)) {
                                                arrValoresGuardar[obj.name] = obj.value;
                                                if (obj.name.toUpperCase().substring(0, 8) == "DICTAMEN") {
                                                    ComplementoXDictamen(obj.id);
                                                    contDictamen++;
                                                }
                                            } else {
                                                EsRequerido(obj);
                                                guardarOk = false;
                                            }
                                            //console.log("Campo: " + obj.name + " - Valor: " + obj.value);
                                        }
                                    } else {
                                        if (requerido) {
                                            guardarOk = false;
                                            EsRequerido(obj);
                                        }
                                    }
                                }
                                break;
                        }
                    } //TabEnabled
                } else {
                    //console.log("CWHidden");
                }
            } else {
                //console.log("CWDisabled");
            }
        });
        //Obtengo los select
        $('.CWDefault select').each(function (i, obj) {
            if ($(obj).attr("tabName") != undefined && window.pcFormularios.GetTabByName($(obj).attr("tabName")).clientEnabled) {
                var requerido = (obj.parentElement.className.indexOf("CWRec") >= 0) || (obj.parentElement.parentElement.className.indexOf("CWRec") >= 0);

                if (obj.className.indexOf("CWDisabled") < 0) {
                    if (obj.parentElement.className.indexOf("CWHidden") < 0) {
                        var valor = $("#" + obj.name).val();
                        if (valor != undefined && valor != "") {
                            arrValoresGuardar[obj.name] = $("#" + obj.name).val();
                            console.log("Campo: " + obj.name + " - Valor: " + $("#" + obj.name).val());
                        } else {
                            if (requerido) {
                                guardarOk = false;
                                EsRequerido(obj);
                            }
                        }
                    }
                }
            }
        });

        //Obtengo los gps extras
        if (!(arrValoresGuardar["CodSMS"] != undefined && arrValoresGuardar["CodSMS"] != "" || arrValoresGuardar["CelularSMS_Actualizado"] != undefined && arrValoresGuardar["CelularSMS_Actualizado"] != "")) {
            $("input:hidden[gps=True]").each(function (i, obj) {
                var requerido = (obj.parentElement.className.indexOf("CWRec") >= 0) || (obj.parentElement.parentElement.className.indexOf("CWRec") >= 0);
                if (obj.value != undefined && obj.value != "") {
                    arrValoresGuardar[obj.id] = obj.value;
                } else {
                    if (requerido) {
                        guardarOk = false;
                        EsRequerido(obj);
                    }
                }
            });
        } else {
            contDictamen++;
        }
        

        for (var k in arrValoresGuardar) {
            // use hasOwnProperty to filter out keys from the Object.prototype
            if (arrValoresGuardar.hasOwnProperty(k)) {
                console.log('key: ' + k + ', value: ' + arrValoresGuardar[k]);
                arrGestionData.push(k + "|" + arrValoresGuardar[k]);
            }
        }
    } else {
        guardarOk = false;
        alert("No se pudo obtener la posición GPS, por favor revise la configuración de su navegador e intente nuevamente.");
    }
    if (contDictamen!=1) {
           guardarOk = false;
           alert("No se puede guardar la gestión, Por favor reporte este problema a soporte indicando las opciones seleccionadas.");
    }
    if ($(".CWValidacion").length >= 1) {
        guardarOk = false;
        alert("No se puede guardar la gestión, se tienen campos en alerta. ");
    }
    if (guardarOk && arrGestionData.length > 12) {
        $.ajaxSettings.traditional = true;

        $.post("/CapturaWeb/GuardarGestion", {
            resp: arrGestionData
        }, function (data) {
            $("#btGuardar").html('Guardar');
            $("#btGuardar").removeAttr('disabled');

            if (data == "OK") {
                location.reload(true);
            } else {
                alert("Ocurrió un error al intentar guardar la gestión, por favor intentelo nuevamente. Si el problema continua reportelo a soporte.");
            }
        }).error(function (error) {
            $("#btGuardar").html('Guardar');
            $("#btGuardar").removeAttr('disabled');
            alert(error);
        });
    } else {
        $("#btGuardar").html('Guardar');
        $("#btGuardar").removeAttr('disabled');
    }
}

function EsRequerido(obj) {
    console.log("Requerido: " + obj.name);
    $("#pcFormularios_T" + window.pcFormularios.GetTabByName($(obj).attr("tabName")).index).css("backgroundColor", "rgb(238, 136, 136)");
    $("#pcFormularios_AT" + window.pcFormularios.GetTabByName($(obj).attr("tabName")).index).css("backgroundColor", "rgb(238, 170, 170)");
    //window.pcFormularios.GetTabByName($(obj).attr("tabName")).addClass("CWNoOK");
    $(".CWDefault." + obj.name).each(function () {
        $(this).addClass("CWValidacion");
    });
}

function FormatearFecha(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    seconds = seconds < 10 ? '0' + seconds : seconds;
    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;

    return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
}

function GuardarValor(inputActual) {

    if (!window.ValidaFormato(inputActual))
        return false;

    var obj = inputActual;
    if (obj.className.indexOf("CWDisabled") < 0) {
        if (obj.parentElement.className.indexOf("CWHidden") < 0) {
            switch (obj.type) {
                case "radio":
                    var valRadioSel = $("input[name='" + obj.name + "']:checked").val();
                    if (valRadioSel != undefined) {
                        arrValores[obj.name.toUpperCase()] = valRadioSel;
                        //console.log("Campo: " + obj.name + " - Valor: " + valRadioSel);
                    }
                    break;
                case "checkbox":
                    var valCheckSel = $("input[name='" + obj.name + "']:checked")[0];
                    if (valCheckSel != undefined) {
                        arrValores[obj.name.toUpperCase()] = obj.checked.toString();
                        //console.log("Campo: " + obj.name + " - Valor: " + obj.checked.toString());
                    } else {
                        arrValores[obj.name.toUpperCase()] = undefined;
                    }
                    break;
                case "file":
                    //console.log("file");
                    break;
                case "select-one":
                    if (obj.className.indexOf("CWDisabled") < 0) {
                        if (obj.parentElement.className.indexOf("CWHidden") < 0) {
                            var valor = $("#" + obj.name).val();
                            if (valor != undefined && valor != "") {
                                arrValores[obj.name.toUpperCase()] = $("#" + obj.name).val();
                                //console.log("Campo: " + obj.name + " - Valor: " + $("#" + obj.name).val());
                            } else {
                                arrValores[obj.name.toUpperCase()] = undefined;
                            }
                        }
                    }
                    break;
                default:
                    if (obj.className.indexOf("hasDatepicker") >= 0) {
                        //Si es un campo de fecha se evalua el valor del padre
                        //if (obj.parentElement.parentElement.className.indexOf("CWDisabled") < 0)
                           // if (obj.parentElement.parentElement.className.indexOf("CWHidden") < 0)
                                //console.log(i + ": " + obj.id + " - class: " + obj.className + " - parent: "
                                  //  + obj.parentElement.parentElement.className + " - Valor: " + obj.value);
                    } else {
                        if (obj.value != "" && obj.value != undefined)
                            if (obj.defaultValue != obj.value) {
                                arrValores[obj.name.toUpperCase()] = obj.value;
                                //console.log("Campo: " + obj.name + " - Valor: " + obj.value);
                            }
                    }
                    break;
            }
        } else {
            //console.log("CWHidden");
        }
    } else {
        //console.log("CWDisabled");
    }
    return false;
}

function ValC(campo, valor) {
    var valBuscado = arrValores[campo.toUpperCase()];
    if (valBuscado != undefined) {
        return valBuscado.toUpperCase() == valor.toUpperCase();
    }
    return false;
}

function Mostrar(campo, show) {
    if (show) {
        $("." + campo).show();
        $("." + campo).removeClass("CWHidden");
    }
    else {
        $("." + campo).hide();
        $("." + campo).addClass("CWHidden");
    }
}

function ValidaFormato(elemento) {
    var validar = $(elemento).val();
    var patron = $(elemento).attr("Validacion");
    if (patron == "") {
        return true;
    }
    var regExp = new RegExp(patron);
    if (regExp.test(validar)) {
        $(elemento).parent().removeClass("CWValidacion");
        return true;
    }
    else {
        $(elemento).parent().addClass("CWValidacion");
        return false;
    }

}

function Calc(operacion) {
    operacion = operacion.replace(/\(/g, "").replace(/\)/g, "");
    var sumandos = operacion.split("+");
    var comparadores = operacion.split("=");
    var res = 0;

    if (sumandos.length>1) {
        $.each(sumandos, function (index, contenido) {
            var valT = $("#" + contenido).val();
            valT++;
            valT--;
            res = res + valT;

        });
        return res.toFixed(4);
    }
    
    if (comparadores.length > 1) {
        var crypto = $("#" + comparadores[0]).attr("crypto");
        if (crypto!=undefined && crypto!="") {
            if ($("#" + comparadores[0]).attr("crypto") == $("#" + comparadores[1]).attr("crypto")) {
                return "Correcto";
            }
            else {
                return "Incorrecto";
            }
        }
        else if ($("#" + comparadores[0]).val() == $("#" + comparadores[1]).val()) {
            return "Correcto";
        } else {
            return "Incorrecto";
        }
        
    }

    return 0;
}

function ComplementoXDictamen(dictamen) {
    if (dictamen.toUpperCase().indexOf("STM") > 0) {
        
        var tipoSTM = $("#IM_OPC_SELEC").val().substring(6, 7);
        arrValoresGuardar["IM_OPC_SELEC"] = $("#IM_OPC_SELEC").val();
        arrValoresGuardar["IM_PRIMER_PAGO_SELEC"] = $("#IM_OPC" + tipoSTM + "_STM_PRIMER_PAGO").val();
        arrValoresGuardar["IM_SELEC_STM_PAGO_SUBSEC"] = $("#IM_OPC" + tipoSTM + "_STM_PAGO_SUBSEC").val();
        arrValoresGuardar["IM_OPC1_STM_PRIMER_PAGO"] = $("#IM_OPC1_STM_PRIMER_PAGO").val();
        arrValoresGuardar["IM_OPC1_STM_PAGO_SUBSEC"] = $("#IM_OPC1_STM_PAGO_SUBSEC").val();
        arrValoresGuardar["IM_PRIMA_SEG_DAN"] = $("#IM_PRIMA_SEG_DAN").val();
        arrValoresGuardar["IM_SALDO_SEG_DAN"] = $("#IM_SALDO_SEG_DAN").val();
        arrValoresGuardar["IM_BENSELEC_STM"] = $("#IM_BENSELEC_STM").val();
    }
    else if (dictamen.toUpperCase().indexOf("DCP") > 0) {
        arrValoresGuardar["Res_factorDCP"] = $("#Res_factorDCP").val();
        arrValoresGuardar["factorSinFee"] = $("#factorSinFee").val();
    }
    else if (dictamen.toUpperCase().indexOf("PAGO") > 0) {
        
        switch ($("#montoPromesa").val()) {
            case "MEN":
                arrValoresGuardar["ppagoMensualAct"] = $("#ppagoMensualAct").val();
            break;
            case "TOM":
                arrValoresGuardar["ppagoOmisosOms"] = $("#ppagoOmisosOms").val();
                arrValoresGuardar["ppagoOmisosAct"] = $("#ppagoOmisosAct").val();
                break;
        }
    }
    else if (dictamen.toUpperCase().indexOf("LIQUIDA") > 0) {
        arrValoresGuardar["IM_MONTO_LIQUIDAR_CON_DESCUENTO"] = $("#IM_MONTO_LIQUIDAR_CON_DESCUENTO").val();
    }
    else if (dictamen.toUpperCase().indexOf("FPP") > 0) {
        arrValoresGuardar["AportacionFPP"] = $("#AportacionFPP").val();
        arrValoresGuardar["CopagoFPP"] = $("#CopagoFPP").val();
        arrValoresGuardar["MensualidadFPP"] = $("#MensualidadFPP").val();
    }
    else if (dictamen.toUpperCase().indexOf("BCN") > 0) {
        arrValoresGuardar["BCNMesActual"] = $("#BCNMesActual").val();
        arrValoresGuardar["BCNSigMeses"] = $("#BCNSigMeses").val();
    }
    
    return null;
}

function SendInformationSolicitud(data, functionSuccess, functionerror) {

    $.ajax({
        url: urlAjaxSolicitud,
        type: 'POST',
        data: data,
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: functionSuccess,
        error: function (msg) {
            functionerror;
        }
    });
}

function encriptaClave(codigo) {
    $("#CodSMS").removeAttr("crypto");
    $.post("/CapturaWeb/EncriptaCodigo", {
        codigo: codigo
    }, function (data) {
        $("#CodSMS").attr("crypto", data);
        validaSMS();
    }).error(function (error) {
        $("#CodSMS").attr("crypto", "xxxx");
        validaSMS();
    });
}