
$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.user').parent().addClass('bg-light');
$('.nav-link.user').css('color', 'orange');
function filteruser() {
/*    *//*alert('pagination called');*/
/*    alert('filteruser called');*/
    var pageIndex = $('.use .active a').attr('id');
    var keyword = $('#search').val();
    $.ajax({
        url: "/Admin/User",
        type: "POST",
        data: {
            SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (response) {
            //console.log($(response).find('.pagination').html());
            //  $('#nouser').html($(response).find('#nouser').html());
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
            /*   $('#nouser').html($(response).find('#nouser').html());*/
            $('.table').empty().html($(response).find('.table').html());
            $('.page').empty().html($(response).find('.page').html());

        }
    })
}

