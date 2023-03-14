function showLoginDialog() {
    document.getElementById('login-dialog').showModal();
}
function closeLoginDialog() {
    var dialog = document.querySelector('dialog');
    dialog.close();
}
//
// window.addEventListener('load', function () {
//     // alert('load')
//     var token = this.localStorage.getItem('token'); // get token
//     alert(token)
//     if (token == null) {
//         // Path: KNT_SHOP\Content\js\Dialog.js
//         var divId = document.getElementById('container-dialog');
//         var html_LoginDialog = '<dialog id="login-dialog">\n' +
//             '        <form method="post" class="login_form">\n' +
//             '            <div>\n' +
//             '                <h2 class="login_title">\n' +
//             '                    Welcome to <span>KNT Shop</span>\n' +
//             '                </h2>\n' +
//             '            </div>\n' +
//             '            <div>\n' +
//             '                <div class="login_inputs">\n' +
//             '                    <div>\n' +
//             '                        <label for="Username" class="login_label">Username</label> \n' +
//             '                        <input type="text" id="Username" class="form-control" placeholder="Username" />\n'+
//             '                    </div>\n' +
//             '    \n' +
//             '                    <div>\n' +
//             '                        <label for="" class="login_label">Password</label>\n' +
//             '                        <div class="login_box">\n' +
//             '                            <input type="password" id="input-pass" class="form-control" placeholder="Password" />\n' +
//             '                            <i class="ri-eye-off-line login-eye" id="input-icon"></i>\n' +
//             '                        </div>\n' +
//             '                    </div>\n' +
//             '                </div>\n' +
//             '    \n' +
//             '                <div class="login_check">\n' +
//             '                    <input type="checkbox" class="login_check-input" id="check">\n' +
//             '                    <label for="check" class="login_label-check">Remember me</label>\n' +
//             '                </div>\n' +
//             '            </div>\n' +
//             '            <div>\n' +
//             '                <div class="login_buttons">\n' +
//             '                    <button type="submit" id="btn-login" class="btn btn-primary" formmethod="post">Login</button>\n' +
//             '                    <button type="button" id="btn-signup" class="btn btn-outline-primary" onclick="RegisterClick()">SignUp</button>\n' +
//             '                </div>\n' +
//             '                <a href="" class="login_forgot">Forgot Password?</a>\n' +
//             '            </div>\n' +
//             '    \n' +
//             '        </form>\n' +
//             '        <button id="close-dialog" class="btn btn-secondary" onclick="closeLoginDialog()">Close</button>\n' +
//             '    </dialog>';
//
//         divId.innerHTML = html_LoginDialog;
//     }
//     // else {
//     //     var divId = document.getElementById('container-dialog');
//     //     var html_LoginDialog = '<dialog id="login-dialog">\n' +
//     //         '<div class="mb-3">\n' +
//     //         '    <label class="form-control">Tên Tài Khoản:<span> @TaiKhoan.TenTaiKhoan</span> </label>\n' +
//     //         '    <label class="form-control">Email:<span>@Html.DisplayFor(@model => @TaiKhoan.Email) </span> </label>\n' +
//     //         '    <label class="form-control">Giới Tính:<span>@TaiKhoan.GioiTinh </span> </label>\n' +
//     //         '    <label class="form-control">Ngày Sinh:<span> @TaiKhoan.NgaySinh</span> </label>\n' +
//     //         '    <label class="form-control">Địa Chỉ:<span> @TaiKhoan.DiaChi</span> </label>\n' +
//     //         '    <label class="form-control">Số Điện Thoại:<span> @TaiKhoan.SĐT</span> </label>\n' +
//     //         '</div>\n' +
//     //         '<button id="close-dialog" class="btn btn-secondary" onclick="closeLoginDialog()">Close</button>\n' +
//     //         '</dialog>';
//     //     divId.innerHTML = html_LoginDialog;
//     // }
// });
//
// logout
function Logout() {
    var url = "https://localhost:44376/";
    window.location.href = url;
}
//


