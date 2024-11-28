const searchBox = document.querySelector('.search-box');
const options = document.querySelectorAll('.options li');
const selectedOption = document.querySelector('input[name="ingreName"]');
const selectedOptionId = document.querySelector('input[name="ingreId"]');
const clearButton = document.getElementById('clear-button');

searchBox.addEventListener('input', () => {
    const searchTerm = searchBox.value.toLowerCase();

    options.forEach(option => {
        const text = option.textContent.toLowerCase();
        if (text.includes(searchTerm)) {
            option.style.display = 'block';
        } else {
            option.style.display = 'none';
        }
    });
});

for (const option of options) {
    option.addEventListener('click', () => {
        const value = option.getAttribute('data-value');
        selectedOption.value = option.textContent;
        selectedOptionId.value = option.attributes.getNamedItem("data-value").value;
        for (const opt of options) {
            opt.style.display = 'block';
        }
    });
}

//clearButton.addEventListener('click', function () {
//    selectedOption.textContent = 'Select an option';
//    searchBox.value = '';
//    options.forEach(option => {
//        option.style.display = 'block';
//    });
//});