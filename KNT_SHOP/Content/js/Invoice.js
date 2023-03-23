function InvoiceDetail(mahd) {
    var url = "/Invoice/Details?id=" + mahd;
    window.location.href = url;
}

function BackToInvoice()
{
    var url = "/Invoice";
    window.location.href = url;
}
    