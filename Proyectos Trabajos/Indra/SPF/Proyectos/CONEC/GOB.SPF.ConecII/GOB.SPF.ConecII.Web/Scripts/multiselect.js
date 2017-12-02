(function ($) {
    $('#public-methods').multiSelect();
    $('#select-all').click(function () {
        $('#public-methods').multiSelect('select_all');
        return false;
    });
    
    $('#deselect-all').click(function () {
        $('#public-methods').multiSelect('deselect_all');
        return false;
    });

    $('#refresh').on('click', function () {
        $('#public-methods').multiSelect('refresh');
        return false;
    });

    $('add-option').on('click', function () {
        $('#public-methods').multiSelect('addoption')
    });

})(jQuery);