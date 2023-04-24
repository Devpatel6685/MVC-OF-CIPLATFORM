
$(document).on('click', '.theme li', function (e) {
    e.preventDefault();
    $('.theme li').each(function () {
        $(this).removeClass('theactive');
    })
    $(this).addClass('theactive');
    filterthemes();
});
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
            alert("hello");
            $('.table').html($(response).find('.table').html());
            $('.pagination').html($(response).find('.pagination').html());


        }
    })
}
$(document).ready(function () {

    $('#themesearch').keyup(function () {
        alert('hi');
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
            alert('called');

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
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
}
function DeleteTheme(themeId) {
    alert("delete theme called");
    $.ajax({
        url: '/admin/DeleteTheme',
        type: 'GET',
        data: {
            themeid: themeId
        },
        success: function (result) {
            $('#loadPartialView').html($(result).find('#loadPartialView').html());


        }
    });
}