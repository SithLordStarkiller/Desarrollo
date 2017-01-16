var AccionPadre = null;
var block = 0;

$(document).ready(function () {
    $(document).on("focus", ".HelpText",function () {
        SetHelpVisible(true, $(this), 'Mensaje');
    });
    $(document).on("blur", ".HelpText", function () {
        SetHelpVisible(false, $(this), '');
    });
    $(document).on("mouseover", ".HelpText:not(:input)", function (e) {
        SetHelpVisible(true, $(this), 'Mensaje');
    });
    $(document).on("mouseout", ".HelpText:not(:input)", function (e) {
        SetHelpVisible(false, $(this), '');
    });
});

function validateString(evt) {
    var tecla;
    tecla = (document.all) ? evt.keyCode : evt.which;
    var patron;
    if (evt.charCode == 32) { return true; }
    patron = /[\b@.abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789áéíóúÁÉÍÓÚ&_-]/;//especiales
    var te;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}
function validateStringD(evt) {
    var tecla;
    tecla = (document.all || evt.keyCode == 9) ? evt.keyCode : evt.which;
    var patron;
    patron = /[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789?_\b\t]/;
    var te;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}
function validateStringU(evt) {
    var tecla;
    tecla = (document.all || evt.keyCode == 9) ? evt.keyCode : evt.which;
    var patron;
    patron = /[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_\b\t]/;
    var te;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}
function validatePassU(campo) {
    var regExPattern = /(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/;
    if (!regExPattern.test(campo.val())) {
        return true;
    } else {
        return false;
    }
}


function estatusHandler(obj, e, max) {
    e = e || event;
    max = max || 50;

    if (obj.value.length >= max && e.charCode > 46) {
        return false;
    }
    return validateInteger(e);
}

function noCopyKey(e) {
    var forbiddenKeys = new Array('c', 'x');
    var keyCode = (e.keyCode) ? e.keyCode : e.which;
    var isCtrl;


    if (window.event)
        isCtrl = e.ctrlKey;
    else
        isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;


    if (isCtrl) {
        for (i = 0; i < forbiddenKeys.length; i++) {
            if (forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                return false;
            }
        }
    }
    return true;
}

function validateInteger(evt) {
    if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {

        return false;
    }
}


function LimpiarBloqueoMultiplesClicks(boton) {
    boton.clicks = 0;
    //boton[0].clicks = 0;
}

function BloquearMultiplesClicks(boton) {
    if (boton.clicks != 1) {
        boton.clicks = 1;
        return true;
    } else {
        return false;
    }
}

function BloquearMultiplesClicksConf(boton, parametro,mensajeAlternativo) {
    AccionPadre = boton;
    if (block == 0) {
        var mensaje = mensajeAlternativo == 1 ? document.getElementById("ConfBtnMensajePlantilla") : document.getElementById("ConfBtnMensajePlantilla2");
        document.getElementById("ConfBtnMensaje").innerText = mensaje.innerText.replace("{0}", parametro);
        ConfirmacionBoton.Show();
    } else if (block == 1) {
        return true;
        
    } 
    return false;
}

function AceptaAccion() {
    document.getElementById("ConfirmacionBoton_HCB-1").click();  
    block = block + 1;
    AccionPadre.click();
    AccionPadre.disabled = true;
    return false;
}

function CancelarAccion() {
    document.getElementById("ConfirmacionBoton_HCB-1").click();
    return false;
}
function SetHelpVisible(value, $element) {
    $("#HelpText").html($element.attr("HelpText"));
    var popupControl = ASPxPopupHintClientControl;
    var offset = $element.offset();
    var left = offset.left;
    var top = offset.top; 
   var isPPVisible= $(".dxpcLite.dxpclW").is(":visible");

   if (!isPPVisible) {
       if (!$element.is(":input")) {
           top += $element.outerHeight() + 10;
           popupControl.PopupHorizontalAlign = "OutsideRight";
       } else {
           if ((($(document).width() - left) / 2 < $element.outerWidth())) {
               top += $element.outerHeight() + 10;
               popupControl.PopupHorizontalAlign = "OutsideLeft";
           } else {
               left += $element.outerWidth();
               popupControl.PopupHorizontalAlign = "OutsideRight";
           }
       }
   } else {
       top += $element.outerHeight() + 10;
       left += 250;
       popupControl.PopupHorizontalAlign = "OutsideRight";
   }
    ////$(".dxpcLite.dxpclW:visible").position().top
    
    popupControl.ShowAtPos(left, top);
    if (value)
        popupControl.ShowWindow();
    else
        popupControl.HideWindow();
}

function validarRFC(rfc) {
    var exp = new RegExp(/^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$/);
    if (exp.test(rfc))
        return true;
    else
        return false;
}