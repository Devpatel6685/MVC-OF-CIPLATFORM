
$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.user').parent().addClass('bg-light');
$('.nav-link.user').css('color', 'orange');

$(document).on('click', '.useli li', function (e) {
    e.preventDefault();
    $('.useli li').each(function () {
        $(this).removeClass('usactive');
    })
    $(this).addClass('usactive');
    filteruser();
});
function filteruser() {
    var pageIndex = $('.useli .usactive a').attr('id');
    var keyword = $('#search').val();
    $.ajax({
        url: "/Admin/User",
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

    $('#search').keyup(function () {

        $('.pagination .active').removeClass('active');
        filterSearch();

    });
});
function filterSearch() {
    var keyword = $('#search').val();

    $.ajax({
        url: "/Admin/User",
        type: "POST",
        data: {
            SearchInputdata: keyword,

        },
        success: function (response) {

            console.log(response);
            console.log("the id element", $(response).find("#nouser").html());
            
            $('.table').empty().html($(response).find('.table').html());
            $('.page').empty().html($(response).find('.page').html());

        }
    })
}
function showModal(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "This User will be de-activated",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Deleteuser(id);
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
}
function Deleteuser(userid) {
    /*alert("delete misson called");*/
    $.ajax({
        url: '/admin/Deleteuser',
        type: 'GET',
        data: {
            userid: userid,
        },
        success: function (result) {
            $('#loadPartialView').html($(result).find('#loadPartialView').html());
            toastr.success("mission is deleted ");

        }
    });
}

