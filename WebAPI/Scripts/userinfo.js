$(document).ready(function () {

    LoadData();

    function LoadData() {
        function getCookie(name) {
            let matches = document.cookie.match(new RegExp(
                "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
            ));
            return matches ? decodeURIComponent(matches[1]) : undefined;
        }

        $.ajax({
            type: 'GET',
            url: '/api/profiles/' + getCookie("id"),
            dataType: 'json',
            success: function (data) {
                $('#firstName').val(data.FirstName);
                $('#lastName').val(data.LastName);
                $('#patronymic').val(data.Patronymic);
                $('#phone').val(data.Phone);
                $('#email').val(data.Email);
                $('#role').val(data.Role);
                $('#room').val(data.Room);
            },
            error: function () {
                console.log("Error");
            }
        });
    };
})