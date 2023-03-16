function constraint() {
    const input = document.getElementById("inputQuantity");
    const inputValue = input.value;
    const regex = /^(?:100|[1-9]\d|\d)$/
    if (!regex.test(inputValue)) {
        input.value = "1"
    } 
}
