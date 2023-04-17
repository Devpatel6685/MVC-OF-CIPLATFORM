var Model = $('#model')
$('#contact-link').click(() => {
    if (!Model.attr('data-userid')) {
        showModal();
    } else {
        let userid = $('#hiddeninput').val();
        $.ajax({
            url: '/Home/Addcontact',
            type: "POST",
            data: {
                Userid: userid,
            },
            success: function (result) {
                $('#modal-contact').html(result);
                $('#staticBackdrop3').modal('show');

            }
        })
    }
})
function contactsave() {
    var subject = $('#subject').val();
    var message = $('#message').val();

    $.ajax({
        url: '/Home/editcontact',
        type: "POST",
        data: {
            subject: subject,
            message: message
        },
        success: function (result) {
            console.log(result);
            
            toastr.success('Problem is sent!', '', { "timeOut": 0 });
        }
    })
}

function showModal() {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please Login!',
    })
}