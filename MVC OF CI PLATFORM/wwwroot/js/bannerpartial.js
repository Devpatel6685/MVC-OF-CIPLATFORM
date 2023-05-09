
$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.banner').parent().addClass('bg-light');
$('.nav-link.banner').css('color', 'orange');

function filterbanner() {

    var pageIndex = $('.banner .banactive a').attr('id');
    var keyword = $('#bannersearch').val();
    $.ajax({
        url: "/Admin/Banner",
        type: "POST",
        data: {
            SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (response) {
            $('.table').html($(response).find('.table').html());
            $('.page').html($(response).find('.page').html());

        }
    })
}
$(document).ready(function () {
    $('#bannersearch').keyup(function () {
        $('.pagination .banactive').removeClass('banactive');
        filterSearch();
    });
});
function filterSearch() {
    var keyword = $('#bannersearch').val();

    $.ajax({
        url: "/Admin/Banner",
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
function deleteBanner(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to delete this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Admin/deleteBanner",
                type: "POST",
                data: {
                    id: id
                },
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        'Your banner has been deleted.',
                        'success'
                    )
                    $('#loadPartialView').html($(response).find('#loadPartialView').html());
                }
            });
        }
    })
}
