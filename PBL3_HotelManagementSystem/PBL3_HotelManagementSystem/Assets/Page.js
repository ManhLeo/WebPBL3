
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
document.addEventListener("DOMContentLoaded", function () {
    // Hàm cập nhật dropdown menu
    function updateDropdown(dropdownId, items, defaultOptionText = 'Chọn loại phòng') {
        const dropdown = document.getElementById(dropdownId);
        if (dropdown) {
            dropdown.innerHTML = ''; // Xóa các tùy chọn cũ

            // Thêm tùy chọn "Chọn loại phòng" vào đầu danh sách
            const allOption = document.createElement('option');
            allOption.value = '';
            allOption.textContent = defaultOptionText;
            dropdown.appendChild(allOption);

            // Thêm các tùy chọn mới
            items.forEach(item => {
                const option = document.createElement('option');
                option.value = item;
                option.textContent = item;
                dropdown.appendChild(option);
            });
        } else {
            console.error(`Không tìm thấy phần tử HTML với id là ${dropdownId}`);
        }
    }

    // Hàm lấy dữ liệu từ server
    function fetchData(url, callback) {
        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => callback(data))
            .catch(error => console.error('Lỗi khi lấy dữ liệu:', error));
    }

    // Lấy loại phòng và cập nhật dropdown menu
    fetchData('/Home/GetRoomTypes', function (roomTypes) {
        updateDropdown('roomType', roomTypes, 'Chọn loại phòng');
    });
});



$(document).ready(function () {
    $('#Form1').submit(function (event) {
        event.preventDefault(); // Ngăn chặn form gửi đi mặc định

        var formData = $(this).serialize(); // Chuẩn bị dữ liệu form

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                // Xử lý kết quả từ Controller
                if (response.success) {
                    alert(response.message); // Hiển thị thông báo thành công
                    // Cập nhật giao diện hoặc thực hiện các thao tác khác sau khi đặt phòng thành công
                } else {
                    alert(response.message); // Hiển thị thông báo lỗi
                }
            },
            error: function (xhr, status, error) {
                // Xử lý lỗi
                console.log(xhr);
                console.log(status);
                console.log(error);
            }
        });
    });
});

$(document).ready(function () {
    $('#Form2').submit(function (event) {
        event.preventDefault(); // Ngăn chặn form gửi đi mặc định

        var formData = $(this).serialize(); // Chuẩn bị dữ liệu form

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                // Xử lý kết quả từ Controller
                if (response.success) {
                    alert(response.message); // Hiển thị thông báo thành công
                    // Cập nhật giao diện hoặc thực hiện các thao tác khác sau khi đặt phòng thành công
                } else {
                    alert(response.message); // Hiển thị thông báo lỗi
                }
            },
            error: function (xhr, status, error) {
                // Xử lý lỗi
                console.log(xhr);
                console.log(status);
                console.log(error);
            }
        });
    });
});



$(document).ready(function () {
    $("#personalInfoForm").submit(function (e) {
        e.preventDefault();

        var formData = {
            IDKH: $("#txtIDKH").val(),
            HoTen: $("#txtHoTen").val(),
            CCCD: $("#txtCCCD").val(),
            SDT: $("#txtSDT").val(),
            Email: $("#txtEmail").val(),
            GioiTinh: $("input[name='GioiTinh']:checked").val() === "true",
            DiaChi: $("#txtDiaChi").val()
        };

        $.ajax({
            url: "/Home/UpdatePersonalInfo",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.success) {
                    alert("Thông tin đã được cập nhật thành công!");
                } else {
                    alert("Cập nhật thông tin không thành công: " + result.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Cập nhật thông tin không thành công. Vui lòng thử lại sau!");
            }
        });
    });
});