

$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.skill').parent().addClass('bg-light');
$('.nav-link.skill ').css('color', 'orange');



$(document).on('click', '.skill li', function (e) {
    e.preventDefault();
    $('.skill li').each(function () {
        $(this).removeClass('skiactive');
    })
    $(this).addClass('skiactive');
    filterskills();
});
function filterskills() {

    var pageIndex = $('.skill .skiactive a').attr('id');
    var keyword = $('#skillsearch').val();
    $.ajax({
        url: "/Admin/Skill",
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

    $('#skillsearch').keyup(function () {
        alert('hi');
        $('.pagination .skiactive').removeClass('skiactive');
        filterSearch();

    });
});

function filterSearch() {
    var keyword = $('#skillsearch').val();

    $.ajax({
        url: "/Admin/Skill",
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