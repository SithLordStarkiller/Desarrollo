function obtiene_fecha() {
    var fecha_actual = new Date()
    var dia = fecha_actual.getDate()
    var mes = fecha_actual.getMonth() + 1
    var anio = fecha_actual.getFullYear()

    if (mes < 10)
        mes = '0' + mes

    if (dia < 10)
        dia = '0' + dia
    return (dia + "/" + mes + "/" + anio)
}

function MostrarFecha() {
    document.formReg.Fecha_actu.value = obtiene_fecha();
}  
