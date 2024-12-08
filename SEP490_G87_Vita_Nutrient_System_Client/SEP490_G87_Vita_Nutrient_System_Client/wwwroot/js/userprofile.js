﻿function openTab(event, tabId) {
    var tabButtons = document.querySelectorAll(".tab-button");
    var tabContents = document.querySelectorAll(".tab-content");

    tabButtons.forEach(button => button.classList.remove("active"));
    tabContents.forEach(content => content.classList.remove("active"));

    event.currentTarget.classList.add("active");
    document.getElementById(tabId).classList.add("active");
}

// Open Modal Function
function openChangePassModal() {
    document.getElementById("modalOverlay").style.display = "flex";
}

// Close Modal Function
function closeModal() {
    document.getElementById("modalOverlay").style.display = "none";
}

// Close modal when clicking outside
window.onclick = function (event) {
    const modalOverlay = document.getElementById("modalOverlay");
    if (event.target === modalOverlay) {
        closeModal();
    }
}

function submitAvaForm() {
    var form = document.getElementById("upAvaForm");
    form.submit();
}

function isValidPhoneNumber(phone) {
    var phonePattern = /^0\d{9}$/;
return phonePattern.test(phone);
}

document.getElementById("updateUserInfoForm").onsubmit = function(event) {
    var phone = document.getElementById("user-phone-input").value;

    if (!isValidPhoneNumber(phone)) {
        showErrorToast("Phone number must contain 10 number and start with 0!");
        event.preventDefault();
    }
};
