function MyLogin() {
    document.getElementById('TTLogin').style.color = "black";
    document.getElementById('LineLogin').style.opacity = 1;
    document.getElementById('Login').style.display = "flex";
    document.getElementById('TTSigup').style.color = "lightgray";
    document.getElementById('LineSigup').style.opacity = 0;
    document.getElementById('Sigup').style.display = "none";
}

function MySigup() {
    document.getElementById('TTLogin').style.color = "lightgray";
    document.getElementById('LineLogin').style.opacity = 0;
    document.getElementById('Login').style.display = "none";
    document.getElementById('TTSigup').style.color = "black";
    document.getElementById('LineSigup').style.opacity = 1;
    document.getElementById('Sigup').style.display = "flex";
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

