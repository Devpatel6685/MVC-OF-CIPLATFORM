
$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.cms').parent().addClass('bg-light');
$('.nav-link.cms ').css('color', 'orange');



$(document).on('click', '.cms li', function (e) {
    e.preventDefault();
    $('.cms li').each(function () {
        $(this).removeClass('cmsactive');
    })
    $(this).addClass('cmsactive');
    filtercms();
});
function filtercms() {

    var pageIndex = $('.cms .cmsactive a').attr('id');
    var keyword = $('#cmssearch').val();
    $.ajax({
        url: "/Admin/Cmspage",
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
    $('#cmssearch').keyup(function () {
        $('.pagination .cmsactive').removeClass('cmsactive');
        filterSearch();
    });
});

function filterSearch() {
    var keyword = $('#cmssearch').val();
    $.ajax({
        url: "/Admin/Cmspage",
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
        text: "This CMSPage will be de-activated",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteCmspage(id);
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
}
function DeleteCmspage(cmspageid) {
    alert("delete misson called");
    $.ajax({
        url: '/admin/DeleteCMSPage',
        type: 'GET',
        data: {
            cmspageId: cmspageid
        },
        success: function (result) {
            $('#loadPartialView').html($(result).find('#loadPartialView').html());
            toastr.success("mission is deleted ");

        }
    });
}