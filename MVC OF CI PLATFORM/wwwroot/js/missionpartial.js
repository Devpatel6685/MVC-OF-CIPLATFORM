
$(document).on('click', '.paginationmis li', function (e) {
    e.preventDefault();
    $('.paginationmis li').each(function () {
        $(this).removeClass('missionactive');
    })
    $(this).addClass('missionactive');
    console.log($(this).children().attr("id"))
    filtermission();
});
function filtermission() {
    var pageIndex = $('.paginationmis .missionactive a').attr('id');
    $.ajax({
        url: "/Admin/Mission",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (result) {

            $('.table').html($(result).find('.table').html());
            $('.paginationmis').html($(result).find('.paginationmis').html());

            // Change the URL in the browser to reflect the updated page state
            // history.pushState(null, null, "/Admin/UserpageInAdmin?pageindex=" + pageIndex);

        }
    })
}