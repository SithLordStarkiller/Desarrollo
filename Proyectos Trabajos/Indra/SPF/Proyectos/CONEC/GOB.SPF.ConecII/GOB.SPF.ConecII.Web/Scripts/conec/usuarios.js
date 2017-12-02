Ui.prototype.init = function () {
    var controls = gob.Controls;
    var treeview = new controls.Treeview("treeView", {field:"id",text:"nombre",node:"child"});
    var array = [{ id: 1, nombre: "AAAA", child: [{ id: 1, nombre: "AAAA", child: [] }, { id: 1, nombre: "BBBB", child: [{ id: 6, nombre: "PPPP" }, { id: 5, nombre: "OOOO" }] }, { id: 1, nombre: "CCCC", child: [{ id: 7, nombre: "IIIII" }, { id: 8, nombre: "JJJJJ" }] }, { id: 1, nombre: "DDDD", child: [] }] }, { id: 1, nombre: "BBBB", child: [{ id: 6, nombre: "PPPP" }, { id: 5, nombre: "OOOO" }] }, { id: 1, nombre: "CCCC", child: [{ id: 7, nombre: "IIIII" }, { id: 8, nombre: "JJJJJ" }] }, { id: 1, nombre: "DDDD", child: [] }]
    treeview.init(array);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();