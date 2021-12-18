// check if there's a token item in local storage
if (localStorage.getItem("auth_token") == null) {
    document.getElementById("rec").style.display = "none";
}