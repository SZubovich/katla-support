var creatorid = getCookie("id");
if (creatorid > 0) {
    document.getElementById("creatorid").setAttribute("value", creatorid)
}

function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : -1;
}