/// <reference path="../../Controls/chosen.jquery.js" />
/// <reference path="../../Controls/chosen.jquery.js" />

$(document).ready(function () {
    var backProcess = function () {
        $('#BackProcess').css('display', 'block');
    };
    var moveProcess = function () {
        $('#BackProcess').css('display', 'none');
    };
    $.ajaxSetup({
        
    });
    $(document)
		.ajaxError(responseError)
		.ajaxStart(backProcess)
        .ajaxComplete(moveProcess);;
    //if ($.browser.msie && $.browser.version.substr(0, 1) < 7) {
    //    $('li').has('ul').mouseover(function () {
    //        $(this).children('ul').show();
    //    }).mouseout(function () {
    //        $(this).children('ul').hide();
    //    })
    //}
});


var Ui = function () {
};

Ui.prototype = {
    constructor: Ui
}

Ui.prototype.getUrl = function (url) {
    if (this.urlBase === undefined) {
        this.urlBase = document.getElementById('UrlBase').value;
    }

    return '{k1}{k2}'.format({ k1: (this.urlBase.length > 1) ? this.urlBase : '', k2: url });
};

Ui.prototype.removeValidations = function (o) {
    $(o).find(".input-validation-error").removeClass("input-validation-error").addClass("input-validation-valid");
    $(o).find(".field-validation-error").removeClass("field-validation-error").addClass("field-validation-valid").html("");
};

Ui.prototype.confirmacion = function (contentBody, options) {
    var modal = document.createElement('div');
    modal.id = "modal";
    modal = $(modal);
    var defaultOptions = { title: '', aceptar: {}, cancel: {} };
    var obtenerEstructura = function (options) {
        var isHtml = function (contentBody) {
            if (contentBody === 'undefined' || contentBody === null || contentBody === '') return false;
            var doc = new DOMParser().parseFromString(contentBody, "text/html");
            return doc.hasChildNodes();

        };
        modal.addClass("modal fade");
        modal.attr("role", "dialog");
        var content = document.createElement("div");
        var modalDialog = document.createElement("div");
        content.className = "modal-content ";
        modalDialog.className = "modal-dialog ";
        var modalHeader = document.createElement("div");
        var modalButton = document.createElement("button");
        var modalTitle = document.createElement("h4");
        var modalBody = document.createElement("div");
        var modalFooter = document.createElement("div");
        var modalContainerButtons = document.createElement("div");
        var btnFooterAceptar = document.createElement("button");
        var btnFooterCancel = document.createElement("button");
        btnFooterAceptar.type = "button";
        btnFooterAceptar.className = "btn btn-default";
        btnFooterAceptar.innerHTML = "Aceptar";
        btnFooterCancel.type = "button";

        btnFooterCancel.className = "btn btn-default";
        btnFooterCancel.innerHTML = "Cancelar";
        modalBody.className = "modal-body";
        modalFooter.className = "modal-footer";

        modalHeader.className = "modal-header ";
        modalButton.className = "close ";
        modalButton.setAttribute("data-dismiss", "modal");
        modalTitle.className = "modal-title ";
        modalContainerButtons.className = "pull-right";
        modalTitle = options.title;
        modalHeader.appendChild(modalButton);
        modalHeader.innerHTML = modalTitle;
        content.appendChild(modalHeader);
        if (isHtml(contentBody)) {
            modalBody.innerHTML = contentBody;
        }
        else {
            var parrafo = document.createElement("p");
            parrafo.innerHTML = contentBody;
            modalBody.appendChild(parrafo);
        }
        $(btnFooterAceptar).on("click", function () {
            if (options.aceptar && typeof (options.aceptar) === 'function') {
                $.when(options.aceptar.call()).done(function () {
                    modal.remove();
                    $(".modal-backdrop").remove();
                });
            }
        });
        $(btnFooterCancel).on("click", function (evt) {
            if (options.cancel && typeof (options.cancel) === 'function') {
                $.when(options.cancel.call()).done(function () {
                    modal.remove();
                    $(".modal-backdrop").remove();
                });
            } else {
                modal.remove();
                $(".modal-backdrop").remove();
            }
        });
        modalContainerButtons.appendChild(btnFooterCancel);
        modalContainerButtons.appendChild(btnFooterAceptar);
        modalFooter.appendChild(modalContainerButtons);
        content.appendChild(modalBody);
        content.appendChild(modalFooter);
        modalDialog.appendChild(content);
        modal.append(modalDialog);
    };
    options = $.extend(defaultOptions, options, true);
    $.when(obtenerEstructura(options)).done(function () { $(modal).modal({ show: "true", backdrop: "static" }) });
};
Ui.prototype.showMessage = function (contentBody, options) {
    var modal = document.createElement('div');
    modal.id = "modal";
    modal = $(modal);
    var defaultOptions = { title: '', aceptar: {}, cancel: {} };
    var obtenerEstructura = function (options) {
        var isHtml = function (contentBody) {
            var doc = new DOMParser().parseFromString(contentBody, "text/html");
            return 0;//return Array.from(doc.body.childNodes).some(node => node.nodeType === 1);
        };
        modal.addClass("modal fade");
        modal.attr("role", "dialog");
        var content = document.createElement("div");
        var modalDialog = document.createElement("div");
        content.className = "modal-content ";
        modalDialog.className = "modal-dialog ";
        var modalHeader = document.createElement("div");
        var modalButton = document.createElement("button");
        var modalTitle = document.createElement("h4");
        var modalBody = document.createElement("div");
        var modalFooter = document.createElement("div");
        var modalContainerButtons = document.createElement("div");
        var btnFooterAceptar = document.createElement("button");

        btnFooterAceptar.type = "button";
        btnFooterAceptar.className = "btn btn-default";
        btnFooterAceptar.innerHTML = "Aceptar";

        modalBody.className = "modal-body";
        modalFooter.className = "modal-footer";

        modalHeader.className = "modal-header ";
        modalButton.className = "close ";
        modalButton.setAttribute("data-dismiss", "modal");
        modalTitle.className = "modal-title ";
        modalContainerButtons.className = "text-center";
        modalTitle = options.title;
        modalHeader.appendChild(modalButton);
        modalHeader.innerHTML = modalTitle;
        content.appendChild(modalHeader);
        if (isHtml(contentBody)) {
            modalBody.appendChild(contentBody);
        }
        else {
            var parrafo = document.createElement("p");
            parrafo.innerHTML = contentBody;
            modalBody.appendChild(parrafo);
        }
        $(btnFooterAceptar).on("click", function () {
            if (options.aceptar && typeof (options.aceptar) === 'function') {
                $.when(options.aceptar.call()).done(function () {
                    modal.remove();
                    $(".modal-backdrop").remove();
                });
            } else {
                modal.remove();
                $(".modal-backdrop").remove();
            }
        });
        modalContainerButtons.appendChild(btnFooterAceptar);
        modalFooter.appendChild(modalContainerButtons);
        content.appendChild(modalBody);
        content.appendChild(modalFooter);
        modalDialog.appendChild(content);
        modal.append(modalDialog);
    };
    options = $.extend(defaultOptions, options, true);
    $.when(obtenerEstructura(options)).done(function () { $(modal).modal({ show: "true", backdrop: "static" }) });
};

Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: JSON.stringify(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function
    });
};


Ui.prototype.loadCombo = function (data, destino) {
    $(destino).empty();
    if (!destino.multiple) {
        var opcion = document.createElement("option");
        opcion.value = "";
        opcion.innerHTML = "Seleccione";
        destino.appendChild(opcion);
    }

    for (var i = 0; i < data.length; i++) {
        var opcion = document.createElement("option");
        opcion.value = data[i].Identificador;
        opcion.innerHTML = data[i].Nombre;
        $(opcion).data("option", data[i]);
        destino.appendChild(opcion);
    };
};
function responseError(event, xmlHttpRequest, textStatus, errorThrown) {
    var xml = undefined;
    var status = xmlHttpRequest.status;

    switch (status) {
        case 0:  // Cancel request
            break;

        case 303:  // Sessión expired 
            try {
                var result = $.parseJSON(xmlHttpRequest.responseText);

                window.location = result.url;
            }
            catch (e) {
                alert('Error irrecuperable');
            }
            break;

        case 404:  // Not found
            alert('Tiempo de inactividad agotado. Envie de nuevo su solicitud');
            break;

        default:
            alert('Se sucito un error, intente de nuevo, si persiste la situción contacte a su administrador');

    }
}