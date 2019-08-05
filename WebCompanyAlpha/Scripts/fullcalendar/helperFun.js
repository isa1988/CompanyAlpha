$(document).ready(function () {
    $('#SignUpbtn').click(function () {
        var url = $('#signup').data('url');

        $.get(url, function (data) {
            $('#modal-content').html(data);

            $('#modal-content').modal('show');
        });
    });
});