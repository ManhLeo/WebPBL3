
function MyForm() {
    document.getElementById("Form").style.display = "flex";
}
function Mydetail(id) {
    const Room = document.getElementById(`${id}_infor`);
    const Room_infor = Room.getElementsByTagName("li");
    const roomDetailElement = document.getElementById("Room_detail");

    // Display the detail element
    document.getElementById("detail").style.display = "flex";

    const tt = document.getElementById("Room");
    tt.innerHTML = '';
    tt.innerHTML = document.getElementById(id).textContent + `<div onclick="MyCloseDetail()">
        <i class="fa-solid fa-circle-xmark"></i>
    </div>`;
    // Clear any existing content in roomDetailElement
    roomDetailElement.innerHTML = '';

    // Iterate over the Room_infor HTMLCollection and clone each <li> element
    Array.from(Room_infor).forEach(item => {
        const li = document.createElement('li');
        li.textContent = item.textContent.trim();
        roomDetailElement.appendChild(li);
    });
}

function Close() {
    document.getElementById("Form").style.display = "none";
}
function MyCloseDetail() {
    document.getElementById("detail").style.display = "none";
}
function MyLogin() {
    window.location.href = "./Login.html";
}
function MainPage() {
    document.getElementById("Page").style.display = "flex";
    document.getElementById("HistoryContainer").style.display = "none";
    document.getElementById("PersonalInfor").style.display = "none";
    document.getElementById("Taskbar_item_1").style.color = "black";
    document.getElementById("Taskbar_item_2").style.color = "rgb(116, 114, 114)";
    document.getElementById("Taskbar_item_3").style.color = "rgb(116, 114, 114)";
    const elements = document.getElementsByClassName("Room_item");

    // Check if elements exist
    if (elements.length > 0) {
        // Iterate through each element and adjust the width
        for (let i = 0; i < elements.length; i++) {
            elements[i].style.width = "900px";
        }
    }
    const listItems = document.getElementsByClassName("li");

    // Loop through each <li> element
    if (listItems.length > 0) {
        // Iterate through each element and adjust the width
        for (let i = 0; i < listItems.length; i++) {
            listItems[i].style.width = "500px";
        }
    }
}

function RoomHistory() {
    document.getElementById("Page").style.display = "none";
    document.getElementById("HistoryContainer").style.display = "flex";
    document.getElementById("PersonalInfor").style.display = "none";
    document.getElementById("Taskbar_item_1").style.color = "rgb(116, 114, 114)";
    document.getElementById("Taskbar_item_2").style.color = "rgb(116, 114, 114)";
    document.getElementById("Taskbar_item_3").style.color = "black";
    const elements = document.getElementsByClassName("Room_item");

    // Check if elements exist
    if (elements.length > 0) {
        // Iterate through each element and adjust the width
        for (let i = 0; i < elements.length; i++) {
            elements[i].style.width = "700px";
        }
    }

    const listItems = document.getElementsByClassName("li");

    // Loop through each <li> element
    if (listItems.length > 0) {
        // Iterate through each element and adjust the width
        for (let i = 0; i < listItems.length; i++) {
            listItems[i].style.width = "300px";
        }
    }
}
function PersonalInfor() {
    document.getElementById("Page").style.display = "none";
    document.getElementById("HistoryContainer").style.display = "none";
    document.getElementById("PersonalInfor").style.display = "flex";
    document.getElementById("Taskbar_item_1").style.color = "rgb(116, 114, 114)";
    document.getElementById("Taskbar_item_2").style.color = "black";
    document.getElementById("Taskbar_item_3").style.color = "rgb(116, 114, 114)";
}
function MyInfor() {
    document.getElementById("Check").style.opacity = 1;
    document.getElementById("Check").disabled = false;
}
