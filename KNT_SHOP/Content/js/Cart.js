// $(function(){
//     $('.checkbox').on('change',function(){
//         $('#form').submit();
//     });
// });

function updateCart(masp) {    
    var url = "https://localhost:44376/Cart/UpdateCart?masp=" + masp;
    window.location.href = url;    
}
function StepUp(masp) {
    const num = document.getElementById('num');
    var value = parseInt(num.value);
    if(value >= 1){
        value++;
    }
    if(value < 1) {
        value = 1;
    }
    
    num.value = value;
    var url = "https://localhost:44376/Cart/UpdateSoLuong?masp=" + masp + "&soLuong=" + num.value;
    window.location.href = url;
    
}
function StepDown(masp) {
    const num = document.getElementById('num');
    var value = parseInt(num.value);
    if(value >= 1){
        value--;
    }
    if(value < 1) {
        value = 1;
    }
    num.value = value;
    var url = "https://localhost:44376/Cart/UpdateSoLuong?masp=" + masp + "&soLuong=" + num.value;
    window.location.href = url;
}

function get_item() {
    const checkbox = document.getElementById("check");
    if (checkbox.checked) {
       
    }
}
