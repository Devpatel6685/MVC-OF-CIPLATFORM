
$(document).on('click', '.paginationcms li', function (e) {
    e.preventDefault();
    $('.paginationcms li').each(function () {
        $(this).removeClass('activecms');
    })
    $(this).addClass('activecms');
    console.log($(this).children().attr("id"))
    filtercms();
});
function filtercms() {
    var pageIndex = $('.paginationcms .activecms a').attr('id');
    $.ajax({
        url: "/Admin/cmspage",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (result) {

            $('.table').html($(result).find('.table').html());
            $('.paginationcms').html($(result).find('.paginationcms').html());

            // Change the URL in the browser to reflect the updated page state
            // history.pushState(null, null, "/Admin/UserpageInAdmin?pageindex=" + pageIndex);

        }
    })
}