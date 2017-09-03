var Ui = function () {
};

Ui.prototype = {
    constructor: Ui
}

Ui.prototype.getUrl = function (url) {
    if (this.urlBase === undefined) {
        this.urlBase = $('#UrlBase').val();
    }

    return '{k1}{k2}'.format({ k1: (this.urlBase.length > 1) ? this.urlBase : '', k2: url });
};


//$.validator.setDefaults({
//    submitHanler: function () {
//        alert("submitted");
//    }
//});


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


Ui.prototype.confirmacion = function (contentBody, options) {
    var modal = $('#modal');
    var defaultOptions = { title: '', aceptar: {}, cancel: {} };
    var obtenerEstructura = function (options) {
        var isHTML = function (contentBody) {
            var doc = new DOMParser().parseFromString(contentBody, "text/html");
            return 0;//;Array.from(doc.body.childNodes).some(node => node.nodeType === 1);
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
        modalHeader.append(modalButton);
        modalHeader.append(modalTitle);        
        content.appendChild(modalHeader);
        if (isHTML(contentBody)) {
            modalBody.appendChild(contentBody);
        }
        else {
            var parrafo = document.createElement("p");
            parrafo.innerHTML = contentBody;
            modalBody.appendChild(parrafo);
        }
        $(btnFooterAceptar).on("click", function () {
            if (options.aceptar && typeof (options.aceptar) === 'function') {
                options.aceptar;
            }
            modal.modal('hide');
            $(".modal-dialog").remove();
        });
        $(btnFooterCancel).on("click", function (evt) { if (options.cancel && typeof (options.cancel) === 'function') { options.cancel; modal.modal('hide'); } else { modal.modal('hide'); } });
        modalContainerButtons.appendChild(btnFooterCancel);
        modalContainerButtons.appendChild(btnFooterAceptar);        
        modalFooter.appendChild(modalContainerButtons);
        content.appendChild(modalBody);
        content.appendChild(modalFooter);
        modalDialog.appendChild(content);
        modal.append(modalDialog);
    };
    var options = $.extend(defaultOptions, options, true);
    obtenerEstructura(options);
    modal.modal({ show: "true", backdrop:"static" });
};
Ui.prototype.showMessage = function (contentBody, options) {
    var modal = $('#modal');
    var defaultOptions = { title: '', aceptar: {}, cancel: {} };
    var obtenerEstructura = function (options) {
        var isHTML = function (contentBody) {
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
        modalHeader.append(modalButton);
        modalHeader.append(modalTitle);
        content.appendChild(modalHeader);
        if (isHTML(contentBody)) {
            modalBody.appendChild(contentBody);
        }
        else {
            var parrafo = document.createElement("p");
            parrafo.innerHTML = contentBody;
            modalBody.appendChild(parrafo);
        }
        $(btnFooterAceptar).on("click", function () {
            if (options.aceptar && typeof (options.aceptar) === 'function') {
                options.aceptar;
            }
            modal.modal('hide');
            $(".modal-dialog").remove();
        });
        modalContainerButtons.appendChild(btnFooterAceptar);
        modalFooter.appendChild(modalContainerButtons);
        content.appendChild(modalBody);
        content.appendChild(modalFooter);
        modalDialog.appendChild(content);
        modal.append(modalDialog);
    };
    var options = $.extend(defaultOptions, options, true);
    obtenerEstructura(options);
    modal.modal({ show: "true", backdrop: "static" });
};


function responseError(event, XMLHttpRequest, textStatus, errorThrown) {
    var xml = undefined;
    var status = XMLHttpRequest.status;

    switch (status) {
        case 0:  // Cancel request
            break;

        case 303:  // Sessión expired 
            try {
                var result = $.parseJSON(XMLHttpRequest.responseText);

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
