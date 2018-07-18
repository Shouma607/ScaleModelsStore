function DropdownSettings() {
    document.getElementById("settingsDropdown").classList.toggle("show");
    var dropdownOrders = document.getElementById("ordersDropdown");
    if (dropdownOrders.classList.contains('show')) {
        dropdownOrders.classList.remove('show');
    }
}
function DropdownOrders() {
    document.getElementById("ordersDropdown").classList.toggle("show");
    var dropdownSettings = document.getElementById("settingsDropdown");
    if (dropdownSettings.classList.contains('show')) {
        dropdownSettings.classList.remove('show');
    }
}


window.onclick = function (e) {
    if (!e.target.matches('.dropbtn')) {
        var dropdownSettings = document.getElementById("settingsDropdown");
        var dropdownOrders = document.getElementById("ordersDropdown");
        if (dropdownSettings.classList.contains('show')) {
            dropdownSettings.classList.remove('show');
        }
        if (dropdownOrders.classList.contains('show')) {
            dropdownOrders.classList.remove('show');
        }
    }
}