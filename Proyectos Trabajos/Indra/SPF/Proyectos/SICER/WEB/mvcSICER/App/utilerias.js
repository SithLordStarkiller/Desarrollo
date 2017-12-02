function session() {
    Ext.Ajax.request({
        method: 'POST',
        url: 'Account/revisarSesion',
        failure: function () {
            window.location = '';
        },
        success: function (response) {
            var retorno = Ext.decode(response.responseText);
            if (retorno == true) {
                //window.location = 'http://intranetspf.ssp.gob.mx';
                window.location = '?xcode=500';
            }
        }
    });
}


function sessionExamen() {
//    Ext.Ajax.request({
//        method: 'POST',
//        url: 'Account/revisarSesionExamen',
//        failure: function () {
//            window.location = '';
//        },
//        success: function (response) {
//            var retorno = Ext.decode(response.responseText);
//            if (retorno == true) {
//                //window.location = 'http://intranetspf.ssp.gob.mx';
//                window.location = '?xcode=500';
//            }
//        }
//    });
}

function esFechaValida(fecha) {
    if (fecha != undefined && fecha != "") {

        if (!/^\d{2}\/\d{2}\/\d{4}$/.test(fecha)) {


            return false;
        }
        var dia = parseInt(fecha.substring(0, 2), 10);
        var mes = parseInt(fecha.substring(3, 5), 10);
        var anio = parseInt(fecha.substring(6), 10);

        if (anio <= 1900) { return false; }

        switch (mes) {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                numDias = 31;
                break;
            case 4: case 6: case 9: case 11:
                numDias = 30;
                break;
            case 2:
                if (comprobarSiBisisesto(anio)) { numDias = 29 } else { numDias = 28 };
                break;
            default:

                return false;
        }
        if (dia > numDias || dia == 0) {

            return false;
        }
        return true;
    }
}
function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}

function quitaacentos(t) {

    á = "a"; é = "e"; í = "i"; ó = "o"; ú = "u"; Á = "A"; É = "E"; Í = "I"; Ó = "O"; Ú = "U";
    acentos = /[áéíóúÁÉÍÓÚ]/g;
    return t.replace(acentos,
    function ($1)
    { return eval($1) }
    );
}

function Guid() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
}

function newGuid() {
    var guid = (Guid() + Guid() + "-" + Guid() + "-" + Guid() + "-" +
Guid() + "-" + Guid() + Guid() + Guid()).toUpperCase();
    return guid;
}