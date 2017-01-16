var $body = $("body");
var MaskAjax = ".MaskAjax";
var BtManualAssignment = "#BtAsignarManual";
var BtManualCancel="#BtCancelarManual";
var BtManualReallocation = "#BtReasignarManual";
var BtManualAuthorize = "#BtAutorizarManual";
var LbContainerPageTitle = "#ContainerPage";
var BtUpload = "#Cargar";
var BtFileInput= "#archivo";
var BtProcessData= "#BtnProcesar";
var BtClearData= "#BtnLimpiaM";
var TextAreaCredits= "#TextAreaEdicion";
var CreditsTable = "#TCreditosManual tbody";
var CreditsTableCreditColumn = 'NumCredCM';
var CreditsTableUserColumn = 'UsuarioCM';
var CreditsTableAuxiliarColumn = 'AuxiliarCM';
var BtRefreshTable= ".btnLimpiar";
var EmptyTable = "";
var EmptyTableRows = 18;
var Explorer = 10;
var UrlAjaxAssignment = "";
var UrlAjaxCancel = "";
var UrlAjaxReallocation = "";
var UrlAjaxAuthorize = "";
var ContainerPage = ["",""];//1:AsignaOrdenes,2:OrdenesAsignadas,3:Gestionadas
var resultLines=[];
var $ManualAssignment = {
    HeadBoard: {
        FullLine: "",
        Delimiter1: "",
        Delimiter2: "",
        UserHeader: "usuario",
        AuxHeader: "auxiliar",
        ContainsUser: true,
        ContainsAux: true
    },
    Initialize: function() {
        for (var i = 0; i < EmptyTableRows; i++) {
            EmptyTable += "<tr><td>&nbsp;</td> <td>&nbsp;</td> <td>&nbsp;</td></tr>";
        }
        $(LbContainerPageTitle + "1").hide();
        $(LbContainerPageTitle + "2").hide();
        $(LbContainerPageTitle + "3").hide();
        $(BtManualAssignment).hide();
        $(BtManualCancel).hide();
        $(BtManualReallocation).hide();
        $(BtManualAuthorize).hide();
        if (Explorer==8) 
            $('#BtnProcesar,#BtnLimpiaM').addClass('IE8');
        if (Explorer == 9)
            $('#BtnProcesar,#BtnLimpiaM').addClass('IE9');
        switch (ContainerPage[1]) {
        case "1":
            $(BtManualAssignment).show();
            $(LbContainerPageTitle + "1").show();
            break;
        case "2":
            $(BtManualCancel + "," + BtManualAssignment).show();
            $(BtManualAssignment).html("Cambiar asignación");
            $(LbContainerPageTitle + "2").show();
            break;
        case "3":
            $(BtUpload).css("margin-top", "18px");
            $(BtManualAuthorize + "," + BtManualCancel + "," + BtManualAssignment + "," + BtManualReallocation).css("margin-top", "5px").show();
            $(BtManualAssignment).html("Cambiar asignación");
            $(LbContainerPageTitle + "3").show();
            break;
        }
        $(BtManualAssignment).on("click", function(e) {
            e.preventDefault();
            if ($(CreditsTable).find("tr").length < 1) {
                alert("No hay información a procesar");
                return false;
            }
            $ManualAssignment.SendInformation(UrlAjaxAssignment, ContainerPage);
            return false;
        });
        $(BtManualCancel).on("click", function(e) {
            e.preventDefault();
            if ($(CreditsTable).find("tr").length < 1) {
                alert("No hay información a procesar");
                return false;
            }
            $ManualAssignment.SendInformation(UrlAjaxCancel, ContainerPage);
            return false;
        });
        $(BtManualReallocation).on("click", function(e) {
            e.preventDefault();
            if ($(CreditsTable).find("tr").length < 1) {
                alert("No hay información a procesar");
                return false;
            }
            $ManualAssignment.SendInformation(UrlAjaxReallocation, ContainerPage);
            return false;
        });
        $(BtManualAuthorize).on("click", function(e) {
            e.preventDefault();
            if ($(CreditsTable).find("tr").length < 1) {
                alert("No hay información a procesar");
                return false;
            }
            $ManualAssignment.SendInformation(UrlAjaxAuthorize, ContainerPage);
            return false;
        });
        $(BtClearData).on("click", function() {
            $ManualAssignment.RefreshContaners();
            return false;
        });
        $(BtProcessData).on("click", function () {
            $(MaskAjax).show();

            var processor = setInterval(function() {
                $ManualAssignment.ProcessDataToTable();
                clearInterval(processor);
            }, 1);
            return false;
        });
        $ManualAssignment.UploadFile.Initialize();
    },
    ProcessDataToTable: function() {
        $ManualAssignment.HeadBoard.ContainsUser = true;
        $ManualAssignment.HeadBoard.ContainsAux = true;
        
        var textProcess = $(TextAreaCredits).val();
        var user = "";
        var aux = "";
        var credits = [];
        var rowsHtml = "";
        var delimiter1 = -1;
        var delimiter2 = -1;

        if ($.trim(textProcess) == "") {
            $(MaskAjax).hide();
            alert("No hay información a procesar");
            return false;
        }
        var rows = textProcess.split("\n");
        for (var i = 0; i < rows.length; i++) {
            user = "";
            aux = "";
            if ($.trim(rows[i])== "") {
                continue;
            }
            delimiter1 = (rows[i].indexOf("|") != -1 ? rows[i].indexOf("|") : rows[i].indexOf("\t") != -1 ? rows[i].indexOf("\t") : rows[i].indexOf(",") != -1 ? rows[i].indexOf(",") : rows[i].indexOf(";") != -1 ? rows[i].indexOf(";") : -1);
            if (delimiter1 > 0) {
                delimiter2 = (rows[i].replace("|", " ").indexOf("|") != -1 ? (rows[i].replace("|", " ")).indexOf("|") : (rows[i].replace("\t", " ")).indexOf("\t") != -1 ? (rows[i].replace("\t", " ")).indexOf("\t") : (rows[i].replace(",", " ")).indexOf(",") != -1 ? (rows[i].replace(",", " ")).indexOf(",") : (rows[i].replace(";", " ")).indexOf(";") != -1 ? (rows[i].replace(";", " ")).indexOf(";") : -1);
            }

            var numCred = (delimiter1 > 0) ? $.trim(rows[i].substring(0, delimiter1)) : (ContainerPage[1] != "1") ? $.trim(rows[i]) : "0";
            if (i == 0 && isNaN(numCred)) {
                $ManualAssignment.HeadBoard.FullLine = rows[i];
                $ManualAssignment.HeadBoard.Delimiter1 = delimiter1;
                $ManualAssignment.HeadBoard.delimiter2 = delimiter2;
                if (delimiter1 > 0 && delimiter2 < 0) {
                    if ((rows[i].substring(delimiter1 + 1, rows[i].length)).toUpperCase() == $ManualAssignment.HeadBoard.UserHeader.toUpperCase()) {
                        $ManualAssignment.HeadBoard.ContainsAux = false;
                    } else if ((rows[i].substring(delimiter1 + 1, rows[i].length)).toUpperCase() == $ManualAssignment.HeadBoard.AuxHeader.toLocaleUpperCase()) {
                        $ManualAssignment.HeadBoard.ContainsUser = false;
                    }
                }

                continue;
            }
            if (isNaN(numCred)) {
                $(MaskAjax).hide();
                alert("'" + numCred + "' valor no permitido como crédito");
                return false;
            }
            if (delimiter1 > 0 && delimiter2 > 0) {
                user = rows[i].substring(delimiter1 + 1, delimiter2);
                aux = (delimiter1 > 0 && delimiter2 > 0) ? rows[i].substring(delimiter2 + 1, rows[i].length) : "&nbsp;";
                aux = aux.length > 20 ? aux.substring(0, 20) : aux;
            } else if (delimiter1 > 0) {
                if ($ManualAssignment.HeadBoard.ContainsUser) {
                    user = rows[i].substring(delimiter1 + 1, rows[i].length);
                } else if ($ManualAssignment.HeadBoard.ContainsAux) {
                    aux = rows[i].substring(delimiter1 + 1, rows[i].length);
                }
            }

            credits.push(numCred);
            if (numCred.length < 10) {
                for (var j = numCred.length; j < 10; j++) {
                    numCred = "0" + numCred;
                }
            } else if (numCred.length >= 11) {
                $(MaskAjax).hide();
                alert("'" + numCred + "' valor no permitido como crédito");
                return false;
            }
            rowsHtml += "<tr><td class='" + CreditsTableCreditColumn + "'>" + numCred + "</td><td class='" + CreditsTableUserColumn + "'>" + $ManualAssignment.CleanUpData(user) + "</td><td class='" + CreditsTableAuxiliarColumn + "'>" + $ManualAssignment.CleanUpData(aux) + "</td></tr>";
        }
        var repetidos = $ManualAssignment.FindRepeated(credits);
        if (repetidos != null) {
            $(MaskAjax).hide();
            alert("El crédito '" + repetidos + "' se encuentra repetido");
            return false;
        } else {
            $(CreditsTable).find("tr").remove();
            $(CreditsTable).append(rowsHtml);
            $ManualAssignment.EnableButtons();
            $(MaskAjax).hide();
        }
        return false;
    },
    EnableButtons: function() {
        switch (ContainerPage[1]) {
            case "1":
                $(BtManualAssignment).removeAttr("disabled");
                break;
            case "2":
                $(BtManualAssignment + "," + BtManualCancel).removeAttr("disabled");
                break;
            case "3":
                $(BtManualCancel + "," + BtManualReallocation + "," + BtManualAuthorize + "," + BtManualAssignment).removeAttr("disabled");
                break;
        }
    },
    DisableButtons: function (general) {
        if (general) {
            $(BtUpload).attr("disabled", "true");
            $(BtProcessData).attr("disabled", "true");
        } 
            $(BtManualAssignment).attr("disabled", "true");
            $(BtManualCancel).attr("disabled", "true");
            $(BtManualReallocation).attr("disabled", "true");
            $(BtManualAuthorize).attr("disabled", "true");    
    },
    SendInformation: function (urlAjax, containerPage) {
        $ManualAssignment.DisableButtons(true);
        var creditsArr = [];
        $(CreditsTable).find("tr").each(function (i) {
            var rowsTd = $(this).find("td");
            creditsArr.push(rowsTd[0].innerText + "||" + $ManualAssignment.CleanUpData(rowsTd[1].innerText) + "||" + $ManualAssignment.CleanUpData(rowsTd[2].innerText));
        });
        var creditsInfo = JSON.stringify({ "creditosList": creditsArr, "paginaActual": containerPage });
        $(MaskAjax).show();
        $(MaskAjax + " img").show();
        
        if (BloquearMultiplesClicks($(BtManualAssignment))) {
            $.ajax({
                url: urlAjax,
                type: 'POST',
                data: creditsInfo,
                contentType: 'application/json; charset=utf-8',
                success: $ManualAssignment.ShowResponseM,
                error: function (msg, text, thrown) {
                    alert("error " + thrown);
                }
            });


        }
    },
    CleanUpData: function (dataText) {
        dataText = dataText.replace(/\'/gi, '');
        dataText = dataText.replace(/\"/gi, '');
        dataText = dataText.replace(/\,/gi, '');
        return $.trim(dataText);
    },
    showPopUpManual: function() {
        PopUpAsignacionM.Show();
        $ManualAssignment.RefreshContaners();
    },
    ShowResponseM: function (response) {
        $(MaskAjax).hide();
        if (response.Tipo == "-1" || response.Tipo == "1") {
            $(BtRefreshTable).click();
        }
    },
    RefreshContaners: function () {
        $(TextAreaCredits).val("");
        $(CreditsTable).find("tr").remove();
        $(CreditsTable).append(EmptyTable);
        $ManualAssignment.DisableButtons(false);
    },
    UploadFile: {
        Initialize: function () {
            
            $(BtUpload).click(function () {
                //$(MaskAjax).show();
                var isChrome= navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
                if (!window.FileReader) {
                    //$(MaskAjax).hide();
                    alert("El navegador no soporta esta acción");
                    return false;
                }
                var input = $(BtFileInput).get(0);
                var reader = new FileReader();
                if (input.files.length) {
                    var textFile = input.files[0];
                    if (textFile.type == "csv" || textFile.type == "application/vnd.ms-excel" || textFile.type == "text/plain") {
                        reader.readAsText(textFile);
                       // $(MaskAjax).show();
                        if (isChrome) {
                          //  $(MaskAjax).find("img").hide();
                            $(reader).on('load', $ManualAssignment.UploadFile.ProcessFile);
                        } else {
                            $(reader).on('load', $ManualAssignment.UploadFile.ProcessFileAsync);
                        }
                        if ($(TextAreaCredits).val().trim().length == 0) {
                            alert("La información del archivo no cumple con el formato predefinido, favor de verificar el contenido.");
                        }
                    }
                    else {
                     //   $(MaskAjax).hide();
                        alert("Archivo no soportado");
                        return false;
                    }
                    
                } else {
                   // $(MaskAjax).hide();
                    alert("Para continuar debe cargar un archivo");
                }
                return false;
            });
        },
        ProcessFileAsync: function (e) {
            var file = e.target.result, result;
            
            if (file && file.length) {
                $(TextAreaCredits).val("");
                result = file.split("\n");
                
                var i = 0, limit = result.length, busy = false;
                var processor = setInterval(function () {
                    if (!busy) {
                        $ManualAssignment.UploadFile.Parser(result[i], i);
                        if (++i == limit) {
                            clearInterval(processor);
                            $(TextAreaCredits).val($ManualAssignment.CleanUpData(resultLines.toString()));
                            $(MaskAjax).hide();
                        }
                    }
                    busy = false;
                }, 1);
            }
        },
        ProcessFile: function(e) {
            var file = e.target.result, result;
            
            if (file && file.length) {
                $(TextAreaCredits).val("");
                result = file.split("\n");
                for (var i = 0; i < result.length; i++) {

                    var delimiter1 = -1;
                    var delimiter2 = -1;
                    delimiter1 = (result[i].indexOf("|") != -1 ? result[i].indexOf("|") : result[i].indexOf("\t") != -1 ? result[i].indexOf("\t") : result[i].indexOf(",") != -1 ? result[i].indexOf(",") : result[i].indexOf(";") != -1 ? result[i].indexOf(";") : -1);
                    if (delimiter1 > 0) {
                        delimiter2 = (result[i].replace("|", " ").indexOf("|") != -1 ? (result[i].replace("|", " ")).indexOf("|") : (result[i].replace("\t", " ")).indexOf("\t") != -1 ? (result[i].replace("\t", " ")).indexOf("\t") : (result[i].replace(",", " ")).indexOf(",") != -1 ? (result[i].replace(",", " ")).indexOf(",") : (result[i].replace(";", " ")).indexOf(";") != -1 ? (result[i].replace(";", " ")).indexOf(";") : -1);
                    }
                    if (delimiter1 > 0 && delimiter2 > 0) {
                          $(TextAreaCredits).val($(TextAreaCredits).val() + $ManualAssignment.CleanUpData(result[i].substring(0, delimiter1)) + "|" + $ManualAssignment.CleanUpData(result[i].substring(delimiter1 + 1, delimiter2)) + "|" + $ManualAssignment.CleanUpData(result[i].substring(delimiter2 + 1, result[i].length)) + "\r");
                        
                    } else if (delimiter1 > 0) {
                          $(TextAreaCredits).val($(TextAreaCredits).val() + $ManualAssignment.CleanUpData(result[i].substring(0, delimiter1)) + "|" + $ManualAssignment.CleanUpData(result[i].substring(delimiter1 + 1, result[i].length)) + "\r");
                        
                    }

                }
                $(MaskAjax).hide();
            }
        },
        Parser: function (container,index) {
            var delimiter1 = -1;
            var delimiter2 = -1;
            delimiter1 = (container.indexOf("|") != -1 ? container.indexOf("|") : container.indexOf("\t") != -1 ? container.indexOf("\t") : container.indexOf(",") != -1 ? container.indexOf(",") : container.indexOf(";") != -1 ? container.indexOf(";") : -1);
            if (delimiter1 > 0) {
                delimiter2 = (container.replace("|", " ").indexOf("|") != -1 ? (container.replace("|", " ")).indexOf("|") : (container.replace("\t", " ")).indexOf("\t") != -1 ? (container.replace("\t", " ")).indexOf("\t") : (container.replace(",", " ")).indexOf(",") != -1 ? (container.replace(",", " ")).indexOf(",") : (container.replace(";", " ")).indexOf(";") != -1 ? (container.replace(";", " ")).indexOf(";") : -1);
            }
            if (delimiter1 > 0 && delimiter2 > 0) {
                resultLines[index] = ($(TextAreaCredits).val() + $ManualAssignment.CleanUpData(container.substring(0, delimiter1)) + "|" + $ManualAssignment.CleanUpData(container.substring(delimiter1 + 1, delimiter2)) + "|" + $ManualAssignment.CleanUpData(container.substring(delimiter2 + 1, container.length)) + "\r");
            } else if (delimiter1 > 0) {
                resultLines[index] = ($(TextAreaCredits).val() + $ManualAssignment.CleanUpData(container.substring(0, delimiter1)) + "|" + $ManualAssignment.CleanUpData(container.substring(delimiter1 + 1, container.length)) + "\r");
            }
        }
    },
    FindRepeated: function (dataOrigin) {
        var seed;
        if (dataOrigin.length > 1) {
            seed = $.trim(dataOrigin[0]);
        } else {
            return null;
        }
        for (var i = 1; i < dataOrigin.length; i++) {
            for (var j = i; j < dataOrigin.length; j++) {
                if ($.trim(dataOrigin[j]) == seed) {
                    return seed;
                }
            }
            seed = $.trim(dataOrigin[i]);
        }
        return null;
    }
};

$(document).ready(function () {
    $ManualAssignment.Initialize();
});
