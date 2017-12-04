Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    var foto = new ArrayBuffer();
    this.urls = {
        guardar: this.getUrl('/Solicitud/GuardarSolicitud'),
        cuotasConsulta: this.getUrl('/Catalogo/CuotasConsulta'),
        entidadFederativa: this.getUrl('/Catalogo/ObtenerEstado')
    };
    this.controls = {
        buttons: {
            file: document.getElementById('foto'),
            enviar: document.getElementById('btnEnviar')

        },
        imagenes: {
            imgFoto: document.getElementById('imgFoto')
        },
        combobox: {
            competencia: document.getElementById('estandarCompetencia'),
            entidadFederativa: document.getElementById('entidadFederativa'),
            municipioDelegacion: document.getElementById('municipioDelegacion'),
        }
    };
    this.variable = {
        foto: new ArrayBuffer()
    };

    this.loadControls();

    this.load();

};

Ui.prototype.load = function (evt) {
    var self = this;
    $(self.controls.imagenes.imgFoto).attr("src", self.getUrl('/Content/Conec/images/fotoSolicitud.jpg'));
    $(self.controls.buttons.file).change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.readAsDataURL(this.files[0]);
            reader.onload = function (e) {
                self.variable.foto = e.target.result.replace('data:image/jpeg;base64,', '');
                $('#imgFoto').attr("src", e.target.result);
            };

        }
    });

    var succesCuotas = function (data) {
        var listCuostas = data.List;
        if (listCuostas.length > 0) {
            $.each(listCuostas, function (index, value) {
                var cuota = {
                    Concepto: value.Concepto,
                    CuotaBase: value.CuotaBase
                };
                self.controls.combobox.competencia.append(new Option(cuota.Concepto, cuota.CuotaBase));
            });
        }

    };

    var succesEntidadFederativa = function (data) {
        var lisrEntidad = data.List;
        if (lisrEntidad.length > 0) {
            $.each(lisrEntidad, function (index, value) {
                var estado = {
                    Identificador: value.Identificador,
                    Nombre: value.Nombre
                };
                self.controls.combobox.entidadFederativa.append(new Option(estado.Nombre, estado.Identificador));
            });
        }
    };

    var data = { page: 1, rows: 5 };
    this.SendAjax('POST', self.urls.cuotasConsulta, 'json', data, succesCuotas);
    this.SendAjax('POST', self.urls.entidadFederativa, 'json', data, succesEntidadFederativa);



};

Ui.prototype.municipioDelegacion = function () {
    var idMunicipio = $('#entidadFederativa').val();
    var selMunicipio = $('#municipioDelegacion');

    var succesMunicipios = function (data) {
        var listMunicipios = data.List;
        if (listMunicipios.length > 0) {
            selMunicipio.empty();
            $.each(listMunicipios, function (index, value) {
                var minicipio = {
                    Nombre: value.Nombre,
                    Identificador: value.Identificador
                };

                selMunicipio.append(new Option(minicipio.Nombre, minicipio.Identificador));
            });
        }
    };

    this.SendAjax('GET', this.getUrl('/Catalogo/MunicipiosObtener') + '/' + idMunicipio, 'json', null, succesMunicipios);

};


Ui.prototype.loadControls = function () {
    var self = this;

    Ui.prototype.DatepikerNormal();

    $(self.controls.buttons.enviar).click(function () {

        var solicitud = {
            foto: self.variable.foto,
            estandarCompetencia: self.controls.combobox.competencia.attr('selected', 'selected').val(),
            fechaRegistro: $('#fechaRegistro').val(),
            apPaterno: $('apPaterno').val(),
            apMaterno: $('apMaterno').val(),
            nombre: $('nombre').val(),
            lugarNacimiento: $('lugarNacimiento').val(),
            fechaNacimiento: $('fechaNacimiento').val(),
            fechaRegistro: $('fechaRegistro').val(),
            curp: $('curp').val(),
            gradoEscolar: $('gradoEscolar').val(),
            entidadFederativa: self.controls.combobox.entidadFederativa.attr('selected', 'selected').val(),
            municipioDelegacion: self.controls.combobox.municipioDelegacion.attr('selected', 'selected').val(),
            colonia: $('colonia').val(),
            calle: $('calle').val(),
            numeroExterior: $('numeroExterior').val(),
            numeroInterior: $('numeroInterior').val(),
            codigoPostal: $('codigoPostal').val(),
            anoServicio: $('anoServicio').val(),
            cuip: $('cuip').val(),
            edad: $('edad').val(),
            sexo: $('sexo').val(),
            rfc: $('rfc').val(),
            grado: $('grado').val(),
            cargo: $('cargo').val(),
            telefonoCasa: $('telefonoCasa').val(),
            telefonoCelular: $('telefonoCelular').val(),
            telefonoLaboral: $('telefonoLaboral').val(),
            emailPersonal: $('emailPersonal').val(),
            emailLaboral: $('emailLaboral').val()
        };

        var succesEnviar = function (data) {
            alert('Datos enviados');
        };

        var data = {
            ObjectResult: solicitud,
            Paging: {
                All: true
            }
        };

        Ui.prototype.SendAjax('POST', self.urls.guardar, 'json', data, succesEnviar);

    });


}


Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: method,
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function
    });
};

Ui.prototype.DatepikerNormal = function () {

    $('.datepickerFechaActua').datepicker({
        dateFormat: 'yy/mm/dd'
    });
}


function init() {
    var ui = new Ui();
    ui.init();
};

function isNullOrEmpty(s) {
    return (s == null || s === "");
}

init();