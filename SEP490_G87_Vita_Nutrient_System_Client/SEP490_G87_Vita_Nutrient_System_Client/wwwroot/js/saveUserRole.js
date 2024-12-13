﻿function SaveRole(userId, page, baseAPIAddress) {
    const roleId = document.getElementById("urole").value;
    const url = baseAPIAddress + `/Users/UpdateUserRole/${userId}/${roleId}`;

    fetch(url)
        .then(response => {
            if (response.ok) {
                showSuccessToast("Update user role successful!");
                setTimeout(() => {
                    if (page === 'user') {
                        window.location.href = '/admin/usermanagement/listuser';
                    } else {
                        window.location.href = '/admin/nutritionistmanagement/listnutritionist';
                    }
                }, 1000);
            } else {
                showErrorToast("Update user role failed!");
            }
        })
        .catch(error => {
            console.error("Error:", error);
            showErrorToast("Update user role failed!");
        });
}