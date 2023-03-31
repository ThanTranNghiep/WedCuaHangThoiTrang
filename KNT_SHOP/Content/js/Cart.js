// $(function(){
//     $('.checkbox').on('change',function(){
//         $('#form').submit();
//     });
// });

function updateCart(masp) {    
    var url = "/Cart/UpdateCart?masp=" + masp;
    window.location.href = url;    
}
function StepUp(masp) {
    var url = "/Cart/StepUp?masp=" + masp;
    window.location.href = url;    
}
function StepDown(masp) {
    const num = document.getElementById('num');
    var value = parseInt(num.value);
    if(value > 1){
        var url = "/Cart/StepDown?masp=" + masp;
        window.location.href = url;
    }
}

function DeleteSanPham(masp) {
    var url = "/Cart/DeleteSanPham?masp=" + masp;
    console.log("true-----" + masp);
    window.location.href = url;
}


