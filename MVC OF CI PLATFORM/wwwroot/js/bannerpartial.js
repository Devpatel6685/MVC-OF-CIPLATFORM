
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
