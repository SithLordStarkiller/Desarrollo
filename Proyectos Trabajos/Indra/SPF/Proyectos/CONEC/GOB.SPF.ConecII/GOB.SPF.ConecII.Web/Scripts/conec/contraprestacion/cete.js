

Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.Cete = {
        Listado: [],
        Documentos: []
    };

    $(function () {
        // We can attach the `fileselect` event to all file inputs on the page
        $(document).on('change', ':file', function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            $("#fileUrl").val(label);
            input.trigger('fileselect', [numFiles, label]);
        });
    });

    this.urls = {
        cete: this.getUrl('/Contraprestacion/Cete'),
        cetes: this.getUrl('/Contraprestacion/CetesConsulta'),
        buscar: this.getUrl('/Contraprestacion/CetesConsulta'),
        guardar: this.getUrl('/Contraprestacion/CetesGuardar'),
        documento: this.getUrl('/Files/UploadFile'),
        documentos: this.getUrl('/Contraprestacion/CeteDocumentos'),
        previsualizacion: this.getUrl('/Contraprestacion/CetesPreview'),
    };

    this.mensajes = {
        fileError: 'El archivo no puede ser leido.',
        fileErrorType: 'El archivo no es del tipo requerido, solo puede cargar archivos con la extensión xls o xlsx.',
        fileErrorSchema: 'El archivo no tiene el formato correcto.',
        saveMessage: 'Tasa de Rendimiento CETES ha sido cargado correctamente.'
    };

    var gridColumns = {
        detailColumns: [
           { text: 'No.', iskey: true, ishisHierarchy: true, css: 'col-xs-1', visible: true },
           { text: 'Fecha', field: 'Fecha', css: 'col-xs-9' },
           { text: 'Tasa de rendimiento', field: 'TasaRendimiento', css: 'col-xs-3' }]
    };

    this.controls = {
        grids: {
            cete: new controls.Grid('divCetes', {
                maxHeight: 400,
                pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }
            })
        },
        buttons: {
            create: $("#btnCreate"),
            back: "#btnBack",
            save: "#btnSave",
            upload: "#btnUpload",
            search: "#btnBuscar"
        }
    };

    this.controls.grids.cete.init(gridColumns.detailColumns);
    this.controls.grids.cete.addListener("onPage", function (evt) {
        self.controls.grids.cete.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle();
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle();
            $(".flip div.front").fadeIn();
        }
    };

    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.cete.reload([]);
                self.controls.grids.cete.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
        else {
            self.showMessage(data.Message);
        }
    };
    var successSave = function (data) {
        if (data.Result === 1) {
            success(data);
            self.showMessage(self.mensajes.saveMessage);
        }
        else {
            self.showMessage(data.Message);
        }
    };

    /* Realiza la búsqueda de los cetes por rango */
    $(this.controls.buttons.search).click(function () {
        var data = {
            ObjectResult: {
                Anio: $("#Anio").val() === '' ? null : parseInt($("#Anio").val()),
                Mes: $("#Mes").val() === '' ? null : parseInt($("#Mes").val()),
                FechaInicial: $("#fechaInicial").val() === '' ? null : $("#fechaInicial").val(),
                FechaFinal: $("#fechaFinal").val() === '' ? null : $("#fechaFinal").val()
            }, Paging: {
                CurrentPage: self.controls.grids.cete.currentPage,
                Rows: self.controls.grids.cete.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
    });

    $(this.controls.buttons.create).click(function () {
        var getPartial = function (data) {
            self.functions.hideGrid(data);

            $("#btnBack").click(function () {
                self.functions.hideForm(data);
            });

            $("#btnSave").on('click', function () {
                /* Validar si tiene archivo cargado, Cargar los datos en la base */
                var transformDate = function (list) {
                    return list.filter(function (item) {
                        var data = $.extend(item, {}, true);
                        if (!(data.Fecha instanceof Date))
                            data.Fecha = new Date(parseInt(data.Fecha.replace(/\/Date\(|\)\//g, '')));
                        return data;
                    });
                };
                messageOptions = {
                    title: "Confirmar",
                    aceptar: function () {
                        var cetes = {
                            List: transformDate(self.Cete.Listado)
                        }

                        self.SendAjax('POST', self.urls.guardar, 'json', cetes, successSave);
                    }
                };
                self.confirmacion("¿Está de acuerdo en cargar la información del archivo?", messageOptions);
            });

            self.documentos();
        };

        self.SendAjax('POST', self.urls.cete, 'html', {}, getPartial);
    });

    this.load();
};

Ui.prototype.documentos = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    var columnsPreview = [
           { text: 'No.', iskey: true, ishisHierarchy: true, css: 'col-xs-1', visible: true },
           { text: 'Fecha', field: 'Fecha', css: 'col-xs-9' },
           { text: 'Tasa de rendimiento', field: 'TasaRendimiento', css: 'col-xs-3' }];

    this.controls.grids = $.extend(this.controls.grids, {
        documentos: new controls.Grid('ceteDocumentos', { maxHeight: 400, pager: { show: false } }),
        cetePreview: new controls.Grid('ceteList', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 } })
    }, true);

    var columns = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' }];

    this.controls.grids.documentos.init(columns);
    this.controls.grids.documentos.loadRows([]);
    self.controls.grids.cetePreview.init(columnsPreview);
    self.controls.grids.cetePreview.reload([]);

    var jqXHRData = undefined;
    var getPartial = function (data) {
        if (data.length) {
            this.controls.grids.documentos.loadRows(data);
        }

        'use strict';
        $('#documento').fileupload({
            formData: { Directory: ceteModel.UniqueId },
            maxFileSize: 5000000000,
            url: self.urls.documento,
            dataType: 'json',
            add: function (e, data) {
                jqXHRData = data;
                /// Validar la extensión xls y xlsx
                var m = self.fileExtension(data.files[0].name, /(\.|\/)(xls|xlsx)$/i);
                if (m && jqXHRData) {
                    jqXHRData.submit();
                }
                else {
                    self.showMessage('Error al cargar el archivo: ' + self.mensajes.fileErrorType);
                }
            },
            done: function (event, data) {
                if (data.result.isUploaded) {
                    if (self.Cete.Documentos.length > 0) {
                        self.Cete.Documentos[0] = data.result.documento
                    }
                    else {
                        data.result.documento.Numero = self.Cete.Documentos.length + 1;
                        self.Cete.Documentos.push(data.result.documento);
                    }
                    self.controls.grids.documentos.loadRows([]);
                    self.controls.grids.documentos.loadRows(self.Cete.Documentos);

                    self.cetes();
                }
                else {

                }
                //alert(data.result.message);
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });
        $(document).on('change', ':file', function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            $("#fileUrl").val(label);
            input.trigger('fileselect', [numFiles, label]);
        });
    };

    self.SendAjax('POST', self.urls.documentos, 'json', {}, getPartial);
};

Ui.prototype.cetes = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    var getAsyncData = function (data) {
        /* Acá cargamos el grid de previsualizacion */
        if (data.Result === 1) {

            self.Cete.Listado = data.List;

            self.controls.grids.cetePreview.reload([]);
            self.controls.grids.cetePreview.reload(self.Cete.Listado);
        }
        else {
            self.showMessage(data.Message);
        }
        $("#LoadingExcelData").css("display", "none");
        $("#ceteList").css("display", "block");
    };

    var ceteFile = {
        UniqueId: ceteModel.UniqueId,
        Documento: self.Cete.Documentos[0],
    };

    $("#LoadingExcelData").css("display", "block");
    self.SendAjax('Post', self.urls.previsualizacion, 'Json', ceteFile, getAsyncData);
};

Ui.prototype.busquedaTipo = function (value) {
    if (value) {
        $("#busquedaRango").fadeOut("fast", function() {
            $("#busquedaAnioMes").fadeIn("fast");
        });
    } else {
        $("#busquedaAnioMes").fadeOut("fast", function() {
                $("#busquedaRango").fadeIn("fast");
            });
    }
    $("#Anio").val('');
    $("#Mes").val('');
    $("#fechaInicial").val('');
    $("#fechaFinal").val('');
};

Ui.prototype.fileExtension = function (fileName, extensions) {
    var regex = extensions;
    var str = fileName;
    return regex.exec(str);
};

Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.cete.currentPage;
    var rows = self.controls.grids.cete.pageSize;
    var loadGrid = function (data) {

        self.controls.grids.cete.reload([]);
        if (data.Result) {
            self.controls.grids.cete.currentPage = data.Paging.CurrentPage;
            self.controls.grids.cete.pages = data.Paging.Pages;
            self.controls.grids.cete.reload(data.List);
        }
    };

    var data = {
        page: page, rows: rows,
        Query: {
            IsActive: $("input[name=estatus]:checked").val(),
        },
    };

    this.SendAjax('POST', self.urls.cetes, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();

    $("#fechaInicial").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#fechaFinal").datepicker({ dateFormat: 'dd/mm/yy' });
    $("input[name=tipoBusqueda]").on("click",
        function (e) {
            var value = $(this).val() == 'true';
            ui.busquedaTipo(value);
        });
};

init();
