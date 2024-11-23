function openTab(event, tabId) {
    var tabButtons = document.querySelectorAll(".tab-button");
    var tabContents = document.querySelectorAll(".tab-content");

    tabButtons.forEach(button => button.classList.remove("active"));
    tabContents.forEach(content => content.classList.remove("active"));

    event.currentTarget.classList.add("active");
    document.getElementById(tabId).classList.add("active");
}
