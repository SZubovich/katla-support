$(document).ready(function () {

    $("#register").click(function (event) {
        event.preventDefault();
        Register();
    });

    function Register() {
        var accountData = {
            Login: $('#login').val(),
            Password: $('#password').val(),
            FirstName: $('#firstName').val(),
            LastName: $('#lastName').val(),
            Patronymic: $('#patronymic').val(),
            Phone: $('#phone').val(),
            Email: $('#email').val(),
            Role: $('#role').val(),
            Room: $('#room').val(),
        };

        $.ajax({
            type: 'PUT',
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
                    $.ajax({
                        type: 'POST',
                        url: '/api/profiles',
                        data: 'firstName=' + accountData.FirstName + "&lastName=" + accountData.LastName +
                            "&patronymic=" + accountData.Patronymic + "&phone=" + accountData.Phone + "&email=" + accountData.Email +
                            "&room=" + accountData.Room + "&role=" + accountData.Role,
                        dataType: 'json',
                        success: function (data) {
                            document.location.href = "/Support/Index";
                        },
                        error: function () {
                            console.log("Error");
                        }

                    });
                }
            },
            error: function () {
                console.log("Error");
            }

        });
    };
})