
$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.theme').parent().addClass('bg-light');
$('.nav-link.theme').css('color', 'orange');
function filterthemes() {

    var pageIndex = $('.theme .theactive a').attr('id');
    var keyword = $('#themesearch').val();
    $.ajax({
        url: "/Admin/Theme",
        type: "POST",
        data: {
            SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (response) {
            
            $('.table').html($(response).find('.table').html());
            $('.pagination').html($(response).find('.pagination').html());


        }
    })
}
$(document).ready(function () {

    $('#themesearch').keyup(function () {

        $('.pagination .misactive').removeClass('misactive');
        filterSearch();

    });
});

function filterSearch() {
    var keyword = $('#themesearch').val();

    $.ajax({
        url: "/Admin/Theme",
        type: "POST",
        data: {
            SearchInputdata: keyword,

        },
        success: function (response) {


            $('.table').empty().html($(response).find('.table').html());
            $('.page').empty().html($(response).find('.page').html());

        }
    })
}

function showModal(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "This Theme will be de-activated",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteTheme(id);
        }
    })
}
function DeleteTheme(themeId) {
    /*alert("delete theme called");*/
    $.ajax({
        url: '/admin/DeleteTheme',
        type: 'GET',
        data: {
            themeid: themeId
        },
        success: function (result) {
           /* alert('result called');*/
            console.log(result);
            if (result == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Deleted',
                    text: 'Your theme is been deleted',
                    footer: '<a href="">You cannot de-activate it</a>'
                }).then(() => {
                    window.location = "/Admin/Theme/"
                })

            }
            else if (result == false) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'This theme is already used by Mission or User',
                    footer: '<a href="">You cannot de-activate it</a>'
                })
            }


        }
    });
}


