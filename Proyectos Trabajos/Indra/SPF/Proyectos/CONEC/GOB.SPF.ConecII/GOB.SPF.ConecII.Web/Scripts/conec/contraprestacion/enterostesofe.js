

Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.Enteros = {
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
        entero: this.getUrl('/Contraprestacion/EnteroTesofe'),
        enteros: this.getUrl('/Contraprestacion/EnterosTesofeConsulta'),
        buscar: this.getUrl('/Contraprestacion/EnterosTesofeConsulta'),
        guardar: this.getUrl('/Contraprestacion/EnterosTesofeGuardar'),
        documento: this.getUrl('/Files/UploadFile'),
        documentos: this.getUrl('/Contraprestacion/EnterosTesofeDocumentos'),
        previsualizacion: this.getUrl('/Contraprestacion/EnterosTesofePreview'),
    };

    this.mensajes = {
        fileError: 'El archivo no puede ser leido.',
        fileErrorType: 'El archivo no cuenta con la extensión .xls, .xlsx, seleccione archivo con formato correcto.',
        fileErrorSchema: 'El archivo no tiene el formato correcto.',
        saveMessage: 'La información de los Enteros TESOFE se cargó exitosamente.'
    };

    var gridColumns = {
        detailColumns: [
            { text: 'No.', iskey: true, ishisHierarchy: true, css: 'col-xs-1', visible: true },
            { text: 'Operación', field: 'NumeroOperacion', css: 'col-xs-1' },
            { text: 'RFC', field: 'RFC', css: 'col-xs-1' },
            { text: 'Razón Social', field: 'RazonSocial', css: 'col-xs-1' },
            { text: 'Fecha de presentacion', field: 'FechaPresentacion', css: 'col-xs-1' },
            { text: 'Importe', field: 'Importe', css: 'col-xs-1' },
            { text: 'Cantidad pagada', field: 'CantidadPagada', css: 'col-xs-1' },
            { text: 'Clave DPA', field: 'ClaveReferenciaDPA', css: 'col-xs-1' },
            { text: 'Cadena de la dependencia', field: 'CadenaDependencia', css: 'col-xs-1' },
            { text: 'Importe IVA', field: 'ImporteIVA', css: 'col-xs-1' },
            { text: 'Total Pagado', field: 'TotalEfectivamentePagado', css: 'col-xs-1' }]
    };

    this.controls = {
        grids: {
            tesofe: new controls.Grid('divEnterosTesofe', {
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

    this.controls.grids.tesofe.init(gridColumns.detailColumns);
    this.controls.grids.tesofe.addListener("onPage", function (evt) {
        self.controls.grids.tesofe.currentPage = evt.currentPage;
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
                self.controls.grids.tesofe.reload([]);
                self.controls.grids.tesofe.reload(data);
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
                // TODO: cambiar por los filtros para Enteros Tesofe
                FechaPresentacionInicial: $("#fechaInicialPresentacion").val() === '' ? null : $("#fechaInicialPresentacion").val(),
                FechaPresentacionFinal: $("#fechaFinalPresentacion").val() === '' ? null : $("#fechaFinalPresentacion").val(),
                RazonSocial: $("#razonSocial").val() === '' ? null : $("#razonSocial").val(),
                LlavePago: $("#llavePago").val() === '' ? null : $("#llavePago").val(),
                NumeroOperacion: $("#numeroOperacion").val() === '' ? null : $("#numeroOperacion").val(),
                RFC: $("#rfc").val() === '' ? null : $("#rfc").val(),
                //ClaveReferenciaDPA: $("#ClaveReferenciaDPA").val() === '' ? null : $("#ClaveReferenciaDPA").val(),
                FechaCargaInicial: $("#fechaInicialCarga").val() === '' ? null : $("#fechaInicialCarga").val(),
                FechaCargaFinal: $("#fechaFinalCarga").val() === '' ? null : $("#fechaFinalCarga").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.tesofe.currentPage,
                Rows: self.controls.grids.tesofe.pageSize
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
                        if (!(data.FechaPresentacion instanceof Date))
                            data.FechaPresentacion = new Date(parseInt(data.FechaPresentacion.replace(/\/Date\(|\)\//g, '')));
                        return data;
                    });
                };
                messageOptions = {
                    title: "Confirmar",
                    aceptar: function () {
                        var enterosData = {
                            List: transformDate(self.Enteros.Listado)
                        }

                        self.SendAjax('POST', self.urls.guardar, 'json', enterosData, successSave);
                    }
                };
                self.confirmacion("¿Está de acuerdo en cargar la información del archivo?", messageOptions);
            });

            self.documentos();
        };

        self.SendAjax('POST', self.urls.entero, 'html', {}, getPartial);
    });

    this.load();
};

Ui.prototype.documentos = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    var columnsPreview = [
        { text: 'No.', iskey: true, ishisHierarchy: true, css: 'col-xs-1', visible: true },
        { text: 'Operación', field: 'NumeroOperacion', css: 'col-xs-1' },
        { text: 'RFC', field: 'RFC', css: 'col-xs-1' },
        { text: 'Razón Social', field: 'RazonSocial', css: 'col-xs-1' },
        { text: 'Fecha de presentacion', field: 'FechaPresentacion', css: 'col-xs-1' },
        { text: 'Importe', field: 'Importe', css: 'col-xs-1' },
        { text: 'Cantidad pagada', field: 'CantidadPagada', css: 'col-xs-1' },
        { text: 'Clave DPA', field: 'ClaveReferenciaDPA', css: 'col-xs-1' },
        { text: 'Cadena de la dependencia', field: 'CadenaDependencia', css: 'col-xs-1' },
        { text: 'Importe IVA', field: 'ImporteIVA', css: 'col-xs-1' },
        { text: 'Total Pagado', field: 'TotalEfectivamentePagado', css: 'col-xs-1' }];

    this.controls.grids = $.extend(this.controls.grids, {
        documentos: new controls.Grid('enteroDocumentos', { maxHeight: 400, pager: { show: false } }),
        enteroPreview: new controls.Grid('enteroList', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 } })
    }, true);

    var columns = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' }];

    this.controls.grids.documentos.init(columns);
    this.controls.grids.documentos.loadRows([]);
    self.controls.grids.enteroPreview.init(columnsPreview);
    self.controls.grids.enteroPreview.reload([]);

    var jqXHRData = undefined;
    var getPartial = function (data) {
        if (data.length) {
            this.controls.grids.documentos.loadRows(data);
        }

        'use strict';
        $('#documento').fileupload({
            formData: { Directory: enteroModel.UniqueId },
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
                    if (self.Enteros.Documentos.length > 0) {
                        self.Enteros.Documentos[0] = data.result.documento;
                    }
                    else {
                        data.result.documento.Numero = self.Enteros.Documentos.length + 1;
                        self.Enteros.Documentos.push(data.result.documento);
                    }
                    self.controls.grids.documentos.loadRows([]);
                    self.controls.grids.documentos.loadRows(self.Enteros.Documentos);

                    self.enteros();
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

Ui.prototype.enteros = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    var getAsyncData = function (data) {
        /* Acá cargamos el grid de previsualizacion */
        if (data.Result === 1) {

            self.Enteros.Listado = data.List;

            self.controls.grids.enteroPreview.reload([]);
            self.controls.grids.enteroPreview.reload(self.Enteros.Listado);
        }
        else {
            self.showMessage(data.Message);
        }
        $("#LoadingExcelData").css("display", "none");
        $("#enteroList").css("display", "block");
    };

    var enteroFile = {
        UniqueId: enteroModel.UniqueId,
        Documento: self.Enteros.Documentos[0]
    };

    $("#LoadingExcelData").css("display", "block");
    self.SendAjax('Post', self.urls.previsualizacion, 'Json', enteroFile, getAsyncData);
};

Ui.prototype.fileExtension = function (fileName, extensions) {
    var regex = extensions;
    var str = fileName;
    return regex.exec(str);
};

Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.tesofe.currentPage;
    var rows = self.controls.grids.tesofe.pageSize;
    var loadGrid = function (data) {

        self.controls.grids.tesofe.reload([]);
        if (data.Result) {
            self.controls.grids.tesofe.currentPage = data.Paging.CurrentPage;
            self.controls.grids.tesofe.pages = data.Paging.Pages;
            self.controls.grids.tesofe.reload(data.List);
        }
    };

    var data = {
        page: page, rows: rows,
        Query: {
            IsActive: $("input[name=estatus]:checked").val(),
        },
    };

    this.SendAjax('POST', self.urls.enteros, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();

    $("#fechaInicialPresentacion").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#fechaFinalPresentacion").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#fechaInicialCarga").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#fechaFinalCarga").datepicker({ dateFormat: 'dd/mm/yy' });
    //$("input[name=tipoBusqueda]").on("click",
    //    function (e) {
    //        var value = $(this).val() == 'true';
    //        ui.busquedaTipo(value);
    //    });
};

init();
