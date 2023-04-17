
$(document).on('click', '.pagination li', function (e) {
    e.preventDefault();
    $('.pagination li').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    console.log($(this).children().attr("id"))
    filteruser();
});
function filteruser() {
    var pageIndex = $('.pagination .active a').attr('id');
    $.ajax({
        url: "/Admin/User",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (result) {
            
            $('.table').html($(result).find('.table').html());
            $('.pagination').html($(result).find('.pagination').html());
         /*   $("#loadPartialView").html(response);*/

            // Change the URL in the browser to reflect the updated page state
            // history.pushState(null, null, "/Admin/UserpageInAdmin?pageindex=" + pageIndex);

        }
    })
}