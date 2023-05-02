$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.story').parent().addClass('bg-light');
$('.nav-link.story ').css('color', 'orange');



$(document).on('click', '.story li', function (e) {
    e.preventDefault();
    $('.story li').each(function () {
        $(this).removeClass('stactive');
    })
    $(this).addClass('stactive');
    filterskills();
});
function filterskills() {

    var pageIndex = $('.story .stactive a').attr('id');
    var keyword = $('#storysearch').val();
    $.ajax({
        url: "/Admin/Story",
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

    $('#storysearch').keyup(function () {

        $('.pagination .stactive').removeClass('stactive');
        filterSearch();

    });
});

function filterSearch() {
    var keyword = $('#storysearch').val();

    $.ajax({
        url: "/Admin/Story",
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
function approvestory(storyId) {
    $.ajax({
        url: "/Admin/ApproveStory",
        type: "POST",
        data: {
            storyid: storyId,
        },
        success: function (response) {


            $('#loadPartialView').html($(response).find('#loadPartialView').html());
            toastr.success("Story is Approved");
        }
    })
}
function declinestory(storyId) {
    $.ajax({
        url: "/Admin/DeclineStory",
        type: "POST",
        data: {
            storyid: storyId,
        },
        success: function (response) {
            $('#loadPartialView').html($(response).find('#loadPartialView').html());
            toastr.success("Story  is Deleted");
        }
    })
}