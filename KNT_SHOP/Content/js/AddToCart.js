// (document).ready(function () {
//     (".btn-buy").click(function (e) {
//         alert("================")
//         var button = (e.target)
//         .post("/api/AddToCart", {
//             id: button.attr("data-sanpham-id")
//         })
//             .done(function () {
//                 alert("Added to cart!");
//             })
//             .fail(function () {
//                 alert("Something failed!");
//             });
//     });
// });

function AddToCart() {
    const btn = document.getElementsByClassName("btn-buy");
    for (let i = 0; i < btn.length; i++) {
        btn[i].addEventListener("click", function () {
            var url = btn[i].getAttribute("data-sanpham-id");
            fetch("https://localhost:44376/api/AddToCart", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        id: btn[i].getAttribute("data-sanpham-id")
                    })
                }
            )
        });
    }
}

window.addEventListener("load", AddToCart);


