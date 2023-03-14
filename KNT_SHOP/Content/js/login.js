/*=============== SHOW HIDDEN - PASSWORD ===============*/
const showPassword = (inputPass, inputIcon) => {
    const input = document.getElementById(inputPass),
        iconEye = document.getElementById(inputIcon);
    iconEye.addEventListener('click', () => {
        //change password text
        if (input.type === 'password') {
            input.type = 'text';
            iconEye.classList.add('ri-eye-line');
            iconEye.classList.remove('ri-eye-off-line');

        }else
        {
            input.type = 'password';
            iconEye.classList.remove('ri-eye-line');
            iconEye.classList.add('ri-eye-off-line');
        }
    });
}
showPassword('input-pass', 'input-icon');
showPassword('input-confirm-pass', 'input-icon-confirm');




