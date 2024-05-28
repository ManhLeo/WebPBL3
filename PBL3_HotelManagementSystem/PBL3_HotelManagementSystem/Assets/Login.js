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


document.getElementById('loginForm').addEventListener('submit', function (e) {
    e.preventDefault();
    let email = document.getElementById("txtEmail").value;
    let password = document.getElementById("txtPassword").value;

    fetch('/Account/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email: email, password: password })
    })
        .then(response => {
            if (response.ok) {
                window.location.href = response.url; // Chuyển hướng đến URL được trả về nếu đăng nhập thành công
            } else {
                // Nếu đăng nhập không thành công, hiển thị thông báo lỗi
                document.getElementById('Note').style.opacity = 1;
            }
        });
});

