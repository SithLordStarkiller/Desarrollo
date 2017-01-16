var ArrIdOrdenes = [];
var ArrIdOrdenesA = [];
var NewManager = "0";
var OrdAsig = "0";
var BtRefreshTable = ".btnLimpiar";
// ContainerPage = 0;//1:AsignaOrdenes,2:OrdenesAsignadas,3:Gestionadas     var included in AsignacionManual.js
var MaxGestor = 1000;
var MaxSuper = 10000;

var $OrdenesController = {
    UrlAjaxAuthorize: "/Ordenes/AutorizarOrdenes",
    UrlAjaxCancel: "/Ordenes/CancelarOrdenes",
    UrlAjaxReallocation: "/Ordenes/ReasignarOrdenes",
    UrlAjaxAssignmentGestor: '/Ordenes/AsignarGestorOrdenes',
    UrlAjaxAssignmentDifGestor: "/Ordenes/AsignarDifGestorOrdenes",
    UrlAjaxReverse: "/Ordenes/ReversarOrdenes",
    UrlAjaxReallocationIncomplete: "/Ordenes/ReasignarOrdenesIncompletas",
    ////URL in SIRA
    UrlAjaxAssignmentSIRAProgGestor: '/Ordenes/AsignarGestorOrdenesSiraProg',
    UrlAjaxAssignmentSIRANoProgGestor: '/Ordenes/AsignarGestorOrdenesSiraNoProg',

    extraVal : "",
    Initialize: function () {
        $("button[class*='MovType']").on("click", function () {
            var index = $(this).attr("class").indexOf("MovType");
            var movType = $(this).attr("class").substring(index + 7, index + 9);
            var extra = $(this).attr("class").indexOf("Extra");
            $OrdenesController.extraVal = extra > 0 ? $(this).attr("class").substring(extra + 5, $(this).attr("class").length) : "";
            if (OrdAsig == "1") {
                NewManager = "1";
            }
            if (ContainerPage[1] == "3") {
                ArrIdOrdenes = ArrIdOrdenesA;
            }
            if ((movType == "21" || movType == "11") && NewManager == "0" ) {
                    alert("Selecciona un usuario");
                    return false;
            }
            if ((movType == "21" || movType == "22" || movType == "23" || movType == "11" || movType == "4" || movType == "12" || movType == "15") && ArrIdOrdenes.length < 1) {
                alert("Selecciona al menos un crédito para procesar");
                return false;
            }
            
            if (BloquearMultiplesClicksConf(this, movType == "23" ? "reenviado" : movType == "22" ? "reversa" : movType == "11" || movType == "21" ? "asigna" : movType == "12" ? "cancela" : movType == "15" ? "reasigna" : movType == "4" ? "autoriza" : "asigna", movType == "22" ? 2 : 1)) {
                var jsonOrder = (movType == "11" || movType == "21" || movType == "12") ? JSON.stringify({ "ordenesList": ArrIdOrdenes, "idNuevoGestor": NewManager, "paginaActual": ContainerPage }) : JSON.stringify({ "ordenesList": ArrIdOrdenes, "paginaActual": ContainerPage });
                return $OrdenesController.ProcessOrder(movType, jsonOrder);
                
            }
            return false;
        });
    },
    ProcessOrder: function(processStatus,ordenInfo) {
        var  $oC=$OrdenesController;
        switch (processStatus) {
            case "4":
                $OrdenesController.SendInformation($oC.UrlAjaxAuthorize, ordenInfo);
            break;
            case "11":
                if ($OrdenesController.extraVal.indexOf("SIRA") < 0) {
                    $OrdenesController.SendInformation($oC.UrlAjaxAssignmentGestor, ordenInfo);
                } else {
                    $OrdenesController.SendInformation($OrdenesController.extraVal.indexOf("NO") < 0 ? $oC.UrlAjaxAssignmentSIRAProgGestor : $oC.UrlAjaxAssignmentSIRANoProgGestor, ordenInfo);
                }
            break;
            case "12":
                $OrdenesController.SendInformation($oC.UrlAjaxCancel, ordenInfo);
                break;
            case "15":
                $OrdenesController.SendInformation($oC.UrlAjaxReallocation, ordenInfo);
                break;
            case "21":
                $OrdenesController.SendInformation($oC.UrlAjaxAssignmentDifGestor, ordenInfo);
                break;
            case "22":
                $OrdenesController.SendInformation($oC.UrlAjaxReverse, ordenInfo);
                break;
            case "23":
                $OrdenesController.SendInformation($oC.UrlAjaxReallocationIncomplete, ordenInfo);
                break; 
                
        }
        return false;
    },
    SendInformation: function (urlAjax, ordenInfo) {
        $(MaskAjax).show();
        var img = $(MaskAjax + " img");
        if (img != null)
            $(MaskAjax + " img").show();
            $.ajax({
                url: urlAjax,
                type: 'POST',
                data: ordenInfo,
                contentType: 'application/json; charset=utf-8',
                success: $OrdenesController.ShowResponseOrder,
                error: function (msg, text, thrown) {
                    $(MaskAjax).hide();
                    alert("error " + thrown);
                }
            });
    },
    ShowResponseOrder: function() {
        //$(MaskAjax).hide();
        //if (response.Tipo == "-1" || response.Tipo == "1") {
        $(BtRefreshTable).click();
        //}
    }
};


$(document).ready(function () {
    $OrdenesController.Initialize();
});
