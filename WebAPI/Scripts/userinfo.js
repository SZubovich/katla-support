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
                $('#firstName').text(data.FirstName);
                $('#lastName').text(data.LastName);
                $('#patronymic').text(data.Patronymic);
                $('#phone').text(data.Phone);
                $('#email').text(data.Email);
                $('#role').text(data.Role);
                $('#room').text(data.Room);
            },
            error: function () {
                console.log("Error");
            }
        });
    };
})