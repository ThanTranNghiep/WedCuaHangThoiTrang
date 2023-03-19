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

function DeleteSanPham(masp) {
    var url = "https://localhost:44376/Cart/DeleteSanPham?masp=" + masp;
    console.log("true-----" + masp);
    window.location.href = url;
}


function uncheckAll(currentCheckbox) {
    var checkboxes = document.getElementsByTagName('input[type=checkbox]');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i] !== currentCheckbox) {
            checkboxes[i].checked = false;
        }
    }
}