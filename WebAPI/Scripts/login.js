$(document).ready(function () {
    $("#login").click(function (event) {
        event.preventDefault();
        Login();
    });

    function Login() {
        var accountData = {
            Login: $('#addName').val(),
            Password: $('#addAuthor').val(),
        };

        $.ajax({
            type: 'POST',
            url: '/api/accounts',
            data: 'login=' + accountData.Login + "&password=" + accountData.Password,
            dataType: 'json',
            success: function (data) {
                document.cookie = "id=" + data;
                if (data == -1) {
                    //$('#info').css('visibility') = 'visible';
                    var elem = document.getElementById("status");
                    elem.style.setProperty("visibility", "visible");
                }
                else {
                    document.location.href = "/Support/Index";
                }
            },
            error: function () {
                console.log("Error");
            }

        });
    };
})