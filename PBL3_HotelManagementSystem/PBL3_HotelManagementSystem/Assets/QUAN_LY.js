function roomManagement() {
    document.getElementById("Room_management").style.display = "flex";
    document.getElementById("Actor_management").style.display = "none";
    document.getElementById("Service_management").style.display = "none";
    document.getElementById("Bill_management").style.display = "none";
    //thiết lập chiều cao
    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function(element) {
        element.style.height = "40%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function(element) {
        element.style.height = "60%";
    });
}

function actorManagement() {
    document.getElementById("Room_management").style.display = "none";
    document.getElementById("Actor_management").style.display = "flex";
    document.getElementById("Service_management").style.display = "none";
    document.getElementById("Bill_management").style.display = "none";
    // document.querySelector("td:nth-child(3)").style.color = "rgb(92, 92, 92)";
    // document.querySelector("td:nth-child(4)").style.color = "red";

    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function(element) {
        element.style.height = "20%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function(element) {
        element.style.height = "80%";
    });
}

function serviceManagement(){
    document.getElementById("Room_management").style.display = "none";
    document.getElementById("Actor_management").style.display = "none";
    document.getElementById("Service_management").style.display = "flex";
    document.getElementById("Bill_management").style.display = "none";
    // document.querySelector("td:nth-child(3)").style.color = "rgb(92, 92, 92)";
    // document.querySelector("td:nth-child(4)").style.color = "red";

    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function(element) {
        element.style.height = "20%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function(element) {
        element.style.height = "80%";
    });
}

function billManagement(){
    document.getElementById("Room_management").style.display = "none";
    document.getElementById("Actor_management").style.display = "none";
    document.getElementById("Service_management").style.display = "none";
    document.getElementById("Bill_management").style.display = "flex";
    // document.querySelector("td:nth-child(3)").style.color = "rgb(92, 92, 92)";
    // document.querySelector("td:nth-child(4)").style.color = "red";

    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function(element) {
        element.style.height = "20%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function(element) {
        element.style.height = "80%";
    });
}

function MyForm(ID){
    document.getElementById(ID).style.display= "Flex";
}
function CloseForm(ID){
    document.getElementById(ID).style.display="none";
}