var ReferenciaMyo = function (tipo, nombre, valor, comentario, urlDoc,idSelec) {
    this.Nombre = nombre;
    this.Tipo = tipo;
    this.Url = urlDoc;
    this.Valor = valor;
    this.Comentario = comentario;
    this.IdDocumento = idSelec;

    this.verifica = function() {
        if (this.Valor == "No" && this.Comentario == "") {
            alert("Si la referencia no es valida, debe proporcionar un comentario.");
            return false;
        }
        
        return true;
        
    };
}

var DocumentoMyo = function (tipo, nombre, valor, comentario, urlDoc,idSelec) {
    this.Nombre = nombre;
    this.Tipo = tipo;
    this.Url = urlDoc;
    this.Valor = valor;
    this.Comentario = comentario;
    this.IdDocumento = idSelec;

    this.verifica = function () {
        if (this.Valor == "No" && this.Comentario == "") {
            alert("Si el documento no es valido, debe proporcionar un comentario.");
            return false;
        }
        //if (this.Tipo == 2 && this.Url == "") {
        //    alert("Debe subir todos los documentos solicitados.");
        //    return false;
        //}

        return true;
    };
}


var RevisionMyo = function (tipo, nombre, valor, comentario, urlDoc,idSelec) {
    this.Nombre = nombre;
    this.Tipo = tipo;
    this.Url = urlDoc;
    this.Valor = valor;
    this.Comentario = comentario;
    this.IdDocumento = idSelec;

    this.verifica = function () {
        if (this.Valor == "No" && this.Comentario == "") {
            alert("Si el documento no es valido, debe proporcionar un comentario.");
            return false;
        }

        return true;
    };
}

