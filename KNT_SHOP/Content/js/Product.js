function ImgToDetail(id) {
    window.location.href = "/Product/Detail/" + id;
}

function ListIcon() {
    window.location.href = "/Product/ListSanPham";
}

function TableIcon() {
    window.location.href = "/Product/SanPham";
}

function AddToCart(id) {
    var confirm = window.confirm("Bạn có muốn thêm sản phẩm này vào giỏ hàng?");
    if (confirm === true) {
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

function AddPrice(id) {
    window.location.href = "/Product/AddPrice?id=" + id;
}

function AddNewPrice(id) {
    const priceInput = document.getElementById("Price");
    let price = parseInt(priceInput.value)
    if (isNaN(price) || priceInput.value === "" || priceInput.value === null) {
        alert("Giá phải là số");
    } else {
        // window.location.href = "/Product/AddNewPrice?id=" + id + "&price=" + priceInput.value;
    }
}