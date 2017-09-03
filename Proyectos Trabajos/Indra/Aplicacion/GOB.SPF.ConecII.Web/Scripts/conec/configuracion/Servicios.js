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
        tiempo: this.getUrl('/Catalogo/TiempoConsulta')
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
        }
    };

    function createPanes(data) {
        alert('si sale')
    }

    this.load();

};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var listRegimenFiscal;
    var succes = function (data) {
        var listServicios = data.List;
        if (listServicios.length > 0) {
            $.each(listServicios, function (index, value) {
                var servicio = {
                    Action: value.Action,
                    Clave: value.Clave,
                    Descripcion: value.Descripcion,
                    Identificador: value.Identificador,
                    IsActive: value.IsActive,
                    Name: value.Name
                };
                self.controls.selects.selectTiposervicio.append(new Option(servicio.Name, servicio.Identificador));
            });
        }
    };
    var succesArea = function (data) {
        var listArea = data.List;
        if (listArea.length > 0) {
            $.each(listArea, function (index, value) {
                var area = {
                    Action: value.Action,
                    Identificador: value.Identificador,
                    IsActive: value.IsActive,
                    Name: value.Name
                }
                self.controls.selects.selectArea.append(new Option(area.Name, area.Identificador));
            });
        }
    };

    var succesRegimenFiscal = function (data) {
        var panelDefault = $(self.controls.paneles.panelRegimen).clone();
        $(self.controls.paneles.panelRegimen).empty();
        listRegimenFiscal = data.List;
        if (listRegimenFiscal.length > 0) {
            $.each(listRegimenFiscal, function (index, value) {
                var panelFinal = panelDefault;
                var regimenFiscal = {
                    Identificador: value.Identificador,
                    Name: value.Name,
                };
                panelFinal.find('a[data-region=title]').attr({
                    'href': '#collapse' + regimenFiscal.Identificador
                }).text(regimenFiscal.Name);

                panelFinal.find('div[data-region=content]').attr({
                    'id': 'collapse' + regimenFiscal.Identificador
                });
                panelFinal.find('div[data-region=tipoPago]').attr({
                    'id': 'tipoPago' + regimenFiscal.Identificador
                });
                panelFinal.find('div[data-region=actividades]').attr({
                    'id': 'actividades' + regimenFiscal.Identificador
                });
                panelFinal.find('div[data-region=tiempo]').attr({
                    'id': 'tiempo' + regimenFiscal.Identificador
                });
                panelFinal.find('div[data-region=documentos]').attr({
                    'id': 'documentos' + regimenFiscal.Identificador
                });
                panelFinal.find('input[data-region=boton]').attr({
                    'data-regimen': regimenFiscal.Identificador
                });
                $(self.controls.paneles.panelRegimen).append(panelFinal.html());
            });
        }
    };

    var succesTipoPago = function (data) {

        $.each(listRegimenFiscal, function (index, value) {
            var regimenFiscal = {
                Identificador: value.Identificador,
                Name: value.Name,
            };
            var conten = $('#tipoPago' + regimenFiscal.Identificador);
            var listTipoPago = data.List;
            if (listTipoPago.length > 0) {
                $.each(listTipoPago, function (index, value) {
                    var tipoPago = {
                        Action: value.Action,
                        Actividad: value.Actividad,
                        Descripcion: value.Descripcion,
                        IdRegimenFiscal: value.IdRegimenFiscal,
                        Identificador: value.Identificador,
                        IsActive: value.IsActive,
                        Name: value.Name
                    };
                    var check = '<div class="checkbox"><label><input type="checkbox" data-tipoPago="' + tipoPago.Identificador + '">' + tipoPago.Name + '</label></div>';
                    conten.append(check);

                });
            }
        });
    };

    var succesActividades = function (data) {

        $.each(listRegimenFiscal, function (index, value) {
            var regimenFiscal = {
                Identificador: value.Identificador,
                Name: value.Name,
            };
            var contenActividad = $('#actividades' + regimenFiscal.Identificador);
            var contenActividadConfig = $('#tiempo' + regimenFiscal.Identificador);
            var listTipoPago = data.List;
            if (listTipoPago.length > 0) {
                $.each(listTipoPago, function (index, value) {
                    var actividad = {
                        Action: value.Action,
                        Descripcion: value.Descripcion,
                        IdFase: value.IdFase,
                        IdTipoPago: value.IdTipoPago,
                        Identificador: value.Identificador,
                        IsActive: value.IsActive,
                        Name: value.Name,
                        SePuedeAplicarPlazo: value.SePuedeAplicarPlazo
                    };
                    if (!actividad.SePuedeAplicarPlazo) {
                        var check = '<div class="checkbox col-sm-3"><label>' +
                                '<input type="checkbox" data-actividad="' + actividad.Identificador + '">' + actividad.Name + '</label></div>';
                        contenActividad.append(check);
                    } else {
                        var checkConfig = '<div class="checkbox"><label>' +
                                    '<input type="checkbox" data-regimen="' + regimenFiscal.Identificador + '" data-actividad="' + actividad.Identificador + '" name="' + actividad.Name + '" onchange="Ui.prototype.checkActividad(this)" >' + actividad.Name + '</label>' +
                                    '<input type="number" value="0" min="0" data-regimen="' + regimenFiscal.Identificador + '" data-actividad="' + actividad.Identificador + '" onchange="SetTiempo(this)" style="width:50px;height:25px;margin-left:20px;" /></div>';
                        contenActividadConfig.append(checkConfig);
                    }
                });
            }
        });
    };

    var data = { page: 1, rows: 5 };

    this.SendAjax('POST', self.urls.tiposServicio, 'json', data, succes);
    this.SendAjax('POST', self.urls.areas, 'json', data, succesArea);
    this.SendAjax('POST', self.urls.regimenFiscal, 'json', data, succesRegimenFiscal);
    this.SendAjax('POST', self.urls.tipoPago, 'json', data, succesTipoPago);
    this.SendAjax('POST', self.urls.actividades, 'json', data, succesActividades);


};

Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function
    });
};

Ui.prototype.checkActividad = function (object) {
    var self = this;
    var check = $(object);
    var regimen = check.data('regimen');
    var actividad = check.data('actividad');
    this.urls = {
        tiposDoumento: this.getUrl('/Catalogo/TipoDocumentoConsultaCriterio')
    };
    var sesultTipoDocumento = function (data) {
        var listDocumento = data.List;
        var documentos = $('#documentos' + check.attr('data-regimen'))
        if (listDocumento.length > 0) {
            documentos.append('<div id="' + regimen + actividad + '" ></div>');
            var contentDoc = $('#' + regimen + actividad);
            contentDoc.append('<label>' + check.attr('name') + ':</label><br /><span style="font-size: 10px;padding-left: 14px;">Aplica/Obligatorio</span>');
            $.each(listDocumento, function (index, value) {
                var documento = {
                    Action: value.Action,
                    Confidencial: value.Confidencial,
                    Descripcion: value.Descripcion,
                    IdActividad: value.IdActividad,
                    Identificador: value.Identificador,
                    IsActive: value.IsActive,
                    Name: value.Name
                };
                var checks = '<div class="checkbox" data-regimen="' + regimen + '" data-actividad="' + actividad + '" data-documento="' + documento.Identificador + '" data-activo="false" data-requerido="false" data-tiempo="0" >' +
                           '<label style="padding-right:15px;"><input type="checkbox" name="documento" onchange="ActivaRequerido(this)"></label>' +
                           '<label><input type="checkbox" name="requerido" disabled="disabled" onchange="Requerido(this)"><span style="padding-left:15px;">' + documento.Name + '</span></label>' +
                           '</div';
                contentDoc.append(checks);
            });
        }
    };

    var data = {
        ObjectResult: {
            IdActividad: check.val(),
        }, Paging: {
            All: true
        }
    };
    if (check.is(':checked')) {
        this.SendAjax('POST', self.urls.tiposDoumento, 'json', data, sesultTipoDocumento);
    } else {
        $('#' + regimen + actividad).remove();
    }

}

Ui.prototype.guardar = function (object) {
    var self = this;
    this.urls = {
        guardarConfiguracion: this.getUrl('/Configuracion/ConfiguracionServicioGuardar')
    };
    var tipoServicio = $('#selTipoServicio option:selected').val();
    var area = $('#selArea option:selected').val();
    if (isNullOrEmpty(area) || isNullOrEmpty(tipoServicio)) {
        alert("Debes seleccionar un tipo de servicio/area");
        return;
    }
        
    var buttom = $(object);
    var listConfiguracion = new Array();
    var configuracion = {};
    var regimen = buttom.data('regimen');

    var tipoPago = $('#tipoPago' + regimen).find('input:checked');
    var actividades = $('#actividades' + regimen).find('input:checked');
    var documentosActivos = $('#tiempo' + regimen).find('input:checked').length;
    var documentos = $('#documentos' + regimen).find('div[class=checkbox]');

    if(tipoPago.length==0){
        alert("Debes seleccionar un tipo de pago");
        return;
    }

    if (documentosActivos == 0 && actividades.length == 0) {
        alert("Debes seleccionar alguna actividad");
        return;
    }


    $.each(tipoPago, function (index, valueTipoPago) {
        $.each(actividades, function (index, valueActividad) {
            var tipoPago = $(valueTipoPago).data('tipopago');
            var actividad = $(valueActividad).data('actividad');
            configuracion = {
                ["IdTipoServicio"]: tipoServicio,
                ["IdCentroCostos"]: area,
                ["IdRegimenFiscal"]: regimen,
                ["IdTipoPago"]: tipoPago,
                ["IdActividad"]: actividad,
                ["IdTipoDocumento"]: 0,
                ["Tiempo"]: 0,
                ["Aplica"]: false,
                ["Obigatoriedad"]: false
            };
            listConfiguracion.push(configuracion);
        });
    });

    
    $.each(tipoPago, function (index, valueTipoPago) {
        var tipoPago = $(valueTipoPago).data('tipopago');
        $.each(documentos, function (index, value) {
            var documento = $(value);
            configuracion = {
                ["IdTipoServicio"]: tipoServicio,
                ["IdCentroCostos"]: area,
                ["IdRegimenFiscal"]: regimen,
                ["IdTipoPago"]: tipoPago,
                ["IdActividad"]: documento.data('actividad'),
                ["IdTipoDocumento"]: documento.data('documento'),
                ["Tiempo"]: documento.data('tiempo'),
                ["Aplica"]: documento.data('activo'),
                ["Obigatoriedad"]: documento.data('requerido')
            };
            listConfiguracion.push(configuracion)
        });
    });


    //alert(documentos.length);
    var succesInsertaConfiguracion = function (data) {
        alert('Configuracion guardada!!!');
    };


    var data = {
        List: listConfiguracion
        , Paging: {
            All: true
        }
    };

    this.SendAjax('POST', self.urls.guardarConfiguracion, 'json', data, succesInsertaConfiguracion);

}

function ActivaRequerido(object) {
    var check = $(object);
    var regimen = check.data("regimen");
    var checkReq = check.parent().parent().find('input[type=checkbox][name=requerido]');
    if (check.is(':checked')) {
        checkReq.removeAttr("disabled");
        check.parent().parent().attr('data-activo', true);
        var tiempo = $('#tiempo' + regimen);
    } else {
        checkReq.attr("disabled", true);
        checkReq.prop('checked', false);
        check.parent().parent().attr('data-activo', false);
        check.parent().parent().attr('data-requerido', false);
    }
}

function Requerido(object) {
    var check = $(object);
    var checkReq = check.parent().parent().find('input[type=checkbox][name=requerido]');
    if (check.is(':checked')) {
        checkReq.parent().parent().attr('data-requerido', true);
    } else {
        checkReq.parent().parent().attr('data-requerido', false);
    }
}

function SetTiempo(object) {
    var text = $(object);
    var regimen = text.data('regimen');
    var actividad = text.data('actividad');
    var dataTiempo = $('#' + regimen + actividad).find('div[class=checkbox]').attr('data-tiempo', text.val());
}

function init() {
    var ui = new Ui();
    ui.init();
};

function isNullOrEmpty(s) 
{
    return (s == null || s === "" );
}

init();