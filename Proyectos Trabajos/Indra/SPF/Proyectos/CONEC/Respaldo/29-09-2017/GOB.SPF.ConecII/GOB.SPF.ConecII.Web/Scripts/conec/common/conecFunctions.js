Ui.prototype.loadEventDomicilio = function (parameters) {
    var self = this;
    this.defaultData = {
        codigoPostal: {
            url: '',
            field: '',
            text: '',
            value: '',
            control: ''
        },
        estado: {
            url: '',
            field: '',
            text: '',
            value: '',
            control: '',
            destino: ''
        },
        municipio: {
            url: '',
            field: '',
            text: '',
            value: '',
            control: '',
            destino: ''
        },
        asentamiento: {
            url: '',
            field: '',
            text: '',
            value: '',
            control: '',
            destino: ''
        }
    };

    var parameter = {
        IdEstado: 0,
        IdMunicipio: 0,
        IdAsentamiento: 0,
        codigoPostal: ''
    };
    this.parameters = $.extend(this.defaultData, parameters, true);

    $(this.parameters.codigoPostal.control).on('keydown', function (e) {
        var key = e.charCode || e.keyCode || 0;
       
        var selectItem = function (data) {
            var data = data;

            var sdata = data;

        };
        if (key == 13) {
            e.preventDefault();
            var values = $.extend(parameter, { codigoPostal: $(this).val() }, true);
            $.when(self.ExecuteFillCombo(self.parameters.codigoPostal.urlDestino, values, self.parameters.codigoPostal.destino));
        }

        return
        key == 8 ||
        key == 9 ||
        key == 46 ||
        key == 110 ||
        key == 190 ||
        (key >= 35 && key <= 40) ||
        (key >= 48 && key <= 57) ||
        (key >= 96 && key <= 105);
    });
};

Ui.prototype.loadMunicipios = function (estado, destino, url, municipio) {
    var self = this;
    var fillDropDownList = function (data) {
        switch (data.Result) {
            case 1:
                if (data.List.length > 0) {
                    self.loadCombo(data.List, destino);
                    if (municipio !== undefined)
                        $(destino).find(" option[value=" + municipio.Identificador + "]").attr("selected", true);
                }
                break;
            case 0:
                break;
        }
    };
    if (estado !== "" && estado !== undefined)
        $.get(url + estado, fillDropDownList);
    else {
        destino.selectedIndex = 0;
    }
};

Ui.prototype.loadAsentamientos = function (estado,municipio, destino, url) {
    var self = this;
    var fillDropDownList = function (data) {
        switch (data.Result) {
            case 1:
                if($(data.List).length) {
                //if (data.List.length > 0) {
                    self.loadCombo(data.List, destino);
                }
                break;
            case 0:
                break;
        }
    };
    var data = {
        IdEstado: estado,
        IdMunicipio: municipio,
        CodigoPostal:''
    };

    if ((estado !== "" && municipio !== "") && (estado !== undefined && municipio !== undefined)){
        self.SendAjax("POST", url, 'Json', data, fillDropDownList);
    }        
    else {
        destino.selectedIndex = 0;
    }
    
};

Ui.prototype.ExecuteFillCombo = function (url, data,destino, $completeFunc) {
    var self = this;

    if (Array.isArray(destino)) {
        destino.forEach(function(item){
            var fillDropDownList = function (data) {
                switch (data.Result) {
                    case 1:
                        self.loadCombo(data.List, destino);
                        break;
                    case 0:
                        self.showMessage(self.mensajes.codigoPostalNotFound, function () { });
                        break;
                }
            };
            self.SendAjax("POST", url, 'Json', data, fillDropDownList);
        }); 
    } else {
        var fillDropDownList = function (data) {            
            switch (data.Result) {
                case 1:
                    var estado = self.parameters.estado.control;
                    if (data.List.length > 0) {
                        $(estado).find(" option[value=" + data.List[0].Estado.Identificador + "]").attr("selected", true);
                        self.loadCombo(data.List, destino);
                        self.loadMunicipios(data.List[0].Estado.Identificador, self.parameters.estado.destino, self.parameters.estado.urlDestino, data.List[0].Municipio);
                    }
                    else {
                        self.showMessage(self.mensajes.codigoPostalNotFound, function () { });
                    }
                    break;
                case 0:
                    self.showMessage(self.mensajes.codigoPostalNotFound, function () { });
                    break;
            }
            if ($completeFunc) $completeFunc();
        };
        self.SendAjax("POST", url, 'Json', data, fillDropDownList);
        
    }
};

Ui.prototype.findCodigoPostal = function (codigoPostal) {

};