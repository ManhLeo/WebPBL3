
function MyForm(ID) {
    document.getElementById(ID).style.display = "flex";
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

function Close(id) {
    document.getElementById(id).style.display = "none";
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
    $('#Form3').submit(function (event) {
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
    $('#Form4').submit(function (event) {
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

// Hàm xử lý sự kiện Tìm kiếm
function searchRooms() {
    var searchInput = document.getElementById('searchInput').value.toLowerCase();
    var roomItems = document.querySelectorAll('.Room_item');

    // Biến để kiểm tra xem đã tìm thấy Room item phù hợp hay chưa
    var found = false;

    roomItems.forEach(function (room) {
        var roomTitle = room.querySelector('.Room_title').innerText.toLowerCase();
        var roomInfor = room.querySelector('.li').innerText.toLowerCase();

        // Kiểm tra xem tiêu đề hoặc thông tin có chứa từ khóa tìm kiếm không
        if (roomTitle.includes(searchInput) || roomInfor.includes(searchInput)) {
            room.style.display = 'block'; // Hiển thị Room item nếu phù hợp
            // Nếu từ khóa tìm kiếm là "phòng tổng thống" và Room item đang xét là Room item đầu tiên
            if (searchInput === 'phòng tổng thống' && !found) {
                room.scrollIntoView({ behavior: 'smooth' }); // Cuộn để hiển thị Room item đầu tiên
                found = true; // Đánh dấu đã tìm thấy Room item phù hợp
            }
            else if (searchInput === 'phòng hạng sang' && !found) {
                room.scrollIntoView({ behavior: 'smooth' }); // Cuộn để hiển thị Room item đầu tiên
                found = true; // Đánh dấu đã tìm thấy Room item phù hợp
            }
            else if(searchInput === 'phòng thường' && !found) {
                room.scrollIntoView({ behavior: 'smooth' }); // Cuộn để hiển thị Room item đầu tiên
                found = true; // Đánh dấu đã tìm thấy Room item phù hợp
            }
        } else {
            room.style.display = 'none'; // Ẩn Room item nếu không phù hợp
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    const checkboxes = document.querySelectorAll('.service-checkbox');

    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            const serviceInputs = this.closest('label').nextElementSibling.querySelectorAll('.service-input');
            serviceInputs.forEach(function (input) {
                input.disabled = !checkbox.checked;
                input.required = checkbox.checked;
            });
        });
    });
});