function PagingBusqueda(currentPage, pageSize) {
    var PagingData;
    if (isNaN($("#IdParteDocumentoBusqueda").val())) {
        PagingData = {
            CurrentPage: currentPage,//self.controls.grids.instituciones.currentPage,
            Rows: pageSize,//self.controls.grids.instituciones.pageSize,
            All: false
        };
    }
    else {
        PagingData = {
            CurrentPage: 0,
            Rows: 0,
            All: true
        };
    }
    return PagingData;
}