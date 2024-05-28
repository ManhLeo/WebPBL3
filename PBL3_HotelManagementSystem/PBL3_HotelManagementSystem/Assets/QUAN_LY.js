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









