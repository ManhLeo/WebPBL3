function MyLogin()
{
    document.getElementById('TTLogin').style.color="black";
    document.getElementById('LineLogin').style.opacity=1;
    document.getElementById('Login').style.display="flex";
    document.getElementById('TTSigup').style.color="lightgray";
    document.getElementById('LineSigup').style.opacity=0;
    document.getElementById('Sigup').style.display="none";
}
function MySigup()
{
    document.getElementById('TTLogin').style.color="lightgray";
    document.getElementById('LineLogin').style.opacity=0;
    document.getElementById('Login').style.display="none";
    document.getElementById('TTSigup').style.color="black";
    document.getElementById('LineSigup').style.opacity=1;
    document.getElementById('Sigup').style.display="flex";
}
function MyFunction()
{
    let TK= document.getElementById("txtEmail").value;
    let MK= document.getElementById("txtPassword").value;
    if(TK == "correct_login@example.com" && MK == "C0rr3Ct_P@55w0rd") window.location.href="./Page.html";
    else
    {
        document.getElementById('Note').style.opacity=1;
    }
}