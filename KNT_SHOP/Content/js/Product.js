﻿function ImgToDetail(id){
    window.location.href = "/Product/Detail/" + id;
}

function ListIcon(){
    window.location.href = "/Product/ListSanPham";
}
function TableIcon(){
    window.location.href = "/Product/SanPham";
}

function AddToCart(id){
    var confirm = window.confirm("Bạn có muốn thêm sản phẩm này vào giỏ hàng?");
    if (confirm === true)
    {
        window.location.href = "/Product/AddToCart?id=" + id;
    }    
}

function EditProductDetail(id) {
    window.location.href = "/Product/Edit/" + id;
}

function EditProduct() {
    if (confirm("Are you sure you want to delete this item?")) {
        document.querySelectorAll('input').forEach(input => {
            input.readOnly = false;
        });
        console.log("Account is now editable");
    } else {
        document.querySelectorAll('input').forEach(input => {
            input.readOnly = true;
        });
        console.log("Account is now editable");
    }
}