function ImgToDetail(id){
    window.location.href = "/Product/Detail/" + id;
}

function ListIcon(){
    window.location.href = "/Home/ListSanPham";
}
function TableIcon(){
    window.location.href = "/Home/SanPham";
}

function AddToCart(id){
    var confirm = window.confirm("Bạn có muốn thêm sản phẩm này vào giỏ hàng?");
    if (confirm === true)
    {
        window.location.href = "/Product/AddToCart?id=" + id;
    }    
}
