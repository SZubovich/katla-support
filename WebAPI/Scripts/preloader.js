$(document).ready(function () {

    $("#logout-btn").click(function (event) {
        event.preventDefault();
        document.cookie = "id=-1";
        document.location.href = "/Support/Login";
    });

    var loginBtn = document.getElementById("login-btn");
    var registerBtn = document.getElementById("register-btn");
    var profileBtn = document.getElementById("profile-btn");
    var tasksBtn = document.getElementById("tasks-btn");
    var logoutBtn = document.getElementById("logout-btn");

    if (getCookie("id") > 0) {
        loginBtn.style.setProperty("display", "none");
        registerBtn.style.setProperty("display", "none");
        profileBtn.style.setProperty("display", "inline-block");
        tasksBtn.style.setProperty("display", "inline-block");
        logoutBtn.style.setProperty("display", "inline-block");
    }
    else {
        loginBtn.style.setProperty("display", "inline-block");
        registerBtn.style.setProperty("display", "inline-block");
        profileBtn.style.setProperty("display", "none");
        tasksBtn.style.setProperty("display", "none");
        logoutBtn.style.setProperty("display", "none");
    }

    function getCookie(name) {
        let matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : -1;
    }
});