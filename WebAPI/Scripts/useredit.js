$(document).ready(function () {

    $("#Save").click(function (event) {
        event.preventDefault();
        SaveData();
    });

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
                $('#FirstName').val(data.FirstName);
                $('#LastName').val(data.LastName);
                $('#Patronymic').val(data.Patronymic);
                $('#Phone').val(data.Phone);
                $('#Email').val(data.Email);
                $('#Role').val(data.Role);
                $('#Room').val(data.Room);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    function SaveData() {
        var accountData = {
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Patronymic: $('#Patronymic').val(),
            Phone: $('#Phone').val(),
            Email: $('#Email').val(),
            Role: $('#Role').val(),
            Room: $('#Room').val(),
        };

        $.ajax({
            type: 'PUT',
            url: '/api/profiles',
            data: 'id=' + getCookie("id") + '&firstName=' + accountData.FirstName + "&lastName=" + accountData.LastName +
                "&patronymic=" + accountData.Patronymic + "&phone=" + accountData.Phone + "&email=" + accountData.Email +
                "&room=" + accountData.Room + "&role=" + accountData.Role,
            dataType: 'json',
            success: function (data) {
                document.location.href = "/Support/UserInfo";
            },
            error: function () {
                console.log("Error");
            }
        });
    };
})