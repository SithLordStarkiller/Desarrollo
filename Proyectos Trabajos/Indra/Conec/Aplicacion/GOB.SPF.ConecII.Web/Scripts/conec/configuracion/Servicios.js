Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        tiposServicio: this.getUrl('/Catalogo/TiposServicioConsulta'),
        areas: this.getUrl('/Catalogo/AreasConsulta'),
        regimenFiscal: this.getUrl('/Catalogo/RegimenFiscalConsulta'),
        tipoPago: this.getUrl('/Catalogo/TipoPagoConsulta'),
        actividades: this.getUrl('/Catalogo/ActividadesConsulta'),
        tiempo: this.getUrl('/Catalogo/TiempoConsulta'),
        obtieneConfiguraciones: this.getUrl('/Configuracion/ConfiguracionDetalle')
    };
    this.controls = {
        buttons: {
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        },
        selects: {
            selectTiposervicio: document.getElementById("selTipoServicio"),
            selectArea: document.getElementById("selArea")
        },
        paneles: {
            panelRegimen: document.getElementById('pnlRegimen')
        },
        grids: {
            serviciosConfigurados: new controls.Grid('divServicio', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
        }
    };

    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
    { text: 'Régimen Fiscal', field: 'RegimenFiscal', css: 'col-xs-3' },
    { text: 'Servicio', field: 'TipoServicio', css: 'col-xs-3' },
    { text: 'Tipo de pago', field: 'TipoPago', css: 'col-xs-3' },
    { text: 'Fecha de registro', field: 'FechaRegistro', css: 'col-xs-3' },
    { text: 'Estatus', field: 'Estatus', css: 'col-xs-1' },
    { text: '', field: '', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
    { text: '', field: '', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    this.controls.grids.serviciosConfigurados.init(columns);
    this.controls.grids.serviciosConfigurados.addListener("onPage", function (evt) {
        self.controls.grids.serviciosConfigurados.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function () {
            $(".flip div.front").slideToggle()
            $(".flip div.back").fadeIn();
        },
        hideForm: function () {
            $(".flip div.back").hide();
            $(".flip div.front").fadeIn();
        }
    };

    this.controls.grids.serviciosConfigurados.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        self.functions.hideGrid();
        $('#btnBack').on('click', function () {
            self.functions.hideForm(data);
        });

        switch (evt.event) {
            case 'Edit':
                Ui.prototype.LoadForm(evt.dataRow.IdTipoServicio, evt.dataRow.IdCentroCosto);

                $('#selTipoServicio').val(evt.dataRow.IdTipoServicio).trigger('change');
                $('#selArea').val(evt.dataRow.IdCentroCosto).trigger('change');

                $('#formConfiguracion input').attr('disabled', false).show();
                $('#formControlConfiguracion input').attr('disabled', false).show();

                break;
            case 'View':
                Ui.prototype.LoadForm(evt.dataRow.IdTipoServicio, evt.dataRow.IdCentroCosto);

                $('#selTipoServicio').val(evt.dataRow.IdTipoServicio).trigger('change');
                $('#selArea').val(evt.dataRow.IdCentroCosto).trigger('change');

                $('#formConfiguracion input').attr('disabled', true);
                $('#formControlConfiguracion input').attr('disabled', true);
                break;
            case 'New':

                $('#selTipoServicio').val('').trigger('change');
                $('#selArea').val('').trigger('change');

                $('#formConfiguracion input').attr('disabled', false).show();
                $('#formControlConfiguracion input').attr('disabled', false).show();
                $('#formControlConfiguracion').find('iput[type=number]').val(0);
                $('#formConfiguracion input[type=checkbox]').attr('checked', false)
                break;
            case 'Active':
            case 'Inactive':

                break;
        }
    });

    this.load();
};

Ui.prototype.load = function (evt) {
    var self = this;
    $('#selTipoServicio').change(function () {
        $('#selTipoServicioToltip').addClass('errorInvisible');
    });
    $('#selArea').change(function () {
        $('#selAreaTooltip').addClass('errorInvisible');
    });
    var page = self.controls.grids.serviciosConfigurados.currentPage;
    var rows = self.controls.grids.serviciosConfigurados.pageSize;
    var succesObtieneConfiguraciones = function (data) {
        self.controls.grids.serviciosConfigurados.loadRows([]);
        if (data.Result) {
            self.controls.grids.serviciosConfigurados.currentPage = data.Paging.CurrentPage;
            self.controls.grids.serviciosConfigurados.pages = data.Paging.Pages;
            self.controls.grids.serviciosConfigurados.loadRows(data.List);
            self.functions.hideForm();
        }
    }
    var data = { page: page, rows: rows };
    this.SendAjax('POST', self.urls.obtieneConfiguraciones, 'json', data, succesObtieneConfiguraciones);

    $('#btnBuscar').click(function () {
        var tipoServicio = $('#selBuscaTipoServicio').val();
        var data = {
            Pagin: {
                page: page,
                rows: rows
            }, ObjectSearch: {
                IdTipoServicio: tipoServicio
            }
        };


        Ui.prototype.SendAjax('POST', self.urls.obtieneConfiguraciones, 'json', data, succesObtieneConfiguraciones);
    });

    $('#selArea').select2();
    $('#selBuscaTipoServicio').select2();
    $('#selTipoServicio').select2();
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

Ui.prototype.checkActividad = function (object) {
    var self = this;
    var check = $(object);
    this.urls = {
        tiposDoumento: '/Configuracion/DocumentoConfigurado'
    };
    var tiempos = $('#tiempo' + check.attr('data-regimen'));
    var documentos = $('#documentos' + check.attr('data-regimen'));

    var actConfig = tiempos.find('input:checked');

    var tipoServicio = $('#selTipoServicio option:selected').val();
    var area = $('#selArea option:selected').val();

    $('span[name=actPlazo]').addClass('errorInvisible');
    $('div[name=actBorderAact]').removeClass('errorInvisibleBorder');
    $('div[name=actBorPlaxo]').removeClass('errorInvisibleBorder');
    

    var listConfiguracion = new Array();
    $.each(actConfig, function (index, value) {
        var name = $(value).attr('name');
        var actividad = $(value).data('actividad');
        var regimen = $(value).data('regimen');
        var tiempo = tiempos.find('input[type=number][data-regimen=' + regimen + '][data-actividad=' + actividad + ']').val()
        var configuracion = {
            Nombre: name,
            IdActividad: actividad,
            IdRegimen: regimen,
            Tiempo: tiempo,
            Activo: false,
            Requerido: false,
            IdArea: area,
            IdServicio: tipoServicio
        };
        listConfiguracion.push(configuracion);
    });

    documentos.load(self.urls.tiposDoumento, { listConfiguracion: listConfiguracion });
}

Ui.prototype.disable = function (object) {
    var check = $(object);
    var tipoPago = check.data('tipopago');
    var regimen = check.data('regimen');
    var contenActividades = $('#actividades' + regimen);
    var contentTipoPago = $('#tipoPago' + regimen);
    $('#tipopagoTooltip' + regimen).addClass('errorInvisible');
    $('div[name=actBorTipPag'+regimen+']').removeClass('errorInvisibleBorder');
    if (tipoPago == 2) {
        contenActividades.find('input[type=checkbox]').attr('disabled', true);
        contenActividades.find('input[type=checkbox]').attr('checked', false);
        if (!check.is(':checked')) {
            contenActividades.find('input[type=checkbox]').attr('disabled', false);
        }
    } else {
        contenActividades.find('input[type=checkbox]').attr('disabled', false);
    }
}

Ui.prototype.guardar = function (object) {
    var self = this;
    this.urls = {
        guardarConfiguracion: this.getUrl('/Configuracion/ConfiguracionServicioGuardar')
    };
    var tipoServicio = $('#selTipoServicio option:selected').val();
    var area = $('#selArea option:selected').val();

    if (isNullOrEmpty(tipoServicio)) {
        $('#selTipoServicioToltip').removeClass('errorInvisible');
    }
    if (isNullOrEmpty(area)) {
        $('#selAreaTooltip').removeClass('errorInvisible');
    }
    var listConfiguracion = new Array();
    var listRegimen = $(object).data('regimen');

    if (!$('input[type=checkbox][name=actRegFis]').is(':checked')) {
        $('span[name=actRegFisT]').removeClass('errorInvisible');
    }

    $.each(listRegimen, function (index, regimen) {
        var configuracion = {};
        var listTipoPago = $('#tipoPago' + regimen).find('input:checked');
        var listActividades = $('#actividades' + regimen).find('input:checked');
        var listDOcumentos = $('#documentos' + regimen).find('div[data-regimen=' + regimen + ']');

        

        if ($('#regact' + regimen).is(':checked')) {

            if (listTipoPago.length == 0) {
                $('#tipopagoTooltip' + regimen).removeClass('errorInvisible');
                $('div[name=actBorTipPag' + regimen + ']').addClass('errorInvisibleBorder');

            }

            if (listActividades.length == 0) {
                $('span[name=actPlazo]').removeClass('errorInvisible');
                $('div[name=actBorderAact]').addClass('errorInvisibleBorder');
                $('div[name=actBorPlaxo]').addClass('errorInvisibleBorder');
            }

            $.each(listTipoPago, function (indexPago, pago) {
                $.each(listActividades, function (indexAct, actValue) {
                    var tipoPago = $(pago).data('tipopago');
                    configuracion = {
                        IdTipoServicio: tipoServicio,
                        IdCentroCostos: area,
                        IdRegimenFiscal: regimen,
                        IdTipoPago: tipoPago,
                        IdActividad: $(actValue).data('actividad'),
                        IdTipoDocumento: 0,
                        Tiempo: 0,
                        Aplica: false,
                        Obigatoriedad: false
                    };
                    if (tipoPago != 2)
                        listConfiguracion.push(configuracion);
                });
            });

            $.each(listTipoPago, function (index, pago) {
                $.each(listDOcumentos.children(), function (documento, actValue) {
                    var tipoPago = $(pago).data('tipopago');
                    var actividad = $(actValue).parent('div[data-activo=true]').data('actividad');
                    var activo = $(actValue).parent('div[data-activo=true]').data('activo');
                    var documento = $(actValue).parent('div[data-activo=true]').data('documento');
                    var requerido = $(actValue).parent('div[data-activo=true]').data('requerido');
                    var tiempo = $(actValue).parent('div[data-activo=true]').data('tiempo');
                    configuracion = {
                        IdTipoServicio: tipoServicio,
                        IdCentroCostos: area,
                        IdRegimenFiscal: regimen,
                        IdTipoPago: tipoPago,
                        IdActividad: actividad,
                        IdTipoDocumento: documento,
                        Tiempo: tiempo,
                        Aplica: activo,
                        Obigatoriedad: requerido
                    };
                    if (activo)
                        listConfiguracion.push(configuracion);
                });
            });
        }
    });

    var succesInsertaConfiguracion = function (data) {
        alert('Configuracion guardada!!!');
        location.reload();
    };


    var data = {
        List: listConfiguracion
        , Paging: {
            All: true
        }
    };

    if (listConfiguracion.length > 0) {
        this.SendAjax('POST', self.urls.guardarConfiguracion, 'json', data, succesInsertaConfiguracion);
    }
}

Ui.prototype.LoadForm = function (IdTipoServicio, IdCentroCosto) {
    var self = this;
    this.url = {
        consultaConfiguracion: this.getUrl('/Configuracion/ConsultaConfiguracionServicioPorIds')
    };
    var configuracion = {};
    var listonfiguracion = new Array();
    var tipoServicio = IdTipoServicio;
    var area = IdCentroCosto;

    $('#selTipoServicio').val(tipoServicio);

    var succesConsultaConfiguracion = function (data) {
        var configuracion = {};
        listonfiguracion = data.List;
        if (listonfiguracion.length > 0) {
            $.each(listonfiguracion, function (index, value) {
                configuracion = {
                    Aplica: value.Aplica,
                    IdActividad: value.IdActividad,
                    IdCentroCostos: value.IdCentroCostos,
                    IdRegimenFiscal: value.IdRegimenFiscal,
                    IdTipoDocumento: value.IdTipoDocumento,
                    IdTipoPago: value.IdTipoPago,
                    IdTipoServicio: value.IdTipoServicio,
                    Obigatoriedad: value.Obigatoriedad,
                    Tiempo: value.Tiempo
                };

                $('#tipoPago' + configuracion.IdRegimenFiscal).find('input[type=checkbox][data-tipopago=' + configuracion.IdTipoPago + ']').attr('checked', true);

                $('#actividades' + configuracion.IdRegimenFiscal).find('input[type=checkbox][data-actividad=' + configuracion.IdActividad + ']').attr('checked', true);

                $('#tiempo' + configuracion.IdRegimenFiscal).find('input[type=checkbox][data-actividad=' + configuracion.IdActividad + ']').attr('checked', true);

                $('#tiempo' + configuracion.IdRegimenFiscal).find('input[type=number][data-actividad=' + configuracion.IdActividad + ']').val(configuracion.Tiempo);


                $('#regact' + configuracion.IdRegimenFiscal).attr('checked', true);

                Ui.prototype.checkActividad($('#tiempo' + configuracion.IdRegimenFiscal).find('input[type=checkbox][data-actividad=' + configuracion.IdActividad + ']'));
            });
        }
    }



    configuracion = {
        IdTipoServicio: tipoServicio,
        IdCentroCostos: area
    };

    var data = {
        ObjectResult: configuracion
        , Paging: {
            All: true
        }
    };
    this.SendAjax('POST', self.url.consultaConfiguracion, 'json', data, succesConsultaConfiguracion);
}

function removeTooltip(object) {
    if ($(object).is('input')) {
        $('span[name=actPlazo]').addClass('errorInvisible');
        $('div[name=actBorderAact]').removeClass('errorInvisibleBorder');
        $('div[name=actBorPlaxo]').removeClass('errorInvisibleBorder');
        $('span[name=actRegFisT]').addClass('errorInvisible');
    } else {
        $('span[name=actRegFis]').addClass('errorInvisible');
        $(object).parent().find('a').click();
    }
}

function ActivaRequerido(object) {
    var check = $(object);
    var idConten = check.data("parent");
    var conten = $('#' + idConten);
    check.attr('checked', check.is(':checked'));
    conten.attr('data-activo', check.is(':checked'));
    if (check.is(':checked')) {
        $(conten.find('input[type=checkbox][name=requerido]')).removeAttr("disabled");
    } else {
        conten.find('input[type=checkbox][name=requerido]').removeAttr('checked');
        conten.find('input[type=checkbox][name=requerido]').attr('disabled', true);
        conten.attr('data-requerido', false);
        conten.attr('data-tiempo', 0);
    }
}

function Requerido(object) {
    var check = $(object);
    var idConten = check.data("parent");
    var conten = $('#' + idConten);
    check.attr('checked', check.is(':checked'));
    conten.attr('data-requerido', check.is(':checked'));
}

function SetTiempo(object) {
    var text = $(object);
    var regimen = text.data('regimen');
    var actividad = text.data('actividad');
    var tiempo = text.val();
    $('#documentos' + regimen).find('div[data-regimen=' + regimen + '][data-actividad=' + actividad + ']').attr('data-tiempo', tiempo);

}

function init() {
    var ui = new Ui();
    ui.init();
};

function isNullOrEmpty(s) {
    return (s == null || s === "");
}

init();
