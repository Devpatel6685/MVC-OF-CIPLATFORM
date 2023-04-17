
$(document).on('click', '.paginationapplication li', function (e) {
    e.preventDefault();
    $('.paginationapplication li').each(function () {
        $(this).removeClass('activeapplication');
    })
    $(this).addClass('activeapplication');
    console.log($(this).children().attr("id"))
    filtercms();
});
function filtercms() {
    var pageIndex = $('.paginationapplication .activeapplication a').attr('id');
    $.ajax({
        url: "/Admin/MissionApplication",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (result) {

            $('.table').html($(result).find('.table').html());
            $('.paginationapplication').html($(result).find('.paginationapplication').html());

            // Change the URL in the browser to reflect the updated page state
            // history.pushState(null, null, "/Admin/UserpageInAdmin?pageindex=" + pageIndex);

        }
    })
}