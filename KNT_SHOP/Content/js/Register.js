﻿
function RegisterClick(){
    window.location.href = "/Account/Register";
}
function Register()
{
    const input = document.getElementsByClassName("login_input");
    let check = false;
    for (let i = 0; i < input.length; i++) {
        if(input[i].value === ""){
            alert("Please fill in all the fields");   
            check = true;
            return;
        }
        else 
        {
            check = false;
        }
    }
    if(check === false) 
    {
        if (input[3].value.length < 8) {
            alert("Password must be at least 8 characters");
        }
        else {
            if (input[3].value !== input[4].value) {
                alert("Password and confirm password do not match");
            }
            else {
                alert("Register successfull");
                const btn = document.getElementById("btn-signup");
                btn.type = "submit";
                btn.submit();
            }
        }        

    }
    
}

// show/hide dialog login form
// function showLoginDialog() {
//     alert("test")
//    
//     // console.log('show dialog');
//     // // var dialog = document.querySelector('dialog');
//     // document.getElementById('login-dialog').showModal();
//     // console.log('show dialog');
//     // // dialog.showModal();
// }

// // show/hide dialog login form
// var dialog = document.querySelector('dialog');
// document.querySelector('#show-dialog').onclick = function() {
//     console.log('show dialog');
//     dialog.show();
// };
// document.querySelector('#close-dialog').onclick = function() {
//     dialog.close();
// };

