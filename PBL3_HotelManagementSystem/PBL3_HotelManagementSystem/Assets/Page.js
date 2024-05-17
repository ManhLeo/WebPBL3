function MyForm(){
    document.getElementById("Form").style.display = "flex";  
}
function CloseForm(){
    document.getElementById("Form").style.display = "none"; 
}
function MyLogin() {
    window.location.href = "/Accounts/Login"; // Chuyển hướng đến action Login trong controller Accounts
}
