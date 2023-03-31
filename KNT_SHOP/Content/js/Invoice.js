function InvoiceDetail(mahd) {
    var url = "/Invoice/Details?id=" + mahd;
    window.location.href = url;
}

function BackToInvoice() {
    var url = "/Invoice";
    window.location.href = url;
}

// function fillToInvoice() {
//     const select = document.getElementById("invoiceItem");
//     select.setAttribute("onclick", function () {
//         console.log(select.value.toString());
//     });
// }
//
// const select = document.getElementById("invoiceItem");
// select.addEventListener("selectionchange", function () {
//     fillToInvoice();
// });

function fillToInvoice() {
    const select = document.getElementById("invoiceItem");
    console.log('test--------------------')
    var url = "/Invoice/fillToInvoice?id=" + select.value.toString();
    window.location.href = url;
}