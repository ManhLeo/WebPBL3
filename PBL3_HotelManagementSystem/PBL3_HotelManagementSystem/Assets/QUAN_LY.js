function roomManagement() {
    document.getElementById("Room_management").style.display = "flex";
    document.getElementById("Actor_management").style.display = "none";
    document.getElementById("Service_management").style.display = "none";
    document.getElementById("Bill_management").style.display = "none";
    //thiết lập chiều cao
    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function (element) {
        element.style.height = "40%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function (element) {
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
    managerItem1Elements.forEach(function (element) {
        element.style.height = "20%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function (element) {
        element.style.height = "80%";
    });
}

function serviceManagement() {
    document.getElementById("Room_management").style.display = "none";
    document.getElementById("Actor_management").style.display = "none";
    document.getElementById("Service_management").style.display = "flex";
    document.getElementById("Bill_management").style.display = "none";
    // document.querySelector("td:nth-child(3)").style.color = "rgb(92, 92, 92)";
    // document.querySelector("td:nth-child(4)").style.color = "red";

    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function (element) {
        element.style.height = "20%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function (element) {
        element.style.height = "80%";
    });
}

function billManagement() {
    document.getElementById("Room_management").style.display = "none";
    document.getElementById("Actor_management").style.display = "none";
    document.getElementById("Service_management").style.display = "none";
    document.getElementById("Bill_management").style.display = "flex";
    // document.querySelector("td:nth-child(3)").style.color = "rgb(92, 92, 92)";
    // document.querySelector("td:nth-child(4)").style.color = "red";

    let managerItem1Elements = document.querySelectorAll('.manager_item1');
    managerItem1Elements.forEach(function (element) {
        element.style.height = "20%";
    });

    var managerItem2Elements = document.querySelectorAll('.manager_item2');
    managerItem2Elements.forEach(function (element) {
        element.style.height = "80%";
    });
}

function MyForm(ID) {
    document.getElementById(ID).style.display = "Flex";
}
function CloseForm(ID) {
    document.getElementById(ID).style.display = "none";
}

//===================================================================================//
// Function to delete a customer
function deleteCustomer(customerID) {
    // Confirm with the user before deleting
    if (confirm("Are you sure you want to delete this customer?")) {
        // Send a POST request to the server to delete the customer
        fetch('/Admin/DeleteCustomer', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: customerID })
        })
            .then(response => {
                // Check if the request was successful
                if (response.ok) {
                    // Reload the page to reflect the changes
                    window.location.reload();
                } else {
                    // Display an error message
                    alert("Failed to delete customer.");
                }
            })
            .catch(error => {
                // Display an error message
                console.error('Error:', error);
                alert("An error occurred while deleting customer.");
            });
    }
}

// Function to delete a room
function deleteRoom(roomID) {
    // Confirm with the user before deleting
    if (confirm("Are you sure you want to delete this room?")) {
        // Send a POST request to the server to delete the room
        fetch('/Admin/DeleteRoom', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: roomID })
        })
            .then(response => {
                // Check if the request was successful
                if (response.ok) {
                    // Reload the page to reflect the changes
                    window.location.reload();
                } else {
                    // Display an error message
                    alert("Failed to delete room.");
                }
            })
            .catch(error => {
                // Display an error message
                console.error('Error:', error);
                alert("An error occurred while deleting room.");
            });
    }
}

// Function to delete a service
function deleteService(serviceID) {
    // Confirm with the user before deleting
    if (confirm("Are you sure you want to delete this service?")) {
        // Send a POST request to the server to delete the service
        fetch('/Admin/DeleteService', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: serviceID })
        })
            .then(response => {
                // Check if the request was successful
                if (response.ok) {
                    // Reload the page to reflect the changes
                    window.location.reload();
                } else {
                    // Display an error message
                    alert("Failed to delete service.");
                }
            })
            .catch(error => {
                // Display an error message
                console.error('Error:', error);
                alert("An error occurred while deleting service.");
            });
    }
}

// Function to delete a bill
function deleteBill(billID) {
    // Confirm with the user before deleting
    if (confirm("Are you sure you want to delete this bill?")) {
        // Send a POST request to the server to delete the bill
        fetch('/Admin/DeleteBill', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: billID })
        })
            .then(response => {
                // Check if the request was successful
                if (response.ok) {
                    // Reload the page to reflect the changes
                    window.location.reload();
                } else {
                    // Display an error message
                    alert("Failed to delete bill.");
                }
            })
            .catch(error => {
                // Display an error message
                console.error('Error:', error);
                alert("An error occurred while deleting bill.");
            });
    }
}

//========================================================================================//
// Function to search services
function searchServices() {
    var searchText = document.getElementById("searchServiceInput").value;

    // Gửi yêu cầu tìm kiếm dịch vụ đến server
    fetch('/Admin/SearchServices', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ searchText: searchText })
    })
        .then(response => response.json())
        .then(data => {
            // Xóa các hàng hiện có trong bảng
            var tbody = document.querySelector('#serviceTable tbody');
            tbody.innerHTML = '';

            // Hiển thị kết quả tìm kiếm trong bảng
            data.forEach(service => {
                var row = document.createElement('tr');
                row.innerHTML = `<td>${service.IDDV}</td>` +
                    `<td>${service.TenDV}</td>` +
                    `<td>${service.DonGia}</td>` +
                    `<td>${service.SoLuongMax}</td>` +
                    `<td>${service.Soluong}</td>` +
                    `<td>` +
                    '<button onclick="Update(this)"><i class="fas fa-pen-to-square"></i></button>'+
                    `<button onclick="deleteService('${service.IDDV}')"><i class="fas fa-trash-alt"></i></button>` +
                    `</td>`;
                // Thêm hàng vào bảng
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            // Hiển thị thông báo lỗi nếu có lỗi xảy ra
            console.error('Error:', error);
            alert("An error occurred while searching services.");
        });
}
// Function to search services
function searchCustomers() {
    var searchText = document.getElementById("searchCustomerInput").value;

    // Gửi yêu cầu tìm kiếm dịch vụ đến server
    fetch('/Admin/SearchCustomers', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ searchText: searchText })
    })
        .then(response => response.json())
        .then(data => {
            // Xóa các hàng hiện có trong bảng
            var tbody = document.querySelector('#customerTable tbody');
            tbody.innerHTML = '';

            // Hiển thị kết quả tìm kiếm trong bảng
            data.forEach(customer => {
                var row = document.createElement('tr');
                row.innerHTML = `<td>${customer.IDKH}</td>` +
                    `<td>${customer.HoTen}</td>` +
                    `<td>${customer.CCCD}</td>` +
                    `<td>${customer.SDT}</td>` +
                    `<td>${customer.Email}</td>` +
                    `<td>${customer.DiaChi}</td>`; 

                // Thêm hàng vào bảng
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            // Hiển thị thông báo lỗi nếu có lỗi xảy ra
            console.error('Error:', error);
            alert("An error occurred while searching customers.");
        });
}

// Hàm chuyển đổi định dạng ngày từ /Date(xxx)/ sang dd/MM/yyyy
function formatJsonDate(jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    var day = String(date.getDate()).padStart(2, '0');
    var month = String(date.getMonth() + 1).padStart(2, '0');
    var year = date.getFullYear();
    return `${day}/${month}/${year}`;
}
function searchBills() {
    var searchText = document.getElementById("searchBillInput").value;

    // Gửi yêu cầu tìm kiếm dịch vụ đến server
    fetch('/Admin/SearchBills', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ searchText: searchText })
    })
        .then(response => response.json())
        .then(data => {
            // Xóa các hàng hiện có trong bảng
            var tbody = document.querySelector('#billTable tbody');
            tbody.innerHTML = '';

            // Hiển thị kết quả tìm kiếm trong bảng
            data.forEach(bill => {
                var row = document.createElement('tr');
                row.innerHTML = `<td>${bill.IDHD}</td>` +
                    `<td>${bill.IDKH}</td>` +
                    `<td>${bill.HoTen}</td>` +
                    `<td>${bill.CCCD}</td>` +
                    `<td>${formatJsonDate(bill.NgayNhan)}</td>` +
                    `<td>${formatJsonDate(bill.NgayTra)}</td>` +
                    `<td>${bill.DonGia}</td>` +
                    `<td style="color: red;" >${bill.TrangThai}</td>` +
                    `<td>` +
                    `<button onclick="deleteBill('${bill.IDHD}')"><i class="fas fa-trash-alt"></i></button>` +
                    `</td>`;


                // Thêm hàng vào bảng
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            // Hiển thị thông báo lỗi nếu có lỗi xảy ra
            console.error('Error:', error);
            alert("An error occurred while searching customers.");
        });
}



//=============================================================================================//
document.addEventListener("DOMContentLoaded", function (event) {
    // Lấy loại phòng từ server và cập nhật dropdown menu
    $.ajax({
        url: '/Admin/GetRoomTypes',
        type: 'GET',
        dataType: 'json',
        success: function (roomTypes) {
            var roomTypeDropdown = document.getElementById('condition1');
            if (roomTypeDropdown) {
                roomTypeDropdown.innerHTML = ''; // Xóa các tùy chọn cũ trước khi thêm mới

                // Thêm tùy chọn "Tất cả" vào đầu danh sách
                var allOption = document.createElement('option');
                allOption.value = 'Tất cả';
                allOption.textContent = 'Tất cả';
                roomTypeDropdown.appendChild(allOption);

                roomTypes.forEach(function (roomType) {
                    var option = document.createElement('option');
                    option.value = roomType;
                    option.textContent = roomType;
                    roomTypeDropdown.appendChild(option);
                });
            } else {
                console.error('Không tìm thấy phần tử HTML với id là condition1');
            }
        },
        error: function (xhr, status, error) {
            console.error('Lỗi khi lấy loại phòng:', error);
        }
    });

    

    // Lấy trạng thái phòng từ server và cập nhật dropdown menu
    $.ajax({
        url: '/Admin/GetRoomStatuses',
        type: 'GET',
        dataType: 'json',
        success: function (roomStatuses) {
            var roomStatusDropdown = document.getElementById('condition');
            if (roomStatusDropdown) {
                roomStatusDropdown.innerHTML = ''; // Xóa các tùy chọn cũ trước khi thêm mới

                // Thêm tùy chọn "Tất cả" vào đầu danh sách
                var allOption = document.createElement('option');
                allOption.value = 'Tất cả';
                allOption.textContent = 'Tất cả';
                roomStatusDropdown.appendChild(allOption);

                roomStatuses.forEach(function (roomStatus) {
                    var option = document.createElement('option');
                    option.value = roomStatus;
                    option.textContent = roomStatus;
                    roomStatusDropdown.appendChild(option);
                });
            } else {
                console.error('Không tìm thấy phần tử HTML với id là condition');
            }
        },
        error: function (xhr, status, error) {
            console.error('Lỗi khi lấy trạng thái phòng:', error);
        }
    });
});


document.addEventListener("DOMContentLoaded", function () {
    // Hàm cập nhật dropdown menu
    function updateDropdown(dropdownId, items, defaultOptionText = 'Tất cả') {
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
    fetchData('/Admin/GetRoomTypes', function (roomTypes) {
        updateDropdown('condition2', roomTypes, 'Chọn loại phòng');
    });
});



function searchRooms() {
    var roomTypeElement = document.getElementById('condition1');
    var conditionElement = document.getElementById('condition');
    var fromDateElement = document.getElementById('fromDate');
    var toDateElement = document.getElementById('toDate');

    if (roomTypeElement && conditionElement && fromDateElement && toDateElement) {
        var roomType = roomTypeElement.value;
        var condition = conditionElement.value;
        var fromDate = fromDateElement.value;
        var toDate = toDateElement.value;

        $.ajax({
            url: '/Admin/SearchRooms',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                roomType: roomType,
                condition: condition,
                fromDate: fromDate,
                toDate: toDate
            }),
            success: function (rooms) {
                var tableBody = document.getElementById('roomTableBody');
                tableBody.innerHTML = '';

                rooms.forEach(function (room) {
                    var row = document.createElement('tr');
                    row.innerHTML = `
                        <td>${room.IDPHG}</td>
                        <td>${room.TenPHG}</td>
                        <td>${room.TenLoaiPhong}</td>
                        <td>${room.DonGia}</td>
                        <td>${room.SoGiuong}</td>
                        <td>${room.TrangThai}</td>
                        <td>
                            <button onclick="editRoom('${room.IDPHG}')"><i class="fas fa-edit"></i></button>
                            <button onclick="deleteRoom('${room.IDPHG}')"><i class="fas fa-trash-alt"></i></button>
                        </td>
                    `;
                    tableBody.appendChild(row);
                });
            },
            error: function (xhr, status, error) {
                console.error('Lỗi khi tìm kiếm phòng:', error);
            }
        });
    } else {
        console.error('Không tìm thấy một hoặc nhiều phần tử nhập liệu.');
    }
}



$(document).ready(function () {
    $('#bookingForm').submit(function (event) {
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
    $('#addServiceForm').submit(function (event) {
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
    $('#ServiceForm').submit(function (event) {
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




function createInvoice(roomId) {
    // Gửi yêu cầu tạo hóa đơn đến máy chủ với roomId của phòng đã chọn
    $.ajax({
        url: '/Admin/CreateBill',
        type: 'POST',
        data: { roomId: roomId },
        success: function (response) {
            if (response.success) {
                alert('Hóa đơn đã được tạo thành công.');
                // Cập nhật giao diện hoặc thực hiện các thao tác khác sau khi tạo hóa đơn thành công
            } else {
                alert('Đã xảy ra lỗi: ' + response.message);
            }
        },
        error: function () {
            alert('Đã xảy ra lỗi khi gửi yêu cầu tạo hóa đơn.');
        }
    });
}

function Update(element) {
    // Get the parent row of the clicked element
    var row = element.parentElement.parentElement;
    let Titles = row.parentElement.querySelector('tr').querySelectorAll('th');
    let Container = document.getElementById("Update_container");
    console.log(element.closest('.manager_container').id);
    // Clear the container content before updating
    Container.innerHTML = '';

    // Add title for the form
    Container.innerHTML += `
        <div class="Form_item" style="justify-content: center;">
            <p style="font-weight: 600;font-size: 24px;color: black;">Thông tin</p>
        </div>
    `;
    // Iterate through each title and corresponding cell in the row
    Titles.forEach(function (title, index) {
        if (index != Titles.length - 1 && index != 0) {
            Container.innerHTML += `
            <div class="Form_item">
                <p>${title.innerText}</p>
                <input type="text" class="input" value="${row.cells[index].innerText}">
            </div>
        `;
        }
    });
    Container.innerHTML += `
        <div class="Form_item" style="justify-content: center;margin-top: 20px;">
            <button class="button1">Chỉnh sửa</button>
            <button class="button1" onclick="CloseForm('Update')">Thoát</button>
        </div>
    `;

    // Correcting the display property
    document.getElementById("Update").style.display = "flex";
}