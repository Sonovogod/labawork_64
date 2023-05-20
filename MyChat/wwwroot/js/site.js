$(document).ready(() => {
    let passFields = $('#passwordField');
    let confPassFields = $('#confirmPasswordField');
    let open = $('#openEyeIco');
    let close = $('#closeEyeIco');

    close.on('click', function (event){
        event.preventDefault();
        passFields.attr('type', 'text');
        confPassFields.attr('type', 'text');
        open.removeAttr('hidden');
        close.attr('hidden', 'hidden');
    })

    open.on('click', function (event){
        event.preventDefault();
        passFields.attr('type', 'password');
        confPassFields.attr('type', 'password');
        close.removeAttr('hidden');
        open.attr('hidden', 'hidden');
    })
})
