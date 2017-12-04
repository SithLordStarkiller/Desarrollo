$("#marcarTodo").change(function () {
    if ($(this).is(':checked')) {
        //$("input[type=checkbox]").prop('checked', true); //todos los check
        $("#diasHabilitados input[type=checkbox]").prop('checked', true); //solo los del objeto #diasHabilitados
    } else {
        //$("input[type=checkbox]").prop('checked', false);//todos los check
        $("#diasHabilitados input[type=checkbox]").prop('checked', false);//solo los del objeto #diasHabilitados
    }
});