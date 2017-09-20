Ui.prototype.init = function () {
    var frArchivo;
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        FormGuardar: this.getUrl('/PartesDocumento/GuardarFormato'),
        ImagenGuardar: this.getUrl('/PartesDocumento/GuardarImagen')
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar el contratante?.",
        activequestion: "¿Está seguro de que desea @ el contratante seleccionado?.",
    };


    var success = function (data) {
        if (data.Result === 1) {
            self.showMessage(data.Message);
        }
        else {
            self.showMessage(data.Message);
        }
    };

    var successFoto = function (data) {
        //if (data.Result === 1) {
        //    self.showMessage(data.Message);
        //}
        //else {
        //    self.showMessage(data.Message);
        //}
    };

    
    
    function saveFormato() {
        var mandarArchivo = false;
        if (!window.File || !window.FileReader || !window.FileList || !window.Blob) {
            alert('The File APIs are not fully supported in this browser.');
            return;
        }

        var input = document.getElementById('logotipo');
        if (!input) {
            alert("Um, couldn't find the fileinput element.");
        }
        else if (!input.files) {
            alert("This browser doesn't seem to support the `files` property of file inputs.");
        }
        else if (!input.files[0]) {
            mandarArchivo = false;
        }
        else {


            mandarArchivo = true;
            file = input.files[0];
            ext = file.name.split('.')[1];

            frArchivo = new FileReader();
            frArchivo.onload = receivedFoto;
            frArchivo.readAsDataURL(file);
        }


        InsertarFormato();
    }

    function receivedFoto() {
        var data =
            {
                ObjectResult:
                    {
                        IdParteDocumento: $("#Identificador").val(),
                        Imagen: frArchivo.result
                    }
            };

        self.SendAjax('POST', self.urls.ImagenGuardar, 'json', data, successFoto);
        
    }

    function InsertarFormato() {
        var data =
                {
                    ObjectResult:
                    {
                        Identificador: $("#Identificador").val(),
                        IdTipoDocumento: $("#IdTipoDocumento").val(),
                        RutaLogo: $("#RutaLogo").val(),
                        Paginado: $("#Paginado").is(':checked'),
                        Folio: $("#Folio").val(),
                        Asunto: $("#Asunto").val(),
                        LugarFecha: $("#LugarFecha").val(),
                        Puesto: $("#Puesto").val(),
                        Nombre: $("#Nombre").val(),
                        Direccion: $("#Direccion").val(),
                        Ccp: $("#Ccp").val(),
                        Activo: $("#Activo").val()
                    }

                };
        self.SendAjax('POST', self.urls.FormGuardar, 'json', data, success);
    }

    $('#btnSaveFormato').on('click', function () {
        saveFormato();
    });

};


Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }

    });
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();

$(document).ready(function () {
    $("#logotipo").fileinput({
        overwriteInitial: true,
        maxFileSize: 700,
        showClose: false,
        showCaption: false,
        browseLabel: '',
        removeLabel: '',
        browseIcon: '<i class="glyphicon glyphicon-folder-open"></i>',
        removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
        removeTitle: 'Eliminar Logo',
        elErrorContainer: '#kv-avatar-errors-1',
        msgErrorClass: 'alert alert-block alert-danger',
        //defaultPreviewContent: '<img id="FotoImputado" src="../Content/Images/default_avatar_male.jpg" alt="Fotografía de Perfil" style="width:160px">',
        //layoutTemplates: { main2: '{preview} ' + btnCust + ' {remove} {browse}' },
        layoutTemplates: { main2: '{preview} {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "gif"]
    });

});