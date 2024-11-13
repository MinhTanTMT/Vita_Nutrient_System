async function toggleLike(userId, foodId) {
    try {
        // Gọi API
        const response = await fetch("https://localhost:7045/api/UserFoodAction/UserLikeOrUnlikeFood", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ userId: userId, foodId: foodId })
        });

        // Kiểm tra phản hồi từ API
        if (response.ok) {
            // Nếu OK, cập nhật icon
            var likeIcon = document.getElementById("like-icon");
            if (likeIcon.classList.contains("mdi-thumb-up-outline")) {
                likeIcon.classList.remove("mdi-thumb-up-outline");
                likeIcon.classList.add("mdi-thumb-up");
            } else {
                likeIcon.classList.remove("mdi-thumb-up");
                likeIcon.classList.add("mdi-thumb-up-outline");
            }
        } else {
            // Nếu không OK, hiển thị thông báo lỗi
            showToast("Unexpected error occurred!");
        }
    } catch (error) {
        // Bắt lỗi nếu có vấn đề khi gọi API
        console.error("Error:", error);
        showToast("Unexpected error occurred!");
    }
}

// Hàm hiển thị thông báo toast
function showToast(message) {
    // Tạo một div cho toast
    const toast = document.createElement("div");
    toast.className = "toast-message";
    toast.textContent = message;

    // Thêm style cho toast
    toast.style.position = "fixed";
    toast.style.top = "50px";
    toast.style.right = "20px";
    toast.style.padding = "10px 20px";
    toast.style.backgroundColor = "#333";
    toast.style.color = "#fff";
    toast.style.borderRadius = "5px";
    toast.style.zIndex = "1000";
    toast.style.opacity = "0";
    toast.style.transition = "opacity 0.5s ease";

    // Thêm toast vào body
    document.body.appendChild(toast);

    // Hiện toast
    setTimeout(() => (toast.style.opacity = "1"), 100);

    // Tự động ẩn toast sau 3 giây
    setTimeout(() => {
        toast.style.opacity = "0";
        setTimeout(() => document.body.removeChild(toast), 500);
    }, 3000);
}