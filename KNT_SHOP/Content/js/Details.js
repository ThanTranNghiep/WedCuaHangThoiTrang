function add() {
    const number = document.getElementById("inputQuantity")
    var value = parseInt(number.value)
    var newvalue = value+1
    number.value = newvalue.toString()
    
}
function subtract() {
    const number = document.getElementById("inputQuantity")
    var value = parseInt(number.value)
    if (value > 1) {
        var newvalue = value - 1
        number.value = newvalue.toString()
    }
    else {
        number.value = 1
    }
    
}