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
    
    $('#commentThemeBtn').on('click', function (event){
        event.preventDefault();
        const content = $('#commentTheme').val();
        const model = {
            Content: content
        };
        
        $.ajax({
            type: 'POST',
            url: '/Chats/CommonChat/',
            data: model,
            success: function (response){
                console.log(response)
                let formattedDate = moment(response.dateOfSend).format("DD.MM.YYYY HH:mm:ss");
                $('#commetBlock').append(
                    '<div id="comment-' + response.Id + '" class="media row mt-5">' +
                    '    <div class="col-2">' +
                    '        <div class="mr-3 rounded-circle border border-dark border-1">' +
                    '            <img class="w-100 mr-3 rounded-circle" src="' + response.user.avatar + '" alt="' + response.user.userName + '">' +
                    '        </div>' +
                    '        <h5 class="mt-1 mx-2">' + response.user.userName + '</h5>' +
                    '    </div>' +
                    '    <div class="col-8 px-2">' +
                    '        <small class="text-muted">' + formattedDate + '</small>' +
                    '        <div class="w-50">' +
                    '            <span>' + response.content + '</span>' +
                    '        </div>' +
                    '    </div>' +
                    '</div>'
                );
            },
            error: function (response){
                console.log(response);
                console.log("errrrr");
            }
            
        })
    })
})
