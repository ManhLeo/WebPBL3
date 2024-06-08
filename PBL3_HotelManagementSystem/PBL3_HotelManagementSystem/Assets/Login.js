function myLogin() {
    document.getElementById("Login").style.display = "flex";
    document.getElementById("SignUp").style.display = "none";
    document.getElementById("LineLogin").style.opacity = "1";
    document.getElementById("LineSignUp").style.opacity = "0";
}
function mySignUp() {
    document.getElementById("Login").style.display = "none";
    document.getElementById("SignUp").style.display = "flex";
    document.getElementById("LineLogin").style.opacity = "0";
    document.getElementById("LineSignUp").style.opacity = "1";
}




